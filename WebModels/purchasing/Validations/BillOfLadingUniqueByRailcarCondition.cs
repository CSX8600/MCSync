using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using System;

namespace WebModels.purchasing.Validations
{
    public class BillOfLadingUniqueByRailcarCondition : Condition
    {
        public override bool Evaluate(DataObject dataObject, ITransaction transaction)
        {
            if (!(dataObject is BillOfLading billOfLading))
            {
                throw new InvalidCastException("dataObject must be a BillOfLading");
            }

            if (billOfLading.RailcarID == null)
            {
                return true;
            }

            Search<BillOfLading> billOfLadingSearch = new Search<BillOfLading>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.BillOfLadingID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.NotEquals,
                    Value = billOfLading.BillOfLadingID ?? -1L
                },
                new LongSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.RailcarID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = billOfLading.RailcarID
                },
                new DateTimeSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.DeliveredDate),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Null
                }));

            return !billOfLadingSearch.ExecuteExists(transaction);
        }
    }
}
