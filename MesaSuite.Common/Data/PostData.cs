using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MesaSuite.Common.Data
{
    public class PostData : DataAccess
    {
        public object ObjectToPost { get; set; }
        public PostData(APIs api, string resource) : base(api, resource) { }
        public PostData(APIs api, string resource, object objectToPost) : base(api, resource)
        {
            ObjectToPost = objectToPost;
        }

        public async Task<T> Execute<T>()
        {
            string json = await GetJson();

            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task ExecuteNoResult()
        {
            await GetJson();
        }

        private async Task<string> GetJson()
        {
            HttpWebRequest request = WebRequest.CreateHttp(GetResourceURI());
            request.Method = WebRequestMethods.Http.Post;
            if (RequireAuthentication)
            {
                request.Headers.Add("Authorization", "Bearer " + Authentication.GetAuthToken());
            }
            foreach(KeyValuePair<string, string> kvp in Headers)
            {
                request.Headers.Add(kvp.Key, kvp.Value);
            }
            request.ContentType = "application/json";

            TimeZoneCorrection(ObjectToPost, false);

            try
            {
                JObject jObject = JObject.FromObject(ObjectToPost);
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
                    string responseJson;
                    using (WebResponse response = await request.GetResponseAsync())
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = await reader.ReadToEndAsync();
                    }
                    RequestSuccessful = true;
                    return responseJson;
                }
                catch (WebException ex)
                {
                    return await HandleResponseWebException(ex, new ResponseWebExceptionRetryCallback(GetJson));
                }
            }
            finally
            {
                TimeZoneCorrection(ObjectToPost, true);
            }
        }
    }
}
