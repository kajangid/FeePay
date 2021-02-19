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
        Task<int> AddAsync(Session session, string dbId);
        Task<int> UpdateAsync(Session session, string dbId);
        Task<int> SetDefaultAsync(int Id, string dbId);
        Task<int> DeleteAsync(int Id, string dbId);

        // Find
        Task<Session> FindByIdAsync(int Id, string dbId, bool? isActive = null);
        Task<Session> FindByNameAsync(string Year, string dbId, bool? isActive = null);
        Task<Session> FetchActiveAcadmicSession(string dbId);

        // Get All
        Task<IEnumerable<Session>> GetAllAsync(string dbId, bool? isActive = null);
        Task<IEnumerable<Session>> GetAll_WithAddEditUserAsync(string dbId, bool? isActive = null);
    }
}
