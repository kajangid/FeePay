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
        public ConnectionStringBuilder(IConfiguration configuration,IConnectionStringConfig _IConnectionStringConfig)
        {
            _Configuration = configuration;
            _ICSConfig = _IConnectionStringConfig;
        }

        private readonly IConfiguration _Configuration;
        private readonly IConnectionStringConfig _ICSConfig;

        /// <summary>
        /// Get the application Connection string.
        /// </summary>
        public string GetDefaultConnectionString() => _Configuration.GetConnectionString("DefaultConnection");
        /// <summary>
        /// Get the connection string for the provided school id.
        /// </summary>
        public string GetDynamicSchoolConnectionString(string SchoolUniqueId) => CSBulder(SchoolUniqueId);
        /// <summary>
        /// Get the Demo DB Connection String for SCHOOL AND STUDENT DB.
        /// </summary>
        public string GetSchoolConnectionString() => CSBulder();
        /// <summary>
        /// Get the Master Connection String for creating new db and users.
        /// </summary>
        public string GetMasterConnectionString() => _Configuration.GetConnectionString("MasterConnection");


        private string CSBulder(string SchoolUniqueId = "")
        {
            string CurrentUserDB = "School_" + (SchoolUniqueId ?? "RSSSJ#01012021");
            return $"Data Source={_ICSConfig.Server};Initial Catalog={CurrentUserDB};User ID={_ICSConfig.User};" +
                $"Password={_ICSConfig.Password};{_ICSConfig.PostString}";
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
