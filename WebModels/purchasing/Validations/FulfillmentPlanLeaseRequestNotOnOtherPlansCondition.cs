using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using System;

namespace WebModels.purchasing.Validations
{
    internal class FulfillmentPlanLeaseRequestNotOnOtherPlansCondition : Condition
    {
        public override bool Evaluate(DataObject dataObject, ITransaction transaction)
        {
            if (!(dataObject is FulfillmentPlan fulfillmentPlan))
            {
                throw new InvalidCastException("dataObject must be a FulfillmentPlan");
            }

            if (fulfillmentPlan.LeaseRequestID == null)
            {
                return true;
            }

            SearchConditionGroup existsConditionGroup = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<FulfillmentPlan>()
                {
                    Field = nameof(FulfillmentPlan.LeaseRequestID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = fulfillmentPlan.LeaseRequestID
                });

            if (fulfillmentPlan.FulfillmentPlanID != null)
            {
                existsConditionGroup.SearchConditions.Add(new LongSearchCondition<FulfillmentPlan>()
                {
                    Field = nameof(FulfillmentPlan.FulfillmentPlanID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.NotEquals,
                    Value = fulfillmentPlan.FulfillmentPlanID
                });
            }

            Search<FulfillmentPlan> fulfillmentPlanSearch = new Search<FulfillmentPlan>(existsConditionGroup);
            return !fulfillmentPlanSearch.ExecuteExists(transaction);
        }
    }
}
