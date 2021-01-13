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
        Task<IList<SchoolAdminRole>> GetUserRolesAsync(int userId, string dbId = null);
        Task<int> UserInRoleAsync(int userId, string roleName, string dbId = null);
        Task<IList<SchoolAdminUser>> GetUsersInRoleAsync(string roleName, string dbId = null);
        Task<IList<SchoolAdminUser>> GetUsersInRoleAsync(int roleID, string dbId = null);




        Task<bool> delete(int Id, string dbId = null);
        Task<bool> delete(int userId, int roleId, string dbId = null);
        Task<bool> delete(string userName, int roleId, string dbId = null);
        Task<bool> delete(int userId, string roleName, string dbId = null);
    }
}
