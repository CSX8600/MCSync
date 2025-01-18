using System;
using System.Collections.Generic;

namespace CompanyStudio.Models
{
    public class Quotation
    {
        public long? QuotationID { get; set; }
        public long? CompanyIDFrom { get; set; }
        public Company CompanyFrom { get; set; }
        public long? CompanyIDTo { get; set; }
        public Company CompanyTo { get; set; }
        public long? GovernmentIDFrom { get; set; }
        public Government GovernmentFrom { get; set; }
        public long? GovernmentIDTo { get; set; }
        public Government GovernmentTo { get; set; }
        public bool IsRepeatable { get; set; }
        public DateTime? ExpirationTime { get; set; }

        public List<QuotationItem> QuotationItems { get; set; } = new List<QuotationItem>();
    }
}
