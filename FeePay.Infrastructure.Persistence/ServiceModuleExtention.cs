using System;
using Microsoft.Extensions.DependencyInjection;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Repository.School;
using FeePay.Core.Application.Interface.Repository.Student;
using FeePay.Core.Application.Interface.Repository.SuperAdmin;
using FeePay.Infrastructure.Persistence.Common;
using FeePay.Infrastructure.Persistence.School;
using FeePay.Infrastructure.Persistence.Student;
using FeePay.Infrastructure.Persistence.SuperAdmin;

namespace FeePay.Infrastructure.Persistence
{
    public static class ServiceModuleExtention
    {
        public static IServiceCollection AddInfrastructurePersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IDBVariables, DBVariables>();

            services.AddScoped<ISuperAdminUserRepository, SuperAdminUserRepository>();
            services.AddScoped<ISuperAdminRoleRepository, SuperAdminRoleRepository>();
            services.AddScoped<ISuperAdminUserRoleRepository, SuperAdminUserRoleRepository>();
            services.AddScoped<ISchoolAdminUserRepository, SchoolAdminUserRepository>();
            services.AddScoped<ISchoolAdminRoleRepository, SchoolAdminRoleRepository>();
            services.AddScoped<ISchoolAdminUserRoleRepository, SchoolAdminUserRoleRepository>();
            services.AddScoped<IStudentLoginRepository, StudentLoginRepository>();

            services.AddScoped<IRegisteredSchoolRepository, RegisteredSchoolRepository>();
            services.AddScoped<IFeeTypeRepository, FeeTypeRepository>();
            services.AddScoped<IFeeGroupRepository, FeeGroupRepository>();
            services.AddScoped<IFeeMasterRespository, FeeMasterRespository>();

            services.AddScoped<IClassRepository, ClassRepository>(); 
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IClassSectionRepository, ClassSectionRepository>(); 
            services.AddScoped<ISessionRepository, SessionRepository>();





            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
