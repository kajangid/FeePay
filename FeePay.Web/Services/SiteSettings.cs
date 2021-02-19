using FeePay.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeePay.Web.Services
{
    public class SiteSettings : ISiteSettings
    {
        public string BaseUrl { get; set; }

        public string BasePath { get; set; }

        public string StaticFilesRootDirectory { get; set; }

        public string SchoolFilesDirectory { get; set; }

        public string SchoolPaymentGatewayDocumentsDirectory { get; set; }

        public string StudentFilesDirectory { get; set; }

        public string SuperAdminFilesDirectory { get; set; }
    }
}
