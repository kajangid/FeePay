using FeePay.Core.Domain.Entities.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface IFeeTypeRepository
    {
        #region Execute
        Task<int> AddAsync(FeeType feeType, string dbId);
        Task<int> UpdateAsync(FeeType feeType, string dbId);
        Task<int> DeleteAsync(int id, string dbId);
        #endregion

        #region Find
        Task<FeeType> FindByIdAsync(int id, string dbId, bool? isActive = null);
        Task<FeeType> FindByNameAsync(string name, string dbId, bool? isActive = null);
        Task<FeeType> FindByCodeAsync(string code, string dbId, bool? isActive = null);
        #endregion

        #region Get All
        Task<IEnumerable<FeeType>> GetAllAsync(string dbId, bool? isActive = null);
        Task<IEnumerable<FeeType>> GetAll_WithAddEditUserAsync(string dbId, bool? isActive = null);
        #endregion
    }
}
