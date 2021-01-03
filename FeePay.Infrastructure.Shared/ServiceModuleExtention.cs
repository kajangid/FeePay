using System;
using Microsoft.Extensions.DependencyInjection;

namespace FeePay.Infrastructure.Shared
{
    public static class ServiceModuleExtention
    {
        public static IServiceCollection AddInfrastructureSharedServices(this IServiceCollection services)
        {

            return services;
        }
    }
}
