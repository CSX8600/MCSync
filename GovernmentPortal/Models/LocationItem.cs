using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernmentPortal.Models
{
    public class LocationItem
    {
        public long? LocationItemID { get; set; }
        public long? LocationID { get; set; }
        public Location Location { get; set; }
        public long? GovernmentID { get; set; }
        public Government Government { get; set; }
        public long? ItemID { get; set; }
        public Item Item { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? BasePrice { get; set; }
        public PromotionLocationItem CurrentPromotionLocationItem { get; set; }

        /// <summary>
        /// This is only used during PriceCheck
        /// </summary>
        public List<QuotationItem> QuotedPrices { get; set; }
    }
}
