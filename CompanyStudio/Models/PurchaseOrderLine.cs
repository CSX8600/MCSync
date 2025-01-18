using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStudio.Models
{
    public class PurchaseOrderLine
    {
        public long? PurchaseOrderLineID { get; set; }
        public long? PurchaseOrderID { get; set; }
        public bool IsService { get; set; }
        public string ServiceDescription { get; set; }
        public long? ItemID { get; set; }
        public Item Item { get; set; }
        public string ItemDescription { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? UnfulfilledQuantity => (Quantity ?? 0M) - (Fulfillments?.Sum(x => x.Quantity) ?? 0M);
        public List<Fulfillment> Fulfillments { get; set; } = new List<Fulfillment>();

        public string DisplayString
        {
            get
            {
                if (IsService)
                {
                    return ServiceDescription;
                }

                string retVal = $"{Quantity}x ";
                if (ItemID != null)
                {
                    retVal += Item?.Name;
                    if (!string.IsNullOrEmpty(ItemDescription))
                    {
                        retVal += " - ";
                    }
                }

                if (!string.IsNullOrEmpty(ItemDescription))
                {
                    retVal += ItemDescription;
                }

                return retVal;
            }
        }

        public string DisplayStringNoQuantity
        {
            get
            {
                if (IsService)
                {
                    return ServiceDescription;
                }

                string retVal = "";
                if (ItemID != null)
                {
                    retVal += Item?.Name;
                    if (!string.IsNullOrEmpty(ItemDescription))
                    {
                        retVal += " - ";
                    }
                }

                if (!string.IsNullOrEmpty(ItemDescription))
                {
                    retVal += ItemDescription;
                }

                return retVal;
            }
        }

        public List<FulfillmentPlanPurchaseOrderLine> FulfillmentPlanPurchaseOrderLines { get; set; } = new List<FulfillmentPlanPurchaseOrderLine>();
        public List<RailcarLoad> RailcarLoads { get; set; } = new List<RailcarLoad>();
    }
}
