using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FeePay.Infrastructure.Identity
{
    public static class CustomIdentityServicesProvider
    {
        // Second
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

        // Third
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
    }
}
