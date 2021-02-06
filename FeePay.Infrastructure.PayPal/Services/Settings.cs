using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Infrastructure.PayPal.Interfaces;

namespace FeePay.Infrastructure.PayPal.Services
{
    internal class Settings : ISettings
    {
        public string Mode { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int RequestRetries { get; set; }
        public int ConnectionTimeout { get; set; }
        public string Endpoint { get; set; }
        public string ProxyAddress { get; set; }
        public string ProxyCredentials { get; set; }
        public string Business { get; set; }
        public string MerchantId { get; set; }
        public Webhook Webhook { get; set; }
        public Oauth Oauth { get; set; }
    }
    internal class Webhook
    {
        public string Id { get; set; }
        public string TrustCert { get; set; }
    }
    internal class Oauth
    {
        public string EndPoint { get; set; }
    }
}
