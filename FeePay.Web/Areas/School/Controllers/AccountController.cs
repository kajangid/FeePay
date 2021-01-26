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
            IAdministrationService AdministrationService, IMvcControllerDiscovery mvcControllerDiscovery)
        {
            _ILogger = logger;
            _LoginService = loginService;
            _AdministrationService = AdministrationService;
            _mvcControllerDiscovery = mvcControllerDiscovery;
        }
        private readonly ILogger _ILogger;
        private readonly ILoginService _LoginService;
        private readonly IAdministrationService _AdministrationService;
        private readonly IMvcControllerDiscovery _mvcControllerDiscovery;


        #region Staff Section

        [HttpGet]
        [Route("School/Users")]
        [DisplayName("User List")]
        public async Task<IActionResult> StaffList()
        {
            ViewData["Title"] = "Staff";
            var result = await _AdministrationService.GetAllStaffMemberAsync();
            return View(result.Data);
        }

        [Route("School/User/Manage/{id?}")]
        [DisplayName("Add Or Update User")]
        public async Task<IActionResult> StaffManage(int? id)
        {
            if (id == null || id == 0)
            {
                ViewData["Title"] = "Create Staff Member";
                return View(await _AdministrationService.BindStaffMemberViewModel());
            }
            ViewData["Title"] = "Update Staff Member";
            var result = await _AdministrationService.GetStaffByIdAsync(id ?? 0);
            return View(result.Data);
        }

        [HttpPost]
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
            var result = await _AdministrationService.AddOrEditStaffMemberAsync(model);
            if (result.Succeeded)
            {
                AlertMessage(NotificationType.success, $"Staff Member Successfully {(model.Id == 0 ? "added" : "updated")}.", string.Empty);
                _ILogger.LogInformation($"Staff Member Successfully {(model.Id == 0 ? "added" : "updated")} by user.");
                return RedirectToAction(nameof(StaffList));
            }
            else
            {
                AlertMessage(NotificationType.error, $"Error {(model.Id == 0 ? "adding" : "updating")} staff member please try again.", string.Empty);
                _ILogger.LogError($"Error {(model.Id == 0 ? "adding" : "updating")} staff member.", result.Errors);
                if (result.Errors != null) foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error);
                return View(model);
            }
        }

        [HttpDelete]
        [Route("School/User/Delete/{id}")]
        [DisplayName("Delete User")]
        public async Task<JsonResult> StaffDelete(int id)
        {
            var res = await _AdministrationService.deleteStaffMemberAsync(id);
            return Json(new { success = res.Succeeded, message = res.Message });
        }

        #endregion

        #region Role Section

        [HttpGet]
        [Route("School/Roles")]
        [DisplayName("Roles")]
        public async Task<IActionResult> RoleList()
        {
            ViewData["Title"] = "Roles";
            var result = await _AdministrationService.GetAllStaffRolesAsync();
            return View(result.Data);
        }

        [HttpGet]
        [Route("School/Role/Manage/{id?}")]
        [DisplayName("Add Or Update Role")]
        public async Task<IActionResult> RoleManage(int? id)
        {
            if (id == null || id == 0)
            {
                ViewData["Title"] = "Create Role";
                ViewData["Controllers"] = _mvcControllerDiscovery.GetSchoolControllers();
                return View(await _AdministrationService.BindRoleViewModel());
            }
            ViewData["Title"] = "Update Role";
            var result = await _AdministrationService.GetStaffRoleByIdAsync(id ?? 0);
            var role = result.Data;
            ViewData["Controllers"] = _mvcControllerDiscovery.GetSchoolControllers(role.Access);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("School/Role/Manage/{id?}")]
        [DisplayName("Add Or Update Role")]
        public async Task<IActionResult> RoleManage(RoleViewModel model, int? id)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _AdministrationService.AddOrEditStaffRoleAsync(model);
            if (result.Succeeded)
            {
                AlertMessage(NotificationType.success, $"Role Successfully {(model.Id == 0 ? "added" : "updated")}.", "");
                _ILogger.LogInformation($"Role Successfully {(model.Id == 0 ? "added" : "updated")} by user.");
                return RedirectToAction(nameof(RoleList));
            }
            else
            {
                AlertMessage(NotificationType.error, $"Error {(model.Id == 0 ? "adding" : "updating")} role please try again.", "");
                _ILogger.LogError($"Error {(model.Id == 0 ? "adding" : "updating")} role.", result.Errors);
                return View(model);
            }
        }

        [HttpDelete]
        [Route("School/Role/Delete/{id}")]
        [DisplayName("Delete Role")]
        public async Task<JsonResult> RoleDelete(int id)
        {
            var res = await _AdministrationService.deleteStaffRoleAsync(id);
            return Json(new { success = res.Succeeded, message = res.Message });
        }

        #endregion

    }
}
