using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FeePay.Web.Filters;
using FeePay.Web.Areas.Common;
using FeePay.Web.Extensions;
using FeePay.Core.Application.Interface.Service.SuperAdmin;
using static FeePay.Core.Application.Enums.Notification;
using FeePay.Web.Services.Interfaces;
using FeePay.Core.Application.UseCase;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Wrapper;

namespace FeePay.Web.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [SuperAdminAuthorize]
    public class PaymentGatewayManagerController : AreaBaseController
    {
        private readonly ILogger<PaymentGatewayManagerController> _logger;
        private readonly ISchoolsManagerServices _schoolsManagerServices;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly ISiteSettings _siteSettings;
        public PaymentGatewayManagerController(
            ILogger<PaymentGatewayManagerController> _logger,
            ISiteSettings _siteSettings,
            IAppContextAccessor _appContextAccessor,
            ISchoolsManagerServices _schoolsManagerServices)
        {
            this._logger = _logger;
            this._schoolsManagerServices = _schoolsManagerServices;
            this._siteSettings = _siteSettings;
            this._appContextAccessor = _appContextAccessor;
        }



        [HttpGet]
        public async Task<IActionResult> ListDocument()
        {
            ViewData["Title"] = "Payment Gateway Document List";
            try
            {
                var res = await _schoolsManagerServices.GetPaymentGatewayDocumentList();
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.error, "Error", "Error:" + res.Message);
                _logger.LogError("Error when getting Payment Gateway Document List, Error:{0}", res.Message);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "Error:" + ex.Message);
                _logger.LogError(ex, "Error when getting Payment Gateway Document List, Error:{0}", ex.Message);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ViewDocs(int id)
        {
            ViewData["Title"] = "Payment Gateway Document";
            try
            {
                var res = await _schoolsManagerServices.GetPaymentGatewayDocumentById(id);
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.error, "Error", "Error:" + res.Message);
                _logger.LogError("Error when getting Payment Gateway Document, Id:{0} Error:{1}", id, res.Message);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "Error:" + ex.Message);
                _logger.LogError(ex, "Error when getting Payment Gateway Document, Id:{0} Error:{1}", id, ex.Message);
            }
            return RedirectToAction(nameof(ListDocument));
        }
        [HttpGet]
        public async Task<IActionResult> DownloadAllDocs(int id)
        {
            ViewData["Title"] = "Payment Gateway Document";
            try
            {
                var res = await _schoolsManagerServices.GetPaymentGatewayDocumentById(id);
                if (res.Succeeded)
                {
                    var fileNameList = new List<string>() {
                        res.Data.BusinessPANCard,
                        res.Data.XeroxCopyOfBankPassbook,
                        res.Data.IdentityProof };
                    var fileDirectory = _appContextAccessor.GetRootDirectory(
                        _siteSettings.SchoolPaymentGatewayDocumentsDirectory);
                    var zipPackage = fileNameList.GetZipPackage(fileDirectory);
                    var schoolName = res.Data?.RegisterSchool?.Name?.Replace(' ', '-');
                    var fileName = $"PG_Docs_{schoolName ?? ""}_{Guid.NewGuid():N}.zip";
                    return File(zipPackage, FileMimeType.Zip, fileName);
                }
                AlertMessage(NotificationType.error, "Error", "Error:" + res.Message);
                _logger.LogError("Error when creating Payment Gateway Document download package, Error:{0}", res.Message);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", "Error:" + ex.Message);
                _logger.LogError(ex, "Error when creating Payment Gateway Document download package, Error:{0}", ex.Message);
            }
            return RedirectToAction(nameof(ListDocument));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ApprovePaymentGateway(IsActiveRequestViewModel model)
        {
            var responce = new JsonResponse<bool>();
            try
            {
                var res = await _schoolsManagerServices.ApprovePaymentGateway(model);
                if (res.Succeeded)
                {
                    responce = new JsonResponse<bool>(res.Data);
                }
                else
                {
                    _logger.LogError("Error when Approving Payment Gateway for School Name:{0}, Error:{1}", model.Name, res.Message);
                    responce = new JsonResponse<bool>(res.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when Approving Payment Gateway for School Name:{0} Error:{1}", model.Name, ex.Message);
                responce = new JsonResponse<bool>("no data found");
            }
            return Json(responce);
        }
    }
}
