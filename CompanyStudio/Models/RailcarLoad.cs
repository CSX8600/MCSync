namespace CompanyStudio.Models
{
    public class RailcarLoad
    {
        public long? RailcarLoadID { get; set; }
        public long? RailcarID { get; set; }
        public Railcar Railcar { get; set; }
        public long? ItemID { get; set; }
        public Item Item { get; set; }
        public decimal? Quantity { get; set; }
        public long? PurchaseOrderLineID { get; set; }
        public PurchaseOrderLine PurchaseOrderLine { get; set; }
    }
}
