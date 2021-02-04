using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FeePay.Web.Filters;
using FeePay.Core.Application.Interface.Service;
using FeePay.Web.Areas.Common;

namespace FeePay.Web.Areas.Student.Controllers
{
    [Area("Student")]
    [StudentAuthorize]
    public class AccountController : AreaBaseController
    {
        public AccountController(ILogger<AccountController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }
        private readonly ILogger<AccountController> _logger;
        private readonly ILoginService _loginService;


        public IActionResult Index()
        {
            return View();
        }
    }
}
