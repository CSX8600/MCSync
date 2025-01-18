using API_Company.App_Code;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebModels.company;
using static API_Company.App_Code.EmployeeCache;
using static API_Company.App_Code.RegisterCache;

public class RegisterAdministrativeAction : ActionFilterAttribute
{
    public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
    {
        string playerName = actionContext.Request.Headers.GetValues("PlayerName").FirstOrDefault();
        string registerID = actionContext.Request.Headers.GetValues("RegisterIdentifier").FirstOrDefault();
        if (string.IsNullOrEmpty(playerName) ||
            string.IsNullOrEmpty(registerID) ||
            !Guid.TryParse(registerID, out Guid registerIdentifier))
        {
            actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            return;
        }

        CachedRegister cachedRegister = RegisterCache.GetRegisterByIdentifier(registerIdentifier);
        if (cachedRegister == null)
        {
            actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            return;
        }

        CachedEmployee cachedEmployee = await EmployeeCache.GetCachedEmployee(cachedRegister.CompanyID.Value, playerName);
        if (cachedEmployee == null ||
            !cachedEmployee.PermissionsByLocationID.ContainsKey(cachedRegister.LocationID.Value) ||
            !cachedEmployee.PermissionsByLocationID[cachedRegister.LocationID.Value].Contains(nameof(LocationEmployee.ManageRegisters)))
        {
            actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            return;
        }
    }
}