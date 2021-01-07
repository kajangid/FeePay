using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface
{
    public interface IConnectionStringBuilder
    {
        /// <summary>
        /// Get the application Connection string.
        /// </summary>
        string GetDefaultConnectionString();

        /// <summary>
        /// Get the connection string for the provided school id.
        /// </summary>
        string GetDynamicSchoolConnectionString(string SchoolUniqueId);

        /// <summary>
        /// Get the Demo DB Connection String for SCHOOL AND STUDENT DB.
        /// </summary>
        string GetSchoolConnectionString();

        /// <summary>
        /// Get the Master Connection String for creating new db and users.
        /// </summary>
        string GetMasterConnectionString();

    }
}
