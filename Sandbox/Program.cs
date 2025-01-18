using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Loader;
using ClussPro.ObjectBasedFramework.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebModels.account;
using WebModels.hMailServer.dbo;
using WebModels.netprint;
using WebModels.security;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestDeviceAuth();

            Console.WriteLine("Enter resource name to test.  Type 'quit' to exit.");

            string enteredResource = "";
            while(enteredResource != "quit")
            {
                enteredResource = Console.ReadLine();
                Console.Clear();

                try
                {
                    HttpWebRequest webRequest = WebRequest.CreateHttp("http://localhost:58480/company/PriceCheck/" + enteredResource);
                    webRequest.Method = "GET";
                    HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                    string responseText;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseText = reader.ReadToEnd();
                    }

                    response.Dispose();
                    Console.WriteLine(responseText);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception occurred: {ex.ToString()}");
                }
            }


            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        private static void TestDeviceAuth()
        {
            string clientID = ConfigurationManager.AppSettings.Get("ClientID");
            string oAuthUrlBase = ConfigurationManager.AppSettings.Get("OAuthBase");
            if (string.IsNullOrEmpty(clientID) || string.IsNullOrEmpty(oAuthUrlBase))
            {
                Console.WriteLine("ClientID and OAuthBase are required in the Sandbox's App.config befre testing Device Auth");
                return;
            }

            Console.Clear();
            Console.WriteLine("Requesting new token...");

            string url = $"{oAuthUrlBase}/authorize";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string body = $"client_id={clientID}&response_type=device_code";
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(body);
            }

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return;
            }

            string responseText;
            using(StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseText = reader.ReadToEnd();
            }

            Console.WriteLine("Server responded with:\r\n" + responseText);

            var authorizeResponse = new
            {
                verification_uri = "",
                user_code = "",
                device_code = "",
                interval = ""
            };

            authorizeResponse = JsonConvert.DeserializeAnonymousType(responseText, authorizeResponse);
            Console.WriteLine();
            Console.WriteLine("Please navigate to " + authorizeResponse.verification_uri + " and enter code " + authorizeResponse.user_code);
            int currentConsoleLine = Console.CursorTop;
            int interval = int.Parse(authorizeResponse.interval);
            int attempts = 0;

            string authToken = "";
            while(true)
            {
                Thread.Sleep(interval * 1000);

                Console.WriteLine($"Attempting to get token {attempts} times...");

                request = WebRequest.CreateHttp($"{oAuthUrlBase}/token");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                using(StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write($"grant_type=device_code&client_id={clientID}&code={authorizeResponse.device_code}");
                }

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException wex)
                {
                    if (wex.Response is HttpWebResponse exresponse)
                    {
                        if (exresponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            using (StreamReader reader = new StreamReader(exresponse.GetResponseStream()))
                            {
                                Console.WriteLine(reader.ReadToEnd());
                            }

                            attempts++;
                            continue;
                        }
                    }

                    throw wex;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return;
                }

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }

                var errorObj = new
                {
                    error = "",
                    error_description = ""
                };
                errorObj = JsonConvert.DeserializeAnonymousType(responseText, errorObj);
                if (!string.IsNullOrEmpty(errorObj.error))
                {
                    continue;
                }

                var success = new
                {
                    access_token = "",
                    token_type = "",
                    expires_in = "",
                    refresh_token = ""
                };
                success = JsonConvert.DeserializeAnonymousType(responseText, success);
                authToken = success.access_token;

                Console.WriteLine("Successfully got token! " + authToken);
                Console.WriteLine();
                break;
            }

            while (true)
            {
                Console.Write("Enter a URL to GET from:");
                string address = Console.ReadLine();
                if (address.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                request = WebRequest.CreateHttp(address);
                request.Method = "GET";
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + authToken);
                
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }
                    Console.WriteLine();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine();
                }
            }
        }
    }
}
