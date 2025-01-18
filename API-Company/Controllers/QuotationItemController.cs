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
    public class QuotationItemController : DataObjectController<QuotationItem>
    {
        protected long? CompanyID => long.Parse(Request.Headers.GetValues("CompanyID").First());

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<QuotationItem>(qi => new List<object>()
        {
            qi.QuotationItemID,
            qi.QuotationID,
            qi.ItemID,
            qi.Item.ItemID,
            qi.Item.Name,
            qi.Item.Image,
            qi.MinimumQuantity,
            qi.UnitCost
        });

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new LongSearchCondition<QuotationItem>()
            {
                Field = FieldPathUtility.CreateFieldPathsAsList<QuotationItem>(qi => new List<object>() { qi.Quotation.CompanyIDFrom }).First(),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = CompanyID
            };
        }
    }
}
