using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Service;
using FeePay.Web.Areas.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FeePay.Core.Application.Enums.Notification;

namespace FeePay.Web.Areas.Student.Controllers
{
    [Area("Student")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AuthenticationController : AreaBaseController
    {
        private readonly ILogger _logger;
        private readonly ILoginService _loginService;
        public AuthenticationController(ILogger<AuthenticationController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }


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
            await _loginService.EnsureStudentLogoutAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(StudentLoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.warning, "Warning", "Please fill all required field.");
                return View(model);
            }
            try
            {
                var result = await _loginService.AuthenticateStudentAsync(model);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    TostMessage(NotificationType.success, $"Welcome back { result.Message }.");
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    _logger.LogWarning(result.Message);
                    ModelState.AddModelError(string.Empty, result.Message);
                    AlertMessage(NotificationType.error, "Error", result.Message);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error Login user with schoolcode={0} and username={1}",model.SchoolUniqueId,model.UserName);
                AlertMessage(NotificationType.error, "Error", "Error when logging user.");
            }
            return View(model);
        }

        [HttpGet]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _loginService.SchoolAdminLogout();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(AuthenticationController.Index), "Authentication");
        }
    }
}
