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
        Task<int> AddAsync(FeeMaster feeMaster, string dbId = null);
        Task<int> UpdateAsync(FeeMaster feeMaster, string dbId = null);
        Task<int> DeleteAsync(int Id, int? changeBy, string dbId = null);
        Task<IEnumerable<FeeMaster>> GetAllAsync(string dbId = null);
        Task<FeeMaster> FindByIdAsync(int Id, string dbId = null);
        Task<IEnumerable<FeeMaster>> GetAllActiveAsync(string dbId = null);
        Task<FeeMaster> FindActiveByIdAsync(int Id, string dbId = null);
        Task<IEnumerable<FeeMaster>> GetAllActive_WithAddEditUserAsync(string dbId = null);
        Task<FeeMaster> FindActiveById_WithAddEditUserAsync(int Id, string dbId = null);
        Task<IEnumerable<FeeMaster>> GetAll_WithAddEditUserAsync(string dbId = null);
        Task<FeeMaster> FindById_WithAddEditUserAsync(int Id, string dbId = null);
        Task<IEnumerable<FeeMaster>> GetByFeeGroupIdAsync(int Id, string dbId);
    }
}
