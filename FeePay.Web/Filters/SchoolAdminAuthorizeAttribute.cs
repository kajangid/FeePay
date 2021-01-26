using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace FeePay.Web.Filters
{
    public class SchoolAdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
#nullable enable
        //public string? Roles { get; set; }
#nullable disable

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            string url = context.HttpContext.Request.Path;
            if (!IsProtectedAction(context))
            {
                return;
            }

            if (!IsUserAuthenticated(context))
            {
                context.Result = RedirectToLogin(url);
                return;
            }

            if (!UserHasClaim(context))
            {
                context.Result = RedirectToLogin(url);
                return;
            }

            // TODO: Make this role SYSTEM for only developer use
            // pass admin for all access
            foreach (var role in ClaimUserRoles(context))
                if (!string.IsNullOrEmpty(role) && role.Equals("admin", StringComparison.InvariantCultureIgnoreCase))
                    return;

            var actionId = GetActionId(context);

            foreach (var access in ClaimUserRightAccess(context))
            {
                var accessList = JsonConvert.DeserializeObject<IEnumerable<MvcControllerInfo>>(access);
                if (accessList != null && accessList.SelectMany(c => c.Actions).Any(a => a.Id == actionId))
                    return;
            }

            //context.Result = new ForbidResult();
            context.Result = RedirectToAccessDenied(url);
            return;

        }


        private bool IsProtectedAction(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                return false;

            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var controllerTypeInfo = controllerActionDescriptor.ControllerTypeInfo;
            var actionMethodInfo = controllerActionDescriptor.MethodInfo;

            var authorizeAttribute = controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            authorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            var customAuthorizeAttribute = controllerTypeInfo.GetCustomAttribute<SchoolAdminAuthorizeAttribute>();
            if (customAuthorizeAttribute != null)
                return true;

            customAuthorizeAttribute = actionMethodInfo.GetCustomAttribute<SchoolAdminAuthorizeAttribute>();
            if (customAuthorizeAttribute != null)
                return true;

            return false;
        }

        private bool IsUserAuthenticated(AuthorizationFilterContext context)
        {
            return context.HttpContext.User.Identity.IsAuthenticated;
        }

        private string GetActionId(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var area = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue;
            var controller = controllerActionDescriptor.ControllerName;
            var action = controllerActionDescriptor.ActionName;

            return $"{area}:{controller}:{action}";
        }
        private RedirectToRouteResult RedirectToLogin(string url)
        {
            return new RedirectToRouteResult(new RouteValueDictionary(new { area = "School", controller = "Authentication", action = "Index", returnUrl = url }));
        }
        private RedirectToRouteResult RedirectToDashboard(string url)
        {
            return new RedirectToRouteResult(new RouteValueDictionary(new { area = "School", controller = "Home", action = "Index", returnUrl = url }));
        }
        private RedirectToRouteResult RedirectToAccessDenied(string url)
        {
            return new RedirectToRouteResult(new RouteValueDictionary(new { area = "School", controller = "Authentication", action = "Accessdenied", returnUrl = url }));
        }
        private bool UserHasClaim(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var authenticateAdminResult = claims
                .FirstOrDefault(claim => claim.Type == "SchoolAuthRoute" && claim.Issuer.Equals("SchoolAdmin", StringComparison.InvariantCultureIgnoreCase));

            return authenticateAdminResult != null;
        }
        private List<string> ClaimUserRightAccess(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var authenticateAdminResult = claims?
                .Where(a => a.Type == "RoleAccess" && a.ValueType.Equals("role_access", StringComparison.InvariantCultureIgnoreCase) && a.Issuer.Equals("school", StringComparison.InvariantCultureIgnoreCase));
            return (authenticateAdminResult != null ? authenticateAdminResult.Select(s => s.Value).ToList() : new List<string>());
        }
        private List<string> ClaimUserRoles(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var authenticateAdminResult = claims?
                .Where(a => a.Type == "Role" && a.ValueType.Equals("role_name", StringComparison.InvariantCultureIgnoreCase) && a.Issuer.Equals("school", StringComparison.InvariantCultureIgnoreCase));
            return (authenticateAdminResult != null ? authenticateAdminResult.Select(s => s.Value).ToList() : new List<string>());
        }
    }
}
