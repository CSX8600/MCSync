using API.Common;
using API.Common.Attributes;
using API_Company.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebModels.company;
using WebModels.purchasing;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(RequiredPermissions = new[] { nameof(LocationEmployee.ManagePurchaseOrders) })]
    public class QuotationController : DataObjectController<Quotation>
    {
        protected long? CompanyID => long.Parse(Request.Headers.GetValues("CompanyID").First());

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<Quotation>(q => new List<object>()
        {
            q.QuotationID,
            q.CompanyIDFrom,
            q.CompanyFrom.CompanyID,
            q.CompanyFrom.Name,
            q.CompanyIDTo,
            q.CompanyTo.CompanyID,
            q.CompanyTo.Name,
            q.GovernmentIDFrom,
            q.GovernmentFrom.GovernmentID,
            q.GovernmentFrom.Name,
            q.GovernmentIDTo,
            q.GovernmentTo.GovernmentID,
            q.GovernmentTo.Name,
            q.IsRepeatable,
            q.ExpirationTime,
            q.QuotationItems.First().QuotationItemID,
            q.QuotationItems.First().QuotationID,
            q.QuotationItems.First().ItemID,
            q.QuotationItems.First().Item.ItemID,
            q.QuotationItems.First().Item.Name,
            q.QuotationItems.First().Item.Image,
            q.QuotationItems.First().UnitCost,
            q.QuotationItems.First().MinimumQuantity
        });

        public override bool AllowGetAll => true;

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new LongSearchCondition<Quotation>()
            {
                Field = nameof(Quotation.CompanyIDFrom),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = CompanyID
            };
        }

        public override Task<Quotation> Get(long id)
        {
            Search<Quotation> quotationSearch = new Search<Quotation>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<Quotation>()
                {
                    Field = nameof(Quotation.QuotationID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                },
                new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                    new LongSearchCondition<Quotation>()
                    {
                        Field = nameof(Quotation.CompanyIDFrom),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = CompanyID
                    },
                    new LongSearchCondition<Quotation>()
                    {
                        Field = nameof(Quotation.CompanyIDTo),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = CompanyID
                    })));

            return Task.Run(async () => quotationSearch.GetReadOnlyReader(null, await FieldsToRetrieve()).FirstOrDefault());
        }

        [HttpGet]
        public async Task<List<Quotation>> GetReceived()
        {
            Search<Quotation> quotationSearch = new Search<Quotation>(new LongSearchCondition<Quotation>()
            {
                Field = nameof(Quotation.CompanyIDTo),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = CompanyID
            });

            return await Task.Run(async () => quotationSearch.GetReadOnlyReader(null, await FieldsToRetrieve()).ToList());
        }
    }
}
