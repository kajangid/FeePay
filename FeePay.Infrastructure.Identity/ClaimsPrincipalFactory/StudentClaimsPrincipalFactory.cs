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
    public class StudentClaimsPrincipalFactory : UserClaimsPrincipalFactory<StudentLogin>
    {
        public StudentClaimsPrincipalFactory(UserManager<StudentLogin> userManager,IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor) { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(StudentLogin user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("StudentAuthRoute", "", "", "Student"));
            identity.AddClaim(new Claim("SchoolUniqueId", user.SchoolUniqueId ?? "", "school_id", "Student"));
            return identity;
        }
    }
}
