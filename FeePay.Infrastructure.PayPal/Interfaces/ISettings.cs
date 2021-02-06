using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Infrastructure.PayPal.Services;

namespace FeePay.Infrastructure.PayPal.Interfaces
{
    internal interface ISettings
    {
        string Mode { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        int RequestRetries { get; set; }
        int ConnectionTimeout { get; set; }
        string Endpoint { get; set; }
        string ProxyAddress { get; set; }
        string ProxyCredentials { get; set; }
        string Business { get; set; }
        string MerchantId { get; set; }
        Webhook Webhook { get; set; }
        Oauth Oauth { get; set; }

    }
}
