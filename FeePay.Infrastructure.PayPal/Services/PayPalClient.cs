namespace FeePay.Infrastructure.PayPal.Services
{
    using System;
    using System.IO;
    using System.Text;
    using System.Runtime.Serialization.Json;
    using PayPalHttp;
    using PayPalCheckoutSdk.Core;
    using PayPalCheckoutSdk.Orders;
    using PayPalCheckoutSdk.Payments;
    using FeePay.Infrastructure.PayPal.Interfaces;
    using FeePay.Infrastructure.PayPal.Enums;

    internal class PayPalClient : IPayPalClient
    {
        private readonly ISettings _settings;
        private readonly IValueObject _valueObject;
        public PayPalClient(ISettings settings,
            IValueObject valueObject)
        {
            _settings = settings;
            _valueObject = valueObject;
        }

        /// <summary>
        /// Set up PayPal environment with paypal credentials
        /// </summary>
        /// <returns>PayPal environment</returns>
        public PayPalEnvironment Environment()
        {
            // You may want to create a UAT (user acceptance tester) 
            // role and check for this:
            // "if(await _unitOfWork.GetPaypalSetting(SchoolCode))" instead of setting directives.
            return new SandboxEnvironment(_settings.ClientId, _settings.ClientSecret);
        }

        /// <summary>
        /// Create PayPalHttpClient instance to invoke PayPal APIs.
        /// </summary>
        /// <returns>PayPalHttpClient instance.</returns>
        public PayPalHttpClient Client()
        {
            return new PayPalHttpClient(Environment());
        }

        /// <summary>
        /// Create PayPalHttpClient instance to invoke PayPal APIs.
        /// </summary>
        /// <param name="refreshToken"> RefreshToken of new request. </param>
        /// <returns>PayPalHttpClient instance.</returns>
        public PayPalHttpClient Client(string refreshToken)
        {
            return new PayPalHttpClient(Environment(), refreshToken);
        }

        /// <summary>
        /// Serialize Object to a JSON string.
        /// </summary>
        /// <param name="serializableObject"> Object to String </param>
        /// <returns>Serialized JSON string</returns>
        public string ObjectToJSONString(object serializableObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            var writer = JsonReaderWriterFactory.CreateJsonWriter(memoryStream, Encoding.UTF8, true, true, "  ");

            var ser = new DataContractJsonSerializer(
                serializableObject.GetType(),
                new DataContractJsonSerializerSettings
                {
                    UseSimpleDictionaryFormat = true
                });

            ser.WriteObject(writer, serializableObject);

            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);

            return sr.ReadToEnd();
        }

        public ApplicationContext GetPaypalApplicationContext()
        {
            return new ApplicationContext
            {
                LandingPage = nameof(LandingPage.LOGIN),
                UserAction = nameof(UserAction.PAY_NOW),
                ShippingPreference = nameof(ShippingPreference.NO_SHIPPING),
                Locale = _valueObject.LocalCodeApi.India,

                BrandName = "",
                ReturnUrl ="",
                CancelUrl=""
            };
        }
    }
}
