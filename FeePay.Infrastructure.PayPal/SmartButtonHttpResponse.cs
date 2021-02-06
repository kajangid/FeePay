namespace FeePay.Infrastructure.PayPal
{
    using System;
    using System.Net;
    using System.Net.Http.Headers;
    using PayPalHttp;
    using PayPalCheckoutSdk.Orders;
    public class SmartButtonHttpResponse
    {
        readonly Order _result;
        public SmartButtonHttpResponse(HttpResponse httpResponse)
        {
            Headers = httpResponse.Headers;
            StatusCode = httpResponse.StatusCode;
            _result = httpResponse.Result<Order>();
        }

        public HttpHeaders Headers { get; }
        public HttpStatusCode StatusCode { get; }
        public string OrderID { get; set; }

        public Order Result()
        {
            return _result;
        }
    }
}
