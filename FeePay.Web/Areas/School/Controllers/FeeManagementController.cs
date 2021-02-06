using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Web.Filters;
using System.ComponentModel;
using FeePay.Core.Application.Interface.Repository.School;
using FeePay.Web.Areas.Common;
using static FeePay.Core.Application.Enums.Notification;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Service.Student;
using Microsoft.AspNetCore.Authorization;

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [SchoolAdminAuthorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class FeeManagementController : AreaBaseController
    {
        public FeeManagementController(ILogger<FeeManagementController> logger, ILoginService loginService,
            IFeeManagementService feeManagementService, IAcademicServices academicServices,
            IStudentManagementService studentManagementService)
        {
            _logger = logger;
            _login = loginService;
            _feeManagement = feeManagementService;
            _academic = academicServices;
            _studentManagement = studentManagementService;
        }
        private readonly ILogger<FeeManagementController> _logger;
        private readonly ILoginService _login;
        private readonly IFeeManagementService _feeManagement;
        private readonly IAcademicServices _academic;
        private readonly IStudentManagementService _studentManagement;

        #region FEE TYPE 
        [HttpGet]
        [MvcDiscovery]
        [Route("{Area}/Fees/FeeTypes/List")]
        [DisplayName("List Fee Type")]
        public async Task<IActionResult> FeeTypeList()
        {
            ViewData["Title"] = "Fee Type";
            List<FeeTypeViewModel> model = new List<FeeTypeViewModel>();
            try
            {
                var res = await _feeManagement.GetAllFeeTypeAsync();
                if (res.Succeeded) model = res.Data;
                else
                {
                    _logger.LogError("Error when getting Fee Type list");
                    AlertMessage(NotificationType.error, "Error", "There is an error for sowing list. ");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting Fee Type list");
                AlertMessage(NotificationType.error, "Error", "There is an error for sowing list. ");
            }
            return View(model);
        }

        [HttpGet]
        [MvcDiscovery]
        [Route("{Area}/Fees/FeeTypes/Manage/{id?}")]
        [DisplayName("Add Or Edit Fee Type")]
        public async Task<IActionResult> FeeTypeManage(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    ViewData["Title"] = "Create Fee Type";
                    return View(new FeeTypeViewModel());
                }
                else
                {
                    ViewData["Title"] = "Update Fee Type";
                    var res = await _feeManagement.GetFeeTypeByIdAsync(id ?? 0);
                    if (res.Succeeded) return View(res.Data);
                    else
                    {
                        _logger.LogError("Error when getting FeeType list");
                        AlertMessage(NotificationType.error, "Error", "There is an error when getting data for Fee Type. ");
                        return View(new FeeTypeViewModel());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting FeeType data for Id = " + id);
                AlertMessage(NotificationType.error, "Error", "There is an error when getting data for Fee Type. ");
                return RedirectToAction(nameof(FeeTypeList));

            }
        }

        [HttpPost]
        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [Route("{Area}/Fees/FeeTypes/Manage/{id?}")]
        [DisplayName("Add Or Edit Fee Type")]
        public async Task<IActionResult> FeeTypeManage(FeeTypeViewModel model, int? id)
        {

            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.warning, "Error", "Please fill all required data fields.");
                _logger.LogWarning("model state Error for fee type manage");
                return View(model);
            }
            try
            {
                var res = await _feeManagement.AddOrEditFeeTypeAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"Fee Type is successfully {(id == null || id == 0 ? "added" : "updated")}.");
                    return RedirectToAction(nameof(FeeTypeList));
                }
                else
                {
                    AlertMessage(NotificationType.error, "Error", $"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee type.");
                    _logger.LogError($"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee type.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee type.");
                _logger.LogError(ex, $"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee type.");
                return View(model);
            }
        }

        [HttpDelete]
        [MvcDiscovery]
        [Route("{Area}/Fees/FeeTypes/Delete/{id}")]
        [DisplayName("Delete Fee Type")]
        public IActionResult FeeTypeDelete(int id)
        {
            return View();
        }
        #endregion


        #region FEE GROUP 
        [HttpGet]
        [MvcDiscovery]
        [Route("{Area}/Fees/FeeGroup/List")]
        [DisplayName("List Fee Group")]
        public async Task<IActionResult> FeeGroupList()
        {
            ViewData["Title"] = "Fee Group";
            List<FeeGroupViewModel> model = new List<FeeGroupViewModel>();
            try
            {
                var res = await _feeManagement.GetAllFeeGroupAsync();
                if (res.Succeeded) model = res.Data;
                else
                {
                    _logger.LogError("Error when getting Fee Group list");
                    AlertMessage(NotificationType.error, "Error", "There is an error for sowing Fee Group list. ");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting Fee Group list");
                AlertMessage(NotificationType.error, "Error", "There is an error for sowing Fee Group list. ");
            }
            return View(model);
        }

        [HttpGet]
        [MvcDiscovery]
        [Route("{Area}/Fees/FeeGroup/Manage/{id?}")]
        [DisplayName("Add Or Edit Fee Group")]
        public async Task<IActionResult> FeeGroupManage(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    ViewData["Title"] = "Create Fee Group";
                    return View(new FeeGroupViewModel());
                }
                else
                {
                    ViewData["Title"] = "Update Fee Group";
                    var res = await _feeManagement.GetFeeGroupByIdAsync(id ?? 0);
                    if (res.Succeeded) return View(res.Data);
                    else
                    {
                        _logger.LogError("Error when getting Fee Group list");
                        AlertMessage(NotificationType.error, "Error", "There is an error when getting data for Fee Group. ");
                        return View(new FeeGroupViewModel());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting Fee Group data for Id = " + id);
                AlertMessage(NotificationType.error, "Error", "There is an error when getting data for Fee Group. ");
                return RedirectToAction(nameof(FeeGroupList));
            }
        }

        [HttpPost]
        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [Route("{Area}/Fees/FeeGroup/Manage/{id?}")]
        [DisplayName("Add Or Edit Fee Group")]
        public async Task<IActionResult> FeeGroupManage(FeeGroupViewModel model, int? id)
        {

            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.warning, "Error", "Please fill all required data fields.");
                _logger.LogWarning("model state Error for fee group manage");
                return View(model);
            }
            try
            {
                var res = await _feeManagement.AddOrEditFeeGroupAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"Fee Group is successfully {(id == null || id == 0 ? "added" : "updated")}.");
                    return RedirectToAction(nameof(FeeGroupList));
                }
                else
                {
                    AlertMessage(NotificationType.error, "Error", $"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee group.");
                    _logger.LogError($"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee group.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee group.");
                _logger.LogError(ex, $"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee group.");
                return View(model);
            }
        }

        [HttpDelete]
        [MvcDiscovery]
        [Route("{Area}/Fees/FeeGroup/Delete/{id}")]
        [DisplayName("Delete Fee Group")]
        public IActionResult FeeGroupDelete(int id)
        {
            return View();
        }
        #endregion


        #region FEE MASTER 
        [HttpGet]
        [MvcDiscovery]
        [Route("{Area}/Fees/FeeMaster/List")]
        [DisplayName("List Fee Master")]
        public async Task<IActionResult> FeeMasterList()
        {
            ViewData["Title"] = "Group Fees";
            List<FeeGroupViewModel> model = new List<FeeGroupViewModel>();
            try
            {
                var res = await _feeManagement.GetAllFeeGroupMasterAsync();
                if (res.Succeeded) model = res.Data;
                else
                {
                    _logger.LogError("Error when getting Fee Master list");
                    AlertMessage(NotificationType.error, "Error", "There is an error for sowing Fee Master list. ");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting Fee Master list");
                AlertMessage(NotificationType.error, "Error", "There is an error for sowing Fee Master list. ");
            }
            return View(model);
        }

        [HttpGet]
        [MvcDiscovery]
        [Route("{Area}/Fees/FeeMaster/Manage/{id?}")]
        [DisplayName("Add Or Edit Fee Master")]
        public async Task<IActionResult> FeeMasterManage(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    ViewData["Title"] = "Create Fee Master";
                    return View(await _feeManagement.BindFeeMasterViewModel());
                }
                else
                {
                    ViewData["Title"] = "Update Fee Master";
                    var res = await _feeManagement.GetFeeMasterByIdAsync(id ?? 0);
                    if (res.Succeeded) return View(res.Data);
                    else
                    {
                        _logger.LogError("Error when getting Fee Master list");
                        AlertMessage(NotificationType.error, "Error", "There is an error when getting data for Fee Master. ");
                        return View(await _feeManagement.BindFeeMasterViewModel());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting Fee Master data for Id = " + id);
                AlertMessage(NotificationType.error, "Error", "There is an error when getting data for Fee Master. ");
                return RedirectToAction(nameof(FeeMasterList));
            }
        }

        [HttpPost]
        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [Route("{Area}/Fees/FeeMaster/Manage/{id?}")]
        [DisplayName("Add Or Edit Fee Master")]
        public async Task<IActionResult> FeeMasterManage(FeeMasterViewModel model, int? id)
        {

            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.warning, "Error", "Please fill all required data fields.");
                _logger.LogWarning("model state Error for fee master manage");
                return View(await _feeManagement.BindFeeMasterViewModel(model));
            }
            try
            {
                var res = await _feeManagement.AddOrEditFeeMasterAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"Fee Master is successfully {(id == null || id == 0 ? "added" : "updated")}.");
                    return RedirectToAction(nameof(FeeMasterList));
                }
                else
                {
                    AlertMessage(NotificationType.error, "Error", $"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee master.");
                    _logger.LogError($"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee master.");
                    return View(await _feeManagement.BindFeeMasterViewModel(model));
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee master.");
                _logger.LogError(ex, $"There is an error {(id == null || id == 0 ? "adding" : "updating")} fee master.");
                return View(await _feeManagement.BindFeeMasterViewModel(model));
            }
        }

        [HttpDelete]
        [MvcDiscovery]
        [Route("{Area}/Fees/FeeMaster/Delete/{id}")]
        [DisplayName("Delete Fee Master")]
        public IActionResult FeeMasterDelete(int id)
        {
            return View();
        }
        #endregion


        #region FEE ASSIGN
        [HttpGet]
        [MvcDiscovery]
        [Route("School/Fees/Assign/{id}")]
        [DisplayName("Assign Fees")]
        public async Task<IActionResult> FeesAssign(int id) // FeeGroupId
        {
            ViewData["Title"] = "Assign Fees";
            AssignFeesViewModel model = new AssignFeesViewModel();
            var res = await _academic.GetAllDropDownClassesAsync();
            model.Classes = res.Data;
            return View(model);
        }

        [HttpPost]
        [MvcDiscovery]
        [Route("School/Fees/Assign/{id}")]
        [ValidateAntiForgeryToken]
        [DisplayName("Assign Fees")]
        public async Task<IActionResult> FeesAssign(AssignFeesViewModel data, int id) // FeeGroupId
        {
            ViewData["Title"] = "Assign Fees";
            var res = await _academic.GetAllDropDownClassesAsync();
            data.Classes = res.Data;

            if (!string.IsNullOrEmpty(data.Search)) ModelState.Remove(nameof(data.ClassId));
            if (data.ClassId != 0) ModelState.Remove(nameof(data.Search));
            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.warning, "Error", "Please fill all required data fields for search.");
                return View(data);
            }

            try
            {
                data = await _feeManagement.SearchStudentAndBindAssignViewModel(data, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting student list for assign fees.");
                AlertMessage(NotificationType.error, "Error", "Error when getting student list for assign fees.");
            }
            return View(data);
        }

        [HttpPost]
        [MvcDiscovery]
        [ValidateAntiForgeryToken]
        [DisplayName("Assign Fees Submit")]
        public async Task<JsonResult> AssignToStudent(AssignFeesViewModel data, int id)
        {
            try
            {
                var res = await _feeManagement.AssignFeesToStudents(data, id);
                return Json(new { Success = res.Data, res.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when assign fees");
                return Json(new { Success = false, Message = "Error when assign fees" });
            }
        }
        #endregion

        #region ALL FEE SUMMERY
        [HttpGet]
        [MvcDiscovery]
        [Route("{Area}/Fees/Summary")]
        [DisplayName("All Fees Summary")]
        public async Task<IActionResult> FeesSummaryAll()
        {
            ViewData["Title"] = "All Fees Summary";
            try
            {
                var res = await _feeManagement.GetAllFeeSummaryAsync();
                if (res.Succeeded) return View(res.Data);
                else
                {
                    _logger.LogError("Error when getting All Fees Summary. Error{0}", res.Message);
                    AlertMessage(NotificationType.error, "Error", res.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting All Fees Summary.");
                AlertMessage(NotificationType.error, "Error", "There is an error getting All Fees Summary.");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        [MvcDiscovery]
        [Route("{Area}/Fees/Summary/Class/{id:int}")]
        [DisplayName("Class Fees Summary")]
        public async Task<IActionResult> FeesSummaryClass(int id)
        {
            ViewData["Title"] = "Class Fees Summary";
            try
            {
                var res = await _feeManagement.GetClassFeeSummaryAsync(id);
                if (res.Succeeded) return View(res.Data);
                else
                {
                    _logger.LogError("Error when getting Class Fees Summary. Error{0}", res.Message);
                    AlertMessage(NotificationType.error, "Error", res.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting Class Fees Summary.");
                AlertMessage(NotificationType.error, "Error", "There is an error getting Class Fees Summary.");
                return RedirectToAction(nameof(FeesSummaryAll));
            }
            return View();
        }
        #endregion

        #region FEE COLLECTION
        [HttpGet]
        [MvcDiscovery]
        [Route("{Area}/Fees/Report/Collection")]
        [DisplayName("Fee Collection Report")]
        public async Task<IActionResult> FeeCollection()
        {
            ViewData["Title"] = "Fee Collection Report";
            try
            {
                var res = await _feeManagement.GetAllFeeSummaryAsync();
                AlertMessage(NotificationType.info, "", "Service Temporally Unavailable.");
                if (res.Succeeded) return View();
                else
                {
                    _logger.LogError("Error when getting Fee Collection Report Data. Error{0}", res.Message);
                    AlertMessage(NotificationType.error, "Error", res.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting Fee Collection Report Data.");
                AlertMessage(NotificationType.error, "Error", "There is an error getting Fee Collection Report.");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [MvcDiscovery]
        [Route("{Area}/Fees/Report/Collection/Search")]
        [DisplayName("Fee Collection Report")]
        public async Task<IActionResult> FeeCollectionSearch(string param1)
        {
            ViewData["Title"] = "Fee Collection Report";
            try
            {
                var res = await _feeManagement.GetAllFeeSummaryAsync();
                if (res.Succeeded) return View();
                else
                {
                    _logger.LogError("Error when searching Fee Collection Report Data. Error{0}", res.Message);
                    AlertMessage(NotificationType.error, "Error", res.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when searching Fee Collection Report Data.");
                AlertMessage(NotificationType.error, "Error", "There is an error searching Fee Collection Report.");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        #endregion
        #region FEE TRANSACTION REPORT
        [HttpGet]
        [MvcDiscovery]
        [Route("{Area}/Fees/Report/Transaction")]
        [DisplayName("Fee Transaction Report")]
        public async Task<IActionResult> FeeTransaction()
        {
            ViewData["Title"] = "Fee Transaction Report";
            try
            {
                var res = await _feeManagement.GetAllFeeSummaryAsync();
                AlertMessage(NotificationType.info, "", "Service Temporally Unavailable.");
                if (res.Succeeded) return View();
                else
                {
                    _logger.LogError("Error when getting Fee Transaction Report Data. Error{0}", res.Message);
                    AlertMessage(NotificationType.error, "Error", res.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting Fee Transaction Report Data.");
                AlertMessage(NotificationType.error, "Error", "There is an error getting Fee Transaction Report Data.");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        [MvcDiscovery]
        [Route("{Area}/Fees/Report/Transaction/Search")]
        [DisplayName("Fee Transaction Report")]
        public async Task<IActionResult> FeeTransactionSearch(string param1)
        {
            ViewData["Title"] = "Fee Transaction Report";
            try
            {
                var res = await _feeManagement.GetAllFeeSummaryAsync();
                if (res.Succeeded) return View();
                else
                {
                    _logger.LogError("Error when searching Fee Transaction Report Data. Error{0}", res.Message);
                    AlertMessage(NotificationType.error, "Error", res.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when searching Fee Transaction Report Data.");
                AlertMessage(NotificationType.error, "Error", "There is an error searching Fee Transaction Report Data.");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        #endregion
    }
}
