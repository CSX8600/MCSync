using API.Common;
using API.Common.Attributes;
using API_Company.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System.Collections.Generic;
using System.Linq;
using WebModels.company;
using WebModels.purchasing;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(RequiredPermissions = new[] { nameof(LocationEmployee.ManagePurchaseOrders) })]
    public class QuotationRequestController : DataObjectController<QuotationRequest>
    {
        protected long? CompanyID => long.Parse(Request.Headers.GetValues("CompanyID").First());

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<QuotationRequest>(qr => new List<object>()
        {
            qr.QuotationRequestID,
            qr.CompanyIDFrom,
            qr.CompanyFrom.CompanyID,
            qr.CompanyFrom.Name,
            qr.CompanyIDTo,
            qr.CompanyTo.CompanyID,
            qr.CompanyTo.Name,
            qr.GovernmentIDFrom,
            qr.GovernmentFrom.GovernmentID,
            qr.GovernmentFrom.Name,
            qr.GovernmentIDTo,
            qr.GovernmentTo.GovernmentID,
            qr.GovernmentTo.Name,
            qr.Notes,
            qr.QuotationRequestItems.First().QuotationRequestItemID,
            qr.QuotationRequestItems.First().QuotationRequestID,
            qr.QuotationRequestItems.First().ItemID,
            qr.QuotationRequestItems.First().Item.ItemID,
            qr.QuotationRequestItems.First().Item.Name,
            qr.QuotationRequestItems.First().Item.Image,
            qr.QuotationRequestItems.First().Quantity
        });

        public override bool AllowGetAll => true;

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                new LongSearchCondition<QuotationRequest>()
                {
                    Field = nameof(QuotationRequest.CompanyIDFrom),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = CompanyID
                },
                new LongSearchCondition<QuotationRequest>()
                {
                    Field = nameof(QuotationRequest.CompanyIDTo),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = CompanyID
                });
        }
    }
}
