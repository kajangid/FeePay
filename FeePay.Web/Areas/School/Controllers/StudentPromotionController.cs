using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.Student;
using FeePay.Web.Filters;
using FeePay.Web.Areas.Common;
using System.ComponentModel;
using FeePay.Core.Application.DTOs;
using static FeePay.Core.Application.Enums.Notification;

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [SchoolAdminAuthorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class StudentPromotionController : AreaBaseController
    {
        private readonly ILogger<StudentPromotionController> _logger;
        private readonly IStudentManagementService _studentManagementService;
        private readonly IAppContextAccessor _appContextAccessor;
        public StudentPromotionController(
            ILogger<StudentPromotionController> logger,
            IStudentManagementService studentManagementService,
            IAppContextAccessor appContextAccessor)
        {
            _logger = logger;
            _studentManagementService = studentManagementService;
            _appContextAccessor = appContextAccessor;
        }


        [HttpGet]
        [MvcDiscovery]
        [DisplayName("Search Student")]
        [Route("{Area}/Student/Promotion")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Student Promotion";
            try
            {
                var res = await _studentManagementService.StudentPromotion_SearchStudentAndBindModel();
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.error, "Error", res.Message);
                _logger.LogError("error when initializing data for Student Promotion. Error ={0}", res.Message);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "error when initializing data for Student Promotion.");
                _logger.LogError(ex, "error when initializing data for Student Promotion.");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [MvcDiscovery]
        [DisplayName("Search Student")]
        [Route("{Area}/Student/Promotion")]
        public async Task<IActionResult> Index(StudentPromotionViewModel model)
        {
            ViewData["Title"] = "Student Promotion";
            try
            {
                var res = await _studentManagementService.StudentPromotion_SearchStudentAndBindModel(model);
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.error, "Error", res.Message);
                _logger.LogError("error when getting students list for Promotion. Error ={0}", res.Message);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "error when getting students list for Promotion.");
                _logger.LogError(ex, "error when getting students list for Promotion.");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [MvcDiscovery]
        [DisplayName("Search Student")]
        [Route("{Area}/Student/Promote")]
        public async Task<JsonResult> Promote(StudentPromotionViewModel model)
        {
            ViewData["Title"] = "Student Promotion";
            try
            {
                var res = await _studentManagementService.StudentPromotion_Promote(model);
                if (res.Succeeded) return Json(new { success = true });
                _logger.LogError("error when Promoting students. Error ={0}", res.Message);
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error when Promoting students .");
                return Json(new { success = false, message = "error when Promoting students." });
            }
        }
    }
}
