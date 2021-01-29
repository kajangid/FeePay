using FeePay.Core.Application.Interface.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Services
{
    public class AppContextAccessor : IAppContextAccessor
    {
        public AppContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
        }
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public string GetUserIP()
        {
            var ip = _HttpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            return ip.ToString();
        }
        public List<Claim> GetCurrentUserClaims()
        {
            var claims = _HttpContextAccessor.HttpContext.User?.Claims;
            return claims?.ToList();
        }

        public string GetRequestPath()
        {
            var Path = _HttpContextAccessor.HttpContext?.Request.Path;
            return (Path != null ? Path.ToString() : "");
        }

        public string ClaimSchoolUniqueId()
        {
            var claims = _HttpContextAccessor.HttpContext.User?.Claims;
            var uniqueIdClaim = claims?.FirstOrDefault(claim => claim.Type == "SchoolUniqueId" && claim.ValueType.Equals("school_id", StringComparison.InvariantCultureIgnoreCase));
            return uniqueIdClaim?.Value;
        }
        public ClaimsPrincipal getCurrentUser()
        {
            var user = _HttpContextAccessor.HttpContext.User;
            return user;
        }

        public List<string> ClaimSchoolUserRoles()
        {
            var claims = _HttpContextAccessor.HttpContext.User?.Claims;
            var uniqueIdClaim = claims?.Where(w => w.Type == "Role"
            && w.ValueType.Equals("role_name", StringComparison.InvariantCultureIgnoreCase)
            && w.Issuer.Equals("school", StringComparison.InvariantCultureIgnoreCase));
            return uniqueIdClaim?.Select(s => s.Value).ToList();
        }
    }
}
