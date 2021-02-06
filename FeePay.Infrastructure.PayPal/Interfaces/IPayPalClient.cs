namespace FeePay.Infrastructure.PayPal.Interfaces
{
    using System;
    using PayPalCheckoutSdk.Core;
    using PayPalCheckoutSdk.Orders;
    using PayPalCheckoutSdk.Payments;
    public interface IPayPalClient
    {
        PayPalEnvironment Environment();
        PayPalHttpClient Client();
        PayPalHttpClient Client(string refreshToken);
        string ObjectToJSONString(object serializableObject);
        ApplicationContext GetPaypalApplicationContext();
    }
}
