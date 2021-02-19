using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Infrastructure.Identity.TokenProvider
{
    public static class CustomIdentityBuilderExtensions
    {
        public static IdentityBuilder AddSuperAdminLoginTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(SuperAdminTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("AddSuperAdminLoginTokenProvider", provider);
        }
        public static IdentityBuilder AddSchoolAdminLoginTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(SchoolAdminTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("AddSchoolAdminLoginTokenProvider", provider);
        }
        public static IdentityBuilder AddStudentLoginTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(StudentLoginTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("AddStudentLoginTokenProvider", provider);
        }
    }
}
