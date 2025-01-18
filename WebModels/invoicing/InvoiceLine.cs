using System;
using System.Collections.Generic;
using System.Linq;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using WebModels.mesasys;
using WebModels.purchasing;

namespace WebModels.invoicing
{
    [Table("54DFF210-FA2B-4FAE-904B-36DF6F676CAF")]
    public class InvoiceLine : DataObject
    {
        protected InvoiceLine() : base() { }

        private long? _invoiceLineID;
        [Field("3E66E30B-9B26-41F4-A4A6-B0EF89637411")]
        public long? InvoiceLineID
        {
            get { CheckGet(); return _invoiceLineID; }
            set { CheckSet(); _invoiceLineID = value; }
        }

        private long? _invoiceID;
        [Field("7950AEDA-F6B3-4CB6-AF73-B20F8D097203")]
        [Required]
        public long? InvoiceID
        {
            get { CheckGet(); return _invoiceID; }
            set { CheckSet(); _invoiceID = value; }
        }

        private Invoice _invoice = null;
        [Relationship("F540657C-2F66-41E3-88C8-4D54A0B6DBF3")]
        public Invoice Invoice
        {
            get { CheckGet(); return _invoice; }
        }

        private decimal? _quantity;
        [Field("A97A47C0-0350-4CC6-BE6C-8251DA45CCAE", DataSize = 9, DataScale = 2)]
        [Required]
        public decimal? Quantity
        {
            get { CheckGet(); return _quantity; }
            set { CheckSet(); _quantity = value;}
        }

        private decimal? _unitCost;
        [Field("F282BC93-4834-4DDD-B8B8-A511810840D4", DataSize = 9, DataScale = 2)]
        [Required]
        public decimal? UnitCost
        {
            get { CheckGet(); return _unitCost; }
            set { CheckSet(); _unitCost = value; }
        }

        private decimal? _total;
        [Field("E434CC09-F6F7-4A66-87A0-E030F1FE2AF6", DataSize = 9, DataScale = 2)]
        [Required]
        public decimal? Total
        {
            get { CheckGet(); return _total; }
            set { CheckSet(); _total = value; }
        }

        private string _description;
        [Field("A753B10D-0C8B-4F4A-8E4E-77E88EB00A79", DataSize = 300)]
        public string Description
        {
            get { CheckGet(); return _description; }
            set { CheckSet(); _description = value; }
        }

        private long? _itemID;
        [Field("3C8ECFDD-A6A7-4A44-810C-411A008E3CC4")]
        public long? ItemID
        {
            get { CheckGet(); return _itemID; }
            set { CheckSet(); _itemID = value; }
        }

        private Item _item = null;
        [Relationship("427BC45C-17BA-4DA1-8A82-B051F4ED6726")]
        public Item Item
        {
            get { CheckGet(); return _item; }
        }

        private long? _purchaseOrderLineID;
        [Field("652351BD-3E6A-4D68-BC0F-BA28205E1BCE")]
        public long? PurchaseOrderLineID
        {
            get { CheckGet(); return _purchaseOrderLineID; }
            set { CheckSet(); _purchaseOrderLineID = value; }
        }

        private PurchaseOrderLine _purchaseOrderLine = null;
        [Relationship("D405FE2F-B756-434E-9350-20C5BD277714")]
        public PurchaseOrderLine PurchaseOrderLine
        {
            get { CheckGet(); return _purchaseOrderLine; }
        }

        private Fulfillment _fulfillment = null;
        [Relationship("21B6EF16-D1F4-4F0E-87BC-D34971D19192", OneToOneByForeignKey = true)]
        public Fulfillment Fulfillment
        {
            get { CheckGet(); return _fulfillment; }
        }

        protected override void PreValidate()
        {
            base.PreValidate();

            Total = Math.Round((Quantity ?? 0M) * (UnitCost ?? 0M), 2, MidpointRounding.AwayFromZero);
        }

        protected override bool PreSave(ITransaction transaction)
        {
            if (!IsInsert && IsFieldDirty(nameof(PurchaseOrderLineID)) && PurchaseOrderLineID == null)
            {
                Search<Fulfillment> fulfillmentSearch = new Search<Fulfillment>(new LongSearchCondition<Fulfillment>()
                {
                    Field = nameof(Fulfillment.InvoiceLineID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = InvoiceLineID
                });

                foreach (Fulfillment fulfillment in fulfillmentSearch.GetEditableReader(transaction))
                {
                    fulfillment.PurchaseOrderLineID = null;
                    if (!fulfillment.Save(transaction))
                    {
                        Errors.AddRange(fulfillment.Errors.ToArray());
                        return false;
                    }
                }
            }

            return base.PreSave(transaction);
        }

        protected override bool PostSave(ITransaction transaction)
        {
            decimal previousTotal = (GetDirtyValue(nameof(Total)) as decimal?) ?? 0M;
            if (previousTotal != Total)
            {
                Invoice invoice = DataObject.GetEditableByPrimaryKey<Invoice>(InvoiceID, transaction, null);
                if (invoice.Status == Invoice.Statuses.Sent || invoice.Status == Invoice.Statuses.ReadyForReceipt)
                {
                    invoice.Status = Invoice.Statuses.WorkInProgress;
                    if (!invoice.Save(transaction))
                    {
                        Errors.AddRange(invoice.Errors.ToArray());
                        return false;
                    }
                }
            }

            return base.PostSave(transaction);
        }
    }
}
