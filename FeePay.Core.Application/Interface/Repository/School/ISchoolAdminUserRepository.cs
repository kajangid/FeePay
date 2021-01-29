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
        Task<int> AddAsync(SchoolAdminUser user, string dbId = null);
        Task<int> UpdateAsync(SchoolAdminUser user, string dbId = null);
        Task<int> DeleteAsync(int Id, string dbId = null);



        Task<SchoolAdminUser> FindByIdAsync(int userId, string dbId = null);
        Task<SchoolAdminUser> FindByUserNameAsync(string normalizedUserName, string dbId = null);
        Task<SchoolAdminUser> FindByEmailAsync(string normalizedEmail, string dbId = null);
        Task<SchoolAdminUser> FindActiveByIdAsync(int userId, string dbId = null);
        Task<SchoolAdminUser> FindActiveByUserNameAsync(string normalizedUserName, string dbId = null);
        Task<SchoolAdminUser> FindActiveByEmailAsync(string normalizedEmail, string dbId = null);


        Task<IEnumerable<SchoolAdminUser>> FindAllActiveAsync(string dbId = null);
        Task<IEnumerable<SchoolAdminUser>> FindAllAsync(string dbId = null);



        Task UpdateLoginState(int userId, string Ip, string dbId = null);



        Task<SchoolAdminUser> FindById_WithAddEditUserAsync(int userId, string dbId = null);
        Task<SchoolAdminUser> FindByUserName_WithAddEditUserAsync(string normalizedUserName, string dbId = null);
        Task<SchoolAdminUser> FindByEmail_WithAddEditUserAsync(string normalizedEmail, string dbId = null);
        Task<SchoolAdminUser> FindActiveById_WithAddEditUserAsync(int userId, string dbId = null);
        Task<SchoolAdminUser> FindActiveByUserName_WithAddEditUserAsync(string normalizedUserName, string dbId = null);
        Task<SchoolAdminUser> FindActiveByEmail_WithAddEditUserAsync(string normalizedEmail, string dbId = null);

        Task<IEnumerable<SchoolAdminUser>> FindAllActive_WithAddEditUserAsync(string dbId = null);
        Task<IEnumerable<SchoolAdminUser>> FindAll_WithAddEditUserAsync(string dbId = null);

        Task<SchoolAdminUser> FindUserPasswordByIdAsync(string dbId, int id, bool? isActive = null);
        Task<IEnumerable<SchoolAdminUser>> GetAllUserPasswordAsync(string dbId, bool? isActive = null);
    }
}
