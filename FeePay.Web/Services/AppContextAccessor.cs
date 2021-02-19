using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FeePay.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.DTOs;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FeePay.Web.Services.Interfaces;

namespace FeePay.Web.Services
{
    public class AppContextAccessor : IAppContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly ISession _session;
        private readonly ISiteSettings _siteSettings;
        private readonly LinkGenerator _linkGenerator;
        public AppContextAccessor(
            IHttpContextAccessor _httpContextAccessor,
            IActionContextAccessor _actionContextAccessor,
            IWebHostEnvironment _hostingEnvironment,
            IUrlHelperFactory _urlHelperFactory,
            ISiteSettings _siteSettings,
            LinkGenerator _linkGenerator)
        {
            this._hostingEnvironment = _hostingEnvironment;
            this._httpContextAccessor = _httpContextAccessor;
            this._actionContextAccessor = _actionContextAccessor;
            this._urlHelperFactory = _urlHelperFactory;
            this._linkGenerator = _linkGenerator;
            this._siteSettings = _siteSettings;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public string GetUserIP()
        {
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            return ip.ToString();
        }
        public List<Claim> GetCurrentUserClaims()
        {
            var claims = _httpContextAccessor.HttpContext.User?.Claims;
            return claims?.ToList();
        }

        public string GetRequestPath()
        {
            var Path = _httpContextAccessor.HttpContext?.Request.Path;
            return (Path != null ? Path.ToString() : "");
        }

        public string ClaimSchoolUniqueId()
        {
            var claims = _httpContextAccessor.HttpContext.User?.Claims;
            var uniqueIdClaim = claims?.FirstOrDefault(claim => claim.Type == "SchoolUniqueId" && claim.ValueType.Equals("school_id", StringComparison.InvariantCultureIgnoreCase));
            return uniqueIdClaim?.Value;
        }
        public ClaimsPrincipal GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;
            return user;
        }

        public List<string> ClaimSchoolUserRoles()
        {
            var claims = _httpContextAccessor.HttpContext.User?.Claims;
            var uniqueIdClaim = claims?.Where(w => w.Type == "Role"
            && w.ValueType.Equals("role_name", StringComparison.InvariantCultureIgnoreCase)
            && w.Issuer.Equals("school", StringComparison.InvariantCultureIgnoreCase));
            return uniqueIdClaim?.Select(s => s.Value).ToList();
        }

        /// <summary>
        /// Create Absolute Url 
        /// </summary>
        /// <param name="actionName"> Action Name </param>
        /// <param name="controllerName"> Controller Name </param>
        /// <param name="routeValues"> Query Parameters </param>
        /// <returns> Absolute Url As String </returns>
        public string AbsoluteAction(string actionName, string controllerName, object routeValues = null)
        {
            string scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            return _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext, actionName, controllerName, routeValues, scheme);
        }

        /// <summary>
        /// Create Absolute Url By Page
        /// </summary>
        /// <param name="page"> Page Name/Link </param>
        /// <param name="routeValues"> Query Parameters </param>
        /// <returns> Absolute Url As String </returns>
        public string AbsoluteUriByPage(string page, object routeValues = null)
        {
            string scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            return _linkGenerator.GetUriByPage(
                httpContext: _httpContextAccessor.HttpContext,
                page: page,
                values: routeValues,
                scheme: scheme);
        }

        public string GetRootDirectory(string folderName)
        {
            return $"{_siteSettings.BasePath}{folderName}";
        }
        public string GetDirectoryRootPath(string path)
        {
            string root = "wwwroot";
            return Path.Combine(_hostingEnvironment.ContentRootPath, root, path);
        }
        public string GetDirectoryPath(string path)
        {
            return Path.Combine(_hostingEnvironment.ContentRootPath, path);
        }
        public string GetDirectoryUrl(string baseUrl, string path)
        {
            var splitsParts = path.Split("\\");
            string newPath = "";
            foreach (string ep in splitsParts)
                if (!string.IsNullOrEmpty(ep))
                    newPath += ep + "/";

            var urlContent = _urlHelperFactory
                .GetUrlHelper(_actionContextAccessor.ActionContext)
                .Content("~/" + newPath);
            return baseUrl + urlContent.TrimStart('/');
        }

        #region SITE SESSION
        SessionViewModel IAppContextAccessor.SiteSession_AcademicSession
        {
            get { return _session.Get<SessionViewModel>("6vvAJ9SuhfTTTG4N_ssasn"); }
            set { _session.Set("6vvAJ9SuhfTTTG4N_ssasn", value); }
        }
        #endregion
    }
}
