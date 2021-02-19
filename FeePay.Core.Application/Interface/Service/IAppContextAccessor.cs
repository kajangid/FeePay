using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.DTOs;

namespace FeePay.Core.Application.Interface.Service
{
    public interface IAppContextAccessor
    {
        string GetUserIP();
        List<Claim> GetCurrentUserClaims();
        string GetRequestPath();
        /// <summary>
        /// <!-- get's the current user claim -->
        /// </summary>
        /// <returns> tenant db id </returns>
        string ClaimSchoolUniqueId();
        ClaimsPrincipal GetCurrentUser();
        List<string> ClaimSchoolUserRoles();
        string AbsoluteAction(string actionName, string controllerName, object routeValues = null);
        string AbsoluteUriByPage(string page, object routeValues = null);

        string GetRootDirectory(string folderName);
        string GetDirectoryRootPath(string path);
        string GetDirectoryUrl(string baseUrl, string path);


        #region  SITE SESSION
        SessionViewModel SiteSession_AcademicSession { get; set; }
        #endregion
    }
}
