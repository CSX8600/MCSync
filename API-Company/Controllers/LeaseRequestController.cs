using API.Common.Attributes;
using API_Company.Attributes;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebModels.company;
using WebModels.fleet;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(RequiredPermissions = new[] { nameof(LocationEmployee.ManagePurchaseOrders) })]
    [CompanyAccess(RequiredPermissions = new[] { nameof(Employee.FleetSecurity) + "." + nameof(FleetSecurity.AllowLeasingManagement)})]
    public class LeaseRequestController : ApiController
    {
        private long? CompanyID => long.Parse(Request.Headers.GetValues("CompanyID").First());
        private static readonly List<string> _fields = FieldPathUtility.CreateFieldPathsAsList<LeaseRequest>(lr => new List<object>()
        {
            lr.LeaseRequestID,
            lr.LeaseType,
            lr.RailcarType,
            lr.BidEndTime,
            lr.Purpose,
            lr.LeaseBids.First().LeaseBidID
        });

        [HttpGet]
        public async Task<LeaseRequest> Get(long? id)
        {
            return await Task.Run(() => DataObject.GetReadOnlyByPrimaryKey<LeaseRequest>(id, null, _fields));
        }

        [HttpGet]
        private async Task<List<LeaseRequest>> GetForCompany()
        {
            Search<LeaseRequest> leaseRequestSearch = new Search<LeaseRequest>(new LongSearchCondition<LeaseRequest>()
            {
                Field = nameof(LeaseRequest.CompanyIDRequester),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = CompanyID
            });

            return await Task.Run(() => leaseRequestSearch.GetReadOnlyReader(null, _fields).ToList());
        }

        [HttpGet]
        public async Task<List<LeaseRequest>> GetAll()
        {
            Search<LeaseRequest> leaseRequestSearch = new Search<LeaseRequest>(new LongSearchCondition<LeaseRequest>()
            {
                Field = nameof(LeaseRequest.CompanyIDRequester),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = CompanyID
            });

            return await Task.Run(() => leaseRequestSearch.GetReadOnlyReader(null, _fields).ToList());
        }
    }
}
