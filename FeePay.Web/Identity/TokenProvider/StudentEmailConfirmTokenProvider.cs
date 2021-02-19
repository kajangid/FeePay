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
    public class StudentEmailConfirmTokenProvider<StudentLogin> :
        DataProtectorTokenProvider<StudentLogin>
        where StudentLogin : class
    {
        public StudentEmailConfirmTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<StudentLoginEmailConfirmTokenProviderOptions> options,
            ILogger<StudentEmailConfirmTokenProvider<StudentLogin>> logger)
            : base(dataProtectionProvider, options, logger)
        {
        }
    }
    public class StudentResetPasswordTokenProvider<StudentLogin> :
        DataProtectorTokenProvider<StudentLogin>
        where StudentLogin : class
    {
        public StudentResetPasswordTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<StudentLoginResetPasswordTokenProviderOptions> options,
            ILogger<StudentResetPasswordTokenProvider<StudentLogin>> logger)
            : base(dataProtectionProvider, options, logger)
        {
        }
    }
}
