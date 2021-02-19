using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using FeePay.Web.Filters;
using Microsoft.AspNetCore.Http;

namespace FeePay.Web.Areas.School.Components
{
    [Area("School")]
    [ViewComponent]
    [SchoolAdminAuthorize]
    public class AcademicSession : ViewComponent
    {
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AcademicSession(ILoginService loginService,
            IHttpContextAccessor httpContextAccessor)
        {
            _loginService = loginService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var session = await _loginService.GetCurrentAcademicSession();
            if (session == null)
            {
                string url = $"/School/Authentication?returnUrl={System.Web.HttpUtility.UrlEncode(HttpContext.Request.Path.Value)}";
                _httpContextAccessor.HttpContext.Response.Redirect(url);
            }
            return View();
        }
    }
}
