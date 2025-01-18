using CefSharp;
using MesaSuite.Common.Data;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace MesaSuite.Common
{
    public static class Authentication
    {
        public static event EventHandler OnLoggedIn;
        public static event EventHandler OnLoggedOut;
        public static event EventHandler OnProgramUpdate;
        private static AuthenticationStatuses _authenticationStatus = AuthenticationStatuses.LoggedOut;
        private static object _authenticationLock = new object();
        public static AuthenticationStatuses AuthenticationStatus
        {
            get { lock (_authenticationLock) { return _authenticationStatus; } }
            private set
            {
                _authenticationStatus = value;
                if (_authenticationStatus == AuthenticationStatuses.LoggedIn)
                {
                    OnLoggedIn?.Invoke(null, new EventArgs());
                }
                else
                {
                    OnLoggedOut?.Invoke(null, new EventArgs());
                }
            }
        }
        private static Thread programThread;

        public const int PORT = 48170;
        private static List<string> _programs = new List<string>();
        public static IReadOnlyCollection<string> Programs => _programs;

        private static Guid? _clientID = null;
        private static Guid? ClientID
        {
            get
            {
                if (_clientID == null)
                {
                    string subKey = GlobalSettings.InternalEditionMode ? "MesaSuiteInternalEdition" : "MesaSuite";
                    RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Clussman Productions\" + subKey);
                    string clientIDKey;

#if DEBUG
                    clientIDKey = "ClientIDDev";
#else
                    clientIDKey = "ClientID";
#endif

                    string clientIDString = key.GetValue(clientIDKey) as string;
                    if (Guid.TryParse(clientIDString, out Guid clientID))
                    {
                        _clientID = clientID;
                    }
                }

                return _clientID;
            }
            set
            {
                string subKey = GlobalSettings.InternalEditionMode ? "MesaSuiteInternalEdition" : "MesaSuite";
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Clussman Productions\" + subKey);
                string clientIDKey;

#if DEBUG
                clientIDKey = "ClientIDDev";
#else
                clientIDKey = "ClientID";
#endif

                if (value != null)
                {
                    key.SetValue(clientIDKey, value.ToString());
                }
                else
                {
                    key.DeleteValue(clientIDKey);
                }

                _clientID = value;
            }
        }
        private static string AuthToken { get; set; }
        private static string RefreshToken { get; set; }
        private static DateTime Expiration { get; set; }

        public static string GetAuthToken(bool doLogIn = false, string loginToProgramName = null)
        {
            lock (_authenticationLock)
            {
                if (string.IsNullOrEmpty(AuthToken) && doLogIn)
                {
                    DoLogIn(loginToProgramName);
                }

                if (!string.IsNullOrEmpty(AuthToken) && Expiration <= DateTime.Now)
                {
                    RefreshTokenRequest();
                }

                return AuthToken;
            }
        }

        public static void Register()
        {
            VerifyAuthPortOpen();

            using (HttpListener listener = new HttpListener())
            {
                frmRegister register = new frmRegister();
                register.DialogResult = DialogResult.Cancel;

                ListenForRegisterResponse(listener, register);

                register.ShowDialog();
            }
        }

        private static async void ListenForRegisterResponse(HttpListener listener, frmRegister registerForm)
        {
            listener.Prefixes.Add("http://localhost:" + PORT + "/");
            listener.Start();

            HttpListenerContext listenerContext;
            try
            {
                listenerContext = await listener.GetContextAsync();
            }
            catch
            {
                return;
            }

            string requestData;
            using (StreamReader reader = new StreamReader(listenerContext.Request.InputStream))
            {
                requestData = reader.ReadToEnd();
            }

            NameValueCollection formData = HttpUtility.ParseQueryString(requestData);

            if (string.IsNullOrEmpty(formData.Get("client_id")) || !Guid.TryParse(formData.Get("client_id"), out Guid clientID))
            {
                listenerContext.Response.StatusCode = 400;
                listenerContext.Response.Close();

                MessageBox.Show("The Client ID was not of the expected format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                registerForm.Close();
            }
            else
            {
                ClientID = clientID;
                listenerContext.Response.StatusCode = 200;
                listenerContext.Response.Close();
                registerForm.DialogResult = DialogResult.OK;
                registerForm.Close();
            }
        }

        private static void DoLogIn(string loginToProgramName = null)
        {
            if (ClientID == null)
            {
                Register();
            }

            VerifyAuthPortOpen();

            Guid state = Guid.NewGuid();
            frmLogin login = new frmLogin();
            login.Port = PORT;
            login.ClientID = ClientID.Value;
            login.State = state;
            login.LoginToProgramName = loginToProgramName;
            login.DialogResult = DialogResult.Cancel;

            HttpListener listener = null;
            try
            {
                listener = new HttpListener();
                ListenForResponse(listener, login, state);

                DialogResult result = login.ShowDialog();

                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            finally
            {
                listener.Close();
            }

            if (!string.IsNullOrEmpty(AuthToken))
            {
                AuthenticationStatus = AuthenticationStatuses.LoggedIn;
            }
        }

        private static void VerifyAuthPortOpen()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            IPEndPoint[] listeners = properties.GetActiveTcpListeners();
            if (connections.Any(connection => connection.LocalEndPoint.Port == PORT) || listeners.Any(ipListener => ipListener.Port == PORT))
            {
                throw new Exception("Could not open a port for authentication");
            }
        }

        private static async void ListenForResponse(HttpListener listener, frmLogin login, Guid state)
        {
            listener.Prefixes.Add("http://localhost:" + PORT + "/");
            listener.Start();
            HttpListenerContext context;
            try
            {
                context = await listener.GetContextAsync();
            }
            catch(Exception ex) { return; }

            HttpListenerRequest request = context.Request;
            if (request.QueryString.AllKeys.Contains("error"))
            {
                ResetValues();

                string error = request.QueryString["error"];
                string error_description = request.QueryString.Get("error_description");

                login.Invoke(new MethodInvoker(() =>
                {
                    MessageBox.Show(error + ":\r\n" + error_description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    login.Close();
                }));
                AuthenticationStatus = AuthenticationStatuses.LoggedOut;
            }
            else
            {
                string responseStateString = request.QueryString.Get("state");
                if (!Guid.TryParse(responseStateString, out Guid responseState) || !responseState.Equals(state))
                {
                    ResetValues();

                    login.Invoke(new MethodInvoker(() =>
                    {
                        MessageBox.Show("Sign in rejected by MesaSuite.  Anti-CSRF check failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        login.Close();
                    }));
                    AuthenticationStatus = AuthenticationStatuses.LoggedOut;
                }

                string code = request.QueryString.Get("code");
                AccessTokenRequest(login, code);
            }
        }

        private static void AccessTokenRequest(frmLogin login, string code)
        {
            WebClient client = new WebClient();

            string responseString = null;
            try
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                responseString = client.UploadString(new Uri($"{ConfigurationManager.AppSettings.Get("MesaSuite.Common.AuthHost")}/Token"), "POST", $"grant_type=authorization_code&code={code}&redirect_uri={WebUtility.UrlEncode("http://localhost:" + PORT)}&client_id={ClientID.ToString()}");
            }
            catch (WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();
                }
            }

            var successObject = new
            {
                access_token = "",
                token_type = "",
                expires_in = -1,
                refresh_token = ""
            };

            var failedObject = new
            {
                error = "",
                error_description = ""
            };

            successObject = JsonConvert.DeserializeAnonymousType(responseString, successObject);
            failedObject = JsonConvert.DeserializeAnonymousType(responseString, failedObject);

            if (!string.IsNullOrEmpty(successObject.access_token))
            {
                AuthToken = successObject.access_token;
                RefreshToken = successObject.refresh_token;
                Expiration = DateTime.Now.AddSeconds(successObject.expires_in);

                string subKey = GlobalSettings.InternalEditionMode ? "MesaSuiteInternalEdition" : "MesaSuite";
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Clussman Productions\" + subKey);
                key.SetValue("AuthToken", AuthToken);
                key.SetValue("RefreshToken", RefreshToken);
                key.SetValue("Expiration", Expiration.ToBinary());

                UpdatePrograms();
            }
            else
            {
                ResetValues();

                login.Invoke(new MethodInvoker(() =>
                {
                    MessageBox.Show(failedObject.error + ":\r\n" + failedObject.error_description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    login.Close();
                }));
                AuthenticationStatus = AuthenticationStatuses.LoggedOut;
            }

            login.Invoke(new MethodInvoker(() =>
            {
                login.DialogResult = DialogResult.OK;
                login.Close();
            }));
        }

        private static void RefreshTokenRequest()
        {
            WebClient refreshClient = new WebClient();
            refreshClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");

            string responseString = null;
            try
            {
                responseString = refreshClient.UploadString($"{ConfigurationManager.AppSettings.Get("MesaSuite.Common.AuthHost")}/token", $"grant_type=refresh_token&refresh_token={RefreshToken.ToString()}");
            }
            catch(WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();
                }
            }

            var successObject = new
            {
                access_token = "",
                token_type = "",
                expires_in = -1,
                refresh_token = ""
            };

            var failedObject = new
            {
                error = "",

                error_description = ""
            };

            successObject = JsonConvert.DeserializeAnonymousType(responseString, successObject);
            failedObject = JsonConvert.DeserializeAnonymousType(responseString, failedObject);

            if (!string.IsNullOrEmpty(successObject.access_token))
            {
                AuthToken = successObject.access_token;
                RefreshToken = successObject.refresh_token;
                Expiration = DateTime.Now.AddSeconds(successObject.expires_in);

                string subKey = GlobalSettings.InternalEditionMode ? "MesaSuiteInternalEdition" : "MesaSuite";
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Clussman Productions\" + subKey);
                key.SetValue("AuthToken", AuthToken);
                key.SetValue("RefreshToken", RefreshToken);
                key.SetValue("Expiration", Expiration.ToBinary());

                UpdatePrograms();
            }
            else
            {
                ResetValues();
                AuthenticationStatus = AuthenticationStatuses.LoggedOut;

                throw new Exception(failedObject.error + ":\r\n" + failedObject.error_description);
            }
        }

        public static void LogOut()
        {
            if (AuthenticationStatus == AuthenticationStatuses.LoggedOut || string.IsNullOrEmpty(GetAuthToken()))
            {
                return;
            }

            string authToken = GetAuthToken();

            WebClient logOutClient = new WebClient();
            logOutClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            logOutClient.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + authToken);

            try
            {
                logOutClient.UploadString($"{ConfigurationManager.AppSettings.Get("MesaSuite.Common.AuthHost")}/revoke", "reason=" + WebUtility.UrlEncode("Log Out"));
            }
            catch(WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;

                if (response.Headers.AllKeys.Contains("WWW-Authenticate"))
                {
                    ResetValues();
                    AuthenticationStatus = AuthenticationStatuses.LoggedOut;
                    return;
                }

#if DEBUG
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                    Debugger.Break();
                }
#endif

                throw new Exception("An error occurred while attempting to log out.  Cannot guarantee revoke completed successfully.");
            }

            ResetValues();
            AuthenticationStatus = AuthenticationStatuses.LoggedOut;
        }

        public static void Initialize()
        {
            CefRuntime.SubscribeAnyCpuAssemblyResolver();

            string subKey = GlobalSettings.InternalEditionMode ? "MesaSuiteInternalEdition" : "MesaSuite";
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Clussman Productions\" + subKey);
            string strExpirationEncoded = key.GetValue("Expiration") as string;

            if (!string.IsNullOrEmpty(strExpirationEncoded) && long.TryParse(strExpirationEncoded, out long expirationEncoded))
            {
                Expiration = DateTime.FromBinary(expirationEncoded);
            }

            AuthToken = key.GetValue("AuthToken") as string;
            RefreshToken = key.GetValue("RefreshToken") as string;

            AuthenticationStatus = string.IsNullOrEmpty(AuthToken) ? AuthenticationStatuses.LoggedOut : AuthenticationStatuses.LoggedIn;

            programThread = new Thread(new ThreadStart(ProgramThreadLogic));
            programThread.IsBackground = true;
            programThread.Start();
        }

        public static void Shutdown()
        {
            _requestProgramThreadStop = true;
        }

        private static bool _requestProgramThreadStop = false;
        private static void ProgramThreadLogic()
        {
            do
            {
                try
                {
                    UpdatePrograms();

                    if (Application.OpenForms.Count == 0)
                    {
                        break;
                    }
                }
                catch (Exception) { }

                Thread.Sleep(10000);
            }
            while (!_requestProgramThreadStop);
        }

        private static bool isUpdatingPrograms = false;
        public async static Task UpdatePrograms()
        {
            if (isUpdatingPrograms) { return; }

            isUpdatingPrograms = true;
            try
            {
                List<string> newPrograms;

                if (!string.IsNullOrEmpty(GetAuthToken()))
                {
                    GetData getData = new GetData(DataAccess.APIs.SystemManagement, "Program/GetCurrentPrograms");

                    try
                    {
                        newPrograms = await getData.GetObject<List<string>>() ?? new List<string>();
                    }
                    catch (Exception)
                    {
                        newPrograms = new List<string>();
                    }

                    if (!getData.RequestSuccessful) // Did we get signed out somehow?
                    {
                        HttpWebRequest webRequest = WebRequest.CreateHttp(ConfigurationManager.AppSettings.Get("MesaSuite.Common.AuthHost") + "/Token/VerifyToken");
                        webRequest.Method = "POST";
                        webRequest.ContentType = "application/json";
                        using (StreamWriter writer = new StreamWriter(webRequest.GetRequestStream()))
                        {
                            writer.Write("{\"access_token\": \"" + GetAuthToken() + "\"}");
                        }
                        
                        try
                        {
                            await webRequest.GetResponseAsync();
                        }
                        catch(WebException ex)
                        {
                            if (ex.Response is HttpWebResponse response && response.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                ResetValues();
                                AuthenticationStatus = AuthenticationStatuses.LoggedOut;
                            }

                            // If there's an exception for any other reason, we can't really determine
                            // what the problem is, and we don't really want to be logging people out randomly
                            // so we'll just do nothing and try again later.
                        }
                    }
                }
                else
                {
                    newPrograms = new List<string>();
                }

                List<string> currentPrograms = _programs.ToList();

                bool hasChanges = newPrograms.Count != currentPrograms.Count || newPrograms.Any(np => currentPrograms.Contains(np)) || currentPrograms.Any(cp => newPrograms.Contains(cp));
                _programs = newPrograms;

                if (hasChanges)
                {
                    OnProgramUpdate?.Invoke(Programs, new EventArgs());
                }
            }
            finally
            {
                isUpdatingPrograms = false;
            }
        }

        public enum AuthenticationStatuses
        {
            LoggedIn,
            LoggedOut
        }

        private static void ResetValues()
        {
            AuthToken = null;
            RefreshToken = null;
            Expiration = DateTime.Now;

            string subKey = GlobalSettings.InternalEditionMode ? "MesaSuiteInternalEdition" : "MesaSuite";
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Clussman Productions\" + subKey);
            if (key.GetValue("AuthToken") != null) { key.DeleteValue("AuthToken"); }
            if (key.GetValue("RefreshToken") != null) { key.DeleteValue("RefreshToken"); }

            UpdatePrograms();
        }
    }
}
