using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface IFeeMasterRespository
    {
        #region Execute
        Task<int> AddAsync(FeeMaster feeMaster, string dbId);
        Task<int> UpdateAsync(FeeMaster feeMaster, string dbId);
        Task<int> DeleteAsync(int id, int? changeBy, string dbId);
        #endregion

        #region Find
        Task<FeeMaster> FindByIdAsync(int id, string dbId, bool? isActive = null);
        Task<IEnumerable<FeeMaster>> FindByFeeGroupIdAsync(int id, string dbId, int academicSessionId, bool? isActive = null);
        #endregion

        #region Get All
        Task<IEnumerable<FeeMaster>> GetAllAsync(string dbId, int academicSessionId, bool? isActive = null);
        Task<IEnumerable<FeeMaster>> GetAll_WithAddEditUserAsync(string dbId, int academicSessionId, bool? isActive = null);
        #endregion


    }
}
