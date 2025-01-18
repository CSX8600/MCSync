using ClussPro.Base.Data;
using ClussPro.Base.Data.Operand;
using ClussPro.Base.Data.Query;
using ClussPro.Base.Extensions;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Utility;
using Newtonsoft.Json;
using OAuth.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using WebModels.auth;
using WebModels.security;

namespace OAuth.Controllers
{
    public class AuthorizeController : Controller
    {
        // GET: Authorize
        public ActionResult Index(string response_type, string client_id, string redirect_uri, string state, string loginToProgramName = null)
        {
            if (string.IsNullOrEmpty(response_type) ||
                string.IsNullOrEmpty(client_id) ||
                string.IsNullOrEmpty(redirect_uri) ||
                string.IsNullOrEmpty(state))
            {
                return OAuthError("invalid_request", "Missing parameters");
            }

            Search<Client> clientSearch = new Search<Client>(new StringSearchCondition<Client>()
            {
                Field = "ClientIdentifier",
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = client_id
            });

            Client client = clientSearch.GetReadOnly(null, new List<string>()
            {
                "RedirectionURI"
            });

            if (client == null)
            {
                return OAuthError("unauthorized_client", "The client is not authorized to request an authorization code using this method.");
            }

            if (!client.ContainsRedirectURI(redirect_uri))
            {
                return OAuthError("invalid_request", "The Redirect URI is not valid");
            }

            if (!response_type.Equals("code", System.StringComparison.OrdinalIgnoreCase))
            {
                return OAuthErrorRedirect("invalid_request", "Response Type invalid", state, redirect_uri);
            }

            ModelState.SetModelValue("client_id", new ValueProviderResult(client_id, client_id, CultureInfo.CurrentCulture));
            ModelState.SetModelValue("redirect_uri", new ValueProviderResult(redirect_uri, redirect_uri, CultureInfo.CurrentCulture));
            ModelState.SetModelValue("state", new ValueProviderResult(state, state, CultureInfo.CurrentCulture));
            if (!string.IsNullOrEmpty(loginToProgramName))
            {
                ModelState.SetModelValue("loginToProgramName", new ValueProviderResult(loginToProgramName, loginToProgramName, CultureInfo.CurrentCulture));
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string[] keys = collection.AllKeys;

            List<string> missingParameters = new List<string>();
            if (!keys.Contains("client_id") || string.IsNullOrEmpty(collection["client_id"]))
            {
                missingParameters.Add("client_id");
            }

            string clientID = collection["client_id"];
            Search<Client> clientSearch = new Search<Client>(new StringSearchCondition<Client>()
            {
                Field = "ClientIdentifier",
                SearchConditionType = string.IsNullOrEmpty(clientID) ? SearchCondition.SearchConditionTypes.Null : SearchCondition.SearchConditionTypes.Equals,
                Value = clientID
            });

            Client client = clientSearch.GetReadOnly(null, FieldPathUtility.CreateFieldPathsAsList<Client>(c => new List<object>()
            {
                c.ClientID,
                c.RedirectionURI,
                c.Type,
                c.UserClients.First().UserID
            }));

            if (client == null)
            {
                return OAuthError("unauthorized_client", "The client is not authorized to request an authorization code using this method.");
            }

            if (keys.Contains("response_type") && "device_code".Equals(collection["response_type"], StringComparison.OrdinalIgnoreCase))
            {
                if (client.Type == Client.Types.Device)
                {
                    return HandleDeviceCodeResponse(client.ClientID);
                }
                else
                {
                    return OAuthError("unauthorized_client", "The client is not authorized to request an authorization code using this method.");
                }
            }
            else if (client.Type != Client.Types.BrowserEnabled)
            {
                return OAuthError("unauthorized_client", "The client is not authorized to request an authorization code using this method.");
            }

            if (!keys.Contains("redirect_uri") || string.IsNullOrEmpty(collection["redirect_uri"]))
            {
                missingParameters.Add("redirect_uri");
            }

            if (!keys.Contains("state") || string.IsNullOrEmpty(collection["state"]))
            {
                missingParameters.Add("state");
            }

            if (missingParameters.Any())
            {
                StringBuilder builder = new StringBuilder();
                foreach (string missingParameter in missingParameters)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(", ");
                    }

                    builder.Append(missingParameter);
                }

                return OAuthError("invalid_request", "The following parameters were not specified: " + builder.ToString());
            }

            string redirectUri = collection["redirect_uri"];
            string state = collection["state"];

            if (!client.ContainsRedirectURI(redirectUri))
            {
                return OAuthError("invalid_request", "The Redirect URI is not valid");
            }

            if (!keys.Contains("user"))
            {
                ModelState.AddModelError("user", "Username is a required field");
            }

            if (!keys.Contains("password"))
            {
                ModelState.AddModelError("password", "Password is a required field");
            }

            if (!ModelState.IsValid)
            {
                ModelState.SetModelValue("client_id", new ValueProviderResult(clientID, clientID, CultureInfo.CurrentCulture));
                ModelState.SetModelValue("redirect_uri", new ValueProviderResult(redirectUri, redirectUri, CultureInfo.CurrentCulture));
                ModelState.SetModelValue("state", new ValueProviderResult(state, state, CultureInfo.CurrentCulture));
                if (keys.Contains("user"))
                {
                    ModelState.SetModelValue("user", new ValueProviderResult(collection["user"], collection["user"], CultureInfo.CurrentCulture));
                }
                return View(collection);
            }

            string user = collection["user"];
            string password = collection["password"];
            if (!LDAPLogin.TryLDAPLogin(user, password))
            {
                ModelState.SetModelValue("client_id", new ValueProviderResult(clientID, clientID, CultureInfo.CurrentCulture));
                ModelState.SetModelValue("redirect_uri", new ValueProviderResult(redirectUri, redirectUri, CultureInfo.CurrentCulture));
                ModelState.SetModelValue("state", new ValueProviderResult(state, state, CultureInfo.CurrentCulture));
                ModelState.SetModelValue("user", new ValueProviderResult(collection["user"], collection["user"], CultureInfo.CurrentCulture));
                ModelState.AddModelError("user", "Incorrect username/password");
                return View(collection);
            }

            Search<User> userSearch = new Search<User>(new StringSearchCondition<User>()
            {
                Field = "Username",
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = user
            });

            User dbUser = userSearch.GetReadOnly(null, new string[] { "UserID" });
            if (dbUser == null)
            {
                ModelState.SetModelValue("client_id", new ValueProviderResult(clientID, clientID, CultureInfo.CurrentCulture));
                ModelState.SetModelValue("redirect_uri", new ValueProviderResult(redirectUri, redirectUri, CultureInfo.CurrentCulture));
                ModelState.SetModelValue("state", new ValueProviderResult(state, state, CultureInfo.CurrentCulture));
                ModelState.SetModelValue("user", new ValueProviderResult(collection["user"], collection["user"], CultureInfo.CurrentCulture));
                ModelState.AddModelError("user", "Incorrect username/password");
                return View(collection);
            }

            if (!client.UserClients?.Any(uc => uc.UserID == dbUser.UserID) ?? false)
            {
                TempData["ClientID"] = clientID;
                TempData["State"] = state;
                TempData["RedirectUri"] = redirectUri;
                TempData["NoRedirect"] = collection.AllKeys.Contains("noredirect") ? collection.Get("noredirect") : bool.FalseString;
                TempData["UserID"] = dbUser.UserID.ToString();
                return RedirectToAction("AppGrant");
            }

            return CreateOAuthCode(Guid.Parse(clientID), dbUser.UserID.Value, redirectUri, state, collection.AllKeys.Contains("noredirect") && bool.TryParse(collection.Get("noredirect"), out bool noRedirect) ? noRedirect : false);
        }

        private ActionResult OAuthError(string error, string error_description)
        {
            string errorResponse = JsonConvert.SerializeObject(new
            {
                error,
                error_description
            });

            Response.Output.Write(errorResponse);
            return new HttpStatusCodeResult(400);
        }

        private ActionResult OAuthErrorRedirect(string error, string error_description, string state, string redirectionUri)
        {
            return new RedirectResult(redirectionUri + $"?error={error}&error_description={error_description}&state={state}", false);
        }

        const string DEVICE_CODE_CHARS = "ABCDEFGHIKJLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789";
        private ActionResult HandleDeviceCodeResponse(long? clientID)
        {
            string deviceCode = "";
            int fieldLength = Schema.GetSchemaObject<DeviceCode>().GetField(nameof(DeviceCode.DeviceCodeString)).DataSize;
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                while(deviceCode.Length < fieldLength)
                {
                    byte[] oneByte = new byte[1];
                    rng.GetBytes(oneByte);
                    char byteChar = (char)oneByte[0];
                    if (DEVICE_CODE_CHARS.Contains(byteChar))
                    {
                        deviceCode += byteChar;
                    }
                }
            }

            DeviceCode dbDeviceCode = DataObjectFactory.Create<DeviceCode>();
            dbDeviceCode.ClientID = clientID;
            dbDeviceCode.DeviceCodeString = deviceCode;
            dbDeviceCode.UserCode = new Random().Next(10000, 99999).ToString();
            dbDeviceCode.LastPing = DateTime.Now;
            dbDeviceCode.Status = DeviceCode.Statuses.WaitingOnUser;
            if (!dbDeviceCode.Save())
            {
                return OAuthError("server_error", "An unexpected error occurred on the server.");
            }

            return Json(new
            {
                verification_uri = Request.Url.GetLeftPart(UriPartial.Authority) + "/device",
                user_code = dbDeviceCode.UserCode,
                device_code = deviceCode,
                interval = ConfigurationManager.AppSettings.Get("DeviceCodeInterval")
            });
        }

        private readonly string[] DEVICE_CODE_DATA_KEYS = new string[]
        {
            "ClientID",
            "UserID",
            "DeviceCodeID",
            "RequestType"
        };

        private readonly string[] OAUTH_DATA_KEYS = new string[]
        {
            "ClientID",
            "State",
            "RedirectURI"
        };

        [HttpGet]
        public ActionResult AppGrant()
        {
            string[] requiredFields;
            if (TempData.ContainsKey("RequestType") && "device_code".Equals(TempData["RequestType"] as string, StringComparison.OrdinalIgnoreCase))
            {
                requiredFields = DEVICE_CODE_DATA_KEYS;
            }
            else
            {
                requiredFields = OAUTH_DATA_KEYS;
            }

            if (!requiredFields.All(requiredField => TempData.ContainsKey(requiredField)))
            {
                Response.Output.WriteLine("Bad Request");
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            string clientID = TempData["ClientID"] as string;
            Client client = new Search<Client>(new StringSearchCondition<Client>()
            {
                Field = nameof(Client.ClientIdentifier),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = clientID
            }).GetReadOnly(null, new[] { nameof(Client.ClientName) });

            ViewBag.ClientName = client?.ClientName;

            ViewData["ClientID"] = TempData["ClientID"];
            ViewData["State"] = (string)TempData.GetOrDefault("State", string.Empty);
            ViewData["RedirectUri"] = (string)TempData.GetOrDefault("RedirectUri", string.Empty);
            ViewData["NoRedirect"] = (string)TempData.GetOrDefault("NoRedirect", bool.FalseString);
            ViewData["UserID"] = (string)TempData.GetOrDefault("UserID", string.Empty);
            ViewData["RequestType"] = (string)TempData.GetOrDefault("RequestType", string.Empty);
            ViewData["DeviceCodeID"] = (string)TempData.GetOrDefault("DeviceCodeID", string.Empty);

            TempData.Clear();

            return View();
        }

        [HttpPost]
        public ActionResult AppGrant(FormCollection form)
        {
            string[] requiredKeys;
            bool isDeviceCode = false;
            if (form.AllKeys.Contains("RequestType") && form.Get("RequestType").Equals("device_code", StringComparison.OrdinalIgnoreCase))
            {
                requiredKeys = DEVICE_CODE_DATA_KEYS;
                isDeviceCode = true;
            }
            else
            {
                requiredKeys = OAUTH_DATA_KEYS;
            }

            if (!requiredKeys.All(key => form.AllKeys.Contains(key, StringComparer.OrdinalIgnoreCase)))
            {
                return OAuthError("bad_request", "Required information was missing");
            }

            long deviceCodeID = -1;
            if (isDeviceCode && !long.TryParse(form.Get("DeviceCodeID"), out deviceCodeID))
            {
                return OAuthError("bad_request", "DeviceCodeID must be a long");
            }

            if (!form.AllKeys.Contains("allow"))
            {
                return isDeviceCode ? RejectDeviceCode(deviceCodeID) : OAuthErrorRedirect("unauthorized", "The user rejected authorization of the client", form.Get("State"), form.Get("RedirectUri"));
            }

            if (!Guid.TryParse(form.Get("ClientID"), out Guid clientID))
            {
                return OAuthError("bad_request", "ClientID must be a guid");
            }

            if (!long.TryParse(form.Get("UserID"), out long userID))
            {
                return OAuthError("bad_request", "UserID must be a long");
            }


            Client client = new Search<Client>(new GuidSearchCondition<Client>()
            {
                Field = nameof(Client.ClientIdentifier),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = clientID
            }).GetReadOnly(null, new[] { nameof(Client.ClientID) });

            if (client == null)
            {
                return OAuthErrorRedirect("unauthorized", "The client is not authorized to request an authorization code using this method.", form.Get("State"), form.Get("RedirectUri"));
            }

            UserClient userClient = DataObjectFactory.Create<UserClient>();
            userClient.UserID = userID;
            userClient.ClientID = client.ClientID;
            userClient.AuthorizationTime = DateTime.Now;
            userClient.Save();

            return isDeviceCode ? CreateDeviceCode(clientID, userID, deviceCodeID) : CreateOAuthCode(clientID, userID, form.Get("RedirectUri"), form.Get("State"), form.AllKeys.Contains("NoRedirect") && form.Get("NoRedirect").Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase));
        }

        private ActionResult CreateOAuthCode(Guid clientID, long userID, string redirectUri, string state, bool noRedirect)
        {
            Code code = DataObjectFactory.Create<Code>();
            code.ClientIdentifier = clientID;
            code.AuthCode = Guid.NewGuid();
            code.RedirectURI = redirectUri;
            code.Expiration = DateTime.Now.AddMinutes(5);
            code.UserID = userID;

            if (!code.Save())
            {
                return OAuthErrorRedirect("server_error", "An unexpected error occurred on the server.", state, redirectUri);
            }

            if (noRedirect)
            {
                Response.Output.WriteLine($"code={code.AuthCode.ToString()}&state={state}");
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new RedirectResult($"{code.RedirectURI}?code={code.AuthCode.ToString()}&state={state}", false);
        }

        private ActionResult CreateDeviceCode(Guid clientID, long userID, long deviceCodeID)
        {
            using (ITransaction transaction = SQLProviderFactory.GenerateTransaction())
            {
                Code code = DataObjectFactory.Create<Code>();
                code.ClientIdentifier = clientID;
                code.AuthCode = Guid.NewGuid();
                code.Expiration = DateTime.Now.AddMinutes(5);
                code.UserID = userID;

                if (!code.Save(transaction))
                {
                    return OAuthError("server_error", "An unexpected error occurred on the server.");
                }

                DeviceCode deviceCode = DataObject.GetEditableByPrimaryKey<DeviceCode>(deviceCodeID, transaction, new string[0]);
                deviceCode.CodeID = code.CodeID;
                deviceCode.Status = DeviceCode.Statuses.Accepted;
                if (!deviceCode.Save(transaction))
                {
                    return OAuthError("server_error", "An unexpected error occurred on the server.");
                }

                transaction.Commit();
            }

            return View("~/Views/Device/Success.cshtml");
        }

        private ActionResult RejectDeviceCode(long deviceCodeID)
        {
            DeviceCode deviceCode = DataObject.GetEditableByPrimaryKey<DeviceCode>(deviceCodeID, null, null);
            deviceCode.Status = DeviceCode.Statuses.Rejected;
            if (!deviceCode.Save())
            {
                return OAuthError("server_error", "An unexpected error occurred on the server.");
            }

            return View("~/Views/Device/Rejected.cshtml");
        }
    }
}