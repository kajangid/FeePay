using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface ISchoolAdminUserRepository
    {
        Task<int> AddUserAsync(SchoolAdminUser user, string dbId = null);
        Task<int> UpdateUserAsync(SchoolAdminUser user, string dbId = null);
        Task<int> DeleteUserAsync(int Id, string dbId = null);
        Task<SchoolAdminUser> FindUserByUserIdAsync(int userId, string dbId = null);
        Task<SchoolAdminUser> FindUserByUserNameAsync(string normalizedUserName, string dbId = null);
        Task<SchoolAdminUser> FindUserByUserEmailAsync(string normalizedEmail, string dbId = null);
        Task<SchoolAdminUser> FindActiveUserByUserIdAsync(int userId, string dbId = null);
        Task<SchoolAdminUser> FindActiveUserByUserNameAsync(string normalizedUserName, string dbId = null);
        Task<SchoolAdminUser> FindActiveUserByUserEmailAsync(string normalizedEmail, string dbId = null);
        Task<IList<SchoolAdminUser>> FindAllActiveUserAsync(string dbId = null);
        Task UpdateLoginState(int userId, string Ip, string dbId = null);
    }
}
