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
        UserManager<SuperAdminUser> _userManager;
        public SuperAdminClaimsPrincipalFactory(UserManager<SuperAdminUser> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor) { _userManager = userManager; }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(SuperAdminUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("SuperAdminAuthRoute", string.Empty, string.Empty, "SuperAdmin"));
            var Roles = (await _userManager.GetRolesAsync(user))?.ToList();
            Roles.ForEach(f =>
            {
                identity.AddClaim(new Claim("Role", f.Trim(), "role_name", "superadmin"));
            });
            return identity;
        }
    }
}
