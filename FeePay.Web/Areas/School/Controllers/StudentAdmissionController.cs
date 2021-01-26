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

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [SchoolAdminAuthorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class StudentAdmissionController : AreaBaseController
    {
        private readonly ILogger<StudentAdmissionController> _logger;
        private readonly IStudentManagementService _studentManagementService;
        public StudentAdmissionController(ILogger<StudentAdmissionController> logger,
            IStudentManagementService studentManagementService)
        {
            _logger = logger;
            _studentManagementService = studentManagementService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Student Admission
        [HttpGet]
        [Route("School/Students")]
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

        [HttpGet]
        [Route("School/Student/Add")]
        [DisplayName("Add Student")]
        public async Task<IActionResult> StudentAdd()
        {
            ViewData["Title"] = "New Admission";
            return View(await _studentManagementService.BindStudentAdmissionViewModelAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("School/Student/Add")]
        [DisplayName("Add Student")]
        public async Task<IActionResult> StudentAdd(StudentAdmissionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "Error", "Please fill all required fields and save again.");
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

        [HttpGet]
        [Route("School/Student/Edit/{id}")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("School/Student/Edit/{id}")]
        [DisplayName("Edit Student")]
        public async Task<IActionResult> StudentEdit(StudentAdmissionViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "Error", "Please fill all required fields and save again.");
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

        [HttpDelete]
        [Route("School/Student/Delete")]
        [DisplayName("Delete Student")]
        public IActionResult StudentDelete()
        {
            return View();
        }

        [HttpGet]
        [Route("School/Student/Ledger/{id}")]
        [DisplayName("Student Ledger")]
        public async Task<IActionResult> StudentProfile(int id)
        {
            if (id == 0) return RedirectToAction(nameof(StudentList));
            ViewData["Title"] = "Student Ledger";
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

        
        #endregion

        [HttpGet]
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
        [HttpGet]
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
