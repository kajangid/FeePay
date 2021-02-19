using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Web.Services.Interfaces
{
    public interface ISiteSettings
    {
        string BaseUrl { get; set; }
        string BasePath { get; set; }
        string StaticFilesRootDirectory { get; set; }
        string SchoolFilesDirectory { get; set; }
        string SchoolPaymentGatewayDocumentsDirectory { get; set; }
        string StudentFilesDirectory { get; set; }
        string SuperAdminFilesDirectory { get; set; }
    }
}
