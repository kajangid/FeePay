using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.App;
using Microsoft.Extensions.Options;

namespace FeePay.Infrastructure.Identity.TokenProvider
{
    public class SuperAdminEmailConfirmTokenProvider<SuperAdminUser> :
        DataProtectorTokenProvider<SuperAdminUser>
        where SuperAdminUser : class
    {
        public SuperAdminEmailConfirmTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<SuperAdminEmailConfirmTokenProviderOptions> options,
            ILogger<SuperAdminEmailConfirmTokenProvider<SuperAdminUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {
        }
    }
    public class SuperAdminResetPasswordTokenProvider<SuperAdminUser> :
        DataProtectorTokenProvider<SuperAdminUser>
        where SuperAdminUser : class
    {
        public SuperAdminResetPasswordTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<SuperAdminResetPasswordTokenProviderOptions> options,
            ILogger<SuperAdminResetPasswordTokenProvider<SuperAdminUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {
        }
    }
}
