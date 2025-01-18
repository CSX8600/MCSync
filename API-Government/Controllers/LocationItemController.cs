using API.Common;
using API.Common.Attributes;
using API_Government.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System.Collections.Generic;
using System.Linq;
using WebModels.company;
using WebModels.gov;

namespace API_Government.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("gov")]
    [GovernmentAccess(RequiredPermissions = new[] { nameof(Official.ManagePurchaseOrders) })]
    public class LocationItemController : DataObjectController<LocationItem>
    {
        private long GovernmentID => long.Parse(Request.Headers.GetValues("GovernmentID").First());

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<LocationItem>(li => new List<object>()
        {
            li.LocationItemID,
            li.LocationID,
            li.GovernmentID,
            li.ItemID,
            li.BasePrice,
            li.Quantity,
            li.Item.ItemID,
            li.Item.Name,
            li.Item.IsFluid,
            li.Item.Image,
            li.CurrentPromotionLocationItem.PromotionLocationItemID,
            li.CurrentPromotionLocationItem.PromotionID,
            li.CurrentPromotionLocationItem.LocationItemID,
            li.CurrentPromotionLocationItem.PromotionPrice,
            li.CurrentPromotionLocationItem.Promotion.PromotionID,
            li.CurrentPromotionLocationItem.Promotion.Name,
            li.CurrentPromotionLocationItem.Promotion.StartTime,
            li.CurrentPromotionLocationItem.Promotion.EndTime
        });

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new LongSearchCondition<LocationItem>()
            {
                Field = nameof(LocationItem.GovernmentID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = GovernmentID
            };
        }

        public override bool AllowGetAll => true;
    }
}
