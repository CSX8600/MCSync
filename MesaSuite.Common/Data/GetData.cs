using CefSharp.DevTools.Debugger;
using MesaSuite.Common.Collections;
using MesaSuite.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MesaSuite.Common.Data
{
    public class GetData : DataAccess
    {
        public MultiMap<string, string> QueryString { get; set; } = new MultiMap<string, string>();

        private bool _retry = false;

        public GetData(APIs api, string resource) : base(api, resource) { }

        private async Task<string> GetJson()
        {
            StringBuilder uriBuilder = new StringBuilder(GetResourceURI());
            bool first = true;
            foreach(KeyValuePair<string, HashSet<string>> queryString in QueryString)
            {
                foreach (string queryStringValue in queryString.Value)
                {
                    if (queryStringValue == null)
                    {
                        continue;
                    }

                    if (first)
                    {
                        uriBuilder.Append("?");
                        first = false;
                    }
                    else
                    {
                        uriBuilder.Append("&");
                    }

                    uriBuilder.Append(queryString.Key);
                    uriBuilder.Append("=");
                    uriBuilder.Append(Uri.EscapeDataString(queryStringValue));
                }
            }

            if (RequestFields != null)
            {
                foreach (string field in RequestFields)
                {
                    if (first)
                    {
                        uriBuilder.Append("?");
                        first = false;
                    }
                    else
                    {
                        uriBuilder.Append("&");
                    }

                    uriBuilder.Append("requestField=");
                    uriBuilder.Append(Uri.EscapeDataString(field));
                }
            }

            if (AdditionalFields != null)
            {
                foreach(string field in AdditionalFields)
                {
                    if (first)
                    {
                        uriBuilder.Append("?");
                        first = false;
                    }
                    else
                    {
                        uriBuilder.Append("&");
                    }

                    uriBuilder.Append("additionalField=");
                    uriBuilder.Append(Uri.EscapeDataString(field));
                }
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriBuilder.ToString());
            request.Method = WebRequestMethods.Http.Get;

            if (RequireAuthentication)
            {
                request.Headers.Add("Authorization", "Bearer " + Authentication.GetAuthToken());
            }

            foreach(KeyValuePair<string, string> headerValuePair in Headers)
            {
                request.Headers.Add(headerValuePair.Key, headerValuePair.Value);
            }

            try
            {
                string json;
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    json = await reader.ReadToEndAsync();
                }

                RequestSuccessful = true;

                return json;
            }
            catch(WebException ex)
            {
                return await HandleResponseWebException(ex, new ResponseWebExceptionRetryCallback(GetJson));
            }
        }

        public async Task<T> GetAnonymousObject<T>(T anonymous)
        {
            string json = await GetJson();

            if (!string.IsNullOrEmpty(json))
            {
                T obj = JsonConvert.DeserializeAnonymousType(json, anonymous);
                TimeZoneCorrection(obj, true);
                return obj;
            }

            return default(T);
        }

        public async Task<T> GetObject<T>()
        {
            string json = await GetJson();

            if (!string.IsNullOrEmpty(json))
            {
                T retObject = JsonConvert.DeserializeObject<T>(json);
                TimeZoneCorrection(retObject, true);
                return retObject;
            }

            return default(T);
        }
    }
}
