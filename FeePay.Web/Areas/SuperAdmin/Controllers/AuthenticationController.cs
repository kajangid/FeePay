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
using FeePay.Web.Models;

namespace FeePay.Web.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class AuthenticationController : AreaBaseController
    {
        private readonly SignInManager<SuperAdminUser> _SignInManager;
        public AuthenticationController(ILogger<AuthenticationController> logger,
            SignInManager<SuperAdminUser> _signInManager)
        {
            _ILogger = logger;
            _SignInManager = _signInManager;
        }
        private readonly ILogger _ILogger;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (User.Identity.IsAuthenticated)
                return RedirectToLocal(returnUrl);


            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Ensure all logout 
            await _SignInManager.SignOutAsync();

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SuperAdminLoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _ILogger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }



        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            else return RedirectToAction("Index", "Home");
        }
    }
}
