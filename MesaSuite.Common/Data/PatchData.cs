using MesaSuite.Common.Attributes;
using MesaSuite.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MesaSuite.Common.Data
{
    public class PatchData : DataAccess
    {
        private PatchMethods _patchMethod;
        private long? _primaryKey;
        private Dictionary<string, object> _values;
        public PatchData(APIs api, string resource, PatchMethods patchMethod, long? primaryKeyOfRecord, Dictionary<string, object> valuesToUpdate) : base(api, resource)
        {
            _patchMethod = patchMethod;
            _primaryKey = primaryKeyOfRecord;
            _values = valuesToUpdate;
        }

        public PatchMethods PatchMethod { get => _patchMethod; set => _patchMethod = value; }
        public long? PrimaryKey { get => _primaryKey; set => _primaryKey = value; }
        public Dictionary<string, object> Values { get => _values; set => _values = value; }

        public async Task Execute()
        {
            await InternalExecute();
        }

        private async Task<string> InternalExecute()
        {
            HttpWebRequest request = WebRequest.CreateHttp(GetResourceURI());
            request.ContentType = "application/json";
            request.Method = "PATCH";

            if (RequireAuthentication)
            {
                request.Headers.Add("Authorization", "Bearer " + Authentication.GetAuthToken());
            }
            foreach (KeyValuePair<string, string> kvp in Headers)
            {
                request.Headers.Add(kvp.Key, kvp.Value);
            }

            Dictionary<string, object> valueEdits = new Dictionary<string, object>();
            foreach(KeyValuePair<string, object> kvp in Values)
            {
                if (kvp.Value != null && (kvp.Value is DateTime? || kvp.Value is DateTime))
                {
                    DateTime currentValue;
                    if (kvp.Value is DateTime?)
                    {
                        currentValue = ((DateTime?)kvp.Value).Value;
                    }
                    else
                    {
                        currentValue = (DateTime)kvp.Value;
                    }

                    currentValue = currentValue.ConvertToServerTime();

                    valueEdits[kvp.Key] = currentValue;
                }
            }

            foreach(KeyValuePair<string, object> edit in valueEdits)
            {
                Values[edit.Key] = edit.Value;
            }

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {

                string patchRequest = JsonConvert.SerializeObject(new PatchInfo()
                {
                    PrimaryKey = _primaryKey,
                    Method = _patchMethod.GetValue(),
                    Values = _values,
                    RequestFields = RequestFields,
                    AdditionalFields = AdditionalFields
                });

                writer.Write(patchRequest);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync()) { }
                RequestSuccessful = true;
            }
            catch(WebException ex)
            {
                await HandleResponseWebException(ex, new ResponseWebExceptionRetryCallback(InternalExecute));
            }

            return await Task.FromResult<string>(null);
        }

        public enum PatchMethods
        {
            [EnumValue("add")]
            Add,
            [EnumValue("remove")]
            Remove,
            [EnumValue("replace")]
            Replace
        }
    }
}
