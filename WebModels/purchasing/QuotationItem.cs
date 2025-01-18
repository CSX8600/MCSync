using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.mesasys;

namespace WebModels.purchasing
{
    [Table("447DC9FE-A4AF-496B-9ADF-D1F767F9FDEA")]
    public class QuotationItem : DataObject
    {
        protected QuotationItem() : base() { }

        private long? _quotationItemID;
        [Field("7EF10C8C-9007-4398-981F-47E4815B6358")]
        public long? QuotationItemID
        {
            get { CheckGet(); return _quotationItemID; }
            set { CheckGet(); _quotationItemID = value; }
        }

        private long? _quotationID;
        [Field("AFCB6D92-2726-4D0D-9675-5108FCAC753A")]
        [Required]
        public long? QuotationID
        {
            get { CheckGet(); return _quotationID; }
            set { CheckGet(); _quotationID = value; }
        }

        private Quotation _quotation = null;
        [Relationship("775F82B6-D05E-4616-A086-0447F103F83D")]
        public Quotation Quotation
        {
            get { CheckGet(); return _quotation; }
        }

        private long? _itemID;
        [Field("DCEE2145-1DE8-4D01-BF6E-738939191683")]
        [Required]
        public long? ItemID
        {
            get { CheckGet(); return _itemID; }
            set { CheckGet(); _itemID = value; }
        }

        private Item _item = null;
        [Relationship("A58EA92E-8D10-488C-B66D-BA9031A75D7C")]
        public Item Item
        {
            get { CheckGet(); return _item; }
        }

        private decimal? _unitCost;
        [Field("332F250E-B882-43D2-984B-9CEF763C7336", DataSize = 9, DataScale = 2)]
        [Required]
        public decimal? UnitCost
        {
            get { CheckGet(); return _unitCost; }
            set { CheckGet(); _unitCost = value; }
        }

        private decimal? _minimumQuantity;
        [Field("80F86F12-EF3A-405A-9542-03CC62127787", DataSize = 9, DataScale = 2)]
        public decimal? MinimumQuantity
        {
            get { CheckGet(); return _minimumQuantity; }
            set { CheckGet(); _minimumQuantity = value; }
        }
    }
}
