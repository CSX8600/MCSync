using API.Common.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebModels.fleet;

namespace API_Fleet.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess(new[] { "gov", "company" })]
    public class RailcarRouteController : ApiController
    {
        private readonly List<string> _fieldsToRetrieve = FieldPathUtility.CreateFieldPathsAsList<RailcarRoute>(rr => new List<object>()
        {
            rr.RailcarRouteID,
            rr.SortOrder,
            rr.RailcarID,
            rr.Railcar.RailcarID,
            rr.Railcar.ReportingMark,
            rr.Railcar.ReportingNumber,
            rr.CompanyIDFrom,
            rr.CompanyFrom.CompanyID,
            rr.CompanyFrom.Name,
            rr.CompanyIDTo,
            rr.CompanyTo.CompanyID,
            rr.CompanyTo.Name,
            rr.GovernmentIDFrom,
            rr.GovernmentFrom.GovernmentID,
            rr.GovernmentFrom.Name,
            rr.GovernmentIDTo,
            rr.GovernmentTo.GovernmentID,
            rr.GovernmentTo.Name
        });

        [HttpGet]
        public async Task<List<RailcarRoute>> GetForRailcar(long? id)
        {
            Search<RailcarRoute> railcarRouteSearch = new Search<RailcarRoute>(new LongSearchCondition<RailcarRoute>()
            {
                Field = nameof(RailcarRoute.RailcarID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = id
            });

            return await Task.Run(() => railcarRouteSearch.GetReadOnlyReader(null, _fieldsToRetrieve).OrderBy(rr => rr.SortOrder).ToList());
        }
    }
}
