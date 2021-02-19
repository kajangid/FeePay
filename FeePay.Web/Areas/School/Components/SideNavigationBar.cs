using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.Extensions;
using FeePay.Web.Filters;
using FeePay.Core.Application.IoC;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;

namespace FeePay.Web.Areas.School.Components
{
    [Area("School")]
    [ViewComponent]
    [SchoolAdminAuthorize]
    public class SideNavigationBar : ViewComponent
    {
        private readonly ILogger<SideNavigationBar> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoginService _loginService;
        public SideNavigationBar(
            ILogger<SideNavigationBar> _logger,
            IHttpContextAccessor _httpContextAccessor,
            ILoginService _loginService)
        {
            this._logger = _logger;
            this._httpContextAccessor = _httpContextAccessor;
            this._loginService = _loginService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
