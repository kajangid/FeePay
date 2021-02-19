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
}
