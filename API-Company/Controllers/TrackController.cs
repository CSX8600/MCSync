using API.Common.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebModels.fleet;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    public class TrackController : ApiController
    {
        [HttpGet]
        public List<Track> GetAll()
        {
            Search<Track> trackSearch = new Search<Track>();
            return trackSearch.GetReadOnlyReader(null, FieldPathUtility.CreateFieldPathsAsList<Track>(t => new List<object>()
            {
                t.TrackID,
                t.Name,
                t.CompanyIDOwner
            })).ToList();
        }
    }
}
