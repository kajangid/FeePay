using FeePay.Core.Application.Interface.Service;
using FeePay.Web.Areas.Common;
using FeePay.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [SchoolAdminAuthorize]
    public class HomeController : AreaBaseController
    {
        private readonly ILogger _logger;
        private readonly ILoginService _loginService;
        public HomeController(ILogger<HomeController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View();
        }









        #region LogOut method
        [HttpGet]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _loginService.SchoolAdminLogout();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(AuthenticationController.Index), "Authentication");
        }
        #endregion
    }
}
