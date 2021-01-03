using System;
using System.Configuration;
using FeePay.Core.Application.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FeePay.Core.Application.UseCase;

namespace FeePay.Core.Application
{
    public static class ServiceModuleExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionStringConfig>
                (configuration.GetSection("ConnectionStringsConfigration").Get<ConnectionStringConfig>());
            services.AddSingleton<IConnectionStringBuilder, ConnectionStringBuilder>();



            return services;
        }
    }
}
