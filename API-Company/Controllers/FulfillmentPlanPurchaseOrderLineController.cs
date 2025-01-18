using API.Common;
using API.Common.Attributes;
using API_Company.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.company;
using WebModels.purchasing;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(RequiredPermissions = new[] { nameof(LocationEmployee.ManagePurchaseOrders) })]
    public class FulfillmentPlanPurchaseOrderLineController : DataObjectController<FulfillmentPlanPurchaseOrderLine>
    {
        public override IEnumerable<string> DefaultRetrievedFields => Schema.GetSchemaObject<FulfillmentPlanPurchaseOrderLine>().GetFields().Select(f => f.FieldName);

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                    new ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                    {
                        RelationshipName = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.PurchaseOrderLine.PurchaseOrder.LocationOrigin.LocationEmployees }).First(),
                        ExistsType = ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>.ExistsTypes.Exists,
                        Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                            new LongSearchCondition<LocationEmployee>()
                            {
                                Field = FieldPathUtility.CreateFieldPathsAsList<LocationEmployee>(lp => new List<object>() { lp.Employee.UserID }).First(),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = SecurityProfile.UserID
                            },
                            new BooleanSearchCondition<LocationEmployee>()
                            {
                                Field = nameof(LocationEmployee.ManagePurchaseOrders),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = true
                            })
                    },
                    new ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                    {
                        RelationshipName = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.PurchaseOrderLine.PurchaseOrder.LocationDestination.LocationEmployees }).First(),
                        ExistsType = ExistsSearchCondition<FulfillmentPlanPurchaseOrderLine>.ExistsTypes.Exists,
                        Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                            new LongSearchCondition<LocationEmployee>()
                            {
                                Field = FieldPathUtility.CreateFieldPathsAsList<LocationEmployee>(lp => new List<object>() { lp.Employee.UserID }).First(),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = SecurityProfile.UserID
                            },
                            new BooleanSearchCondition<LocationEmployee>()
                            {
                                Field = nameof(LocationEmployee.ManagePurchaseOrders),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = true
                            })
                    });
        }
    }
}
