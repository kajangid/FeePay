using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.SuperAdmin;

namespace FeePay.Core.Application.Interface.Repository.SuperAdmin
{
    public interface IRegisteredSchoolRepository
    {
        Task<int> AddAsync(RegisteredSchool school);
        Task<int> UpdateAsync(RegisteredSchool school);
        Task<int> DeleteAsync(int Id);
        Task<RegisteredSchool> FindByNameAsync(string normalizedName, bool? isActive = null);
        Task<RegisteredSchool> FindByIdAsync(int schoolId, bool? isActive = null);
        Task<RegisteredSchool> FindByUniqueIdAsync(string schoolUniqueID, bool? isActive = null);
        Task<IEnumerable<RegisteredSchool>> GetAllAsync(bool? isActive = null);
        Task<IEnumerable<RegisteredSchool>> GetAll_WithAddEditUserAsync(bool? isActive = null);

    }
}
