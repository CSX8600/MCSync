namespace CompanyStudio.Models
{
    public class QuotationItem
    {
        public long? QuotationItemID { get; set; }
        public long? QuotationID { get; set; }
        public Quotation Quotation { get; set; }
        public long? ItemID { get; set; }
        public Item Item { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? MinimumQuantity { get; set; }
    }
}
