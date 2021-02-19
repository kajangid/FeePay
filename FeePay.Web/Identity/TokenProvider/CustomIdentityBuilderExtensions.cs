using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FeePay.Infrastructure.Identity.TokenProvider
{
    public static class CustomIdentityBuilderExtensions
    { 
        // EmailConfirm
        public static IdentityBuilder AddSuperAdminEmailConfirmTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(SuperAdminEmailConfirmTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("SuperAdminEmailConfirmTokenProvider", provider);
        }
        public static IdentityBuilder AddSchoolAdminEmailConfirmTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(SchoolAdminEmailConfirmTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("SchoolAdminEmailConfirmTokenProvider", provider);
        }
        public static IdentityBuilder AddStudentLoginEmailConfirmTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(StudentEmailConfirmTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("StudentLoginEmailConfirmTokenProvider", provider);
        }

        // ResetPassword
        public static IdentityBuilder AddSuperAdminResetPasswordTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(SuperAdminResetPasswordTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("SuperAdminResetPasswordTokenProvider", provider);
        }
        public static IdentityBuilder AddSchoolAdminResetPasswordTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(SchoolAdminResetPasswordTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("SchoolAdminResetPasswordTokenProvider", provider);
        }
        public static IdentityBuilder AddStudentLoginResetPasswordTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(StudentResetPasswordTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("StudentLoginResetPasswordTokenProvider", provider);
        }
    }
}
