using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.SuperAdmin
{
    public interface ISuperAdminUserRoleRepository
    {
        Task<int> AssignRoleToUserAsync(SuperAdminUser user, string roleName, string dbId = null);
        Task<int> UnassignUserFromRoleAsync(SuperAdminUser user, string roleName, string dbId = null);
        Task<IList<SuperAdminRole>> GetUserRolesAsync(SuperAdminUser user, string dbId = null);
        Task<int> UserInRoleAsync(SuperAdminUser user, string roleName, string dbId = null);
        Task<IList<SuperAdminUser>> GetUsersInRoleAsync(string roleName, string dbId = null);
    }
}
