﻿using API.Common;
using API_Company.App_Code;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API_Company.Attributes
{
    internal class ImmersibrookAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            SecurityProfile securityProfile = actionContext.Request.Properties["SecurityProfile"] as SecurityProfile;
            if (securityProfile == null || !ImmersibrookUserCache.IsImmersibrookUser(securityProfile.UserID))
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}
