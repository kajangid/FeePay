using FeePay.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FeePay.Web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorController : Controller
    {
        // TODO: Make complete pages with error description
        [Route("/Error")]
        public IActionResult Index()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                ViewBag.ErrorMessage = exceptionFeature.Error.Message;
                ViewBag.RouteOfException = exceptionFeature.Path;
            }

            //return View("~/Views/Error/InternalServerError.cshtml");
            return View("~/Views/Error/ServiceUnavailable.cshtml");
        }
        [HttpGet("/Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return View("~/Views/Error/BadRequest.cshtml");
                case 401:
                    return View("~/Views/Error/Unauthorized.cshtml");
                case 403:
                    return View("~/Views/Error/Forbidden.cshtml");
                case 404:
                    return View("~/Views/Error/NotFound.cshtml");
                default:
                    return View(); // Something Went Wrong Page
            }
        }


        //[HttpGet("/Error")]
        //public IActionResult Index()
        //{
        //    IExceptionHandlerPathFeature
        //    iExceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        //    if (iExceptionHandlerFeature != null)
        //    {
        //        string path = iExceptionHandlerFeature.Path;
        //        Exception exception = iExceptionHandlerFeature.Error;
        //        //Write code here to log the exception details
        //        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //    }
        //    return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        //[HttpGet("/Error/NotFound/{statusCode}")]
        //public IActionResult NotFound(int statusCode)
        //{
        //    var iStatusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
        //    return View("NotFound", iStatusCodeReExecuteFeature.OriginalPath);
        //}
    }
}
