using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Core.Application.Interface.Service.Student;
using FeePay.Core.Application.Interface.Service.SuperAdmin;
using FeePay.Infrastructure.Identity;
using FeePay.Infrastructure.Identity.Service;
using FeePay.Infrastructure.Identity.IdentityStore;
using FeePay.Infrastructure.Identity.ClaimsPrincipalFactory;
using FeePay.Infrastructure.Identity.TokenProvider;

namespace FeePay.Web.IoC
{
    public static class IdentityServiceExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<SuperAdminUser, SuperAdminRole>()
                .AddClaimsPrincipalFactory<SuperAdminClaimsPrincipalFactory>()
                .AddDefaultTokenProviders()
                .AddSuperAdminEmailConfirmTokenProvider()
                .AddSuperAdminResetPasswordTokenProvider();

            services.AddSecondIdentity<SchoolAdminUser, SchoolAdminRole>()
                .AddClaimsPrincipalFactory<SchoolAdminClaimsPrincipalFactory>()
                .AddDefaultTokenProviders()
                .AddSchoolAdminEmailConfirmTokenProvider()
                .AddSchoolAdminResetPasswordTokenProvider();

            services.AddThirdIdentity<StudentLogin>()
                .AddClaimsPrincipalFactory<StudentClaimsPrincipalFactory>()
                .AddDefaultTokenProviders()
                .AddStudentLoginEmailConfirmTokenProvider()
                .AddStudentLoginResetPasswordTokenProvider();

            // identity stores
            services.AddTransient<IUserStore<SuperAdminUser>, SuperAdminUserStore>();
            services.AddTransient<IRoleStore<SuperAdminRole>, SuperAdminRoleStore>();
            services.AddTransient<IUserStore<SchoolAdminUser>, SchoolAdminUserStore>();
            services.AddTransient<IRoleStore<SchoolAdminRole>, SchoolAdminRoleStore>();
            services.AddTransient<IUserStore<StudentLogin>, StudentLoginStore>();

            //login services
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ISchoolAdminRegistrationService, SchoolAdminRegistrationService>();
            services.AddScoped<IStudentRegistrationService, StudentRegistrationService>();
            services.AddScoped<ISuperAdminRegistrationService, SuperAdminRegistrationService>();

            // TODO on all Identity validation
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;
                // SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "#Cookie_955#649";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.LoginPath = "/Student/Login";
                options.LogoutPath = "/Student/Logout";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
            return services;
        }
    }
}
