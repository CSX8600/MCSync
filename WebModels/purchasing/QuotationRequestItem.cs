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
    [Table("5ACC6D4F-244E-44D5-8FAD-95BAF4DB54A0")]
    public class QuotationRequestItem : DataObject
    {
        protected QuotationRequestItem() : base() { }

        private long? _quotationRequestItemID;
        [Field("2DF4C2E2-FD60-4C41-97E6-CB9CA39CE107")]
        public long? QuotationRequestItemID
        {
            get { CheckGet(); return _quotationRequestItemID; }
            set { CheckSet(); _quotationRequestItemID = value; }
        }

        private long? _quotationRequestID;
        [Field("A3A7F3E8-C8CB-43DD-A95A-375EEFAC3446")]
        [Required]
        public long? QuotationRequestID
        {
            get { CheckGet(); return _quotationRequestID; }
            set { CheckSet(); _quotationRequestID = value; }
        }

        private QuotationRequest _quotationRequest = null;
        [Relationship("F81CB8BF-634C-465E-AE1D-4B04AD61E2CD")]
        public QuotationRequest QuotationRequest
        {
            get { CheckGet(); return _quotationRequest; }
        }

        private long? _itemID;
        [Field("48F23441-3A0F-4087-B448-93773912F6FF")]
        [Required]
        public long? ItemID
        {
            get { CheckGet(); return _itemID; }
            set { CheckSet(); _itemID = value; }
        }

        private Item _item = null;
        [Relationship("C007B1D1-1849-4215-AAD3-673983529C83")]
        public Item Item
        {
            get { CheckGet(); return _item; }
        }

        private decimal? _quantity;
        [Field("8D03B78D-18CC-40A8-BF09-4A7CB63DD6B8", DataSize = 9, DataScale = 2)]
        [Required]
        public decimal? Quantity
        {
            get { CheckGet(); return _quantity; }
            set { CheckSet(); _quantity = value; }
        }
    }
}
