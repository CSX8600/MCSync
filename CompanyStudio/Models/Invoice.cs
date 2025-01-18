using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStudio.Models
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
        public long? PurchaseOrderID { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
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
        public decimal? Amount { get; set; }
        public bool AutoReceive { get; set; }

        public List<InvoiceLine> InvoiceLines { get; set; }
        public List<InvoiceSalesTax> InvoiceSalesTaxes { get; set; }
    }
}
