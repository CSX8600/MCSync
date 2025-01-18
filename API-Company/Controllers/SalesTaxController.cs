using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using API.Common.Attributes;
using API_Company.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using WebModels.company;
using WebModels.gov;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    public class SalesTaxController : ApiController
    {
        [HttpGet]
        public List<SalesTax> GetEffectiveSalesTaxForLocation(long id)
        {
            Search<SalesTax> salesTaxSearch = new Search<SalesTax>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new DateTimeSearchCondition<SalesTax>()
                {
                    Field = nameof(SalesTax.EffectiveDate),
                    SearchConditionType = SearchCondition.SearchConditionTypes.LessEquals,
                    Value = DateTime.Now
                },
                new ExistsSearchCondition<SalesTax>()
                {
                    RelationshipName = $"{nameof(SalesTax.Government)}.{nameof(Government.LocationGovernments)}",
                    ExistsType = ExistsSearchCondition<SalesTax>.ExistsTypes.Exists,
                    Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                        new LongSearchCondition<LocationGovernment>()
                        {
                            Field = nameof(LocationGovernment.LocationID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = id
                        },
                        new BooleanSearchCondition<LocationGovernment>()
                        {
                            Field = nameof(LocationGovernment.PaySalesTax),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = true
                        })
                }));

            List<string> fields = new List<string>()
            {
                nameof(SalesTax.SalesTaxID),
                nameof(SalesTax.GovernmentID),
                $"{nameof(SalesTax.Government)}.{nameof(Government.GovernmentID)}",
                $"{nameof(SalesTax.Government)}.{nameof(Government.Name)}",
                nameof(SalesTax.EffectiveDate),
                nameof(SalesTax.Rate)
            };

            Dictionary<long, SalesTax> salesTaxesByGovernmentID = new Dictionary<long, SalesTax>();
            foreach(SalesTax salesTax in salesTaxSearch.GetReadOnlyReader(null, fields))
            {
                if (!salesTaxesByGovernmentID.ContainsKey(salesTax.GovernmentID.Value))
                {
                    salesTaxesByGovernmentID.Add(salesTax.GovernmentID.Value, salesTax);
                }
                else if (salesTaxesByGovernmentID[salesTax.GovernmentID.Value].EffectiveDate < salesTax.EffectiveDate)
                {
                    salesTaxesByGovernmentID[salesTax.GovernmentID.Value] = salesTax;
                }
            }

            return salesTaxesByGovernmentID.Values.ToList();

            
        }
    }
}