using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FeePay.Infrastructure.Identity.TokenProvider
{
    public class SchoolAdminTokenProvider<SchoolAdminUser> :
        DataProtectorTokenProvider<SchoolAdminUser>
        where SchoolAdminUser : class
    {
        public SchoolAdminTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<CustomTokenProviderOptions> options)
            : base(dataProtectionProvider, options)
        {
        }
    }
}
