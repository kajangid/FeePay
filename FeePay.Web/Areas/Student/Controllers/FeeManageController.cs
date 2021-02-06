using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FeePay.Web.Filters;
using FeePay.Web.Areas.Common;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.Student;
using static FeePay.Core.Application.Enums.Notification;
using FeePay.Core.Application.DTOs;

namespace FeePay.Web.Areas.Student.Controllers
{
    [Area("Student")]
    [StudentAuthorize]
    public class FeeManageController : AreaBaseController
    {
        public FeeManageController(ILogger<AccountController> logger,
            ILoginService loginService,
            IStudentFeeDepositManagerService studentFeeDepositManagerService)
        {
            _logger = logger;
            _loginService = loginService;
            _studentFeeDepositManagerService = studentFeeDepositManagerService;
        }
        private readonly ILogger<AccountController> _logger;
        private readonly ILoginService _loginService;
        private readonly IStudentFeeDepositManagerService _studentFeeDepositManagerService;




        [HttpGet]
        [DisplayName("Fees")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Fees";
            try
            {
                var res = await _studentFeeDepositManagerService.GetStudentFees();
                if (res.Succeeded) return View(res.Data);
                AlertMessage(NotificationType.warning, "Not Found", res.Message);
                _logger.LogError("Error getting student fees Student Panel Error:{0}", res.Message);
            }
            catch (Exception ex)
            {
                AlertMessage(NotificationType.error, "Error", $"Error when getting student fees.");
                _logger.LogError(ex, "Error getting student fees Student Panel Error:{0}", ex.Message);
            }
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [DisplayName("Fees Deposit")]
        public async Task<IActionResult> Index(SelectedFeeDepositViewModel model)
        {
            ViewData["Title"] = "Fees Deposit";
            var view = "~/Areas/Student/Views/FeeManage/FeeDeposit.cshtml";
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await _studentFeeDepositManagerService.GetSelectedFeeSummary(model);
                    if (res.Succeeded) return View(view, res.Data);
                    AlertMessage(NotificationType.warning, "Not Found", res.Message);
                    _logger.LogError("Error getting student fees Student Panel Error:{0}", res.Message);
                }
                catch (Exception ex)
                {
                    AlertMessage(NotificationType.error, "Error", $"Error when processing your request, Please try in some time.");
                    _logger.LogError(ex, "Error Fee Deposit Error:{0}", ex.Message);
                }
            }
            else
            {
                AlertMessage(NotificationType.error, "Error", $"Error when processing your request, Please try in some time.");
                _logger.LogError("Error Fee Deposit Error:{0}", string.Join(",", GetErrorListFromModelState(ModelState).ToArray()));
            }
            return View();
        }


        public async Task<IActionResult> PayNow(SelectedFeeDepositViewModel model)
        {

            // Create Payment 
            // Add payment id to data base 
            // capture payment 
            // if successfully update database with succes 
            // else update database with fail 
            // notify user 
            // send mail and sms to user  on succes
            var res = await _studentFeeDepositManagerService.GenerateFeeDeposit(model);

            return View();
        }
    }
}
