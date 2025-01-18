using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModels.company.Validation
{
    public class FluidItemIsUniqueForStoreCondition : Condition
    {
        public override bool Evaluate(DataObject dataObject, ITransaction transaction)
        {
            if (!(dataObject is LocationItem locationItem))
            {
                return false;
            }

            if (locationItem.ItemID == null)
            {
                return true;
            }

            LongSearchCondition<LocationItem> entitySearchCondition;
            if (locationItem.LocationID != null)
            {
                entitySearchCondition = new LongSearchCondition<LocationItem>()
                {
                    Field = nameof(LocationItem.LocationID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = locationItem.LocationID
                };
            }
            else
            {
                entitySearchCondition = new LongSearchCondition<LocationItem>()
                {
                    Field = nameof(LocationItem.GovernmentID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = locationItem.GovernmentID
                };
            }

            List<SearchCondition> andConditions = new List<SearchCondition>()
            {
                new LongSearchCondition<LocationItem>()
                {
                    Field = nameof(LocationItem.ItemID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = locationItem.ItemID
                },
                entitySearchCondition
            };

            if (locationItem.LocationItemID != null)
            {
                andConditions.Add(new LongSearchCondition<LocationItem>()
                {
                    Field = nameof(LocationItem.LocationItemID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.NotEquals,
                    Value = locationItem.LocationItemID
                });
            }

            return !new Search<LocationItem>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And, andConditions.ToArray())).ExecuteExists(transaction);
        }
    }
}
