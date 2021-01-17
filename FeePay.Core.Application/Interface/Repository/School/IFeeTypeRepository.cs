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
        Task<int> AddAsync(FeeType feeType, string dbId = null);
        Task<int> UpdateAsync(FeeType feeType, string dbId = null);
        Task<int> DeleteAsync(int Id, string dbId = null);
        Task<IEnumerable<FeeType>> GetAllActiveAsync(string dbId = null);
        Task<FeeType> FindActiveByIdAsync(int Id, string dbId = null);
        Task<FeeType> FindActiveByNameAsync(string Name, string dbId = null);
        Task<FeeType> FindActiveByCodeAsync(string Code, string dbId = null);
        Task<IEnumerable<FeeType>> GetAllAsync(string dbId = null);
        Task<FeeType> FindByIdAsync(int Id, string dbId = null);
        Task<FeeType> FindByNameAsync(string Name, string dbId = null);
        Task<FeeType> FindByCodeAsync(string Code, string dbId = null);


        Task<IEnumerable<FeeType>> GetAllActive_WithAddEditUserAsync(string dbId = null);
        Task<FeeType> FindActiveById_WithAddEditUserAsync(int Id, string dbId = null);
        Task<FeeType> FindActiveByName_WithAddEditUserAsync(string Name, string dbId = null);
        Task<FeeType> FindActiveByCode_WithAddEditUserAsync(string Code, string dbId = null);
        Task<IEnumerable<FeeType>> GetAll_WithAddEditUserAsync(string dbId = null);
        Task<FeeType> FindById_WithAddEditUserAsync(int Id, string dbId = null);
        Task<FeeType> FindByName_WithAddEditUserAsync(string Name, string dbId = null);
        Task<FeeType> FindByCode_WithAddEditUserAsync(string Code, string dbId = null);
    }
}
