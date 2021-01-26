using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Service;
using FeePay.Web.Areas.Common;
using FeePay.Web.Extensions;
using FeePay.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FeePay.Core.Application.Enums.Notification;

namespace FeePay.Web.Areas.School.Controllers
{
    /// <summary>
    /// Authentication school users to enter.
    /// </summary>
    [Area("School")]
    public class AuthenticationController : AreaBaseController
    {
        public AuthenticationController(ILogger<AuthenticationController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }
        private readonly ILogger _logger;
        private readonly ILoginService _loginService;

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (User.Identity.IsAuthenticated && _loginService.CheckUserIdentityClaim())
                return RedirectToLocal(returnUrl);

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Ensure all logout 
            await _loginService.EnsureSchoolUserLogoutAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SchoolLoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _loginService.AuthenticateSchoolUserAsync(model);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    TostMessage(NotificationType.success, $"Welcome back { result.Message }.");
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    AlertMessage(NotificationType.error, "Error", result.Message);
                    return View(model);
                }
            }
            return View(model);

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
