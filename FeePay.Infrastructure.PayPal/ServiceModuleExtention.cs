using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FeePay.Infrastructure.PayPal.Interfaces;
using FeePay.Infrastructure.PayPal.Services;

namespace FeePay.Infrastructure.PayPal
{
    public static class ServiceModuleExtention
    {
        public static IServiceCollection AddPaypalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ISettings>(configuration.GetSection("Paypal:settings").Get<Settings>());
            services.AddSingleton<IValueObject, ValueObject>();
            services.AddTransient<IPayPalClient, PayPalClient>();

            return services;
        }

    }
}
