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
        Task<int> AddAsync(FeeGroup feeGroup, string dbId = null);
        Task<int> UpdateAsync(FeeGroup feeGroup, string dbId = null);
        Task<int> DeleteAsync(int Id, string dbId = null);
        Task<IEnumerable<FeeGroup>> GetAllActiveAsync(string dbId = null);
        Task<FeeGroup> FindActiveByIdAsync(int Id, string dbId = null);
        Task<FeeGroup> FindActiveByNameAsync(string Name, string dbId = null);
        Task<IEnumerable<FeeGroup>> GetAllAsync(string dbId = null);
        Task<FeeGroup> FindByIdAsync(int Id, string dbId = null);
        Task<FeeGroup> FindByNameAsync(string Name, string dbId = null);



        Task<IEnumerable<FeeGroup>> GetAllActive_WithAddEditUserAsync(string dbId = null);
        Task<FeeGroup> FindActiveById_WithAddEditUserAsync(int Id, string dbId = null);
        Task<FeeGroup> FindActiveByName_WithAddEditUserAsync(string Name, string dbId = null);
        Task<IEnumerable<FeeGroup>> GetAll_WithAddEditUserAsync(string dbId = null);
        Task<FeeGroup> FindById_WithAddEditUserAsync(int Id, string dbId = null);
        Task<FeeGroup> FindByName_WithAddEditUserAsync(string Name, string dbId = null);

    }
}
