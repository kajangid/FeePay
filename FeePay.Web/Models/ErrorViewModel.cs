using FeePay.Core.Domain.Settings;
using System;

namespace FeePay.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public RecoveryLink recoveryLink { get; set; }
        public int StatusCode { get; set; }
        public string Path { get; set; }
    }
}
