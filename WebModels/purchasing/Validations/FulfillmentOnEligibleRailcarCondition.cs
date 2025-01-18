using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using WebModels.fleet;

namespace WebModels.purchasing.Validations
{
    public class FulfillmentOnEligibleRailcarCondition : Condition
    {
        public const string MESSAGE = "Railcar must not fulfill multiple concurrent Purchase Orders.";
        public override bool Evaluate(DataObject dataObject, ITransaction transaction)
        {
            if (!(dataObject is Fulfillment fulfillment))
            {
                throw new InvalidCastException("dataObject must be a Fulfillment");
            }

            if (fulfillment.RailcarID == null || fulfillment.PurchaseOrderLineID == null)
            {
                return true;
            }

            Search<Fulfillment> fulfillmentSearch = new Search<Fulfillment>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<Fulfillment>()
                {
                    Field = nameof(Fulfillment.RailcarID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = fulfillment.RailcarID
                },
                new BooleanSearchCondition<Fulfillment>()
                {
                    Field = nameof(Fulfillment.IsComplete),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = false
                }));

            PurchaseOrderLine selectedLine = DataObject.GetReadOnlyByPrimaryKey<PurchaseOrderLine>(fulfillment.PurchaseOrderLineID, transaction, new[] { nameof(PurchaseOrderLine.PurchaseOrderID) });
            List<Fulfillment> fulfillments = fulfillmentSearch.GetReadOnlyReader(transaction, FieldPathUtility.CreateFieldPathsAsList<Fulfillment>(f => new List<object>() { f.PurchaseOrderLine.PurchaseOrderID })).ToList();
            HashSet<long?> purchaseOrderIDs = new HashSet<long?>() { selectedLine.PurchaseOrderID };
            foreach(Fulfillment f in fulfillments)
            {
                if (purchaseOrderIDs.Add(f.PurchaseOrderLine.PurchaseOrderID))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
