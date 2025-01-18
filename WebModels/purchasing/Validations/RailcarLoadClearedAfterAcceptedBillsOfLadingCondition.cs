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
    public class RailcarLoadClearedAfterAcceptedBillsOfLadingCondition : Condition
    {
        public override bool Evaluate(DataObject dataObject, ITransaction transaction)
        {
            if (!(dataObject is RailcarLoad railcarLoad))
            {
                throw new InvalidOperationException("dataObject must be a Fulfillment");
            }

            if (railcarLoad.PurchaseOrderLineID == null)
            {
                return true;
            }

            Search<RailcarLoad> railcarLoadSearch = new Search<RailcarLoad>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<RailcarLoad>()
                {
                    Field = nameof(RailcarLoad.RailcarID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = railcarLoad.RailcarLoadID
                },
                new ExistsSearchCondition<RailcarLoad>()
                {
                    ExistsType = ExistsSearchCondition<RailcarLoad>.ExistsTypes.Exists,
                    RelationshipName = FieldPathUtility.CreateFieldPathsAsList<RailcarLoad>(rl => new List<object>() { rl.Railcar.BillsOfLading }).First(),
                    Condition = new DateTimeSearchCondition<BillOfLading>()
                    {
                        Field = nameof(BillOfLading.DeliveredDate),
                        SearchConditionType = SearchCondition.SearchConditionTypes.NotNull
                    }
                }));

            return !railcarLoadSearch.ExecuteExists(transaction);
        }
    }
}
