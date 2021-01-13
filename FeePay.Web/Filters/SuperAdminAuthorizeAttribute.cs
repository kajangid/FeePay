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
#nullable enable
        public string? Roles { get; set; }
#nullable disable
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
                    var claims = filterContext.HttpContext.User.Claims;
                    var authenticateAdminResult = claims
                        .FirstOrDefault(claim => claim.Type == "SuperAdminAuthRoute" && claim.Issuer.Equals("SuperAdmin", StringComparison.InvariantCultureIgnoreCase));

                    // check if Authorize
                    if (authenticateAdminResult == null) { 
                        //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "SuperAdmin", action = "Index" }));// or send to the return url
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "SuperAdmin", controller = "Authentication", action = "Index", returnUrl = url }));
                    }

                    // check if in role
                    if (authenticateAdminResult != null && !string.IsNullOrEmpty(Roles))
                    {
                        var rolelist = Roles.Trim().ToUpper().Split(new char[] { ',' }).ToList();
                        if (!claims.Any(a => a.Type == "Role" && rolelist.Contains(a.Value.ToUpper()) && a.Issuer.Equals("superadmin", StringComparison.InvariantCultureIgnoreCase)))
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "SuperAdmin", controller = "Authentication", action = "Accessdenied", returnUrl = url }));
                    }
                }
                else
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "SuperAdmin", controller = "Authentication", action = "Index", returnUrl = url }));
            }
        }
    }
}
