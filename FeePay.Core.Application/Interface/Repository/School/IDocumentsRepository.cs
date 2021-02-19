using FeePay.Core.Domain.Entities.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface IDocumentsRepository
    {

        #region Execute
        Task<int> AddAsync(string dbId, Documents document);
        Task<int> BulkAddAsync(List<Documents> documents, string dbId);
        Task<int> UpdateAsync(string dbId, Documents document);
        Task<int> DeleteAsync(string dbId, int id, int userId);
        #endregion

        #region Find
        Task<Documents> FindByIdAsync(string dbId, int id, int userId, string userType, bool? isActive = null);
        Task<Documents> FindByNameAsync(string dbId, string name, int userId, string userType, bool? isActive = null);
        #endregion


        #region Get All
        Task<IEnumerable<Documents>> GetAllAsync(string dbId, int userId, string userType, bool? isActive = null);
        Task<IEnumerable<Documents>> GetAll_WithAddEditUserAsync(string dbId, int userId, string userType, bool? isActive = null);
        #endregion
    }
}
