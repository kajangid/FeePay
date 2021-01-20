using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface ISessionRepository
    {
        Task<int> AddAsync(Session session, string dbId = null);
        Task<int> UpdateAsync(Session session, string dbId = null);
        Task<int> DeleteAsync(int Id, string dbId = null);

        // Find
        Task<Session> FindByIdAsync(int Id, string dbId = null);
        Task<Session> FindByNameAsync(string Year, string dbId = null);
        Task<Session> FindActiveByIdAsync(int Id, string dbId = null);
        Task<Session> FindActiveByNameAsync(string Year, string dbId = null);

        // Get All
        Task<IEnumerable<Session>> GetAllAsync(string dbId = null);
        Task<IEnumerable<Session>> GetAllActiveAsync(string dbId = null);
        Task<IEnumerable<Session>> GetAll_WithAddEditUserAsync(string dbId = null);
        Task<IEnumerable<Session>> GetAllActive_WithAddEditUserAsync(string dbId = null);
    }
}
