using System;

namespace CompanyStudio.Models
{
    public class Fulfillment
    {
        public long? FulfillmentID { get; set; }
        public long? PurchaseOrderLineID { get; set; }
        public PurchaseOrderLine PurchaseOrderLine { get; set; }
        public long? RailcarID { get; set; }
        public Railcar Railcar { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? FulfillmentTime { get; set; }
        public bool IsComplete { get; set; }
        public long? InvoiceLineID { get; set; }
        public InvoiceLine InvoiceLine { get; set; }

        public string DisplayString
        {
            get
            {
                if (FulfillmentID == null)
                {
                    return null;
                }

                return $"{FulfillmentTime?.ToString("MM/dd/yyyy HH:mm") ?? "[Unknown Time]"} - {Quantity?.ToString() ?? "[Unknown Qty]"} - {Railcar?.ReportingID ?? "[No Railcar]"}";
            }
        }
    }
}
