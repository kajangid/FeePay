using System;
using System.Configuration;
using FeePay.Core.Application.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FeePay.Core.Application.UseCase;
using FeePay.Core.Application.Services;
using FeePay.Core.Application.Interface.Service;

namespace FeePay.Core.Application
{
    public static class ServiceModuleExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionStringConfig>
                (configuration.GetSection("ConnectionStringsConfigration").Get<ConnectionStringConfig>());
            services.AddSingleton<IConnectionStringBuilder, ConnectionStringBuilder>();

            services.AddTransient<IAppContextAccessor, AppContextAccessor>();

            return services;
        }
    }
}
