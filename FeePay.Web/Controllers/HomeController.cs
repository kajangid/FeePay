using FeePay.Core.Application.Enums;
using FeePay.Core.Application.Exceptions;
using FeePay.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FeePay.Web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //_logger.LogInformation("Hello, {Name}!", LogFile.FileName.Student);
            // -> Event written to log-Alice.txt

            //_logger.LogInformation("Hello, {Name}!", LogFile.FileName.SuperAdmin);
            // -> Event written to log-Bob.txt

            return View();
        }




        [HttpGet("/Register/School")]
        public IActionResult SchoolRegistration()
        {
            return View();
        }

        //
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
