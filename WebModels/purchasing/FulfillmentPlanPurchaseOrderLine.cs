using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModels.purchasing
{
    [Table("4A3F862C-0346-48BD-960B-BA36DCC8A9BB")]
    public class FulfillmentPlanPurchaseOrderLine : DataObject
    {
        protected FulfillmentPlanPurchaseOrderLine() : base() { }

        private long? _fulfillmentPlanPurchaseOrderLineID;
        [Field("E140134A-44A6-4454-AE56-15F252E79996")]
        public long? FulfillmentPlanPurchaseOrderLineID
        {
            get { CheckGet(); return _fulfillmentPlanPurchaseOrderLineID; }
            set { CheckSet(); _fulfillmentPlanPurchaseOrderLineID = value; }
        }

        private long? _fulfillmentPlanID;
        [Field("B3FC591D-22E5-4C6B-B718-437A97AF27C6")]
        public long? FulfillmentPlanID
        {
            get { CheckGet(); return _fulfillmentPlanID; }
            set { CheckSet(); _fulfillmentPlanID = value; }
        }

        private FulfillmentPlan _fulfillmentPlan = null;
        [Relationship("166C1C48-F0AE-4961-ADCC-78817E173FB9")]
        public FulfillmentPlan FulfillmentPlan
        {
            get { CheckGet(); return _fulfillmentPlan; }
        }

        private long? _purchaseOrderLineID;
        [Field("70B1496A-FD7F-4515-9EDA-48063B09FA1F")]
        public long? PurchaseOrderLineID
        {
            get { CheckGet(); return _purchaseOrderLineID; }
            set { CheckSet(); _purchaseOrderLineID = value; }
        }

        private PurchaseOrderLine _purchaseOrderLine = null;
        [Relationship("D0650DE1-D675-472B-9799-4F066AD7A36E")]
        public PurchaseOrderLine PurchaseOrderLine
        {
            get { CheckGet(); return _purchaseOrderLine; }
        }

        protected override bool PreSave(ITransaction transaction)
        {
            if (!IsSaveFlagSet(SaveFlags.SkipClearTemplateDataForPurchaseOrder) && AnyFieldDirty())
            {
                Search<PurchaseOrder> purchaseOrderSearch = new Search<PurchaseOrder>(new ExistsSearchCondition<PurchaseOrder>()
                {
                    RelationshipName = nameof(PurchaseOrder.PurchaseOrderLines),
                    ExistsType = ExistsSearchCondition<PurchaseOrder>.ExistsTypes.Exists,
                    Condition = new LongSearchCondition<PurchaseOrderLine>()
                    {
                        Field = nameof(PurchaseOrderLine.PurchaseOrderLineID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.List,
                        List = new List<long>()
                        {
                            PurchaseOrderLineID ?? -1L,
                            ((long?)GetDirtyValue(nameof(PurchaseOrderLineID))) ?? -1L
                        }
                    }
                });

                foreach (PurchaseOrder purchaseOrder in purchaseOrderSearch.GetEditableReader(transaction))
                {
                    if (!purchaseOrder.ClearTemplateDataForPurchaseOrder(transaction) || !purchaseOrder.Save(transaction))
                    {
                        Errors.AddRange(purchaseOrder.Errors.ToArray());
                        return false;
                    }
                }
            }

            return base.PreSave(transaction);
        }

        protected override bool PreDelete(ITransaction transaction)
        {
            Search<PurchaseOrder> purchaseOrderSearch = new Search<PurchaseOrder>(new ExistsSearchCondition<PurchaseOrder>()
            {
                RelationshipName = nameof(PurchaseOrder.PurchaseOrderLines),
                ExistsType = ExistsSearchCondition<PurchaseOrder>.ExistsTypes.Exists,
                Condition = new LongSearchCondition<PurchaseOrderLine>()
                {
                    Field = nameof(PurchaseOrderLine.PurchaseOrderLineID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.List,
                    List = new List<long>()
                    {
                        PurchaseOrderLineID ?? -1L,
                        ((long?)GetDirtyValue(nameof(PurchaseOrderLineID))) ?? -1L
                    }
                }
            });

            foreach (PurchaseOrder purchaseOrder in purchaseOrderSearch.GetEditableReader(transaction))
            {
                if (!purchaseOrder.ClearTemplateDataForPurchaseOrder(transaction) || !purchaseOrder.Save(transaction))
                {
                    Errors.AddRange(purchaseOrder.Errors.ToArray());
                    return false;
                }
            }

            return base.PreDelete(transaction);
        }

        protected override bool PostDelete(ITransaction transaction)
        {
            Search<FulfillmentPlanPurchaseOrderLine> otherLinesSearch = new Search<FulfillmentPlanPurchaseOrderLine>(new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
            {
                Field = nameof(FulfillmentPlanID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = FulfillmentPlanID
            });

            if (!otherLinesSearch.ExecuteExists(transaction))
            {
                FulfillmentPlan fulfillmentPlan = DataObject.GetEditableByPrimaryKey<FulfillmentPlan>(FulfillmentPlanID, transaction, null);
                if (!fulfillmentPlan.Delete(transaction))
                {
                    Errors.AddRange(fulfillmentPlan.Errors.ToArray());
                    return false;
                }
            }

            return base.PostDelete(transaction);
        }

        private bool AnyFieldDirty()
        {
            foreach(Field field in Schema.GetSchemaObject<FulfillmentPlanPurchaseOrderLine>().GetFields())
            {
                if (IsFieldDirty(field.FieldName))
                {
                    return true;
                }
            }

            return false;
        }

        public static class SaveFlags
        {
            public static readonly Guid SkipClearTemplateDataForPurchaseOrder = new Guid("8AE58F64-C49C-43E1-AB0C-E6C00FB843E0");
        }
    }
}
