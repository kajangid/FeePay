using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeePay.Core.Application.DTOs;

namespace FeePay.Web.Services.Interfaces
{
    public interface IMvcControllerDiscovery
    {
        IEnumerable<MvcControllerInfo> GetControllers();
        IEnumerable<MvcControllerInfo> GetSchoolControllers(string selectedlist = "");
        IEnumerable<MvcControllerInfo> GetSuperAdminControllers(string selectedlist = "");
    }
}
