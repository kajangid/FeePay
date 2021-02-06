using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FeePay.Web.Areas.Common;
using FeePay.Web.Filters;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Service.Student;
using static FeePay.Core.Application.Enums.Notification;
using Microsoft.AspNetCore.Authorization;
using FeePay.Core.Application.Interface.Service;

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [Route("School/Student")]
    [SchoolAdminAuthorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class StudentAdmissionController : AreaBaseController
    {
        private readonly ILogger<StudentAdmissionController> _logger;
        private readonly IStudentManagementService _studentManagementService;
        private readonly ILoginService _loginService;
        public StudentAdmissionController(ILogger<StudentAdmissionController> logger, ILoginService loginService,
            IStudentManagementService studentManagementService)
        {
            _logger = logger;
            _studentManagementService = studentManagementService;
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Student Admission
        [MvcDiscovery]
        [HttpGet("List")]
        [DisplayName("List Students")]
        public async Task<IActionResult> StudentList()
        {
            try
            {
                ViewData["Title"] = "Students";
                var list = await _studentManagementService.GetListOfStudentsAsync();
                return View(list.Data);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "error when getting Students list data.");
                _logger.LogError(ex, "error when getting Students list data.");
                return RedirectToAction("Index", "Home");
            }
        }

        [MvcDiscovery]
        [HttpGet("Add")]
        [DisplayName("Add Student")]
        public async Task<IActionResult> StudentAdd()
        {
            ViewData["Title"] = "New Admission";
            return View(await _studentManagementService.BindStudentAdmissionViewModelAsync());
        }

        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [HttpPost("Add")]
        [DisplayName("Add Student")]
        public async Task<IActionResult> StudentAdd(StudentAdmissionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "Error", "Please fill all required fields and save again.");
                _logger.LogWarning($"Model State Error..... ActoinName = {nameof(StudentAdd)}, Error: {string.Join(", ", GetErrorListFromModelState(ModelState).ToArray())}");
                return View(await _studentManagementService.BindStudentAdmissionViewModelAsync(model));
            }
            try
            {
                var res = await _studentManagementService.AddOrEditStudentAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"Student Admission data is done creating");
                    return RedirectToAction(nameof(StudentList));
                }
                else
                {
                    AlertMessage(NotificationType.warning, "", "Student Admission data is already present with same form number.");
                    return View(await _studentManagementService.BindStudentAdmissionViewModelAsync(model));
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"error when creating");
                _logger.LogError(ex, $"error when creating");
                return View(await _studentManagementService.BindStudentAdmissionViewModelAsync(model));
            }
        }

        [MvcDiscovery]
        [HttpGet("Edit/{id:int}")]
        [DisplayName("Edit Student")]
        public async Task<IActionResult> StudentEdit(int id)
        {
            if (id == 0) return RedirectToAction(nameof(StudentList));
            ViewData["Title"] = "Update Student";
            try
            {
                var res = await _studentManagementService.FindStudentByIdAsync(id);
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.warning, "Warning", $"There is no student data available for id = {id}");
                return RedirectToAction(nameof(StudentList));
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"error when getting Student data for id = {id}");
                _logger.LogError(ex, $"error when getting Student data for id = {id}");
                return RedirectToAction(nameof(StudentList));
            }
        }

        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [HttpPost("Edit/{id:int}")]
        [DisplayName("Edit Student")]
        public async Task<IActionResult> StudentEdit(StudentAdmissionViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "Error", "Please fill all required fields and save again.");
                _logger.LogWarning($"Model State Error..... ActoinName = {nameof(StudentEdit)}, Error: {string.Join(", ", GetErrorListFromModelState(ModelState).ToArray())}");
                return View(await _studentManagementService.BindStudentAdmissionViewModelAsync(model));
            }
            try
            {
                var res = await _studentManagementService.AddOrEditStudentAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"Student Admission data is done updating");
                    return RedirectToAction(nameof(StudentList));
                }
                else
                {
                    AlertMessage(NotificationType.warning, "", "Student Admission data is already present with same form number.");
                    return View(await _studentManagementService.BindStudentAdmissionViewModelAsync(model));
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"error when updating Student Admission data for id = {id}");
                _logger.LogError(ex, $"error when updating Student Admission data for id = {id}");
                return View(await _studentManagementService.BindStudentAdmissionViewModelAsync(model));
            }
        }

        [MvcDiscovery]
        [HttpDelete("Delete/{id:int}")]
        [DisplayName("Delete Student")]
        public IActionResult StudentDelete(int id)
        {
            return View();
        }

        [MvcDiscovery]
        [HttpGet("Ledger/{id:int}")]
        [DisplayName("Student Profile")]
        public async Task<IActionResult> StudentProfile(int id)
        {
            if (id == 0) return RedirectToAction(nameof(StudentList));
            ViewData["Title"] = "Student Profile";
            try
            {
                var res = await _studentManagementService.StudentLedgerAsync(id);
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.warning, "Warning", $"There is no student data available for id = {id}");
                return RedirectToAction(nameof(StudentList));
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"error when getting Student data for id = {id}");
                _logger.LogError(ex, $"error when getting Student data for id = {id}");
                return RedirectToAction(nameof(StudentList));
            }
        }

        [MvcDiscovery]
        [DisplayName("Profile GetPassword")]
        [HttpGet("Password/{id:int}")]
        public async Task<JsonResult> GetUserPassword(int id)
        {
            try
            {
                var res = await _studentManagementService.GetStudentPassword(id);
                if (res.Succeeded)
                {
                    return Json(new { success = true, data = res.Data });
                }
                _logger.LogWarning("Error editing user profile for id = {0}", id);
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing user profile for id = {0}", id);
            }
            return Json(new { success = false, message = "no data found" });
        }


        #endregion

        [HttpGet("GetClassSection/{id:int}")]
        public async Task<JsonResult> GetClassSection(int id)
        {
            try
            {
                var res = await _studentManagementService.ClassSectionsAsync(id);
                return Json(new { success = res.Succeeded, data = res.Data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error when getting StudentAdmission class section drop down list data for class id = {id}");
                return Json(new { success = false, message = "Oh, Snap! Something went wrong." });
            }
        }
        [HttpGet("GetStateCities/{id:int}")]
        public async Task<JsonResult> GetStateCities(int id)
        {
            try
            {
                var res = await _studentManagementService.StateCitiesAsync(id);
                return Json(new { success = res.Succeeded, data = res.Data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error when getting cities of state where state id is = {id}");
                return Json(new { success = false, message = "Oh, Snap! Something went wrong." });
            }
        }
    }
}
