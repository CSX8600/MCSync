using API.Common.Extensions;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Common
{
    public abstract class DataObjectController<TDataObject> : ApiController where TDataObject : DataObject
    {
        protected event EventHandler<SecurityCheckEventArgs> GetAllSecurityCheck;
        protected event EventHandler<SecurityCheckEventArgs> GetSecurityCheck;
        protected event EventHandler<SecurityCheckEventArgs> PostSecurityCheck;
        protected event EventHandler<SecurityCheckEventArgs> PutSecurityCheck;
        protected event EventHandler<SecurityCheckEventArgs> DeleteSecurityCheck;
        protected event EventHandler<SecurityCheckEventArgs> PatchSecurityCheck;

        public abstract IEnumerable<string> DefaultRetrievedFields { get; }

        protected virtual IEnumerable<string> RequestableFields => Enumerable.Empty<string>();

        private IEnumerable<string> retrieveFields = null;
        protected async Task<IEnumerable<string>> FieldsToRetrieve()
        {
            if (retrieveFields != null)
            {
                return retrieveFields;
            }

            if (RequestableFields == null || !RequestableFields.Any())
            {
                return DefaultRetrievedFields;
            }

            (await Request.Content.ReadAsStreamAsync()).Seek(0, System.IO.SeekOrigin.Begin);
            string body = await Request.Content.ReadAsStringAsync();

            var requestFields = new { requestfields = new string[0], additionalfields = new string[0] };
            requestFields = JsonConvert.DeserializeAnonymousType(body, requestFields);

            if ((requestFields?.requestfields == null || !requestFields.requestfields.Any()) && (requestFields?.additionalfields == null || !requestFields.additionalfields.Any()))
            {
                NameValueCollection queryString = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                if (queryString.AllKeys.Contains("requestField", StringComparer.OrdinalIgnoreCase) || queryString.AllKeys.Contains("additionalField", StringComparer.OrdinalIgnoreCase))
                {
                    requestFields = new { requestfields = queryString.GetValues("requestField"), additionalfields = queryString.GetValues("additionalField") };
                }
                else
                {
                    return DefaultRetrievedFields;
                }
            }

            if (requestFields.requestfields?.Any() ?? false)
            {
                retrieveFields = DefaultRetrievedFields.Concat(RequestableFields).Intersect(requestFields.requestfields);
            }
            else if (requestFields.additionalfields?.Any() ?? false)
            {
                retrieveFields = DefaultRetrievedFields.Concat(RequestableFields.Intersect(requestFields.additionalfields));
            }

            return retrieveFields;
        }

        public virtual bool AllowGetAll { get; }

        public virtual ISearchCondition GetBaseSearchCondition() { return null; }

        [HttpGet]
        public async virtual Task<TDataObject> Get(long id)
        {
            SecurityCheckEventArgs securityCheck = new SecurityCheckEventArgs(id, null);
            GetSecurityCheck?.Invoke(this, securityCheck);
            if (!securityCheck.IsValid)
            {
                return null;
            }

            SchemaObject schemaObject = Schema.GetSchemaObject<TDataObject>();

            ISearchCondition searchCondition = new LongSearchCondition<TDataObject>()
            {
                Field = schemaObject.PrimaryKeyField.FieldName,
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = id
            };

            ISearchCondition baseCondition = GetBaseSearchCondition();
            if (baseCondition != null)
            {
                searchCondition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    searchCondition,
                    baseCondition);
            }

            return new Search<TDataObject>(searchCondition).GetReadOnly(null, await FieldsToRetrieve());
        }

        [HttpGet]
        public async virtual Task<IHttpActionResult> GetAll()
        {
            if (!AllowGetAll)
            {
                return NotFound();
            }

            SchemaObject schemaObject = Schema.GetSchemaObject<TDataObject>();
            List<string> fields = (await FieldsToRetrieve()).ToList();
            fields.Add(schemaObject.PrimaryKeyField.FieldName);

            List<DataObject> retVal = new List<DataObject>();
            Search<TDataObject> search = new Search<TDataObject>(GetBaseSearchCondition());
            foreach(TDataObject dataObject in search.GetReadOnlyReader(null, fields))
            {
                SecurityCheckEventArgs securityCheck = new SecurityCheckEventArgs(dataObject.PrimaryKeyField.GetValue(dataObject) as long?, null);
                GetAllSecurityCheck?.Invoke(this, securityCheck);
                if (!securityCheck.IsValid)
                {
                    continue;
                }

                retVal.Add(dataObject);
            }

            return Ok(retVal);
        }

        [HttpPost]
        public async virtual Task<IHttpActionResult> Post(TDataObject dataObject)
        {
            long? primaryKeyValue = ConvertUtility.GetNullableLong(dataObject.PrimaryKeyField.GetValue(dataObject));
            
            if (primaryKeyValue != null && primaryKeyValue != 0)
            {
                return BadRequest("Updates are not allowed with this method");
            }

            foreach(Field field in Schema.GetSchemaObject<TDataObject>().GetFields().Where(f => !f.HasOperation))
            {
                if (!(await FieldsToRetrieve()).Contains(field.FieldName))
                {
                    field.SetValue(dataObject, field.ReturnType.IsValueType ? Activator.CreateInstance(field.ReturnType) : null);
                }
            }

            PrePostCommit(dataObject);

            using (ITransaction transaction = SQLProviderFactory.GenerateTransaction(Schema.GetSchemaObject<TDataObject>().ConnectionName ?? "_default"))
            {
                if (!dataObject.Save(transaction))
                {
                    return dataObject.HandleFailedValidation(this);
                }

                SecurityCheckEventArgs securityCheck = new SecurityCheckEventArgs(dataObject.PrimaryKeyField.GetValue(dataObject) as long?, transaction);
                PostSecurityCheck?.Invoke(this, securityCheck);
                if (!securityCheck.IsValid)
                {
                    transaction.Rollback();
                    return BadRequest("You do not have permission to save this object");
                }

                SchemaObject schemaObject = Schema.GetSchemaObject<TDataObject>();

                Search<TDataObject> securitySearch = new Search<TDataObject>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    GetBaseSearchCondition(),
                    new LongSearchCondition<TDataObject>()
                    {
                        Field = schemaObject.PrimaryKeyField.FieldName,
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = ConvertUtility.GetNullableLong(schemaObject.PrimaryKeyField.GetValue(dataObject))
                    }));

                if (!securitySearch.ExecuteExists(transaction))
                {
                    transaction.Rollback();
                    return BadRequest("You do not have permission to save this object");
                }

                transaction.Commit();
            }

            return Created("Get/" + dataObject.PrimaryKeyField.GetValue(dataObject), DataObject.GetReadOnlyByPrimaryKey<TDataObject>(ConvertUtility.GetNullableLong(dataObject.PrimaryKeyField.GetValue(dataObject)), null, await FieldsToRetrieve()));
        }

        protected virtual void PrePostCommit(TDataObject dataObject) { }

        [HttpPut]
        public async virtual Task<IHttpActionResult> Put(TDataObject dataObject)
        {
            TDataObject dbDataObject = await GetPutObjectResult(dataObject);

            if (dbDataObject == null)
            {
                return NotFound();
            }

            if (!dbDataObject.Save())
            {
                return dbDataObject.HandleFailedValidation(this);
            }

            return Ok(DataObject.GetReadOnlyByPrimaryKey<TDataObject>(ConvertUtility.GetNullableLong(dbDataObject.PrimaryKeyField.GetValue(dbDataObject)), null, await FieldsToRetrieve()));
        }

        protected async Task<TDataObject> GetPutObjectResult(TDataObject modifiedObject)
        {
            SecurityCheckEventArgs securityCheck = new SecurityCheckEventArgs(modifiedObject.PrimaryKeyField.GetValue(modifiedObject) as long?, null);
            PutSecurityCheck?.Invoke(this, securityCheck);
            if (!securityCheck.IsValid)
            {
                return null;
            }

            SchemaObject schemaObject = Schema.GetSchemaObject<TDataObject>();
            Search<TDataObject> dataObjectSearch = new Search<TDataObject>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                GetBaseSearchCondition(),
                new LongSearchCondition<TDataObject>()
                {
                    Field = schemaObject.PrimaryKeyField.FieldName,
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = ConvertUtility.GetNullableLong(modifiedObject.PrimaryKeyField.GetValue(modifiedObject)),
                }));

            TDataObject dbDataObject = dataObjectSearch.GetEditable();

            if (dbDataObject == null)
            {
                return null;
            }

            foreach (Field field in schemaObject.GetFields().Where(f => !f.HasOperation && f != schemaObject.PrimaryKeyField))
            {
                if (!(await FieldsToRetrieve()).Contains(field.FieldName))
                {
                    continue;
                }

                field.SetValue(dbDataObject, field.GetValue(modifiedObject));
            }

            return dbDataObject;
        }

        [HttpDelete]
        public virtual IHttpActionResult Delete(long id)
        {
            SecurityCheckEventArgs securityCheck = new SecurityCheckEventArgs(id, null);
            DeleteSecurityCheck?.Invoke(this, securityCheck);
            if (!securityCheck.IsValid)
            {
                return Unauthorized();
            }

            SchemaObject schemaObject = Schema.GetSchemaObject<TDataObject>();
            Search<TDataObject> dataObjectSearch = new Search<TDataObject>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                GetBaseSearchCondition(),
                new LongSearchCondition<TDataObject>()
                {
                    Field = schemaObject.PrimaryKeyField.FieldName,
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                }));

            TDataObject dataObject = dataObjectSearch.GetEditable();

            if (dataObject == null)
            {
                return NotFound();
            }

            if (!dataObject.Delete())
            {
                return dataObject.HandleFailedValidation(this);
            }

            return Ok();
        }

        [HttpPatch]
        public async virtual Task<IHttpActionResult> Patch(PatchData patchData)
        {
            SecurityCheckEventArgs securityCheck = new SecurityCheckEventArgs(patchData.PrimaryKey, null);
            PatchSecurityCheck?.Invoke(this, securityCheck);
            if (!securityCheck.IsValid)
            {
                return Unauthorized();
            }

            SchemaObject schemaObject = Schema.GetSchemaObject<TDataObject>();
            Search<TDataObject> dataObjectSearch = new Search<TDataObject>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                GetBaseSearchCondition(),
                new LongSearchCondition<TDataObject>()
                {
                    Field = schemaObject.PrimaryKeyField.FieldName,
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = patchData.PrimaryKey
                }));

            TDataObject dataObject = dataObjectSearch.GetEditable();
            if (dataObject == null)
            {
                return NotFound();
            }

            IEnumerable<string> fieldsToRetrieve = await FieldsToRetrieve();
            dataObject.PatchData(patchData.Method, patchData.Values.Where(kvp => fieldsToRetrieve.Contains(kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value));

            if (!dataObject.Save())
            {
                return dataObject.HandleFailedValidation(this);
            }

            return Ok();
        }

        public SecurityProfile SecurityProfile
        {
            get
            {
                Request.Properties.TryGetValue("SecurityProfile", out object securityProfile);
                return securityProfile as SecurityProfile;
            }
        }

        protected class SecurityCheckEventArgs : EventArgs
        {
            public bool IsValid { get; set; } = true;
            public long? ObjectID { get; }
            public ITransaction Transaction { get; }

            public SecurityCheckEventArgs(long? id, ITransaction transaction)
            {
                ObjectID = id;
                Transaction = transaction;
            }
        }
    }
}
