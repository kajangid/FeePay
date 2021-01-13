using System;
using System.Configuration;
using FeePay.Core.Application.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FeePay.Core.Application.UseCase;
using FeePay.Core.Application.Services;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Services.School;
using FeePay.Core.Application.Interface.Service.School;
using AutoMapper;
using FeePay.Core.Application.Mapping;

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
            services.AddScoped<IAdministrationService, AdministrationService>();




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
