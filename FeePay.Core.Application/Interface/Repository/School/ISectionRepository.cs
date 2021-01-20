using FeePay.Core.Domain.Entities.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface ISectionRepository
    {
        Task<int> AddAsync(Section section, string dbId = null);
        Task<int> UpdateAsync(Section section, string dbId = null);
        Task<int> DeleteAsync(int Id, string dbId = null);

        // Find
        Task<Section> FindByIdAsync(int Id, string dbId = null);
        Task<Section> FindByNameAsync(string Name, string dbId = null);
        Task<Section> FindActiveByIdAsync(int Id, string dbId = null);
        Task<Section> FindActiveByNameAsync(string Name, string dbId = null);

        // Get All
        Task<IEnumerable<Section>> GetAllAsync(string dbId = null);
        Task<IEnumerable<Section>> GetAllActiveAsync(string dbId = null);
        Task<IEnumerable<Section>> GetAll_WithAddEditUserAsync(string dbId = null);
        Task<IEnumerable<Section>> GetAllActive_WithAddEditUserAsync(string dbId = null);

    }
}
