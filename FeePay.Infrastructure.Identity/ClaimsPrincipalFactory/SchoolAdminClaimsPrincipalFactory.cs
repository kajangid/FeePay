using FeePay.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Infrastructure.Identity.ClaimsPrincipalFactory
{
    public class SchoolAdminClaimsPrincipalFactory : UserClaimsPrincipalFactory<SchoolAdminUser>
    {
        public SchoolAdminClaimsPrincipalFactory(UserManager<SchoolAdminUser> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor) { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(SchoolAdminUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("SchoolAuthRoute", "", "", "SchoolAdmin"));
            identity.AddClaim(new Claim("SchoolRoutePath", user.SchoolName ?? "", "school name", "SchoolAdmin"));
            identity.AddClaim(new Claim("SchoolUniqueId", user.SchoolUniqueId ?? "", "school_id", "SchoolAdmin"));
            return identity;
        }
    }
}
