namespace GovernmentPortal.Models
{
    public class FulfillmentPlanPurchaseOrderLine
    {
        public long? FulfillmentPlanPurchaseOrderLineID { get; set; }
        public long? FulfillmentPlanID { get; set; }
        public FulfillmentPlan FulfillmentPlan { get; set; }
        public long? PurchaseOrderLineID { get; set; }
        public PurchaseOrderLine PurchaseOrderLine { get; set; }
    }
}
