namespace CompanyStudio.Models
{
    public class QuotationRequestItem
    {
        public long? QuotationRequestItemID { get; set; }
        public long? QuotationRequestID { get; set; }
        public QuotationRequest QuotationRequest { get; set; }
        public long? ItemID { get; set; }
        public Item Item { get; set; }
        public decimal? Quantity { get; set; }
    }
}
