using API.Common;
using API.Common.Attributes;
using API.Common.Extensions;
using API_Company.App_Code;
using API_Company.Models.company;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebModels.company;
using WebModels.gov;
using WebModels.mesasys;
using WebModels.purchasing;

namespace API_Company.Controllers
{
    public class PriceCheckController : ApiController
    {
        [HttpGet]
        public List<Location> GetLocations()
        {
            Search<Location> locationSearch = new Search<Location>(new ExistsSearchCondition<Location>()
            {
                RelationshipName = nameof(Location.LocationItems),
                ExistsType = ExistsSearchCondition<Location>.ExistsTypes.Exists
            });

            return locationSearch.GetReadOnlyReader(null, FieldPathUtility.CreateFieldPathsAsList<Location>(l => new List<object>()
            {
                l.LocationID,
                l.Name,
                l.Company.Name
            })).ToList();
        }

        [HttpGet]
        public List<Government> GetGovernments()
        {
            Search<Government> governmentSearch = new Search<Government>(new ExistsSearchCondition<Government>()
            {
                RelationshipName = nameof(Government.LocationItems),
                ExistsType = ExistsSearchCondition<Government>.ExistsTypes.Exists
            });

            return governmentSearch.GetReadOnlyReader(null, FieldPathUtility.CreateFieldPathsAsList<Government>(g => new List<object>()
            {
                g.GovernmentID,
                g.Name
            })).ToList();
        }

        [HttpGet]
        public async Task<List<QuotedLocationItem>> GetItems([FromUri]long? locationID = null, [FromUri]long? governmentID = null)
        {
            LongSearchCondition<QuotedLocationItem> entitySearchCondition;
            if (locationID != null)
            {
                entitySearchCondition = new LongSearchCondition<QuotedLocationItem>()
                {
                    Field = nameof(QuotedLocationItem.LocationID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = locationID
                };
            }
            else if (governmentID != null)
            {
                entitySearchCondition = new LongSearchCondition<QuotedLocationItem>()
                {
                    Field = nameof(QuotedLocationItem.GovernmentID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = governmentID
                };
            }
            else
            {
                return null;
            }

            Search<QuotedLocationItem> locationItemSearch = new Search<QuotedLocationItem>(entitySearchCondition);

            List<QuotedLocationItem> quotedLocationItems = locationItemSearch.GetReadOnlyReader(null, FieldPathUtility.CreateFieldPathsAsList<LocationItem>(l => new List<object>()
            {
                l.LocationItemID,
                l.LocationID,
                l.GovernmentID,
                l.ItemID,
                l.Item.Name,
                l.Item.ItemNamespace.Namespace,
                l.Item.IsFluid,
                l.BasePrice,
                l.Quantity,
                l.CurrentPromotionLocationItem.PromotionPrice
            })).ToList();

            await QuotedLocationItem.ApplyQuotes(quotedLocationItems, Request, ActionContext);

            return quotedLocationItems;
        }

        [HttpGet]
        public async Task<QuotedLocationItem> GetItem([FromUri]string itemName, [FromUri]decimal? quantity, [FromUri] long? locationID = null, [FromUri] long? governmentID = null)
        {
            LongSearchCondition<QuotedLocationItem> entitySearchCondition;
            if (locationID != null)
            {
                entitySearchCondition = new LongSearchCondition<QuotedLocationItem>()
                {
                    Field = nameof(QuotedLocationItem.LocationID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = locationID
                };
            }
            else if (governmentID != null)
            {
                entitySearchCondition = new LongSearchCondition<QuotedLocationItem>()
                {
                    Field = nameof(QuotedLocationItem.GovernmentID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = governmentID
                };
            }
            else
            {
                return null;
            }

            Search<QuotedLocationItem> locationItemSearch = new Search<QuotedLocationItem>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new StringSearchCondition<QuotedLocationItem>()
                {
                    Field = $"{nameof(QuotedLocationItem.Item)}.{nameof(Item.Name)}",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = itemName
                },
                entitySearchCondition,
                new DecimalSearchCondition<QuotedLocationItem>()
                {
                    Field = nameof(QuotedLocationItem.Quantity),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = quantity
                }));

            QuotedLocationItem quotedLocationItem = locationItemSearch.GetReadOnly(null, FieldPathUtility.CreateFieldPathsAsList<QuotedLocationItem>(l => new List<object>()
            {
                l.LocationItemID,
                l.LocationID,
                l.GovernmentID,
                l.ItemID,
                l.Item.Name,
                l.Item.ItemNamespace.Namespace,
                l.Item.IsFluid,
                l.BasePrice,
                l.Quantity,
                l.CurrentPromotionLocationItem.PromotionPrice
            }));

            await QuotedLocationItem.ApplyQuotes(new List<QuotedLocationItem>() { quotedLocationItem }, Request, ActionContext);

            return quotedLocationItem;
        }

        [HttpGet]
        public async Task<QuotedLocationItem> GetItem([FromUri]long? itemID, [FromUri]decimal? quantity, [FromUri] long? locationID = null, [FromUri] long? governmentID = null)
        {
            LongSearchCondition<QuotedLocationItem> entitySearchCondition;
            if (locationID != null)
            {
                entitySearchCondition = new LongSearchCondition<QuotedLocationItem>()
                {
                    Field = nameof(QuotedLocationItem.LocationID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = locationID
                };
            }
            else if (governmentID != null)
            {
                entitySearchCondition = new LongSearchCondition<QuotedLocationItem>()
                {
                    Field = nameof(QuotedLocationItem.GovernmentID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = governmentID
                };
            }
            else
            {
                return null;
            }

            Search<QuotedLocationItem> locationItemSearch = new Search<QuotedLocationItem>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<QuotedLocationItem>()
                {
                    Field = nameof(QuotedLocationItem.ItemID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = itemID
                },
                entitySearchCondition,
                new DecimalSearchCondition<QuotedLocationItem>()
                {
                    Field = nameof(QuotedLocationItem.Quantity),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = quantity
                }));

            QuotedLocationItem item = await Task.Run(() => locationItemSearch.GetReadOnly(null, FieldPathUtility.CreateFieldPathsAsList<LocationItem>(l => new List<object>()
            {
                l.LocationItemID,
                l.LocationID,
                l.GovernmentID,
                l.ItemID,
                l.Item.Name,
                l.Item.ItemNamespace.Namespace,
                l.Item.IsFluid,
                l.BasePrice,
                l.Quantity,
                l.CurrentPromotionLocationItem.PromotionPrice
            })));

            await QuotedLocationItem.ApplyQuotes(new List<QuotedLocationItem>() { item }, Request, ActionContext);

            return item;
        }

        [HttpGet]
        public async Task<List<QuotedLocationItem>> FindItem([FromUri] string itemName, [FromUri] decimal? quantity = null)
        {
            SearchConditionGroup searchCondition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new StringSearchCondition<QuotedLocationItem>()
                {
                    Field = $"{nameof(QuotedLocationItem.Item)}.{nameof(Item.Name)}",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = itemName
                });

            if (quantity != null)
            {
                searchCondition.SearchConditions.Add(new DecimalSearchCondition<QuotedLocationItem>()
                {
                    Field = nameof(QuotedLocationItem.Quantity),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = quantity
                });
            }

            Search<QuotedLocationItem> locationItemSearch = new Search<QuotedLocationItem>(searchCondition);
            List<QuotedLocationItem> quotedLocationItems = await Task.Run(() => locationItemSearch.GetReadOnlyReader(null, FieldPathUtility.CreateFieldPathsAsList<LocationItem>(l => new List<object>()
            {
                l.LocationItemID,
                l.LocationID,
                l.GovernmentID,
                l.ItemID,
                l.Item.Name,
                l.Item.ItemNamespace.Namespace,
                l.Item.IsFluid,
                l.BasePrice,
                l.Quantity,
                l.CurrentPromotionLocationItem.PromotionPrice
            })).ToList());

            await QuotedLocationItem.ApplyQuotes(quotedLocationItems, Request, ActionContext);

            return quotedLocationItems;
        }
    }
}
