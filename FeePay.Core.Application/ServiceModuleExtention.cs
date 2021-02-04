using System;
using AutoMapper;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FeePay.Core.Application.Mapping;
using FeePay.Core.Application.UseCase;
using FeePay.Core.Application.Services;
using FeePay.Core.Application.Services.School;
using FeePay.Core.Application.Services.Student;
using FeePay.Core.Application.Services.SuperAdmin;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Core.Application.Interface.Service.Student;
using FeePay.Core.Application.Interface.Service.SuperAdmin;

namespace FeePay.Core.Application
{
    public static class ServiceModuleExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionStringConfig>
                (configuration.GetSection("ConnectionStringsConfigration").Get<ConnectionStringConfig>());
            services.AddSingleton<IConnectionStringBuilder, ConnectionStringBuilder>();
            services.AddSingleton<ICommanDataServices, CommanDataServices>();
            services.AddSingleton<IPasswordGenerator, PasswordGenerator>();
            services.AddSingleton<ICultureFormatProvider, CultureFormatProvider>();

            services.AddTransient<IAppContextAccessor, AppContextAccessor>();

            //School 
            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<IFeeManagementService, FeeManagementService>();
            services.AddScoped<IAcademicServices, AcademicServices>();
            services.AddScoped<IStudentManagementService, StudentManagementService>();
            services.AddScoped<IStudentFeeDepositManagerService, StudentFeeDepositManagerService>();


            //Super Admin
            services.AddScoped<ISchoolsManagerServices, SchoolsManagerServices>();
            services.AddScoped<IAdministrativeServices, AdministrativeServices>();



            // Auto Mapper Configurations  
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CustomerProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);



            return services;
        }
    }
}
