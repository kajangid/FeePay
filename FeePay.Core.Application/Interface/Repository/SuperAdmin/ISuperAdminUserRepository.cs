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
        Task<int> AddAsync(SuperAdminUser user);
        Task<int> UpdateAsync(SuperAdminUser user);
        Task<int> DeleteAsync(int Id);

        // Find
        Task<SuperAdminUser> FindByIdAsync(int userId, bool? isActive = null);
        Task<SuperAdminUser> FindByUserNameAsync(string normalizedUserName, bool? isActive = null);
        Task<SuperAdminUser> FindByEmailAsync(string normalizedEmail, bool? isActive = null);

        // Get
        Task<IEnumerable<SuperAdminUser>> GetAllAsync(bool? isActive = null);
        Task<IEnumerable<SuperAdminUser>> GetAll_WithAddEditUserAsync(bool? isActive = null);

        // Search
        /// <summary>
        /// Search data in NormalizedUserName,NormalizedEmail,PhoneNumber,FirstName,LastName
        /// </summary>
        /// <param name="searchParam"> search string </param>
        /// <param name="isActive">active[true]/inactive[false]/all[null]</param>
        /// <returns> List of Super admin user data</returns>
        Task<IEnumerable<SuperAdminUser>> Search_WithAddEdirUserAsync(string searchParam, bool? isActive = null);


        // Misc
        Task UpdateLoginState(int userId, string Ip);
        Task<SuperAdminUser> FindPasswordByIdAsync(int id, bool? isActive = null);
    }
}
