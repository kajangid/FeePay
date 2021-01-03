using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace FeePay.Core.Domain
{
    public static class ServiceModuleExtention
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }
    }
}
