using FeePay.Web.Areas.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    public class AuthenticationController : AreaBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
