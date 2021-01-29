using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeePay.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FeePay.Web.Areas.Common;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Core.Application.DTOs;
using static FeePay.Core.Application.Enums.Notification;

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [SchoolAdminAuthorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AcademicController : AreaBaseController
    {
        public AcademicController(ILogger<AcademicController> logger, IAcademicServices academicServices)
        {
            _logger = logger;
            _academicServices = academicServices;
        }
        private readonly ILogger _logger;
        private readonly IAcademicServices _academicServices;



        #region Classes
        [HttpGet]
        [MvcDiscovery]
        [Route("School/Academics/Classes")]
        [DisplayName("List Classes")]
        public async Task<IActionResult> ClassList()
        {
            try
            {
                ViewData["Title"] = "Classes";
                var list = await _academicServices.GetListOfClassesAsync();
                return View(list.Data);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "error when getting Classes list data.");
                _logger.LogError(ex, "error when getting Classes list data.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [MvcDiscovery]
        [Route("School/Academics/Classes/Manage/{id?}")]
        [DisplayName("Manage Classes")]
        public async Task<IActionResult> ClassManage(int? id)
        {
            if (id != null && id != 0)
            {
                ViewData["Title"] = "Update Class";
                try
                {
                    var res = await _academicServices.FindClassByIdAsync(id ?? 0);
                    if (res.Succeeded) return View(res.Data);
                    AlertMessage(NotificationType.warning, "Warning", $"There is no data available for id = {id}");
                    return RedirectToAction(nameof(ClassList));
                }
                catch (Exception ex)
                {
                    AlertMessage(NotificationType.error, "Error", $"error when getting Class data for id = {id}");
                    _logger.LogError(ex, $"error when getting Class data for id = {id}");
                    return RedirectToAction(nameof(ClassList));
                }
            }
            else
            {
                ViewData["Title"] = "Create Class";
                return View(await _academicServices.BindClassViewModelAsync());
            }
        }

        [HttpPost]
        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [Route("School/Academics/Classes/Manage/{id?}")]
        [DisplayName("Manage Classes")]
        public async Task<IActionResult> ClassManage(ClassViewModel model, int? id)
        {
            var IdPres = (model.Id == 0);
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "Error", "Please fill all required fields and save again.");
                return View(await _academicServices.BindClassViewModelAsync(model));
            }
            try
            {
                var res = await _academicServices.AddOrEditClassAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"Class is done {(IdPres ? "creating" : "updating")}");
                    return RedirectToAction(nameof(ClassList));
                }
                else
                {
                    AlertMessage(NotificationType.warning, "", "Class is already present with same name.");
                    return View(await _academicServices.BindClassViewModelAsync(model));
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"error when {(IdPres ? "creating" : "updating")} class for id = {id}");
                _logger.LogError(ex, $"error when {(IdPres ? "creating" : "updating")} class for id = {id}");
                return View(await _academicServices.BindClassViewModelAsync(model));
            }
        }

        [HttpDelete]
        [MvcDiscovery]
        [Route("School/Academics/Classes/Delete")]
        [DisplayName("Delete Class")]
        public IActionResult ClassDelete()
        {
            return View();
        }
        #endregion

        #region Sections
        [HttpGet]
        [MvcDiscovery]
        [DisplayName("List Sections")]
        [Route("School/Academics/Sections")]
        public async Task<IActionResult> SectionList()
        {
            try
            {
                ViewData["Title"] = "Sections";
                var list = await _academicServices.GetListOfSectionsAsync();
                return View(list.Data);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "error when getting section data.");
                _logger.LogError(ex, "error when getting section data.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [MvcDiscovery]
        [DisplayName("Manage Classes")]
        [Route("School/Academics/Sections/Manage/{id?}")]
        public async Task<IActionResult> SectionManage(int? id)
        {
            if (id != null && id != 0)
            {
                ViewData["Title"] = "Update Section";
                try
                {
                    var res = await _academicServices.FindSectionByIdAsync(id ?? 0);
                    if (res.Succeeded) return View(res.Data);
                    AlertMessage(NotificationType.warning, "Warning", $"There is no data available for id = {id}");
                    return RedirectToAction(nameof(SectionList));
                }
                catch (Exception ex)
                {
                    AlertMessage(NotificationType.error, "Error", $"error when getting section data for id = {id}");
                    _logger.LogError(ex, $"error when getting section data for id = {id}");
                    return RedirectToAction(nameof(SectionList));
                }
            }
            else
            {
                ViewData["Title"] = "Create Section";
                return View(new SectionViewModel());
            }
        }

        [HttpPost]
        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [DisplayName("Manage Classes")]
        [Route("School/Academics/Sections/Manage/{id?}")]
        public async Task<IActionResult> SectionManage(SectionViewModel model, int? id)
        {
            var IdPres = (model.Id == 0);
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "Error", "Please fill all required fields and save again.");
                return View(model);
            }
            try
            {
                var res = await _academicServices.AddOrEditSectionAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"Section is done {(IdPres ? "creating" : "updating")}");
                    return RedirectToAction(nameof(SectionList));
                }
                else
                {
                    AlertMessage(NotificationType.warning, "", "Section is already present with same name.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"error when {(IdPres ? "creating" : "updating")} section for id = {id}");
                _logger.LogError(ex, $"error when {(IdPres ? "creating" : "updating")} section for id = {id}");
                return View(model);
            }
        }

        [HttpDelete]
        [MvcDiscovery]
        [DisplayName("Delete Class")]
        [Route("School/Academics/Sections/Delete")]
        public IActionResult SectionDelete()
        {
            return View();
        }
        #endregion

        #region Session
        [HttpGet]
        [MvcDiscovery]
        [DisplayName("List Sessions")]
        [Route("School/Academics/Sessions")]
        public async Task<IActionResult> SessionList()
        {
            try
            {
                ViewData["Title"] = "Sessions";
                var list = await _academicServices.GetListOfSessionsAsync();
                return View(list.Data);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "error when getting session data.");
                _logger.LogError(ex, "error when getting session data.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [MvcDiscovery]
        [DisplayName("Manage Classes")]
        [Route("School/Academics/Sessions/Manage/{id?}")]
        public async Task<IActionResult> SessionManage(int? id)
        {
            if (id != null && id != 0)
            {
                ViewData["Title"] = "Update Session";
                try
                {
                    var res = await _academicServices.FindSessionByIdAsync(id ?? 0);
                    if (res.Succeeded) return View(res.Data);
                    AlertMessage(NotificationType.warning, "Warning", $"There is no data available for id = {id}");
                    return RedirectToAction(nameof(SessionList));
                }
                catch (Exception ex)
                {
                    AlertMessage(NotificationType.error, "Error", $"error when getting session data for id = {id}");
                    _logger.LogError(ex, $"error when getting session data for id = {id}");
                    return RedirectToAction(nameof(SessionList));
                }
            }
            else
            {
                ViewData["Title"] = "Create Session";
                return View(new SessionViewModel());
            }
        }

        [HttpPost]
        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [DisplayName("Manage Classes")]
        [Route("School/Academics/Sessions/Manage/{id?}")]
        public async Task<IActionResult> SessionManage(SessionViewModel model, int? id)
        {
            var IdPres = (model.Id == 0);
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.error, "Error", "Please fill all required fields and save again.");
                return View(model);
            }
            try
            {
                var res = await _academicServices.AddOrEditSessionAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"Session is done {(IdPres ? "creating" : "updating")}");
                    return RedirectToAction(nameof(SessionList));
                }
                else
                {
                    AlertMessage(NotificationType.warning, "", "Session is already present with same name.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"error when {(IdPres ? "creating" : "updating")} session for id = {id}");
                _logger.LogError(ex, $"error when {(IdPres ? "creating" : "updating")} session for id = {id}");
                return View(model);
            }
        }

        [HttpDelete]
        [MvcDiscovery]
        [DisplayName("Delete Class")]
        [Route("School/Academics/Sessions/Delete")]
        public IActionResult SessionDelete()
        {
            return View();
        }
        #endregion
    }
}
