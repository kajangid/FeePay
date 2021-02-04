using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class AccountController : AreaBaseController
    {
        public AccountController(ILogger<AccountController> logger,
            ILoginService loginService,
            IAdministrativeServices administrativeServices)
        {
            _logger = logger;
            _loginService = loginService;
            _administrativeServices = administrativeServices;
        }
        private readonly ILogger<AccountController> _logger;
        private readonly ILoginService _loginService;
        private readonly IAdministrativeServices _administrativeServices;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet("{Area}/User/List")]
        public async Task<IActionResult> UserList()
        {
            ViewData["Title"] = "List";
            try
            {
                var list = await _administrativeServices.GetUserListAsync();
                if (list.Succeeded) return View(list.Data);
                AlertMessage(NotificationType.error, "Error", list.Message);
                _logger.LogWarning("Error when getting superadminuser list data. Error:{0}", list.Message);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "error when getting Students list data.");
                _logger.LogError(ex, "Error when getting superadminuser list data. Error:{0}", ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet("{Area}/User/Add")]
        public IActionResult UserAdd()
        {
            ViewData["Title"] = "Add";
            return View();
        }
        [HttpPost("{Area}/User/Add")]
        public async Task<IActionResult> UserAdd(SuperAdmin_UserViewModel model)
        {
            ViewData["Title"] = "Add";
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "Error", "Please fill all required fields and save again.");
                _logger.LogWarning($"Model State Error..... ActoinName = {nameof(UserAdd)}, Error: {string.Join(", ", GetErrorListFromModelState(ModelState).ToArray())}");
                return View(model);
            }
            try
            {
                var res = await _administrativeServices.AddOrEditUserAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"User Successfully Added.");
                    return RedirectToAction(nameof(UserList));
                }
                else AlertMessage(NotificationType.warning, "", res.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when creating Error:{0}", ex.Message);
                AlertMessage(NotificationType.error, "Error", "Error Adding User.");
            }
            return View(model);
        }
        [HttpGet("{Area}/User/Edit/{id:int}")]
        public async Task<IActionResult> UserEdit(int id)
        {
            ViewData["Title"] = "Edit";
            try
            {
                var res = await _administrativeServices.GetUserByIdAsync(id);
                if (res.Succeeded) return View(res.Data);
                else AlertMessage(NotificationType.warning, "", res.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when getting user data Error:{0}", ex.Message);
                AlertMessage(NotificationType.error, "Error", "Error Getting User.");
            }
            return RedirectToAction(nameof(UserList));
        }
        [HttpPost("{Area}/User/Edit/{id:int}")]
        public async Task<IActionResult> UserEdit(SuperAdmin_UserViewModel model, int id)
        {
            ViewData["Title"] = "Edit";
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "Error", "Please fill all required fields and save again.");
                _logger.LogWarning($"Model State Error..... ActoinName = {nameof(UserEdit)}, Error: {string.Join(", ", GetErrorListFromModelState(ModelState).ToArray())}");
                return View(model);
            }
            try
            {
                var res = await _administrativeServices.AddOrEditUserAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"User Successfully Edited.");
                    return RedirectToAction(nameof(UserList));
                }
                else AlertMessage(NotificationType.warning, "", res.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when updating Error:{0}", ex.Message);
                AlertMessage(NotificationType.error, "Error", "Error Editing User.");
            }
            return View(model);
        }
        [HttpDelete("{Area}/User/Delete/{id:int}")]
        public async Task<JsonResult> UserDelete(int id)
        {
            try
            {
                var res = await _administrativeServices.DeleteUserAsync(id);
                if (res.Succeeded) return Json(new { success = true });
                _logger.LogError("Error Deleting User Data Error:{0}", res.Message);
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Deleting User Data");
                return Json(new { success = false, message = "Error Deleting User Data." });
            }
        }
        [HttpGet("{Area}/User/Credentials/{id:int}")]
        public async Task<JsonResult> UserCredentials(int id)
        {
            try
            {
                var res = await _administrativeServices.GetUserCredetianlAsync(id);
                if (res.Succeeded) return Json(new { success = true, data = res.Data });
                _logger.LogError("Error Getting User Credential Error:{0}", res.Message);
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting User Credential.");
                return Json(new { success = false, message = "Error Getting User Credential." });
            }
        }
        [HttpGet("{Area}/User/Active/{active:bool}/{id:int}")]
        public async Task<JsonResult> UserActive(int id, bool active)
        {
            string type = (active ? "Activating" : "Inactivating");
            try
            {
                var res = await _administrativeServices.ActiveUserAsync(id, active);
                if (res.Succeeded) return Json(new { success = true });
                _logger.LogError("Error {1} User Data Error:{0}", res.Message, type);
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error {0} User Data", type);
                return Json(new { success = false, message = "Error Activating User Data." });
            }
        }
        [HttpPost("{Area}/User/ChangeCredentials/{id:int}")]
        public async Task<JsonResult> ChnageCredentials(ResetPasswordViewModel model, int id)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = $"Error: {string.Join(", ", GetErrorListFromModelState(ModelState).ToArray())}" });

            try
            {
                var res = await _administrativeServices.ChangeUserCredetianls_AdminAscync(model, id);
                if (res.Succeeded) return Json(new { success = true });
                _logger.LogError("Error Changing Credentials User Data Error:{0}", res.Message);
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Changing Credentials User Data");
                return Json(new { success = false, message = "Error Activating User Data." });
            }
        }
        [HttpGet("{Area}/User/Profile/{id:int}")]
        public async Task<IActionResult> UserProfile(int id)
        {
            ViewData["Title"] = "User Profile";
            try
            {
                var res = await _administrativeServices.GetUserByIdAsync(id);
                if (res.Succeeded) return View(res.Data);
                else AlertMessage(NotificationType.warning, "", res.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when getting user profile Error:{0}", ex.Message);
                AlertMessage(NotificationType.error, "Error", "Error Getting User Profile.");
            }
            return RedirectToAction(nameof(UserList));
        }


    }
}
