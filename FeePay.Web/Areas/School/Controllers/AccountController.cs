using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Web.Areas.Common;
using FeePay.Web.Filters;
using static FeePay.Core.Application.Enums.Notification;
using FeePay.Web.Services.Interfaces;
using System.ComponentModel;

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [SchoolAdminAuthorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AccountController : AreaBaseController
    {
        public AccountController(ILogger<AccountController> logger, ILoginService loginService,
            IAdministrationService administrationService, IMvcControllerDiscovery mvcControllerDiscovery)
        {
            _logger = logger;
            _loginService = loginService;
            _administrationService = administrationService;
            _mvcControllerDiscovery = mvcControllerDiscovery;
        }
        private readonly ILogger _logger;
        private readonly ILoginService _loginService;
        private readonly IAdministrationService _administrationService;
        private readonly IMvcControllerDiscovery _mvcControllerDiscovery;


        #region Staff Section

        [HttpGet]
        [MvcDiscovery]
        [Route("School/Users")]
        [DisplayName("User List")]
        public async Task<IActionResult> StaffList()
        {
            ViewData["Title"] = "Staff";
            var result = await _administrationService.GetAllStaffMemberAsync();
            return View(result.Data);
        }

        [HttpGet]
        [MvcDiscovery]
        [Route("School/User/Manage/{id?}")]
        [DisplayName("Add Or Update User")]
        public async Task<IActionResult> StaffManage(int? id)
        {
            if (id == null || id == 0)
            {
                ViewData["Title"] = "Create Staff Member";
                return View(await _administrationService.BindStaffMemberViewModel());
            }
            ViewData["Title"] = "Update Staff Member";
            var result = await _administrationService.GetStaffByIdAsync(id ?? 0);
            return View(result.Data);
        }

        [HttpPost]
        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [Route("School/User/Manage/{id?}")]
        [DisplayName("Add Or Update User")]
        public async Task<IActionResult> StaffManage(StaffMemberViewModel model, int? id)
        {
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "Error", $"Error validating form please fill all required field with valid data.");
                return View(model);
            }
            var result = await _administrationService.AddOrEditStaffMemberAsync(model);
            if (result.Succeeded)
            {
                AlertMessage(NotificationType.success, $"Staff Member Successfully {(model.Id == 0 ? "added" : "updated")}.", string.Empty);
                _logger.LogInformation($"Staff Member Successfully {(model.Id == 0 ? "added" : "updated")} by user.");
                return RedirectToAction(nameof(StaffList));
            }
            else
            {
                AlertMessage(NotificationType.error, $"Error {(model.Id == 0 ? "adding" : "updating")} staff member please try again.", string.Empty);
                _logger.LogError($"Error {(model.Id == 0 ? "adding" : "updating")} staff member.", result.Errors);
                if (result.Errors != null) foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error);
                return View(model);
            }
        }

        [HttpDelete]
        [MvcDiscovery]
        [Route("School/User/Delete/{id}")]
        [DisplayName("Delete User")]
        public async Task<JsonResult> StaffDelete(int id)
        {
            var res = await _administrationService.DeleteStaffMemberAsync(id);
            return Json(new { success = res.Succeeded, message = res.Message });
        }

        #endregion

        #region Role Section

        [HttpGet]
        [MvcDiscovery]
        [Route("School/Roles")]
        [DisplayName("Roles")]
        public async Task<IActionResult> RoleList()
        {
            ViewData["Title"] = "Roles";
            var result = await _administrationService.GetAllStaffRolesAsync();
            return View(result.Data);
        }

        [HttpGet]
        [MvcDiscovery]
        [Route("School/Role/Manage/{id?}")]
        [DisplayName("Add Or Update Role")]
        public async Task<IActionResult> RoleManage(int? id)
        {
            if (id == null || id == 0)
            {
                ViewData["Title"] = "Create Role";
                ViewData["Controllers"] = _mvcControllerDiscovery.GetSchoolControllers();
                return View(await _administrationService.BindRoleViewModel());
            }
            ViewData["Title"] = "Update Role";
            var result = await _administrationService.GetStaffRoleByIdAsync(id ?? 0);
            var role = result.Data;
            ViewData["Controllers"] = _mvcControllerDiscovery.GetSchoolControllers(role.Access);
            return View(role);
        }

        [HttpPost]
        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [Route("School/Role/Manage/{id?}")]
        [DisplayName("Add Or Update Role")]
        public async Task<IActionResult> RoleManage(RoleViewModel model, int? id)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _administrationService.AddOrEditStaffRoleAsync(model);
            if (result.Succeeded)
            {
                AlertMessage(NotificationType.success, $"Role Successfully {(model.Id == 0 ? "added" : "updated")}.", "");
                _logger.LogInformation($"Role Successfully {(model.Id == 0 ? "added" : "updated")} by user.");
                return RedirectToAction(nameof(RoleList));
            }
            else
            {
                AlertMessage(NotificationType.error, $"Error {(model.Id == 0 ? "adding" : "updating")} role please try again.", "");
                _logger.LogError($"Error {(model.Id == 0 ? "adding" : "updating")} role.", result.Errors);
                return View(model);
            }
        }

        [HttpDelete]
        [MvcDiscovery]
        [Route("School/Role/Delete/{id}")]
        [DisplayName("Delete Role")]
        public async Task<JsonResult> RoleDelete(int id)
        {
            var res = await _administrationService.DeleteStaffRoleAsync(id);
            return Json(new { success = res.Succeeded, message = res.Message });
        }

        #endregion

        [HttpGet]
        [MvcDiscovery]
        [Route("School/User/Password/{id}")]
        [DisplayName("Show Staff Password")]
        public async Task<JsonResult> StaffPassword(int id)
        {
            var userId = _loginService.GetLogedInSchoolAdminId();
            try
            {
                var res = await _administrationService.GetStaffMemberPassword(id);
                if (res.Succeeded)
                {
                    return Json(new { success = true, data = res.Data });
                }
                _logger.LogWarning("Error getting Staff Member password for id = {0}", userId);
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Staff Member password for id = {0}", userId);
            }
            return Json(new { success = false, message = "no data found" });
        }

    }
}
