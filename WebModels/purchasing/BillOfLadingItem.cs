using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using WebModels.mesasys;

namespace WebModels.purchasing
{
    [Table("E1254B10-4CAE-45E4-86F7-D8BE225D6BE4")]
    public class BillOfLadingItem : DataObject
    {
        protected BillOfLadingItem() : base() { }

        private long? _billOfLadingItemID;
        [Field("A576039F-58AB-49AE-9E01-155D364E75FC")]
        public long? BillOfLadingItemID
        {
            get { CheckGet(); return _billOfLadingItemID; }
            set { CheckSet(); _billOfLadingItemID = value; }
        }

        private long? _billOfLadingID;
        [Field("333C7969-F0BE-4374-AB71-6D8A24FBDBA4")]
        public long? BillOfLadingID
        {
            get { CheckGet(); return _billOfLadingID; }
            set { CheckSet(); _billOfLadingID = value; }
        }

        private BillOfLading _billOfLading = null;
        [Relationship("D304FF18-4A03-42EA-9C05-9F6CE26175DE")]
        public BillOfLading BillOfLading
        {
            get { CheckGet(); return _billOfLading; }
        }

        private long? _itemID;
        [Field("C91076C8-9A2D-4CC4-9868-A118E2E3D1A4")]
        public long? ItemID
        {
            get { CheckGet(); return _itemID; }
            set { CheckSet(); _itemID = value; }
        }

        private Item _item = null;
        [Relationship("504B431D-25CA-4860-9227-B04521A3D621")]
        public Item Item
        {
            get { CheckGet(); return _item; }
        }

        private string _itemDescription;
        [Field("1B1F459B-A466-4C9C-88F1-3954A88E7575", DataSize = 100)]
        public string ItemDescription
        {
            get { CheckGet(); return _itemDescription; }
            set { CheckSet(); _itemDescription = value; }
        }

        private decimal? _quantity;
        [Field("CF240C64-DF91-4454-BA1F-8F376FEC484B", DataSize = 9, DataScale = 2)]
        public decimal? Quantity
        {
            get { CheckGet(); return _quantity; }
            set { CheckSet(); _quantity = value; }
        }

        private decimal? _unitCost;
        [Field("F8FA81D8-1524-4153-A673-7889419A066B", DataSize = 9, DataScale = 2)]
        public decimal? UnitCost
        {
            get { CheckGet(); return _unitCost; }
            set { CheckSet(); _unitCost = value; }
        }
    }
}
