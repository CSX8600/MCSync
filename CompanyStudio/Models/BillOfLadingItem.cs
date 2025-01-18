using System.Collections.Generic;
using System.Text;

namespace CompanyStudio.Models
{
    public class BillOfLadingItem
    {
        public long? BillOfLadingItemID { get; set; }
        public long? BillOfLadingID { get; set; }
        public BillOfLading BillOfLading { get; set; }
        public long? ItemID { get; set; }
        public Item Item { get; set; }
        public string ItemDescription { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitCost { get; set; }
        public string ItemString
        {
            get
            {
                List<string> itemInfo = new List<string>();
                if (!string.IsNullOrEmpty(Item?.Name))
                {
                    itemInfo.Add(Item.Name);
                }

                if (!string.IsNullOrEmpty(ItemDescription))
                {
                    itemInfo.Add(ItemDescription);
                }

                return string.Join(" - ", itemInfo);
            }
        }
        public decimal? Value => Quantity * UnitCost;
    }
}
