using API.Common.Attributes;
using API.Common.Extensions;
using API_Company.Attributes;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System.Collections.Generic;
using System.Web.Http;
using WebModels.company;
using WebModels.mesasys;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ImmersibrookAccess]
    public class LocationItemIBAccessController : ApiController
    {
        private static readonly List<string> FIELDS = FieldPathUtility.CreateFieldPathsAsList<LocationItem>(li => new List<object>()
        {
            li.LocationItemID,
            li.LocationID,
            li.GovernmentID,
            li.ItemID,
            li.Quantity,
            li.BasePrice,
            li.Item.ItemID,
            li.Item.Name,
            li.Item.IsFluid,
            li.CurrentPromotionLocationItem.PromotionLocationItemID,
            li.CurrentPromotionLocationItem.PromotionPrice
        });

        [HttpGet]
        public IHttpActionResult Get(long? locationID, string name, decimal? quantity)
        {
            if (locationID == null || string.IsNullOrEmpty(name) || quantity == null)
            {
                return BadRequest();
            }

            Item itemLookup = new Search<Item>(new StringSearchCondition<Item>()
            {
                Field = nameof(Item.Name),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = name
            }).GetReadOnly(null, new[] { nameof(Item.IsFluid) });

            SearchConditionGroup searchConditionGroup = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<LocationItem>()
                {
                    Field = nameof(LocationItem.LocationID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = locationID
                },
                new StringSearchCondition<LocationItem>()
                {
                    Field = $"{nameof(LocationItem.Item)}.{nameof(Item.Name)}",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = name
                });

            if (!(itemLookup?.IsFluid ?? false))
            {
                searchConditionGroup.SearchConditions.Add(
                new DecimalSearchCondition<LocationItem>()
                {
                    Field = nameof(LocationItem.Quantity),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = quantity
                });
            }

            Search<LocationItem> locationItemSearch = new Search<LocationItem>(searchConditionGroup);

            LocationItem item = locationItemSearch.GetReadOnly(null, FIELDS);
            return item == null ? (IHttpActionResult)NotFound() : Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Post(PostParameter parameter)
        {
            Search<Item> itemSearch = new Search<Item>(new StringSearchCondition<Item>()
            {
                Field = nameof(Item.Name),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = parameter.ItemName
            });

            Item item = itemSearch.GetReadOnly(null, new[] { nameof(Item.ItemID) });

            if (item == null)
            {
                return BadRequest("Supplied Item Name was not found");
            }

            LocationItem locationItem = DataObjectFactory.Create<LocationItem>();
            locationItem.LocationID = parameter.LocationID;
            locationItem.ItemID = item.ItemID;
            locationItem.Quantity = parameter.Quantity;
            locationItem.BasePrice = parameter.Price;

            if (!locationItem.Save())
            {
                return locationItem.HandleFailedValidation(this);
            }

            return Created("LocationItemIBAccess/Get?locationID=" + locationItem.LocationID + "&name=" + parameter.ItemName + "&quantity=" + parameter.Quantity, DataObject.GetReadOnlyByPrimaryKey<LocationItem>(locationItem.LocationItemID, null, FIELDS));
        }

        public class PostParameter
        {
            public long? LocationID { get; set; }
            public string ItemName { get; set; }
            public decimal Quantity { get; set; }
            public decimal? Price { get; set; }
        }
    }
}
