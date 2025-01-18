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
using WebModels.fleet;

namespace WebModels.purchasing.Validations
{
    public class FulfillmentPlanRailcarIsIdleCondition : Condition
    {
        public override bool Evaluate(DataObject dataObject, ITransaction transaction)
        {
            if (!(dataObject is FulfillmentPlan fulfillmentPlan))
            {
                throw new InvalidCastException("dataObject must be a FulfillmentPlan");
            }

            if (fulfillmentPlan.RailcarID == null)
            {
                return true;
            }

            FulfillmentPlan fulfillmentPlanForRelationships = DataObject.GetReadOnlyByPrimaryKey<FulfillmentPlan>(fulfillmentPlan.FulfillmentPlanID, transaction, FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlan>(fp => new object[]
            {
                fp.FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.PurchaseOrderID,
                fp.FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.LocationOrigin.CompanyID,
                fp.FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.GovernmentIDOrigin
            }));

            PurchaseOrder fulfillmentPlanPurchaseOrder = fulfillmentPlanForRelationships?.FulfillmentPlanPurchaseOrderLines.First()?.PurchaseOrderLine.PurchaseOrder;
            long? thisPOID = fulfillmentPlanPurchaseOrder?.PurchaseOrderID;
            long? thisCompanyID = fulfillmentPlanPurchaseOrder?.LocationOrigin?.CompanyID;
            long? thisGovernmentID = fulfillmentPlanPurchaseOrder?.GovernmentIDOrigin;

            Railcar railcar = DataObject.GetReadOnlyByPrimaryKey<Railcar>(fulfillmentPlan.RailcarID, transaction, FieldPathUtility.CreateFieldPathsAsList<Railcar>(r => new object[]
            {
                r.CompanyLeasedTo.CompanyID,
                r.GovernmentLeasedTo.GovernmentID,
                r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrderID,
                r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.Status
            }));

            if ((railcar.CompanyLeasedTo?.CompanyID != null && railcar.CompanyLeasedTo.CompanyID != thisCompanyID) || (railcar.GovernmentLeasedTo?.GovernmentID != null && railcar.GovernmentLeasedTo.GovernmentID != thisGovernmentID))
            {
                return false;
            }

            foreach(FulfillmentPlanPurchaseOrderLine fulfillmentPlanPurchaseOrderLine in railcar.FulfillmentPlans?.SelectMany(fp => fp.FulfillmentPlanPurchaseOrderLines))
            {
                PurchaseOrderLine purchaseOrderLine = fulfillmentPlanPurchaseOrderLine.PurchaseOrderLine;
                if (purchaseOrderLine.PurchaseOrder.Status != PurchaseOrder.Statuses.Completed && (thisPOID == null || purchaseOrderLine.PurchaseOrderID != thisPOID))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
