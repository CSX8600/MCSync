using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using System;

namespace WebModels.purchasing.Validations
{
    public class PurchaseOrderHasDetailsOnSubmitCondition : Condition
    {
        public const string VALIDATION_MESSAGE = "Purchase Order must have at least one Purchase Order Line.";

        public override bool Evaluate(DataObject dataObject, ITransaction transaction)
        {
            if (!(dataObject is PurchaseOrder purchaseOrder))
            {
                throw new InvalidCastException("dataObject must be a PurchaseOrder");
            }

            if (purchaseOrder.PurchaseOrderID == null ||
                !purchaseOrder.IsFieldDirty(nameof(PurchaseOrder.Status)) ||
                ((PurchaseOrder.Statuses)purchaseOrder.GetDirtyValue(nameof(PurchaseOrder.Status)) != PurchaseOrder.Statuses.Draft) ||
                purchaseOrder.Status != PurchaseOrder.Statuses.Pending)
            {
                return true;
            }

            Search<PurchaseOrderLine> purchaseOrderLineSearch = new Search<PurchaseOrderLine>(new LongSearchCondition<PurchaseOrderLine>()
            {
                Field = nameof(PurchaseOrderLine.PurchaseOrderID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = purchaseOrder.PurchaseOrderID
            });

            return purchaseOrderLineSearch.ExecuteExists(transaction);
        }
    }
}
