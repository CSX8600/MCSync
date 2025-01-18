using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using API.Common;
using API.Common.Attributes;
using API_Company.App_Code;
using API_Company.Attributes;
using ClussPro.Base.Extensions;
using ClussPro.ObjectBasedFramework.DataSearch;
using WebModels.company;
using WebModels.gov;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [CompanyAccess(RequiredPermissions = new string[] { nameof(Employee.ManageLocations) })]
    public class LocationController : DataObjectController<Location>
    {
        public override bool AllowGetAll => true;
        public override IEnumerable<string> DefaultRetrievedFields => new string[]
        {
            nameof(Location.LocationID),
            nameof(Location.CompanyID),
            nameof(Location.Company) + "." + nameof(Company.CompanyID),
            nameof(Location.Company) + "." + nameof(Company.Name),
            nameof(Location.Name),
            nameof(Location.InvoiceNumberPrefix),
            nameof(Location.NextInvoiceNumber)
        };

        protected override IEnumerable<string> RequestableFields => new string[]
        {
            nameof(Location.EmailImplementationIDPayableInvoice),
            nameof(Location.EmailImplementationIDReadyForReceipt),
            nameof(Location.EmailImplementationIDRegisterOffline),
            nameof(Location.AccountIDStoreRevenue),
            $"{nameof(Location.LocationEmployees)}.{nameof(LocationEmployee.LocationEmployeeID)}",
            $"{nameof(Location.LocationEmployees)}.{nameof(LocationEmployee.LocationID)}",
            $"{nameof(Location.LocationEmployees)}.{nameof(LocationEmployee.EmployeeID)}",
            $"{nameof(Location.LocationEmployees)}.{nameof(LocationEmployee.ManageInvoices)}",
            $"{nameof(Location.LocationEmployees)}.{nameof(LocationEmployee.ManagePrices)}",
            $"{nameof(Location.LocationEmployees)}.{nameof(LocationEmployee.ManageRegisters)}",
            $"{nameof(Location.LocationEmployees)}.{nameof(LocationEmployee.ManageInventory)}",
            $"{nameof(Location.LocationEmployees)}.{nameof(LocationEmployee.ManagePurchaseOrders)}",
            $"{nameof(Location.LocationEmployees)}.{nameof(LocationEmployee.Employee)}.{nameof(Employee.EmployeeID)}",
            $"{nameof(Location.LocationEmployees)}.{nameof(LocationEmployee.Employee)}.{nameof(Employee.EmployeeName)}",
            $"{nameof(Location.LocationGovernments)}.{nameof(LocationGovernment.LocationGovernmentID)}",
            $"{nameof(Location.LocationGovernments)}.{nameof(LocationGovernment.LocationID)}",
            $"{nameof(Location.LocationGovernments)}.{nameof(LocationGovernment.GovernmentID)}",
            $"{nameof(Location.LocationGovernments)}.{nameof(LocationGovernment.PaySalesTax)}",
            $"{nameof(Location.LocationGovernments)}.{nameof(LocationGovernment.Government)}.{nameof(Government.GovernmentID)}",
            $"{nameof(Location.LocationGovernments)}.{nameof(LocationGovernment.Government)}.{nameof(Government.Name)}"
        };

        public override ISearchCondition GetBaseSearchCondition()
        {
            long companyID = long.Parse(Request.Headers.GetValues("CompanyID").First());
            return new LongSearchCondition<Location>()
            {
                Field = "CompanyID",
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = companyID
            };
        }

        [HttpGet]
        [CompanyAccess]
        [LocationAccess]
        public override Task<Location> Get(long id)
        {
            return base.Get(id);
        }

        [HttpPatch]
        [CompanyAccess]
        [LocationAccess]
        public override async Task<IHttpActionResult> Patch(PatchData patchData)
        {
            long companyID = long.Parse(Request.Headers.GetValues("CompanyID").First());
            long locationID = long.Parse(Request.Headers.GetValues("LocationID").First());
            HashSet<string> fields = patchData.Values.Keys.ToHashSet();

            bool requireManageLocations = false;
            bool optionalManageRegisters = false;

            if (fields.Remove(nameof(Location.AccountIDStoreRevenue)) ||
                fields.Remove(nameof(Location.EmailImplementationIDRegisterOffline)))
            {
                optionalManageRegisters = true;
            }

            requireManageLocations = fields.Any();

            bool isRequestValid = true;
            EmployeeCache.CachedEmployee cachedEmployee = await EmployeeCache.GetCachedEmployee(companyID, SecurityProfile.UserID);
            if (requireManageLocations)
            {
                isRequestValid = cachedEmployee.Permissions.Contains(nameof(Employee.ManageLocations));
            }
            else if (optionalManageRegisters)
            {
                isRequestValid = cachedEmployee.PermissionsByLocationID.GetOrDefault(locationID)?.Contains(nameof(LocationEmployee.ManageRegisters)) ?? false;
            }

            if (!isRequestValid)
            {
                return new StatusCodeResult(System.Net.HttpStatusCode.Forbidden, this);
            }

            return await base.Patch(patchData);
        }
    }
}