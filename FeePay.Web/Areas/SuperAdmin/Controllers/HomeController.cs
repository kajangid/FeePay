using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Service.SuperAdmin;
using FeePay.Web.Areas.Common;
using FeePay.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FeePay.Core.Application.Enums.Notification;

namespace FeePay.Web.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [SuperAdminAuthorize]
    public class HomeController : AreaBaseController
    {
        public HomeController(ILogger<HomeController> logger,
            IAdministrativeServices administrativeServices)
        {
            _logger = logger;
            _administrativeServices = administrativeServices;
        }
        private readonly ILogger<HomeController> _logger;
        private readonly IAdministrativeServices _administrativeServices;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{Area}/Profile")]
        public async Task<IActionResult> Profile()
        {
            ViewData["Title"] = "Profile";
            try
            {
                var res = await _administrativeServices.GetUserProfileAsync();
                if (res.Succeeded) return View(res.Data);
                else AlertMessage(NotificationType.warning, "", res.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when getting profile data Error:{0}", ex.Message);
                AlertMessage(NotificationType.error, "Error", "Error Getting Profile Data.");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
