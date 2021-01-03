using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FeePay.Infrastructure.Identity.ClaimsPrincipalFactory
{
    public class SuperAdminClaimsPrincipalFactory : UserClaimsPrincipalFactory<SuperAdminUser>
    {
        public SuperAdminClaimsPrincipalFactory(UserManager<SuperAdminUser> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor) { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(SuperAdminUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("SuperAdminAuthRoute", "", "", "SuperAdmin"));
            return identity;
        }
    }
}
