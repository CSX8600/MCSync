using System;
using System.Collections.Generic;

namespace GovernmentPortal.Models
{
    public class Promotion
    {
        public long? PromotionID { get; set; }
        public long? LocationID { get; set; }
        public Location Location { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Name { get; set; }
        public List<PromotionLocationItem> PromotionLocationItems { get; set; }
    }
}
