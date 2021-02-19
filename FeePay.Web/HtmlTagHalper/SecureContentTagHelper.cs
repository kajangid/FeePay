using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Web.HtmlTagHalper
{
    [HtmlTargetElement("secure-content")]
    public class SecureContentTagHelper : TagHelper
    {
        private readonly ILoginService _loginService;

        public SecureContentTagHelper(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HtmlAttributeName("asp-area")]
        public string Area { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            var user = ViewContext.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
                return;
            }

            //var roles = await (
            //from usr in _dbContext.Users
            //join userRole in _dbContext.UserRoles on usr.Id equals userRole.UserId
            //join role in _dbContext.Roles on userRole.RoleId equals role.Id
            //where usr.UserName == user.Identity.Name
            //select role
            //).ToListAsync();

            // pass admin for all access
            var roles = await _loginService.GetUserAccessRoles(user.Identity.Name, Area);
            foreach (var role in roles)
                if (!string.IsNullOrEmpty(role) && role.Equals("admin", StringComparison.InvariantCultureIgnoreCase))
                    return;


            var actionId = $"{Area}:{Controller}:{Action}";
            var rolesAccess = await _loginService.GetUserAccessRights(user.Identity.Name, Area);
            foreach (var Access in rolesAccess)
            {
                var accessList = JsonConvert.DeserializeObject<IEnumerable<MvcControllerInfo>>(Access);
                if (accessList.SelectMany(c => c.Actions).Any(a => a.Id == actionId))
                    return;
            }

            output.SuppressOutput();
        }
    }
}
