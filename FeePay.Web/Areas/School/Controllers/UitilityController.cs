using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeePay.Web.Filters;
using Microsoft.Extensions.Logging;
using FeePay.Core.Application.Interface.Service.School;

namespace FeePay.Web.Areas.School.Controllers
{
    [Area("School")]
    [SchoolAdminAuthorize]
    public class UitilityController : Controller
    {
        private readonly ILogger<UitilityController> _logger;
        private readonly ISchoolCommonServices _schoolCommonServices;
        public UitilityController(
            ILogger<UitilityController> logger,
            ISchoolCommonServices schoolCommonServices)
        {
            _logger = logger;
            _schoolCommonServices = schoolCommonServices;
        }

        [MvcDiscovery]
        public async Task<JsonResult> GetClassSection(int id)
        {
            try
            {
                var res = await _schoolCommonServices.DDL_ClassSectionsAsync(id);
                return Json(new { success = res.Succeeded, data = res.Data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error when GetClassSection drop down list data for class id = {id}");
                return Json(new { success = false, message = "Oh, Snap! Something went wrong." });
            }
        }

        [MvcDiscovery]
        public async Task<JsonResult> GetStateCities(int id)
        {
            try
            {
                var res = await _schoolCommonServices.DDL_StateCitiesAsync(id);
                return Json(new { success = res.Succeeded, data = res.Data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error when getting cities of state where state id is = {id}");
                return Json(new { success = false, message = "Oh, Snap! Something went wrong." });
            }
        }
    }
}
