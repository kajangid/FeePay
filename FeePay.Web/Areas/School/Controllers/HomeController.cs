using FeePay.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [SchoolAdminAuthorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
