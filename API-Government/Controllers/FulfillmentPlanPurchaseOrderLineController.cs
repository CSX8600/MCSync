using API.Common;
using API.Common.Attributes;
using API_Government.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Utility;
using System.Collections.Generic;
using System.Linq;
using WebModels.gov;
using WebModels.purchasing;

namespace API_Government.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [GovernmentAccess(RequiredPermissions = new[] { nameof(Official.ManagePurchaseOrders) })]
    public class FulfillmentPlanPurchaseOrderLineController : DataObjectController<FulfillmentPlanPurchaseOrderLine>
    {
        public override IEnumerable<string> DefaultRetrievedFields => Schema.GetSchemaObject<FulfillmentPlanPurchaseOrderLine>().GetFields().Select(f => f.FieldName);

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                    new ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                    {
                        RelationshipName = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.PurchaseOrderLine.PurchaseOrder.GovernmentOrigin.Officials }).First(),
                        ExistsType = ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>.ExistsTypes.Exists,
                        Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                            new LongSearchCondition<Official>()
                            {
                                Field = nameof(Official.UserID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = SecurityProfile.UserID
                            },
                            new BooleanSearchCondition<Official>()
                            {
                                Field = nameof(Official.ManagePurchaseOrders),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = true
                            })
                    },
                    new ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                    {
                        RelationshipName = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.PurchaseOrderLine.PurchaseOrder.GovernmentDestination.Officials }).First(),
                        ExistsType = ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>.ExistsTypes.Exists,
                        Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                            new LongSearchCondition<Official>()
                            {
                                Field = nameof(Official.UserID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = SecurityProfile.UserID
                            },
                            new BooleanSearchCondition<Official>()
                            {
                                Field = nameof(Official.ManagePurchaseOrders),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = true
                            })
                    });
        }
    }
}
