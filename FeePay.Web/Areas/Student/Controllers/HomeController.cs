using FeePay.Core.Application.Interface.Service.Student;
using FeePay.Web.Areas.Common;
using FeePay.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static FeePay.Core.Application.Enums.Notification;

namespace FeePay.Web.Areas.Student.Controllers
{
    [Area("Student")]
    [StudentAuthorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : AreaBaseController
    {
        public HomeController(ILogger<HomeController> logger,
            IStudentManagementService studentManagementService)
        {
            _logger = logger;
            _studentManagementService = studentManagementService;
        }
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentManagementService _studentManagementService;

        [HttpGet]
        [DisplayName("Profile")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Profile";
            try
            {
                var res = await _studentManagementService.StudentProfileAsync();
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.warning, "Warning", $"Not Found");
                _logger.LogError($"Error when getting Student profile", res.Message);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"Not Found");
                _logger.LogError(ex, $"Error when getting Student profile.");
            }
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetUserPassword()
        {
            try
            {
                var res = await _studentManagementService.GetStudentPassword(0);
                if (res.Succeeded)
                {
                    return Json(new { success = true, data = res.Data });
                }
                _logger.LogWarning("Error getting username password");
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting username password");
            }
            return Json(new { success = false, message = "no data found" });
        }
    }
}
