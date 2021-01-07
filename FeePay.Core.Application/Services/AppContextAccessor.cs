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
            var ip = _HttpContextAccessor.HttpContext.User?.Claims;
            return ip?.ToList();
        }

        public string GetRequestPath()
        {
            var Path = _HttpContextAccessor.HttpContext?.Request.Path;
            return (Path != null ? Path.ToString() : "");
        }
    }
}
