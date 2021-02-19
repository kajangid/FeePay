using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using FeePay.Core.Application.Interface.Service.SuperAdmin;
using FeePay.Web.Extensions;
using FeePay.Web.Services.Interfaces;

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [SchoolAdminAuthorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class PaymentGatewayManagementController : AreaBaseController
    {
        private readonly ILogger<PaymentGatewayManagementController> _logger;
        private readonly ILoginService _login;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly ISiteSettings _siteSettings;
        private readonly IAdministrationService _administrationService;
        public PaymentGatewayManagementController(
            ILogger<PaymentGatewayManagementController> logger,
            ILoginService loginService,
            IAppContextAccessor appContextAccessor,
            ISiteSettings siteSettings,
            IAdministrationService administrationService)
        {
            _logger = logger;
            _login = loginService;
            _appContextAccessor = appContextAccessor;
            _siteSettings = siteSettings;
            _administrationService = administrationService;
        }


        [HttpGet]
        [MvcDiscovery]
        [DisplayName("Upload Payment Gateway Document")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Upload Document";
            try
            {
                var res = await _administrationService.GetPaymentGatewayDocument();
                if (res.Succeeded) return View(res.Data);
                _logger.LogError("Error when Getting School's Payment Gateway Document, Error:{0}", res.Message);
                AlertMessage(NotificationType.error, "Error", string.Format("Error:{0}", res.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Error when Getting School's Payment Gateway Document, Error:{0}", ex.Message);
                AlertMessage(NotificationType.error, "Error", "Error  when getting school's payment gateway document.");
            }
            return View();
        }

        [HttpPost]
        [MvcDiscovery]
        [DisplayName("Upload Payment Gateway Document")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PaymentGatewayDocumentViewModel model)
        {
            ViewData["Title"] = "Upload Document";
            bool valid = true;
            if (!ModelState.IsValid)
            {
                valid = false;
            }
            if (model.Id == 0)
            {
                if (model.BusinessPANCard_File == null)
                {
                    ModelState.AddModelError("BusinessPANCard", "PAN Card field is required");
                    valid = false;
                }
                if (model.XeroxCopyOfBankPassbook_File == null)
                {
                    ModelState.AddModelError("XeroxCopyOfBankPassbook", "Xerox Copy Of Bank Passbook field is required");
                    valid = false;
                }
                if (model.IdentityProof_File == null)
                {
                    ModelState.AddModelError("IdentityProof", "Identity Proof field is required");
                    valid = false;
                }
            }
            if (!valid)
            {
                AlertMessage(NotificationType.warning, "Error", string.Format("Fill all required fields."));
                return View(model);
            }

            try
            {
                string DocPath = _appContextAccessor.GetDirectoryRootPath(
                        _siteSettings.SchoolPaymentGatewayDocumentsDirectory);
                string DocUrl = _appContextAccessor.GetDirectoryUrl(
                    _siteSettings.BaseUrl,
                    _siteSettings.SchoolPaymentGatewayDocumentsDirectory);

                if (model.BusinessPANCard_File != null)
                    model.BusinessPANCardUploadData = await model.BusinessPANCard_File
                        .UploadFiles(DocPath, DocUrl);
                if (model.XeroxCopyOfBankPassbook_File != null)
                    model.BankPassbookUploadData = await model.XeroxCopyOfBankPassbook_File
                        .UploadFiles(DocPath, DocUrl);
                if (model.IdentityProof_File != null)
                    model.IdentityProofUploadData = await model.IdentityProof_File
                        .UploadFiles(DocPath, DocUrl);

                var res = await _administrationService.AddOrEditPaymentGatewayDocument(model);
                if (res.Succeeded)
                {
                    AlertMessage(NotificationType.success, "Success", "Documents Successfully Uploaded.");
                    return RedirectToAction(nameof(Index));
                }
                _logger.LogError("Error when Getting School's Payment Gateway Document, Error:{0}", res.Message);
                AlertMessage(NotificationType.error, "Error", string.Format("Error:{0}", res.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Error when Getting School's Payment Gateway Document, Error:{0}", ex.Message);
                AlertMessage(NotificationType.error, "Error", "Error  when getting school's payment gateway document.");
            }
            return View();
        }
    }
}
