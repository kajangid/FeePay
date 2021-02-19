using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.App;
using Microsoft.Extensions.Options;

namespace FeePay.Infrastructure.Identity.TokenProvider
{
    public class SuperAdminTokenProvider<SuperAdminUser> :
        DataProtectorTokenProvider<SuperAdminUser>
        where SuperAdminUser : class
    {
        public SuperAdminTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<CustomTokenProviderOptions> options)
            : base(dataProtectionProvider, options)
        {
        }
    }
}
