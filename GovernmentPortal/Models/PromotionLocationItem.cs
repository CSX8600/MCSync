namespace GovernmentPortal.Models
{
    public class PromotionLocationItem
    {
        public long? PromotionLocationItemID { get; set; }
        public long? PromotionID { get; set; }
        public Promotion Promotion { get; set; }
        public long? LocationItemID { get; set; }
        public LocationItem LocationItem { get; set; }
        public decimal? PromotionPrice { get; set; }
    }
}
