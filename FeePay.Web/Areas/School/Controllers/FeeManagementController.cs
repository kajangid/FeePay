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

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [SchoolAdminAuthorize(Roles = "Admin,Manager,Teacher")]
    public class FeeManagementController : AreaBaseController
    {
        public FeeManagementController(ILogger<FeeManagementController> logger, ILoginService loginService,
            IFeeManagementService feeManagementService)
        {
            _logger = logger;
            _loginService = loginService;
            _feeManagementService = feeManagementService;
        }
        private readonly ILogger<FeeManagementController> _logger;
        private readonly ILoginService _loginService;
        private readonly IFeeManagementService _feeManagementService;

        #region FEE TYPE 
        [HttpGet]
        [Route("School/FeeTypes")]
        [DisplayName("List Fee Type")]
        public async Task<IActionResult> FeeTypeList()
        {
            ViewData["Title"] = "Fee Type";
            List<FeeTypeViewModel> model = new List<FeeTypeViewModel>();
            try
            {
                var res = await _feeManagementService.GetAllFeeTypeAsync();
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
        [Route("School/ManageFeeType/{id?}")]
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
                    var res = await _feeManagementService.GetFeeTypeByIdAsync(id ?? 0);
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
        [ValidateAntiForgeryToken]
        [Route("School/ManageFeeType/{id?}")]
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
                var res = await _feeManagementService.AddOrEditFeeTypeAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"Fee Type is successfully {(id != null && id != 0 ? "added" : "updated")}.");
                    return RedirectToAction(nameof(FeeTypeList));
                }
                else
                {
                    AlertMessage(NotificationType.error, "Error", $"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee type.");
                    _logger.LogError($"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee type.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee type.");
                _logger.LogError(ex, $"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee type.");
                return View(model);
            }
        }

        [HttpDelete]
        [Route("School/DeleteFeeType/{id}")]
        [DisplayName("Delete Fee Type")]
        public IActionResult FeeTypeDelete(int id)
        {
            return View();
        }
        #endregion


        #region FEE GROUP 
        [HttpGet]
        [Route("School/FeeGroups")]
        [DisplayName("List Fee Group")]
        public async Task<IActionResult> FeeGroupList()
        {
            ViewData["Title"] = "Fee Group";
            List<FeeGroupViewModel> model = new List<FeeGroupViewModel>();
            try
            {
                var res = await _feeManagementService.GetAllFeeGroupAsync();
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
        [Route("School/ManageFeeGroup/{id?}")]
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
                    var res = await _feeManagementService.GetFeeGroupByIdAsync(id ?? 0);
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
        [ValidateAntiForgeryToken]
        [Route("School/ManageFeeGroup/{id?}")]
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
                var res = await _feeManagementService.AddOrEditFeeGroupAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"Fee Group is successfully {(id != null && id != 0 ? "added" : "updated")}.");
                    return RedirectToAction(nameof(FeeGroupList));
                }
                else
                {
                    AlertMessage(NotificationType.error, "Error", $"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee group.");
                    _logger.LogError($"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee group.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee group.");
                _logger.LogError(ex, $"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee group.");
                return View(model);
            }
        }

        [HttpDelete]
        [Route("School/DeleteFeeGroup/{id}")]
        [DisplayName("Delete Fee Group")]
        public IActionResult FeeGroupDelete(int id)
        {
            return View();
        }
        #endregion


        #region FEE MASTER 
        [HttpGet]
        [Route("School/FeeMasters")]
        [DisplayName("List Fee Master")]
        public async Task<IActionResult> FeeMasterList()
        {
            ViewData["Title"] = "Group Fees";
            List<FeeGroupViewModel> model = new List<FeeGroupViewModel>();
            try
            {
                var res = await _feeManagementService.GetAllFeeGroupMasterAsync();
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
        [Route("School/ManageFeeMaster/{id?}")]
        [DisplayName("Add Or Edit Fee Master")]
        public async Task<IActionResult> FeeMasterManage(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    ViewData["Title"] = "Create Fee Master";
                    return View(await _feeManagementService.BindFeeMasterViewModel());
                }
                else
                {
                    ViewData["Title"] = "Update Fee Master";
                    var res = await _feeManagementService.GetFeeMasterByIdAsync(id ?? 0);
                    if (res.Succeeded) return View(res.Data);
                    else
                    {
                        _logger.LogError("Error when getting Fee Master list");
                        AlertMessage(NotificationType.error, "Error", "There is an error when getting data for Fee Master. ");
                        return View(await _feeManagementService.BindFeeMasterViewModel());
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
        [ValidateAntiForgeryToken]
        [Route("School/ManageFeeMaster/{id?}")]
        [DisplayName("Add Or Edit Fee Master")]
        public async Task<IActionResult> FeeMasterManage(FeeMasterViewModel model, int? id)
        {

            if (!ModelState.IsValid)
            {
                AlertMessage(NotificationType.warning, "Error", "Please fill all required data fields.");
                _logger.LogWarning("model state Error for fee master manage");
                return View(await _feeManagementService.BindFeeMasterViewModel(model));
            }
            try
            {
                var res = await _feeManagementService.AddOrEditFeeMasterAsync(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", $"Fee Master is successfully {(id != null && id != 0 ? "added" : "updated")}.");
                    return RedirectToAction(nameof(FeeMasterList));
                }
                else
                {
                    AlertMessage(NotificationType.error, "Error", $"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee master.");
                    _logger.LogError($"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee master.");
                    return View(await _feeManagementService.BindFeeMasterViewModel(model));
                }
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee master.");
                _logger.LogError(ex, $"There is an error {(id != null && id != 0 ? "adding" : "updating")} fee master.");
                return View(await _feeManagementService.BindFeeMasterViewModel(model));
            }
        }

        [HttpDelete]
        [Route("School/DeleteFeeMaster/{id}")]
        [DisplayName("Delete Fee Master")]
        public IActionResult FeeMasterDelete(int id)
        {
            return View();
        }
        #endregion

        #region FEE ASSIGN
        #endregion
    }
}
