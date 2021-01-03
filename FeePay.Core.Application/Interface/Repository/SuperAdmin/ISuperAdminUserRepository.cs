using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.SuperAdmin
{
    public interface ISuperAdminUserRepository
    {
        Task<int> AddUserAsync(SuperAdminUser user, string dbId = null);
        Task<int> UpdateUserAsync(SuperAdminUser user, string dbId = null);
        Task<int> DeleteUserAsync(int Id, string dbId = null);
        Task<SuperAdminUser> FindUserByUserIdAsync(int userId, string dbId = null);
        Task<SuperAdminUser> FindUserByUserNameAsync(string normalizedUserName, string dbId = null);
        Task<SuperAdminUser> FindUserByUserEmailAsync(string normalizedEmail, string dbId = null);
        Task<SuperAdminUser> FindActiveUserByUserIdAsync(int userId, string dbId = null);
        Task<SuperAdminUser> FindActiveUserByUserNameAsync(string normalizedUserName, string dbId = null);
        Task<SuperAdminUser> FindActiveUserByUserEmailAsync(string normalizedEmail, string dbId = null);
    }
}
