﻿using FeePay.Core.Application.Interface.Service;
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
    public class AccountController : AreaBaseController
    {
        public AccountController(ILogger<AccountController> logger, ILoginService loginService)
        {
            _ILogger = logger;
            _LoginService = loginService;
        }
        private readonly ILogger _ILogger;
        private readonly ILoginService _LoginService;


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _LoginService.SchoolAdminLogout();
            _ILogger.LogInformation("User logged out.");
            return RedirectToAction(nameof(AuthenticationController.Index), "Authentication");
        }
    }
}
