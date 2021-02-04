using FeePay.Core.Application.DTOs;
using FeePay.Web.Areas.Common;
using FeePay.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeePay.Web.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [SuperAdminAuthorize]
    public class HomeController : AreaBaseController
    {
        public HomeController(ILogger<HomeController> Logger)
        {
            _Logger = Logger;
        }
        private readonly ILogger _Logger;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{Area}/Profile")]
        public IActionResult Profile()
        {
            return View();
        }
        [HttpGet("{Area}/Profile/Edit")]
        public IActionResult ProfileEdit()
        {
            return View();
        }
        [HttpPost("{Area}/Profile/Edit")]
        public IActionResult ProfileEdit(SuperAdmin_UserViewModel model)// change to new view model
        {
            return View();
        }
    }
}
