using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface IClassRepository
    {
        Task<int> AddAsync(Classes classes, string dbId = null);
        Task<int> UpdateAsync(Classes classes, string dbId = null);
        Task<int> DeleteAsync(int Id, string dbId = null);

        // Find
        Task<Classes> FindByIdAsync(int Id, string dbId = null);
        Task<Classes> FindByNameAsync(string Name, string dbId = null);
        Task<Classes> FindActiveByIdAsync(int Id, string dbId = null);
        Task<Classes> FindActiveByNameAsync(string Name, string dbId = null);

        // Get All
        Task<IEnumerable<Classes>> GetAllAsync(string dbId = null);
        Task<IEnumerable<Classes>> GetAllActiveAsync(string dbId = null);
        Task<IEnumerable<Classes>> GetAll_WithAddEditUserAsync(string dbId = null);
        Task<IEnumerable<Classes>> GetAllActive_WithAddEditUserAsync(string dbId = null);


    }
}
