using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using API.Common;
using API.Common.Attributes;
using API_Company.App_Code;

namespace API_Company.Attributes
{
    public class LocationAccessAttribute : ActionFilterAttribute
    {
        public string[] RequiredPermissions { get; set; }
        public string[] OptionalPermissions { get; set; }
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<LocationAccessAttribute>().Count > 0)
            {
                await actionContext.ActionDescriptor.GetCustomAttributes<LocationAccessAttribute>()[0].InternalOnActionExecutingAsync(actionContext, cancellationToken);
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

            if (!actionContext.Request.Headers.Contains("LocationID") || !long.TryParse(actionContext.Request.Headers.GetValues("LocationID").First(), out long locationID))
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

            if (!cachedEmployee.PermissionsByLocationID.ContainsKey(locationID))
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                return;
            }

            if (RequiredPermissions != null)
            {
                if (!RequiredPermissions.All(p => cachedEmployee.PermissionsByLocationID[locationID].Contains(p)))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                    return;
                }
            }

            if (OptionalPermissions != null)
            {
                if (!OptionalPermissions.Any(p => cachedEmployee.PermissionsByLocationID[locationID].Contains(p)))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                    return;
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}