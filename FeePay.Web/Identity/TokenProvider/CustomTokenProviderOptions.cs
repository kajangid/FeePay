using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FeePay.Infrastructure.Identity.TokenProvider
{
    public class CustomTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public CustomTokenProviderOptions()
        {
            // update the defaults
            Name = "PasswordlessLoginTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(15);
        }
    }
    public class SuperAdminEmailConfirmTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public SuperAdminEmailConfirmTokenProviderOptions()
        {
            // update the defaults
            Name = "SuperAdminEmailConfirmTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(15);
        }
    }
    public class SchoolAdminEmailConfirmTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public SchoolAdminEmailConfirmTokenProviderOptions()
        {
            // update the defaults
            Name = "SchoolAdminEmailConfirmTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(15);
        }
    }
    public class StudentLoginEmailConfirmTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public StudentLoginEmailConfirmTokenProviderOptions()
        {
            // update the defaults
            Name = "StudentLoginEmailConfirmTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(15);
        }
    }
    public class SuperAdminResetPasswordTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public SuperAdminResetPasswordTokenProviderOptions()
        {
            // update the defaults
            Name = "SuperAdminResetPasswordTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(15);
        }
    }
    public class StudentLoginResetPasswordTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public StudentLoginResetPasswordTokenProviderOptions()
        {
            // update the defaults
            Name = "StudentLoginResetPasswordTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(15);
        }
    }
    public class SchoolAdminResetPasswordTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public SchoolAdminResetPasswordTokenProviderOptions()
        {
            // update the defaults
            Name = "SchoolAdminResetPasswordTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(15);
        }
    }
}
