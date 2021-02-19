using FeePay.Core.Domain.Entities.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface IFeeGroupRepository
    {
        #region Execute
        Task<int> AddAsync(FeeGroup feeGroup, string dbId);
        Task<int> UpdateAsync(FeeGroup feeGroup, string dbId);
        Task<int> DeleteAsync(int id, string dbId);
        #endregion

        #region Find
        Task<FeeGroup> FindByIdAsync(int id, string dbId, bool? isActive = null);
        Task<FeeGroup> FindByNameAsync(string name, string dbId, bool? isActive = null);
        #endregion

        #region Get All
        Task<IEnumerable<FeeGroup>> GetAllAsync(string dbId, bool? isActive = null);
        Task<IEnumerable<FeeGroup>> GetAll_WithAddEditUserAsync(string dbId, bool? isActive = null);
        #endregion

        Task<IEnumerable<FeeGroup>> GetAllWithMasterAandTypeAsync(string dbId, int academicSessionId);

    }
}
