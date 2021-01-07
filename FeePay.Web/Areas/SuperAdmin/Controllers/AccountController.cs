using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Web.Areas.Common;
using FeePay.Web.Filters;
using FeePay.Core.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeePay.Core.Application.Interface.Service;

namespace FeePay.Web.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [SuperAdminAuthorize]
    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AccountController : AreaBaseController
    {
        public AccountController(ILogger<AccountController> logger, ILoginService LoginService)
        {
            _ILogger = logger;
            _LoginService = LoginService;
        }
        private readonly ILogger _ILogger;
        private readonly ILoginService _LoginService;

        [HttpGet]
        public IActionResult Index()
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
        public IActionResult Register(RegisterViewModel model)
        //public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = new SuperAdminUser { UserName = model.Email, Email = model.Email, Password = model.Password, IsActive = true };
            //    var result = await _UserManager.CreateAsync(user, model.Password);
            //    if (result.Succeeded)
            //    {
            //        //await _SignInManager.SignInAsync(user, isPersistent: false);
            //        //_ILogger.LogInformation("User created a new account with password.");
            //        _ILogger.LogInformation("User created a new account with password.");
            //        return RedirectToAction(nameof(Index));
            //    }
            //    AddErrors(result);
            //}
            return View(model);
        }

        [HttpGet]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _LoginService.SuperAdminLogout();
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
