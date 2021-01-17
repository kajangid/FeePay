using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface ISchoolAdminRoleRepository
    {
        Task<int> AddAsync(SchoolAdminRole Role, string dbId = null);
        Task<int> UpdateAsync(SchoolAdminRole Role, string dbId = null);
        Task<int> DeleteAsync(int Id, string dbId = null);

        Task<SchoolAdminRole> FindByIdAsync(int roleId, string dbId = null);
        Task<SchoolAdminRole> FindByNameAsync(string normalizedName, string dbId = null);
        Task<SchoolAdminRole> FindActiveByIdAsync(int roleId, string dbId = null);
        Task<SchoolAdminRole> FindActiveByNameAsync(string normalizedName, string dbId = null);
        Task<IEnumerable<SchoolAdminRole>> GetAllActiveAsync(string dbId = null);
        Task<IEnumerable<SchoolAdminRole>> GetAllAsync(string dbId = null);

        Task<SchoolAdminRole> FindById_WithAddEditUserAsync(int roleId, string dbId = null);
        Task<SchoolAdminRole> FindByName_WithAddEditUserAsync(string normalizedName, string dbId = null);
        Task<SchoolAdminRole> FindActiveById_WithAddEditUserAsync(int roleId, string dbId = null);
        Task<SchoolAdminRole> FindActiveByName_WithAddEditUserAsync(string normalizedName, string dbId = null);
        Task<IEnumerable<SchoolAdminRole>> GetAllActive_WithAddEditUserAsync(string dbId = null);
        Task<IEnumerable<SchoolAdminRole>> GetAll_WithAddEditUserAsync(string dbId = null);
    }
}
