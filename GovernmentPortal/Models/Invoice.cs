using System;
using System.Collections.Generic;

namespace GovernmentPortal.Models
{
    public class Invoice
    {
        public long? InvoiceID { get; set; }
        public long? GovernmentIDFrom { get; set; }
        public Government GovernmentFrom { get; set; }
        public long? LocationIDFrom { get; set; }
        public Location LocationFrom { get; set; }
        public long? GovernmentIDTo { get; set; }
        public Government GovernmentTo { get; set; }
        public long? LocationIDTo { get; set; }
        public Location LocationTo { get; set; }
        public string InvoiceNumber { get; set; }
        public string Description { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        public enum Statuses
        {
            WorkInProgress,
            Sent,
            ReadyForReceipt,
            Complete
        }

        public Statuses? Status { get; set; }

        public long? AccountIDFrom { get; set; }
        public string AccountFromHistorical { get; set; }
        public long? AccountIDTo { get; set; }
        public string AccountToHistorical { get; set; }
        public bool AutoReceive { get; set; }

        public List<InvoiceLine> InvoiceLines { get; set; }
    }
}
