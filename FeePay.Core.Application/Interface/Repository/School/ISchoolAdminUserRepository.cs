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


        Task<IEnumerable<SchoolAdminUser>> FindAllActiveUserAsync(string dbId = null);
        Task<IEnumerable<SchoolAdminUser>> FindAllUserAsync(string dbId = null);



        Task UpdateLoginState(int userId, string Ip, string dbId = null);



        Task<SchoolAdminUser> FindUserByUserId_WithAddEditUserAsync(int userId, string dbId = null);
        Task<SchoolAdminUser> FindUserByUserName_WithAddEditUserAsync(string normalizedUserName, string dbId = null);
        Task<SchoolAdminUser> FindUserByUserEmail_WithAddEditUserAsync(string normalizedEmail, string dbId = null);
        Task<SchoolAdminUser> FindActiveUserByUserId_WithAddEditUserAsync(int userId, string dbId = null);
        Task<SchoolAdminUser> FindActiveUserByUserName_WithAddEditUserAsync(string normalizedUserName, string dbId = null);
        Task<SchoolAdminUser> FindActiveUserByUserEmail_WithAddEditUserAsync(string normalizedEmail, string dbId = null);

        Task<IEnumerable<SchoolAdminUser>> FindAllActiveUser_WithAddEditUserAsync(string dbId = null);
        Task<IEnumerable<SchoolAdminUser>> FindAllUser_WithAddEditUserAsync(string dbId = null);
    }
}
