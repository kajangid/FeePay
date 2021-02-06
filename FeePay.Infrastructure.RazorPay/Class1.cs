using System;
using System.Collections.Generic;
using Razorpay.Api;

namespace FeePay.Infrastructure.RazorPay
{
    public class Class1
    {
        public void CreateOrder()
        {
            RazorpayClient client = new RazorpayClient("rzp_test_umbrFAbJ3slyJ", "su9eXFaihGucmKECVRcRk0Q");

            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", 50000); // amount in the smallest currency unit
            options.Add("receipt", "order_rcptid_11");
            options.Add("currency", "INR");
            Order order = client.Order.Create(options);
        }
    }
}
