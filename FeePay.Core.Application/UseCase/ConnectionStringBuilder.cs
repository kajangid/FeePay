using FeePay.Core.Application.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.UseCase
{
    public class ConnectionStringBuilder : IConnectionStringBuilder
    {
        public ConnectionStringBuilder(IConfiguration configuration,
            IConnectionStringConfig _IConnectionStringConfig,
            IHttpContextAccessor _HttpContentAccessor)
        {
            _Configuration = configuration;
            _ICSConfig = _IConnectionStringConfig;
            _HttpContext = _HttpContentAccessor.HttpContext;
        }

        private readonly HttpContext _HttpContext;
        private readonly IConfiguration _Configuration;
        private readonly IConnectionStringConfig _ICSConfig;

        public string GetDefaultConnectionString() => _Configuration.GetConnectionString("DefaultConnection");
        public string GetConnectionString(string SchoolUniqueId) => CSBulder(SchoolUniqueId);
        public string GetDynamicConnectionString() => CSBulder();
        public string GetSuperUserConnectionString() => _Configuration.GetConnectionString("SuperAdminConnection");


        private string CSBulder(string SchoolUniqueId = "")
        {
            if (string.IsNullOrEmpty(SchoolUniqueId))
            {
                ValidateHttpContext();
                var claims = _HttpContext.User.Claims.ToList();
                SchoolUniqueId = claims?.FirstOrDefault(claim =>
                claim.Type.Equals("SchoolUniqueId", StringComparison.OrdinalIgnoreCase) &&
                claim.Issuer.Equals("Student", StringComparison.InvariantCultureIgnoreCase))?.Value;
            }
            string CurrentUserDB = "School_" + (SchoolUniqueId ?? "RSSSJ#01012021");
            return $"Data Source={_ICSConfig.Server};Initial Catalog={CurrentUserDB};User ID={_ICSConfig.User};" +
                $"Password={_ICSConfig.Password};{_ICSConfig.PostString}";
        }
        private string CSBulderOld()
        {
            //string CurrentUserDB = "School_RSSSJ#01012021";//Rawat Senior Secondary School Jaipur # Date(01/01/2021)    
            ValidateHttpContext();
            var claims = _HttpContext.User.Claims.ToList(); //ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var SchoolUniqueId = claims?.FirstOrDefault(claim => claim.Type.Equals("SchoolUniqueId", StringComparison.OrdinalIgnoreCase) && claim.Issuer.Equals("Student", StringComparison.InvariantCultureIgnoreCase))?.Value;

            string CurrentUserDB = "School_" + (SchoolUniqueId ?? "RSSSJ#01012021");
            return $"Data Source={_ICSConfig.Server};Initial Catalog={CurrentUserDB};User ID={_ICSConfig.User};Password={_ICSConfig.Password};{_ICSConfig.PostString}";
        }

        private void ValidateHttpContext()
        {
            if (this._HttpContext == null)
            {
                throw new ArgumentNullException(nameof(this._HttpContext));
            }
        }

        private static void ValidateSchoolUniqueId(string SchoolUniqueId)
        {
            if (string.IsNullOrEmpty(SchoolUniqueId))
            {
                throw new ArgumentNullException(nameof(SchoolUniqueId));
            }
            //if (!Guid.TryParse(SchoolUniqueId, out Guid SchoolUniqueId))
            //{
            //    throw new ArgumentNullException(nameof(SchoolUniqueId));
            //}
        }


    }
}
