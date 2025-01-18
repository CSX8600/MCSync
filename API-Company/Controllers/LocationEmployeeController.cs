using System.Collections.Generic;
using System.Linq;
using API.Common;
using API.Common.Attributes;
using API_Company.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using WebModels.company;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [CompanyAccess]
    public class LocationEmployeeController : DataObjectController<LocationEmployee>
    {
        public override IEnumerable<string> DefaultRetrievedFields => new string[]
        {
            nameof(LocationEmployee.LocationEmployeeID),
            nameof(LocationEmployee.LocationID),
            nameof(LocationEmployee.EmployeeID),
            nameof(LocationEmployee.ManageInvoices),
            nameof(LocationEmployee.ManagePrices),
            nameof(LocationEmployee.ManageRegisters),
            nameof(LocationEmployee.ManageInventory),
            nameof(LocationEmployee.ManagePurchaseOrders),
        };

        public override ISearchCondition GetBaseSearchCondition()
        {
            long companyID = long.Parse(Request.Headers.GetValues("CompanyID").First());
            return new LongSearchCondition<LocationEmployee>()
            {
                Field = "Location.CompanyID",
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = companyID
            };
        }

        public LocationEmployee GetForCurrentUser(long locationID)
        {
            SecurityProfile profile = (SecurityProfile)Request.Properties["SecurityProfile"];

            Search<LocationEmployee> locationEmployeeSearch = new Search<LocationEmployee>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                GetBaseSearchCondition(),
                new LongSearchCondition<LocationEmployee>()
                {
                    Field = "Employee.UserID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = profile.UserID
                },
                new LongSearchCondition<LocationEmployee>()
                {
                    Field = "LocationID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = locationID
                }));

            return locationEmployeeSearch.GetReadOnly(null, DefaultRetrievedFields);
        }
    }
}