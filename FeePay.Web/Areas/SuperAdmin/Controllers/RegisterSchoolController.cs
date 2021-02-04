using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FeePay.Web.Filters;
using FeePay.Web.Areas.Common;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service.SuperAdmin;
using static FeePay.Core.Application.Enums.Notification;

namespace FeePay.Web.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [SuperAdminAuthorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class RegisterSchoolController : AreaBaseController
    {
        public RegisterSchoolController(ILogger<RegisterSchoolController> logger,
            ILoginService loginService,
            ISchoolsManagerServices schoolsManagerServices)
        {
            _logger = logger;
            _loginService = loginService;
            _schoolsManagerServices = schoolsManagerServices;
        }
        private readonly ILogger<RegisterSchoolController> _logger;
        private readonly ISchoolsManagerServices _schoolsManagerServices;
        private readonly ILoginService _loginService;


        [HttpGet]
        public async Task<IActionResult> List()
        {
            ViewData["Title"] = "List";
            try
            {
                var res = await _schoolsManagerServices.GetRegisterSchoolList();
                return View(res.Data);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "", "Error Getting Registered School List.");
                _logger.LogError(ex, "Error Getting RegisterSchoolList.");
                return View();
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Title"] = "Add";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RegisterSchoolViewModel model)
        {
            ViewData["Title"] = "Add";
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "", "Error Adding Registered School Data.");
                return View(model);
            }
            try
            {
                var res = await _schoolsManagerServices.AddOrEditRegisterSchool(model);
                if (res.Succeeded) return RedirectToAction(nameof(List));
                AlertMessage(NotificationType.warning, "", res.Message);
                _logger.LogError("Error Adding RegisterSchool Data Error:{0}", res.Message);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "", "Error Adding Registered School Data.");
                _logger.LogError(ex, "Error Adding RegisterSchool Data");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit";
            try
            {
                var res = await _schoolsManagerServices.GetRegisterSchoolById(id);
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.warning, "", res.Message);
                _logger.LogError("Error Getting RegisterSchool Data Error:{0}", res.Message);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "", "Error Getting Registered School Data.");
                _logger.LogError(ex, "Error Getting RegisterSchool Data");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegisterSchoolViewModel model)
        {
            ViewData["Title"] = "Edit";
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "", "Error Editing Registered School Data.");
                return View(model);
            }
            try
            {
                var res = await _schoolsManagerServices.AddOrEditRegisterSchool(model);
                if (res.Succeeded) return RedirectToAction(nameof(List)); 
                AlertMessage(NotificationType.warning, "", res.Message);
                _logger.LogError("Error Editing RegisterSchool Data Error:{0}", res.Message);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "", "Error Editing Registered School Data.");
                _logger.LogError(ex, "Error Editing RegisterSchool Data");
            }
            return View();
        }
        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                var res = await _schoolsManagerServices.DeleteRegisterSchool(id);
                if (res.Succeeded) return Json(new { success = true });
                _logger.LogError("Error Deleting RegisterSchool Data Error:{0}", res.Message);
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Deleting RegisterSchool Data");
                return Json(new { success = false, message = "Error Deleting Registered School Data." });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Approve(int id, bool isApprove)
        {
            try
            {
                var res = await _schoolsManagerServices.DeleteRegisterSchool(id);
                if (res.Succeeded) return Json(new { success = true });
                _logger.LogError("Error Approving RegisterSchool Error:{0}", res.Message);
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Approving RegisterSchool.");
                return Json(new { success = false, message = "Error Approving Registered School." });
            }
        }




    }
}
