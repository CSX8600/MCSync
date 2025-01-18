using API.Common.Attributes;
using API_Company.Attributes;
using API_Company.Models;
using ClussPro.ObjectBasedFramework.DataSearch;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebModels.security;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ImmersibrookAccess]
    public class EmployeeIBAccessController : ApiController
    {
        [HttpGet]
        public async Task<List<EmployeeToDoItem>> GetToDoItemsForUser([FromUri]string username)
        {
            Search<User> userSearch = new Search<User>(new StringSearchCondition<User>()
            {
                Field = nameof(WebModels.security.User.Username),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = username
            });

            long? userID = (await Task.Run(() => userSearch.GetReadOnly(null, new[] { nameof(WebModels.security.User.UserID) })))?.UserID;

            if (userID == null)
            {
                return new List<EmployeeToDoItem>();
            }

            return await EmployeeToDoItem.GetForUserID(userID.Value);
        }
    }
}
