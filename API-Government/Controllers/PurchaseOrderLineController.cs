using API.Common;
using API.Common.Attributes;
using API_Government.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebModels.gov;
using WebModels.purchasing;

namespace API_Government.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("gov")]
    [GovernmentAccess(RequiredPermissions = new[] { nameof(Official.ManagePurchaseOrders) })]
    public class PurchaseOrderLineController : DataObjectController<PurchaseOrderLine>
    {
        public override IEnumerable<string> DefaultRetrievedFields => Schema.GetSchemaObject<PurchaseOrderLine>().GetFields().Select(f => f.FieldName)
            .Concat(FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderLine>(pol => new List<object>()
            {
                pol.Item.ItemID,
                pol.Item.Name
            }));

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new ExistsSearchCondition<PurchaseOrderLine>()
            {
                RelationshipName = FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderLine>(pol => new List<object>() { pol.PurchaseOrder.GovernmentOrigin.Officials }).First(),
                ExistsType = ExistsSearchCondition<PurchaseOrderLine>.ExistsTypes.Exists,
                Condition = new LongSearchCondition<Official>()
                {
                    Field = nameof(Official.UserID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = SecurityProfile.UserID
                }
            };
        }

        [HttpGet]
        public async Task<List<PurchaseOrderLine>> GetByPurchaseOrderID(long? id)
        {
            Search<PurchaseOrderLine> purchaseOrderLineSearch = new Search<PurchaseOrderLine>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<PurchaseOrderLine>()
                {
                    Field = nameof(PurchaseOrderLine.PurchaseOrderID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                },
                GetBaseSearchCondition()));

            return purchaseOrderLineSearch.GetReadOnlyReader(null, await FieldsToRetrieve()).ToList();
        }
    }
}
