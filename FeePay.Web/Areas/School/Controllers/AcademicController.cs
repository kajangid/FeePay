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
    [Route("School/Academics")]
    [SchoolAdminAuthorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AcademicController : AreaBaseController
    {
        private readonly ILogger _logger;
        private readonly IAcademicServices _academicServices;
        public AcademicController(ILogger<AcademicController> logger, IAcademicServices academicServices)
        {
            _logger = logger;
            _academicServices = academicServices;
        }



        #region Classes
        [MvcDiscovery]
        [HttpGet("Class/List")]
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

        [MvcDiscovery]
        [HttpGet("Class/Add")]
        [DisplayName("Add Class")]
        public async Task<IActionResult> ClassAdd()
        {
            ViewData["Title"] = "Create Class";
            return View(await _academicServices.BindClassViewModelAsync());
        }

        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [HttpPost("Class/Add")]
        [DisplayName("Add Classes")]
        public async Task<IActionResult> ClassAdd(ClassViewModel model)
        {
            ViewData["Title"] = "Create Class";
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
                    AlertMessage(NotificationType.success, "Success", $"Class is added.");
                    return RedirectToAction(nameof(ClassList));
                }
                else
                {
                    AlertMessage(NotificationType.warning, "", "Class is already present with same name.");
                    _logger.LogWarning("Could not add class  :: Error: {0} ", res.Message);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"error when adding new class.");
                _logger.LogError(ex, $"Error Adding new class.");
            }
            return View(await _academicServices.BindClassViewModelAsync(model));
        }

        [MvcDiscovery]
        [HttpGet("Class/Edit/{id:int}")]
        [DisplayName("Edit Class")]
        public async Task<IActionResult> ClassEdit(int id)
        {
            ViewData["Title"] = "Update Class";
            try
            {
                var res = await _academicServices.FindClassByIdAsync(id);
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.warning, "Warning", $"There is no data available.");
                return RedirectToAction(nameof(ClassList));
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"error when getting Class data.");
                _logger.LogError(ex, $"error when getting Class data for id = {id}");
                return RedirectToAction(nameof(ClassList));
            }
        }

        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [HttpPost("Class/Edit/{id:int}")]
        [DisplayName("Edit Class")]
        public async Task<IActionResult> ClassEdit(ClassViewModel model, int id)
        {
            ViewData["Title"] = "Update Class";
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
                    AlertMessage(NotificationType.success, "Success", $"Class is updated.");
                    return RedirectToAction(nameof(ClassList));
                }
                else
                {
                    AlertMessage(NotificationType.warning, "", "Class is already present with same name.");
                    _logger.LogWarning("Could not update class  :: Error: {0} ", res.Message);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"error when updating new class.");
                _logger.LogError(ex, $"Error updating new class.");
            }
            return View(await _academicServices.BindClassViewModelAsync(model));
        }


        [MvcDiscovery]
        [HttpDelete("Class/Delete/{id:int}")]
        [DisplayName("Delete Class")]
        public IActionResult ClassDelete(int id)
        {
            return View();
        }
        #endregion

        #region Sections
        [MvcDiscovery]
        [DisplayName("List Section")]
        [HttpGet("Section/List")]
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

        [MvcDiscovery]
        [DisplayName("Add Section")]
        [HttpGet("Section/Add")]
        public IActionResult SectionAdd()
        {
            ViewData["Title"] = "Create Section";
            return View(new SectionViewModel());
        }

        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [DisplayName("Add Section")]
        [HttpPost("Section/Add")]
        public async Task<IActionResult> SectionAdd(SectionViewModel model)
        {
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
                    AlertMessage(NotificationType.success, "Success", $"Section Added.");
                    return RedirectToAction(nameof(SectionList));
                }
                else
                {
                    AlertMessage(NotificationType.warning, "", "Section is already present with same name.");
                    _logger.LogWarning("error when adding section.{0}", res.Message, res.Errors);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "error when adding section.");
                _logger.LogError(ex, "error when adding section.");
            }
            return View(model);
        }

        [MvcDiscovery]
        [DisplayName("Edit Section")]
        [HttpGet("Section/Edit/{id:int}")]
        public async Task<IActionResult> SectionEdit(int id)
        {
            ViewData["Title"] = "Update Section";
            try
            {
                var res = await _academicServices.FindSectionByIdAsync(id);
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.warning, "Error", $"Section Not Found.");
                _logger.LogWarning("Error when getting section data for id = {0} :: Error:{1}", id, res.Message);
                return RedirectToAction(nameof(SectionList));
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"Error Getting Section Data.");
                _logger.LogError(ex, $"error when getting section data.");
                return RedirectToAction(nameof(SectionList));
            }
        }

        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [DisplayName("Edit Section")]
        [HttpPost("Section/Edit/{id:int}")]
        public async Task<IActionResult> SectionEdit(SectionViewModel model, int id)
        {
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
                    AlertMessage(NotificationType.success, "Success", $"Section Edited.");
                    return RedirectToAction(nameof(SectionList));
                }
                else
                {
                    AlertMessage(NotificationType.warning, "", "Section is already present with same name.");
                    _logger.LogWarning("error when editing section.sectionId={1}::{0}", res.Message, id);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "error when editing section.");
                _logger.LogError(ex, "error when editing section.sectioId={0}", id);
            }
            return View(model);
        }

        [MvcDiscovery]
        [DisplayName("Delete Section")]
        [HttpDelete("Section/Delete/{id:int}")]
        public IActionResult SectionDelete(int id)
        {
            return View();
        }
        #endregion

        #region Session
        [MvcDiscovery]
        [DisplayName("List Session")]
        [HttpGet("Session/List")]
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

        [MvcDiscovery]
        [DisplayName("Add Session")]
        [HttpGet("Session/Add")]
        public IActionResult SessionAdd()
        {
            ViewData["Title"] = "Create Session";
            return View(new SessionViewModel());
        }

        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [DisplayName("Add Session")]
        [HttpPost("Session/Add")]
        public async Task<IActionResult> SessionAdd(SessionViewModel model)
        {
            ViewData["Title"] = "Create Session";
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
                    AlertMessage(NotificationType.success, "Success", $"Session added");
                    return RedirectToAction(nameof(SessionList));
                }
                else
                {
                    AlertMessage(NotificationType.warning, "", "Session is already present with same name.");
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"Error adding session.");
                _logger.LogError(ex, $"error when adding session");
            }
            return View(model);
        }

        [MvcDiscovery]
        [DisplayName("Edit Session")]
        [HttpGet("Session/Edit/{id:int}")]
        public async Task<IActionResult> SessionEdit(int id)
        {
            ViewData["Title"] = "Update Session";
            try
            {
                var res = await _academicServices.FindSessionByIdAsync(id);
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.warning, "Warning", $"Session is not found.");
                return RedirectToAction(nameof(SessionList));
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"Error getting session.");
                _logger.LogError(ex, $"error when getting session data for id = {id}");
                return RedirectToAction(nameof(SessionList));
            }
        }

        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [DisplayName("Edit Session")]
        [HttpPost("Session/Edit/{id:int}")]
        public async Task<IActionResult> SessionEdit(SessionViewModel model, int id)
        {
            ViewData["Title"] = "Update Session";
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
                    AlertMessage(NotificationType.success, "Success", $"Session edited");
                    return RedirectToAction(nameof(SessionList));
                }
                else
                {
                    AlertMessage(NotificationType.warning, "", "Session is already present with same name.");
                    _logger.LogWarning($"error when editing session. sessionid:{id},Error:{res.Message}");
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"Error editing session.");
                _logger.LogError(ex, $"error when editing session. sessionid:{id}");
            }
            return View(model);
        }

        [MvcDiscovery]
        [DisplayName("Delete Session")]
        [HttpDelete("School/Academics/Sessions/Delete")]
        public IActionResult SessionDelete()
        {
            return View();
        }
        #endregion
    }
}
