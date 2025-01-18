using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using API.Common;
using API.Common.Attributes;
using API.Common.Extensions;
using API_Company.Attributes;
using API_Company.Models;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using WebModels.company;
using WebModels.fleet;
using WebModels.security;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    public class EmployeeController : DataObjectController<Employee>
    {
        public override IEnumerable<string> DefaultRetrievedFields => new List<string>()
        {
            nameof(Employee.EmployeeID),
            nameof(Employee.CompanyID),
            nameof(Employee.UserID),
            nameof(Employee.EmployeeName),
            nameof(Employee.ManageEmails),
            nameof(Employee.ManageEmployees),
            nameof(Employee.ManageAccounts),
            nameof(Employee.ManageLocations),
            nameof(Employee.IssueWireTransfers),
            $"{nameof(Employee.FleetSecurity)}.{nameof(FleetSecurity.AllowSetup)}",
            $"{nameof(Employee.FleetSecurity)}.{nameof(FleetSecurity.AllowLeasingManagement)}",
            $"{nameof(Employee.FleetSecurity)}.{nameof(FleetSecurity.IsYardmaster)}",
            $"{nameof(Employee.FleetSecurity)}.{nameof(FleetSecurity.IsTrainCrew)}",
            $"{nameof(Employee.FleetSecurity)}.{nameof(FleetSecurity.AllowLoadUnload)}"
        };

        [HttpGet]
        public Employee GetForCompany(long id)
        {
            SecurityProfile securityProfile = Request.Properties["SecurityProfile"] as SecurityProfile;

            Search<Employee> employeeSearch = new Search<Employee>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<Employee>()
                {
                    Field = "CompanyID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                },
                new LongSearchCondition<Employee>()
                {
                    Field = "UserID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = securityProfile.UserID
                }));

            return employeeSearch.GetReadOnly(null, new List<string>(DefaultRetrievedFields) { nameof(Employee.EmployeeName) });
        }

        [HttpGet]
        [CompanyAccess(OptionalPermissions = new string[] { nameof(Employee.ManageEmployees), nameof(Employee.ManageLocations), nameof(Employee.ManageAccounts), "FleetSecurity.IsTrainCrew", "FleetSecurity.IsYardmaster" })]
        public List<Employee> GetAllForCompany()
        {
            long companyID = long.Parse(Request.Headers.GetValues("CompanyID").First());

            Search<Employee> employeeSearch = new Search<Employee>(new LongSearchCondition<Employee>()
            {
                Field = "CompanyID",
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = companyID
            });

            return employeeSearch.GetReadOnlyReader(null, new List<string>(DefaultRetrievedFields) { nameof(Employee.EmployeeName) }).ToList();
        }

        [HttpPatch]
        [CompanyAccess(RequiredPermissions = new string[] { nameof(Employee.ManageEmployees) })]
        public async override Task<IHttpActionResult> Patch(PatchData patchData)
        {
            return await base.Patch(patchData);
        }

        [HttpGet]
        [CompanyAccess(RequiredPermissions = new string[] { nameof(Employee.ManageEmployees) })]
        public List<User> GetCandidates()
        {
            long companyID = long.Parse(Request.Headers.GetValues("CompanyID").First());

            Search<User> userSearch = new Search<User>(new ExistsSearchCondition<User>()
            {
                RelationshipName = "Employees",
                ExistsType = ExistsSearchCondition<User>.ExistsTypes.NotExists,
                Condition = new LongSearchCondition<Employee>()
                {
                    Field = "CompanyID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = companyID
                }
            });

            return userSearch.GetReadOnlyReader(null, new string[] { "UserID", "Username" }).ToList();
        }

        [HttpPut]
        [CompanyAccess(RequiredPermissions = new string[] { nameof(Employee.ManageEmployees) })]
        public async override Task<IHttpActionResult> Put(Employee dataObject)
        {
            return await base.Put(dataObject);
        }

        [HttpPost]
        [CompanyAccess(RequiredPermissions = new string[] { nameof(Employee.ManageEmployees) })]
        public async override Task<IHttpActionResult> Post(Employee dataObject)
        {
            return await base.Post(dataObject);
        }

        [HttpPut]
        [CompanyAccess(RequiredPermissions = new[] { nameof(Employee.ManageEmployees) })]
        public IHttpActionResult PutFleetSecurity(FleetSecurity dataObject)
        {
            if (dataObject.EmployeeID == null)
            {
                return BadRequest("Object must at least provide an EmployeeID");
            }

            FleetSecurity fleetSecurity = new Search<FleetSecurity>(new LongSearchCondition<FleetSecurity>()
            {
                Field = nameof(FleetSecurity.EmployeeID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = dataObject.EmployeeID
            }).GetEditable();
            
            if (fleetSecurity == null) // Must've gotten messed up somewhere - we can create one np
            {
                fleetSecurity = DataObjectFactory.Create<FleetSecurity>();
                fleetSecurity.EmployeeID = dataObject.EmployeeID;
            }

            fleetSecurity.AllowSetup = dataObject.AllowSetup;
            fleetSecurity.AllowLeasingManagement = dataObject.AllowLeasingManagement;
            fleetSecurity.IsYardmaster = dataObject.IsYardmaster;
            fleetSecurity.IsTrainCrew = dataObject.IsTrainCrew;
            fleetSecurity.AllowLoadUnload = dataObject.AllowLoadUnload;
            if (!fleetSecurity.Save())
            {
                return fleetSecurity.HandleFailedValidation(this);
            }

            return Ok(fleetSecurity);
        }

        [HttpPatch]
        public IHttpActionResult PatchFleetSecurity(PatchData patchData)
        {
            FleetSecurity fleetSecurity = new Search<FleetSecurity>(new LongSearchCondition<FleetSecurity>()
            {
                Field = nameof(FleetSecurity.EmployeeID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = patchData.PrimaryKey
            }).GetEditable();

            if (fleetSecurity == null)
            {
                fleetSecurity = DataObjectFactory.Create<FleetSecurity>();
                fleetSecurity.EmployeeID = patchData.PrimaryKey;
            }

            Dictionary<string, object> patchValues = patchData.Values.Where(kvp => FleetSecurity.SecurityFields.Contains(kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            fleetSecurity.PatchData(patchData.Method, patchValues);
            if (!fleetSecurity.Save())
            {
                return fleetSecurity.HandleFailedValidation(this);
            }

            return Ok();
        }


        [HttpGet]
        public async Task<List<EmployeeToDoItem>> GetToDoItems()
        {
            return await EmployeeToDoItem.GetForUserID(SecurityProfile.UserID);
        }
    }
}