using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Web.Areas.Common;
using FeePay.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static FeePay.Core.Application.Enums.Notification;

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [SchoolAdminAuthorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : AreaBaseController
    {
        private readonly ILogger _logger;
        private readonly IAdministrationService _administrationService;
        private readonly ILoginService _loginService;
        private readonly ISchoolAdminRegistrationService _schoolAdminRegistrationService;
        public HomeController(
            ILogger<HomeController> logger,
            IAdministrationService administrationService,
            ILoginService loginService,
            ISchoolAdminRegistrationService schoolAdminRegistrationService)
        {
            _logger = logger;
            _administrationService = administrationService;
            _loginService = loginService;
            _schoolAdminRegistrationService = schoolAdminRegistrationService;
        }

        [HttpGet]
        [MvcDiscovery]
        [Route("School/Dashboard")]
        [DisplayName("Dashboard")]
        public IActionResult Index()
        {
            return View();
        }





        #region USER PROFILE
        [HttpGet]
        [Route("School/Profile")]
        [DisplayName("User Profile")]
        [MvcDiscovery]
        public async Task<IActionResult> UserProfile()
        {
            ViewData["Title"] = "User Profile";
            ViewData["BreadCrumb_Title"] = "Profile";
            try
            {
                var res = await _administrationService.GetUserProfileData();
                if (res.Succeeded)
                {
                    return View(res.Data);
                }
                _logger.LogWarning("Error getting user profile data for id = {0}", _loginService.GetLogedInSchoolAdminId());
                AlertMessage(NotificationType.warning, "Profile Data Not found", "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user profile data for id = {0}", _loginService.GetLogedInSchoolAdminId());
                AlertMessage(NotificationType.error, "An error is accord please try again in some time", "");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("School/Profile/Edit")]
        [DisplayName("Edit Profile")]
        [MvcDiscovery]
        public async Task<IActionResult> EditProfile()
        {
            ViewData["Title"] = "Edit Profile";
            ViewData["BreadCrumb_Title"] = "Edit";
            var userId = _loginService.GetLogedInSchoolAdminId();
            try
            {
                var res = await _administrationService.GetUserProfileData();
                if (res.Succeeded)
                {
                    return View(res.Data);
                }
                _logger.LogWarning("Error getting user profile data for id = {0}", userId);
                AlertMessage(NotificationType.warning, "Profile Data Not found", "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user profile data for id = {0}", userId);
                AlertMessage(NotificationType.error, "An error is accord please try again in some time.", "");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [MvcDiscovery]
        [AutoValidateAntiforgeryToken]
        [Route("School/Profile/Edit")]
        [DisplayName("Edit Profile")]
        public async Task<IActionResult> EditProfile(UserProfileViewModel model)
        {
            ViewData["Title"] = "Edit Profile";
            ViewData["BreadCrumb_Title"] = "Edit";
            var userId = _loginService.GetLogedInSchoolAdminId();
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.warning, "", "Please fill all required field.");
                _logger.LogWarning("Error Model Validation user profile for id = {0}", userId);
                return View(model);
            }
            try
            {
                var res = await _administrationService.EditProfile(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", "Profile updated");
                    return RedirectToAction(nameof(EditProfile));
                }
                _logger.LogWarning(res.Message + " :: Error editing user profile for id = {0}", userId);
                AlertMessage(NotificationType.warning, res.Message, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing user profile for id = {0}", userId);
                AlertMessage(NotificationType.error, "An error is accord please try again in some time", "");
            }
            return RedirectToAction(nameof(UserProfile));
        }

        [HttpPost]
        [MvcDiscovery]
        [AutoValidateAntiforgeryToken]
        [Route("School/Profile/ChangePassword")]
        [DisplayName("Reset Password")]
        public async Task<JsonResult> ResetPassword(ResetPasswordViewModel model)
        {
            var userId = _loginService.GetLogedInSchoolAdminId();
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = GetErrorListFromModelState(ModelState).ToList() });
            }
            try
            {
                var res = await _administrationService.ChangeUserPassword(model);
                if (res.Succeeded)
                {
                    return Json(new { success = true, data = res.Data });
                }
                _logger.LogWarning("Error Changing Password for id = {0}", userId);
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Changing Password for id = {0}", userId);
            }
            return Json(new { success = false, message = "no data found" });
        }

        [HttpGet]
        [MvcDiscovery]
        [DisplayName("Profile GetPassword")]
        [Route("School/Profile/Password")]
        public async Task<JsonResult> GetUserPassword()
        {
            var userId = _loginService.GetLogedInSchoolAdminId();
            try
            {
                var res = await _administrationService.GetUserPassword();
                if (res.Succeeded)
                {
                    return Json(new { success = true, data = res.Data });
                }
                _logger.LogWarning("Error editing user profile for id = {0}", userId);
                return Json(new { success = false, message = res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing user profile for id = {0}", userId);
            }
            return Json(new { success = false, message = "no data found" });
        }
        #endregion
    }
}
