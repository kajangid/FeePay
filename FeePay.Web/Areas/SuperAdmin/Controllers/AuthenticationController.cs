using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FeePay.Web.Filters;
using FeePay.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using FeePay.Web.Areas.Common;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Repository;
using static FeePay.Core.Application.Enums.Notification;
using FeePay.Core.Application.Interface.Service;

namespace FeePay.Web.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class AuthenticationController : AreaBaseController
    {
        public AuthenticationController(ILogger<AuthenticationController> Logger,ILoginService LoginService )
        {
            _ILogger = Logger;
            _LoginService = LoginService;
        }
        private readonly ILogger _ILogger;
        private readonly ILoginService _LoginService;

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (User.Identity.IsAuthenticated)
                return RedirectToLocal(returnUrl);


            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            await _LoginService.EnsureSuperAdminLogoutAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SuperAdminLoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _LoginService.AuthenticateSuperAdminAsync(model);
                if (result.Succeeded)
                {
                    _ILogger.LogInformation("update system for login");
                    TostMessage(NotificationType.success, $"Welcome back { result.Message }.");
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    AlertMessage(NotificationType.error,"Error", result.Message);
                    return View(model);
                }
            }
            return View(model);
        }


    }
}
