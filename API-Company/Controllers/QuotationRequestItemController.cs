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
    public class QuotationRequestItemController : DataObjectController<QuotationRequestItem>
    {
        protected long? CompanyID => long.Parse(Request.Headers.GetValues("CompanyID").First());

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<QuotationRequestItem>(qri => new List<object>()
        {
            qri.QuotationRequestItemID,
            qri.QuotationRequestID,
            qri.ItemID,
            qri.Item.ItemID,
            qri.Item.Name,
            qri.Item.Image,
            qri.Quantity
        });

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                new LongSearchCondition<QuotationRequestItem>()
                {
                    Field = FieldPathUtility.CreateFieldPathsAsList<QuotationRequestItem>(qri => new List<object>() { qri.QuotationRequest.CompanyIDFrom }).First(),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = CompanyID
                },
                new LongSearchCondition<QuotationRequestItem>()
                {
                    Field = FieldPathUtility.CreateFieldPathsAsList<QuotationRequestItem>(qri => new List<object>() { qri.QuotationRequest.CompanyIDTo }).First(),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = CompanyID
                });
        }
    }
}
