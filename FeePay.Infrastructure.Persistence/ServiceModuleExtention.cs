using System;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Repository.SuperAdmin;
using FeePay.Infrastructure.Persistence.Common;
using FeePay.Infrastructure.Persistence.SuperAdmin;
using Microsoft.Extensions.DependencyInjection;

namespace FeePay.Infrastructure.Persistence
{
    public static class ServiceModuleExtention
    {
        public static IServiceCollection AddInfrastructurePersistenceServices(this IServiceCollection services)
        {
            services.AddSingleton<IDBVariables, DBVariables>();
            services.AddSingleton<ISuperAdminUserRepository, SuperAdminUserRepository>();
            services.AddSingleton<ISuperAdminRoleRepository, SuperAdminRoleRepository>();
            services.AddSingleton<ISuperAdminUserRoleRepository, SuperAdminUserRoleRepository>();




            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
