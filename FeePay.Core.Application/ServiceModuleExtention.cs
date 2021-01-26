using System;
using AutoMapper;
using System.Configuration;
using FeePay.Core.Application.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FeePay.Core.Application.UseCase;
using FeePay.Core.Application.Services;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Services.School;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Core.Application.Mapping;
using FeePay.Core.Application.Interface.Service.Student;
using FeePay.Core.Application.Services.Student;

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

            services.AddTransient<IAppContextAccessor, AppContextAccessor>();
            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<IFeeManagementService, FeeManagementService>();
            services.AddScoped<IAcademicServices, AcademicServices>();
            services.AddScoped<IStudentManagementService, StudentManagementService>();




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
