using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using API.Common;
using API.Common.Attributes;
using API.Common.Extensions;
using API_Government.Attributes;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using WebModels.fleet;
using WebModels.gov;
using WebModels.security;

namespace API_Government.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("gov")]
    [GovernmentAccess(RequiredPermissions = new string[] { nameof(Official.ManageOfficials) })]
    
    public class OfficialController : DataObjectController<Official>
    {
        public override IEnumerable<string> DefaultRetrievedFields => new string[]
        {
            nameof(Official.OfficialID),
            nameof(Official.GovernmentID),
            nameof(Official.UserID),
            nameof(Official.ManageOfficials),
            nameof(Official.ManageEmails),
            nameof(Official.ManageAccounts),
            nameof(Official.ManageTaxes),
            nameof(Official.OfficialName),
            nameof(Official.CanMintCurrency),
            nameof(Official.ManageInvoices),
            nameof(Official.IssueWireTransfers),
            nameof(Official.CanConfigureInterest),
            nameof(Official.ManageLaws),
            nameof(Official.ManagePurchaseOrders),
            $"{nameof(Official.FleetSecurity)}.{nameof(FleetSecurity.AllowSetup)}",
            $"{nameof(Official.FleetSecurity)}.{nameof(FleetSecurity.AllowLeasingManagement)}",
            $"{nameof(Official.FleetSecurity)}.{nameof(FleetSecurity.IsYardmaster)}",
            $"{nameof(Official.FleetSecurity)}.{nameof(FleetSecurity.IsTrainCrew)}",
            $"{nameof(Official.FleetSecurity)}.{nameof(FleetSecurity.AllowLoadUnload)}"
        };

        [HttpGet]
        [GovernmentAccess(OptionalPermissions = new string[] { nameof(Official.ManageOfficials), nameof(Official.ManageAccounts), "FleetSecurity.IsTrainCrew", "FleetSecurity.IsYardmaster" })]
        public List<Official> GetAllForGovernment()
        {
            long govID = long.Parse(Request.Headers.GetValues("GovernmentID").First());
            Search<Official> officialSearch = new Search<Official>(new LongSearchCondition<Official>()
            {
                Field = "GovernmentID",
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = govID
            });

            return officialSearch.GetReadOnlyReader(null, DefaultRetrievedFields).ToList();
        }

        [HttpGet]
        public List<User> Candidates()
        {
            long govID = long.Parse(Request.Headers.GetValues("GovernmentID").First());
            Search<User> userSearch = new Search<User>(new ExistsSearchCondition<User>()
            {
                RelationshipName = "Officials",
                ExistsType = ExistsSearchCondition<User>.ExistsTypes.NotExists,
                Condition = new LongSearchCondition<Official>()
                {
                    Field = "GovernmentID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = govID
                }
            });

            return userSearch.GetReadOnlyReader(null, new string[] { "UserID", "Username" }).ToList();
        }

        [GovernmentAccess(RequiredPermissions = null)]
        public Official GetForGovernment()
        {
            SecurityProfile securityProfile = (SecurityProfile)Request.Properties["SecurityProfile"];
            long govID = long.Parse(Request.Headers.GetValues("GovernmentID").First());
            Search<Official> officialSeach = new Search<Official>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<Official>()
                {
                    Field = "GovernmentID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = govID
                },
                new LongSearchCondition<Official>()
                {
                    Field = "UserID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = securityProfile.UserID
                }));

            return officialSeach.GetReadOnly(null, DefaultRetrievedFields);
        }

        [HttpPut]
        public IHttpActionResult PutFleetSecurity(FleetSecurity fleetSecurity)
        {
            if (fleetSecurity?.OfficialID == null)
            {
                return BadRequest("Fleet Security must be at least associated to an Official");
            }

            FleetSecurity security = new Search<FleetSecurity>(new LongSearchCondition<FleetSecurity>()
            {
                Field = nameof(FleetSecurity.OfficialID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = fleetSecurity.OfficialID
            }).GetEditable();

            if (security == null)
            {
                security = DataObjectFactory.Create<FleetSecurity>();
                security.OfficialID = fleetSecurity.OfficialID;
            }

            security.AllowSetup = fleetSecurity.AllowSetup;
            security.AllowLeasingManagement = fleetSecurity.AllowLeasingManagement;
            security.IsYardmaster = fleetSecurity.IsYardmaster;
            security.IsTrainCrew = fleetSecurity.IsTrainCrew;
            security.AllowLoadUnload = fleetSecurity.AllowLoadUnload;
            if (!security.Save())
            {
                return security.HandleFailedValidation(this);
            }

            return Ok(security);
        }
    }
}