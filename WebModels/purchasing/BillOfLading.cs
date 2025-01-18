using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.company;
using WebModels.fleet;
using WebModels.gov;

namespace WebModels.purchasing
{
    [Table("DF7A53A4-352A-4B91-8EB6-285EA0AA13E9")]
    public class BillOfLading : DataObject
    {
        protected BillOfLading() : base() { }

        private long? _billOfLadingID;
        [Field("8592CCBD-D7DF-4A51-BBA0-9BCE04974AC2")]
        public long? BillOfLadingID
        {
            get { CheckGet(); return _billOfLadingID; }
            set { CheckSet(); _billOfLadingID = value; }
        }

        private long? _purchaseOrderID;
        [Field("27952030-943F-4436-84B5-783A30F74632")]
        public long? PurchaseOrderID
        {
            get { CheckGet(); return _purchaseOrderID; }
            set { CheckSet(); _purchaseOrderID = value; }
        }

        private PurchaseOrder _purchaseOrder = null;
        [Relationship("0F4D9BE2-308F-49C5-9A5B-67A09D69B6B3")]
        public PurchaseOrder PurchaseOrder
        {
            get { CheckGet(); return _purchaseOrder; }
        }

        private long? _companyIDShipper;
        [Field("20EBF5E1-1D16-4061-A8F1-654B8D4E5247")]
        public long? CompanyIDShipper
        {
            get { CheckGet(); return _companyIDShipper; }
            set { CheckSet(); _companyIDShipper = value; }
        }

        private Company _companyShipper = null;
        [Relationship("45288E1C-9C53-4311-9B8A-904F9405E362", ForeignKeyField = nameof(CompanyIDShipper))]
        public Company CompanyShipper
        {
            get { CheckGet(); return _companyShipper; }
        }

        private long? _governmentIDShipper;
        [Field("1762BDDC-7C3C-4E18-AB2C-7DE2E5907594")]
        public long? GovernmentIDShipper
        {
            get { CheckGet(); return _governmentIDShipper; }
            set { CheckSet(); _governmentIDShipper = value; }
        }

        private Government _governmentShipper = null;
        [Relationship("6B42D05D-550C-4ACF-A560-08B6B336FE5C", ForeignKeyField = nameof(GovernmentIDShipper))]
        public Government GovernmentShipper
        {
            get { CheckGet(); return _governmentShipper; }
        }

        private long? _companyIDConsignee;
        [Field("20EBF5E1-1D16-4061-A8F1-654B8D4E5247")]
        public long? CompanyIDConsignee
        {
            get { CheckGet(); return _companyIDConsignee; }
            set { CheckSet(); _companyIDConsignee = value; }
        }

        private Company _companyConsignee = null;
        [Relationship("45288E1C-9C53-4311-9B8A-904F9405E362", ForeignKeyField = nameof(CompanyIDConsignee))]
        public Company CompanyConsignee
        {
            get { CheckGet(); return _companyConsignee; }
        }

        private long? _governmentIDConsignee;
        [Field("1762BDDC-7C3C-4E18-AB2C-7DE2E5907594")]
        public long? GovernmentIDConsignee
        {
            get { CheckGet(); return _governmentIDConsignee; }
            set { CheckSet(); _governmentIDConsignee = value; }
        }

        private Government _governmentConsignee = null;
        [Relationship("6B42D05D-550C-4ACF-A560-08B6B336FE5C", ForeignKeyField = nameof(GovernmentIDConsignee))]
        public Government GovernmentConsignee
        {
            get { CheckGet(); return _governmentConsignee; }
        }

        private long? _companyIDCarrier;
        [Field("9341E125-B4AB-4CE1-8227-1A12B1117897")]
        public long? CompanyIDCarrier
        {
            get { CheckGet(); return _companyIDCarrier; }
            set { CheckSet(); _companyIDCarrier = value; }
        }

        private Company _companyCarrier = null;
        [Relationship("988F659B-30FD-42EB-A161-F08C8747E998", ForeignKeyField = nameof(CompanyIDCarrier))]
        public Company CompanyCarrier
        {
            get { CheckGet(); return _companyCarrier; }
        }

        private long? _governmentIDCarrier;
        [Field("1C6EF92E-85F5-4B64-AC2C-F9301799B0E3")]
        public long? GovernmentIDCarrier
        {
            get { CheckGet(); return _governmentIDCarrier; }
            set { CheckSet(); _governmentIDCarrier = value; }
        }

        private Government _governmentCarrier = null;
        [Relationship("A3CB66FD-2D79-41EB-A95A-9E80675A69A4", ForeignKeyField = nameof(GovernmentIDCarrier))]
        public Government GovernmentCarrier
        {
            get { CheckGet(); return _governmentCarrier; }
        }

        private long? _railcarID;
        [Field("BEB1325E-640B-41C1-8DB8-4E78A399E0F4")]
        public long? RailcarID
        {
            get { CheckGet(); return _railcarID; }
            set { CheckSet(); _railcarID = value; }
        }

        private Railcar _railcar = null;
        [Relationship("DC8EA176-5B08-47A5-87C7-8C441B4E344C")]
        public Railcar Railcar
        {
            get { CheckGet(); return _railcar; }
        }

        private DateTime? _issuedDate;
        [Field("2F68FCA1-9114-4128-B1F0-77C2960AF500", DataSize = 7)]
        public DateTime? IssuedDate
        {
            get { CheckGet(); return _issuedDate; }
            set { CheckSet(); _issuedDate = value; }
        }

        private DateTime? _deliveredDate;
        [Field("895B6F10-701F-4674-992F-87BE6104807A", DataSize = 7)]
        public DateTime? DeliveredDate
        {
            get { CheckGet(); return _deliveredDate; }
            set { CheckSet(); _deliveredDate = value; }
        }

        [Flags]
        public enum Types
        {
            FirstMile,
            Interchange,
            LastMile
        }

        private Types _type;
        [Field("07EE149C-86F0-4B7B-8B68-455E66DDFC45")]
        public Types Type
        {
            get { CheckGet(); return _type; }
            set { CheckSet(); _type = value; }
        }

        #region Relationships
        #region purchasing
        private List<BillOfLadingItem> _billOfLadingItems = new List<BillOfLadingItem>();
        [RelationshipList("EBB38B8B-3294-43CF-8424-0A1ED3AD177D", nameof(BillOfLadingItem.BillOfLadingID))]
        public List<BillOfLadingItem> BillOfLadingItems
        {
            get { CheckGet(); return _billOfLadingItems; }
        }
        #endregion
        #endregion
    }
}
