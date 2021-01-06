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
        public AccountController(ILogger<AccountController> logger)
        {
            _ILogger = logger;
        }
        private readonly ILogger _ILogger;
        public IActionResult Index()
        {
            return View();
        }
    }
}
