using API.Common;
using API.Common.Attributes;
using API_Company.App_Code;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API_Company.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    /// <summary>Ensures user is an employee</summary>
    public class CompanyAccessAttribute : ActionFilterAttribute
    {
        public string[] RequiredPermissions { get; set; }
        public string[] OptionalPermissions { get; set; }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<CompanyAccessAttribute>().Count > 0)
            {
                await actionContext.ActionDescriptor.GetCustomAttributes<CompanyAccessAttribute>()[0].InternalOnActionExecutingAsync(actionContext, cancellationToken);
                return;
            }

            await InternalOnActionExecutingAsync(actionContext, cancellationToken);
        }

        private async Task InternalOnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            HttpResponseMessage unauthorizedResponse = await MesabrookAuthorizationAttribute.CheckHeadersForSecurity(actionContext);

            if (unauthorizedResponse != null)
            {
                actionContext.Response = unauthorizedResponse;
                return;
            }

            SecurityProfile securityProfile = SecurityCache.Get(actionContext.Request.Headers.Authorization.Parameter);
            if (securityProfile == null || !actionContext.Request.Headers.Contains("CompanyID") || !long.TryParse(actionContext.Request.Headers.GetValues("CompanyID").First(), out long companyID))
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                return;
            }

            EmployeeCache.CachedEmployee cachedEmployee = await EmployeeCache.GetCachedEmployee(companyID, securityProfile.UserID);
            if (cachedEmployee.EmployeeID == 0)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                return;
            }

            if (RequiredPermissions != null)
            {
                if (!RequiredPermissions.All(p => cachedEmployee.Permissions.Contains(p)))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                    return;
                }
            }

            if (OptionalPermissions != null)
            {
                if (!OptionalPermissions.Any(p => cachedEmployee.Permissions.Contains(p)))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                    return;
                }
            }
        }
    }
}