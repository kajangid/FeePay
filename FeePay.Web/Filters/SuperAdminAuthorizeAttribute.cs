using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace FeePay.Web.Filters
{
    public class SuperAdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var controllerActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true);
                if (actionAttributes.Any(x => x is AllowAnonymousAttribute))
                    return;
            }

            if (filterContext != null)
            {
                string url = filterContext.HttpContext.Request.Path;
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    var authenticateAdminResult = filterContext.HttpContext.User.Claims
                        .FirstOrDefault(claim => claim.Type == "SuperAdminAuthRoute" && claim.Issuer.Equals("SuperAdmin", StringComparison.InvariantCultureIgnoreCase));

                    if (authenticateAdminResult == null)
                        //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "SuperAdmin", action = "Index" }));// or send to the return url
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "SuperAdmin", controller = "Authentication", action = "Index" }));
                }
                else
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "SuperAdmin", controller = "Authentication", action = "Index" }));
            }
        }
    }
}
