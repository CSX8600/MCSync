using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WebModels.company;
using WebModels.fleet;
using WebModels.invoicing;
using WebModels.purchasing;

namespace WebModels.mesasys
{
    [Table("3A24E2E4-EC2B-4F02-98EA-777B60BDE5C2")]
    [Unique(new[] { nameof(Hash) })]
    public class Item : DataObject
    {
        protected Item() : base() { }

        private long? _itemID;
        [Field("892EFE98-B6F2-42D7-9552-B6ACC2E1A0B9")]
        public long? ItemID
        {
            get { CheckGet(); return _itemID; }
            set { CheckSet(); _itemID = value; }
        }

        private long? _itemNamespaceID;
        [Field("1BFF506C-5CBD-4940-A48A-864C73D7351F")]
        public long? ItemNamespaceID
        {
            get { CheckGet(); return _itemNamespaceID; }
            set { CheckSet(); _itemNamespaceID = value; }
        }

        private ItemNamespace _itemNamespace = null;
        [Relationship("938F34BB-83A0-4BFA-9B40-DF7C44BACBF5")]
        public ItemNamespace ItemNamespace
        {
            get { CheckGet(); return _itemNamespace; }
        }

        private string _name;
        [Field("ECE7AE7B-8D18-4827-AA68-FCD2942BF18D", DataSize = 100)]
        public string Name
        {
            get { CheckGet(); return _name; }
            set { CheckSet(); _name = value; }
        }

        private byte[] _image;
        [Field("71C6EDE5-3597-4E42-BFE9-546084415E17")]
        public byte[] Image
        {
            get { CheckGet(); return _image; }
            set { CheckSet(); _image = value; }
        }

        private byte[] _hash;
        [Field("4BB393D6-D7F3-4548-A285-8352FD8C9D4B")]
        public byte[] Hash
        {
            get { CheckGet(); return _hash; }
            set { CheckSet(); _hash = value; }
        }

        private bool _isFluid;
        [Field("EFD09E0E-F965-4B10-8ED0-5A4488A1C758")]
        public bool IsFluid
        {
            get { CheckGet(); return _isFluid; }
            set { CheckSet(); _isFluid = value; }
        }

        protected override bool PreSave(ITransaction transaction)
        {
            if (IsFieldDirty(nameof(ItemNamespaceID)) || IsFieldDirty(nameof(Image)))
            {
                string hashString = string.Format("{0}", ItemNamespaceID);
                byte[] utf8Bytes = Encoding.UTF8.GetBytes(hashString);

                byte[] prehashBytes = new byte[utf8Bytes.Length + Image.Length];
                Array.Copy(utf8Bytes, prehashBytes, utf8Bytes.Length);
                Array.Copy(Image, 0, prehashBytes, utf8Bytes.Length, Image.Length);

                using (MD5 md5 = MD5.Create())
                {
                    Hash = md5.ComputeHash(prehashBytes);
                }
            }

            return base.PreSave(transaction);
        }

        #region Relationships
        #region company
        private List<LocationItem> _locationItems = new List<LocationItem>();
        [RelationshipList("AA37942A-10A0-4985-97D7-FD6FC2BCFF17", nameof(LocationItem.ItemID))]
        public IReadOnlyCollection<LocationItem> LocationItems
        {
            get { CheckGet(); return _locationItems; }
        }
        #endregion
        #region fleet
        private List<RailcarLoad> _railcarLoads = new List<RailcarLoad>();
        [RelationshipList("7B69B73F-36B2-4C22-B9DF-8A992415AF26", nameof(RailcarLoad.ItemID))]
        public IReadOnlyCollection<RailcarLoad> RailcarLoads
        {
            get { CheckGet(); return _railcarLoads; }
        }
        #endregion
        #region invoicing
        private List<InvoiceLine> _invoiceLines = new List<InvoiceLine>();
        [RelationshipList("AC6E6BF0-5481-4C13-844C-223161B4A750", nameof(InvoiceLine.ItemID))]
        public IReadOnlyCollection<InvoiceLine> InvoiceLines
        {
            get { CheckGet(); return _invoiceLines; }
        }
        #endregion
        #region purchasing
        private List<PurchaseOrderLine> _purchaseOrderLines = new List<PurchaseOrderLine>();
        [RelationshipList("C1986DB7-9394-435F-BCD9-9718D17C03F6", nameof(PurchaseOrderLine.ItemID))]
        public IReadOnlyCollection<PurchaseOrderLine> PurchaseOrderLines
        {
            get { CheckGet(); return _purchaseOrderLines; }
        }

        private List<BillOfLadingItem> _billOfLadingItems = new List<BillOfLadingItem>();
        [RelationshipList("EF1D2423-77F4-4DF8-9D9D-012CE93C2D44", nameof(BillOfLadingItem.ItemID))]
        public IReadOnlyCollection<BillOfLadingItem> BillOfLadingItems
        {
            get { CheckGet(); return _billOfLadingItems; }
        }

        private List<QuotationItem> _quotationItems = new List<QuotationItem>();
        [RelationshipList("0D19E972-F3C0-499F-BA88-B2D73ED4471D", nameof(QuotationItem.ItemID))]
        public IReadOnlyCollection<QuotationItem> QuotationItems
        {
            get { CheckGet(); return _quotationItems; }
        }

        private List<QuotationRequestItem> _quotationRequestItems = new List<QuotationRequestItem>();
        [RelationshipList("6BED0964-A2B7-4D28-8903-FD973ACAC6E6", nameof(QuotationRequestItem.ItemID))]
        public IReadOnlyCollection<QuotationRequestItem> quotationRequestItems
        {
            get { CheckGet(); return _quotationRequestItems; }
        }
        #endregion
        #endregion
    }
}
