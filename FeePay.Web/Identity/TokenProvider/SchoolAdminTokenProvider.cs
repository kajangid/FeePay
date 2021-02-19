using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FeePay.Infrastructure.Identity.TokenProvider
{
    public class SchoolAdminEmailConfirmTokenProvider<SchoolAdminUser> :
        DataProtectorTokenProvider<SchoolAdminUser>
        where SchoolAdminUser : class
    {
        public SchoolAdminEmailConfirmTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<SchoolAdminEmailConfirmTokenProviderOptions> options,
            ILogger<SchoolAdminEmailConfirmTokenProvider<SchoolAdminUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {
        }
    }

    public class SchoolAdminResetPasswordTokenProvider<SchoolAdminUser> :
        DataProtectorTokenProvider<SchoolAdminUser>
        where SchoolAdminUser : class
    {
        public SchoolAdminResetPasswordTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<SchoolAdminResetPasswordTokenProviderOptions> options,
            ILogger<SchoolAdminResetPasswordTokenProvider<SchoolAdminUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {
        }
    }
}
