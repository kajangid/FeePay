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
        #region Execute
        Task<int> AddAsync(Section section, string dbId);
        Task<int> UpdateAsync(Section section, string dbId);
        Task<int> DeleteAsync(int id, string dbId);
        #endregion

        #region Find
        Task<Section> FindByIdAsync(int id, string dbId, bool? isActive = null);
        Task<Section> FindByNameAsync(string name, string dbId, bool? isActive = null);
        #endregion

        #region Get All
        Task<IEnumerable<Section>> GetAllAsync(string dbId, bool? isActive = null);
        Task<IEnumerable<Section>> GetAll_WithAddEditUserAsync(string dbId, bool? isActive = null);
        #endregion

    }
}
