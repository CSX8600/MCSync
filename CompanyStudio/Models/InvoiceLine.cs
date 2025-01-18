namespace CompanyStudio.Models
{
    public class InvoiceLine
    {
        public long? InvoiceLineID { get; set; }
        public long? InvoiceID { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Total { get; set; }
        public string Description { get; set; }
        public long? ItemID { get; set; }
        public Item Item { get; set; }
        public long? PurchaseOrderLineID { get; set; }
        public PurchaseOrderLine PurchaseOrderLine { get; set; }
        public Fulfillment Fulfillment { get; set; }
    }
}
