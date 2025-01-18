using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModels.purchasing.Validations
{
    internal class PurchaseOrderRoutesValidOnSubmitCondition : Condition
    {
        private readonly List<string> fulfillmentPlanSearchFields = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlan>(fp => new List<object>()
        {
            fp.FulfillmentPlanRoutes.First().CompanyIDFrom,
            fp.FulfillmentPlanRoutes.First().CompanyIDTo,
            fp.FulfillmentPlanRoutes.First().GovernmentIDFrom,
            fp.FulfillmentPlanRoutes.First().GovernmentIDTo,
            fp.FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlanID,
            fp.FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLineID
        });

        public override bool Evaluate(DataObject dataObject, ITransaction transaction)
        {
            if (!(dataObject is PurchaseOrder purchaseOrder))
            {
                throw new InvalidCastException("dataObject must be a PurchaseOrder");
            }

            if (purchaseOrder.PurchaseOrderID == null ||
                !purchaseOrder.IsFieldDirty(nameof(PurchaseOrder.Status)) ||
                ((PurchaseOrder.Statuses)purchaseOrder.GetDirtyValue(nameof(PurchaseOrder.Status)) != PurchaseOrder.Statuses.Draft) ||
                purchaseOrder.Status != PurchaseOrder.Statuses.Pending)
            {
                return true;
            }

            Search<FulfillmentPlan> fulfillmentPlanSearch = new Search<FulfillmentPlan>(new ExistsSearchCondition<FulfillmentPlan>()
            {
                RelationshipName = nameof(FulfillmentPlan.FulfillmentPlanPurchaseOrderLines),
                ExistsType = ExistsSearchCondition<FulfillmentPlan>.ExistsTypes.Exists,
                Condition = new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                {
                    Field = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.PurchaseOrderLine.PurchaseOrderID }).First(),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = purchaseOrder.PurchaseOrderID
                }
            });

            List<FulfillmentPlan> fulfillmentPlans = fulfillmentPlanSearch.GetReadOnlyReader(transaction, fulfillmentPlanSearchFields).ToList();

            Dictionary<long?, List<(long?, long?, long?, long?)>> routeByPurchaseOrderLine = new Dictionary<long?, List<(long?, long?, long?, long?)>>();
            foreach(FulfillmentPlanPurchaseOrderLine fulfillmentPlanPurchaseOrderLine in fulfillmentPlans.SelectMany(fp => fp.FulfillmentPlanPurchaseOrderLines))
            {
                FulfillmentPlan plan = fulfillmentPlans.Single(fp => fp.FulfillmentPlanID == fulfillmentPlanPurchaseOrderLine.FulfillmentPlanID);

                if (!routeByPurchaseOrderLine.ContainsKey(fulfillmentPlanPurchaseOrderLine.PurchaseOrderLineID))
                {
                    routeByPurchaseOrderLine.Add(fulfillmentPlanPurchaseOrderLine.PurchaseOrderLineID, new List<(long?, long?, long?, long?)>());
                    
                    foreach(FulfillmentPlanRoute route in plan.FulfillmentPlanRoutes)
                    {
                        routeByPurchaseOrderLine[fulfillmentPlanPurchaseOrderLine.PurchaseOrderLineID].Add((route.CompanyIDFrom, route.CompanyIDTo, route.GovernmentIDFrom, route.GovernmentIDTo));
                    }
                }
                else
                {
                    List<(long?, long?, long?, long?)> routes = routeByPurchaseOrderLine[fulfillmentPlanPurchaseOrderLine.PurchaseOrderLineID];
                    if (routes.Count != plan.FulfillmentPlanRoutes.Count)
                    {
                        return false;
                    }

                    for(int i = 0; i < routes.Count; i++)
                    {
                        FulfillmentPlanRoute route = plan.FulfillmentPlanRoutes.ElementAt(i);
                        (long?, long?, long?, long?) expectedRoute = routes[i];

                        if (route.CompanyIDFrom != expectedRoute.Item1 ||
                            route.CompanyIDTo != expectedRoute.Item2 ||
                            route.GovernmentIDFrom != expectedRoute.Item3 ||
                            route.GovernmentIDTo != expectedRoute.Item4)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
