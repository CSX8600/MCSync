using API.Common.Attributes;
using API.Common;
using API_Company.App_Code;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.company;
using WebModels.purchasing;
using API.Common.Extensions;

namespace API_Company.Models.company
{
    public class QuotedLocationItem : LocationItem
    {
        protected QuotedLocationItem() : base() { }

        public List<QuotationItem> QuotedPrices { get; set; }

        public static async Task ApplyQuotes(List<QuotedLocationItem> items, System.Net.Http.HttpRequestMessage request, System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            items = items?.Where(i => i != null && i.GovernmentID == null).ToList();

            if (items == null || !items.Any())
            {
                return;
            }

            long? locationIDFrom = items.First().LocationID;
            if (!items.Where(i => i != null).All(i => i.LocationID == locationIDFrom))
            {
                throw new InvalidOperationException("All items must be from the same location.");
            }

            if (!string.IsNullOrEmpty(request.Headers.Authorization?.Parameter) && request.Headers.Contains("CompanyID") && long.TryParse(request.Headers.GetValues("CompanyID").First(), out long companyID))
            {
                var unauthorized = await MesabrookAuthorizationAttribute.CheckHeadersForSecurity(actionContext);
                if (unauthorized == null)
                {
                    SecurityProfile securityProfile = await Task.Run(() => SecurityCache.Get(request.Headers.Authorization.Parameter));
                    if (securityProfile != null)
                    {
                        EmployeeCache.CachedEmployee cachedEmployee = await EmployeeCache.GetCachedEmployee(companyID, securityProfile.UserID);
                        if (cachedEmployee != null)
                        {
                            List<QuotationItem> quotationItems = new List<QuotationItem>();
                            foreach (IEnumerable<QuotedLocationItem> quotedLocationItemEnumerable in items.Batch())
                            {
                                Search<QuotationItem> quotationItemSearch = new Search<QuotationItem>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                                    new LongSearchCondition<QuotationItem>()
                                    {
                                        Field = nameof(QuotationItem.ItemID),
                                        SearchConditionType = SearchCondition.SearchConditionTypes.List,
                                        List = quotedLocationItemEnumerable.Select(qli => qli.ItemID ?? -1L).ToList()
                                    },
                                    new DateTimeSearchCondition<QuotationItem>()
                                    {
                                        Field = FieldPathUtility.CreateFieldPathsAsList<QuotationItem>(qi => new List<object>() { qi.Quotation.ExpirationTime }).First(),
                                        SearchConditionType = SearchCondition.SearchConditionTypes.Greater,
                                        Value = DateTime.Now
                                    },
                                    new LongSearchCondition<QuotationItem>()
                                    {
                                        Field = FieldPathUtility.CreateFieldPathsAsList<QuotationItem>(qi => new List<object>() { qi.Quotation.CompanyIDTo }).First(),
                                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                        Value = companyID
                                    },
                                    new ExistsSearchCondition<QuotationItem>()
                                    {
                                        RelationshipName = FieldPathUtility.CreateFieldPathsAsList<QuotationItem>(qi => new List<object>() { qi.Quotation.CompanyFrom.Locations }).First(),
                                        ExistsType = ExistsSearchCondition<QuotationItem>.ExistsTypes.Exists,
                                        Condition = new LongSearchCondition<Location>()
                                        {
                                            Field = FieldPathUtility.CreateFieldPathsAsList<Location>(loc => new List<object>() { loc.LocationID }).First(),
                                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                            Value = locationIDFrom
                                        }
                                    }));

                                quotationItems.AddRange(await Task.Run(() => quotationItemSearch.GetReadOnlyReader(null, FieldPathUtility.CreateFieldPathsAsList<QuotationItem>(qi => new List<object>()
                                {
                                    qi.Quotation.ExpirationTime,
                                    qi.ItemID,
                                    qi.MinimumQuantity,
                                    qi.UnitCost
                                }))));
                            }

                            foreach (QuotedLocationItem quotedLocationItem in items)
                            {
                                // Find where exceeds minimum quantity, then get lowest price
                                quotedLocationItem.QuotedPrices = quotationItems.Where(qi => qi.ItemID == quotedLocationItem.ItemID).ToList();
                            }
                        }
                    }
                }
            }
        }
    }
}
