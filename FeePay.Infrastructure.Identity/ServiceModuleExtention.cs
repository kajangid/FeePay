using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Infrastructure.Identity.ClaimsPrincipalFactory;
using FeePay.Infrastructure.Identity.IdentityStore;

namespace FeePay.Infrastructure.Identity
{
    public static class ServiceModuleExtention
    {
        public static IdentityBuilder AddSecondIdentity<TUser, TRole>(this IServiceCollection services)
            where TUser : class
            where TRole : class
               => services.AddSecondIdentity<TUser, TRole>(o => { });
        public static IdentityBuilder AddSecondIdentity<TUser, TRole>(
            this IServiceCollection services, Action<IdentityOptions> setupAction)
            where TUser : class
            where TRole : class
        {
            services.TryAddScoped<IUserValidator<TUser>, UserValidator<TUser>>();
            services.TryAddScoped<IPasswordValidator<TUser>, PasswordValidator<TUser>>();
            services.TryAddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
            services.TryAddScoped<IRoleValidator<TRole>, RoleValidator<TRole>>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<TUser>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<TUser>, UserClaimsPrincipalFactory<TUser, TRole>>();
            services.TryAddScoped<UserManager<TUser>, AspNetUserManager<TUser>>();
            services.TryAddScoped<IUserConfirmation<TUser>, DefaultUserConfirmation<TUser>>();
            services.TryAddScoped<SignInManager<TUser>, SignInManager<TUser>>();
            services.TryAddScoped<RoleManager<TRole>, AspNetRoleManager<TRole>>();

            if (setupAction != null)
                services.Configure(setupAction);

            return new IdentityBuilder(typeof(TUser), typeof(TRole), services);
        }

        // third
        public static IdentityBuilder AddThirdIdentity<TUser>(this IServiceCollection services)
            where TUser : class
               => services.AddThirdIdentity<TUser>(o => { });
        public static IdentityBuilder AddThirdIdentity<TUser>(
            this IServiceCollection services, Action<IdentityOptions> setupAction)
            where TUser : class
        {
            services.TryAddScoped<IUserValidator<TUser>, UserValidator<TUser>>();
            services.TryAddScoped<IPasswordValidator<TUser>, PasswordValidator<TUser>>();
            services.TryAddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<TUser>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<TUser>, UserClaimsPrincipalFactory<TUser>>();
            services.TryAddScoped<UserManager<TUser>, AspNetUserManager<TUser>>();
            services.TryAddScoped<IUserConfirmation<TUser>, DefaultUserConfirmation<TUser>>();
            services.TryAddScoped<SignInManager<TUser>, SignInManager<TUser>>();

            if (setupAction != null)
                services.Configure(setupAction);

            return new IdentityBuilder(typeof(TUser), services);
        }
        public static IServiceCollection AddInfrastructureIdentityService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IUserStore<SuperAdminUser>, SuperAdminUserStore>();
            services.AddTransient<IRoleStore<SuperAdminRole>, SuperAdminRoleStore>();
            services.AddTransient<IUserStore<SchoolAdminUser>, SchoolAdminUserStore>();
            services.AddTransient<IRoleStore<SchoolAdminRole>, SchoolAdminRoleStore>();
            services.AddTransient<IUserStore<StudentLogin>, StudentLoginStore>();

            services.AddIdentity<SuperAdminUser, SuperAdminRole>()
                .AddClaimsPrincipalFactory<SuperAdminClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();
            services.AddSecondIdentity<SchoolAdminUser, SchoolAdminRole>()
                .AddClaimsPrincipalFactory<SchoolAdminClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();
            services.AddThirdIdentity<StudentLogin>()
                .AddClaimsPrincipalFactory<StudentClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();

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
            });

            services.ConfigureApplicationCookie(options =>
            {
                //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "9#778987_Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
                options.LoginPath = "/Student/Login";
                options.LogoutPath = "/Student/Logout";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });




            return services;
        }
    }
}
