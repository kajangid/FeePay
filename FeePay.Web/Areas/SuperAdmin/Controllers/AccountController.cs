using FeePay.Core.Domain.Entities.Identity;
using FeePay.Web.Filters;
using FeePay.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeePay.Web.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    //[SuperAdminAuthorize]
    public class AccountController : Controller
    {
        private readonly UserManager<SuperAdminUser> _UserManager;
        private readonly SignInManager<SuperAdminUser> _SignInManager;
        public AccountController(ILogger<AccountController> logger,
            UserManager<SuperAdminUser> _userManager,
            SignInManager<SuperAdminUser> _signInManager)
        {
            _ILogger = logger;
            _UserManager = _userManager;
            _SignInManager = _signInManager;
        }
        private readonly ILogger _ILogger;


        public IActionResult SuperAdminUserList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new SuperAdminUser { UserName = model.Email, Email = model.Email, Password = model.Password, IsActive = true };
                var result = await _UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await _SignInManager.SignInAsync(user, isPersistent: false);
                    //_ILogger.LogInformation("User created a new account with password.");
                    _ILogger.LogInformation("User created a new account with password.");
                    return RedirectToAction(nameof(SuperAdminUserList));
                }
                AddErrors(result);
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [SuperAdminAuthorize]
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            _ILogger.LogInformation("User logged out.");
            return RedirectToAction(nameof(AuthenticationController.Index), "Authentication");
        }




        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

    }
}
