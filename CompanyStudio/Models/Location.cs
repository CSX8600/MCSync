using System.Collections.Generic;

namespace CompanyStudio.Models
{
    public class Location
    {
        public long? LocationID { get; set; }
        public long? CompanyID { get; set; }
        public Company Company { get; set; }
        public string Name { get; set; }
        public string InvoiceNumberPrefix { get; set; }
        public string NextInvoiceNumber { get; set; }
        public long? EmailImplementationIDPayableInvoice { get; set; }
        public long? EmailImplementationIDReadyForReceipt { get; set; }
        public long? EmailImplementationIDRegisterOffline { get; set; }
        public long? AccountIDStoreRevenue { get; set; }

        public List<LocationEmployee> LocationEmployees { get; set; }
        public List<LocationGovernment> LocationGovernments { get; set; }

        public string DisplayName => $"{Company?.Name} ({Name})";
    }
}
