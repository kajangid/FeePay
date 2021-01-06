using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface ISchoolAdminUserRoleRepository
    {
        Task<int> AssignRoleToUserAsync(SchoolAdminUser user, string roleName, string dbId = null);
        Task<int> UnassignUserFromRoleAsync(SchoolAdminUser user, string roleName, string dbId = null);
        Task<IList<SchoolAdminRole>> GetUserRolesAsync(SchoolAdminUser user, string dbId = null);
        Task<int> UserInRoleAsync(SchoolAdminUser user, string roleName, string dbId = null);
        Task<IList<SchoolAdminUser>> GetUsersInRoleAsync(string roleName, string dbId = null);
    }
}
