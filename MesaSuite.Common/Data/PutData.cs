using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MesaSuite.Common.Data
{
    public class PutData : DataAccess
    {
        private object _objectToPut;
        public object ObjectToPut
        {
            get => _objectToPut;
            set => _objectToPut = value;
        }

        public PutData(APIs api, string resource, object objectToPut) : base(api, resource)
        {
            _objectToPut = objectToPut;
        }

        private async Task<string> InternalExecute()
        {
            string uri = GetResourceURI();

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            request.Method = WebRequestMethods.Http.Put;
            if (RequireAuthentication)
            {
                request.Headers.Add("Authorization", "Bearer " + Authentication.GetAuthToken());
            }
            foreach(KeyValuePair<string, string> kvp in Headers)
            {
                request.Headers.Add(kvp.Key, kvp.Value);
            }
            request.ContentType = "application/json";

            TimeZoneCorrection(_objectToPut, false);

            try
            {
                JObject jObject = JObject.FromObject(_objectToPut ?? new object());

                if (RequestFields != null && RequestFields.Any())
                {
                    jObject.Add(new JProperty("requestfields", RequestFields));
                }

                if (AdditionalFields != null && AdditionalFields.Any())
                {
                    jObject.Add(new JProperty("additionalfields", AdditionalFields));
                }

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    await writer.WriteAsync(jObject.ToString());
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
                catch (WebException ex)
                {
                    await HandleResponseWebException(ex, new ResponseWebExceptionRetryCallback(InternalExecute));
                }
            }
            finally
            {
                TimeZoneCorrection(_objectToPut, true);
            }

            return null;
        }

        public async Task<T> Execute<T>()
        {
            string json = await InternalExecute();

            if (string.IsNullOrEmpty(json))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task ExecuteNoResult()
        {
            await InternalExecute();
        }
    }
}
