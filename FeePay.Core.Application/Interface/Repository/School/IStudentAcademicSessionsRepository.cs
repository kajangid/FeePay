using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface IStudentAcademicSessionsRepository
    {
        #region Execute
        Task<int> AddAsync(StudentAcademicSession session, string dbId);
        Task<int> UpdateAsync(StudentAcademicSession session, string dbId);
        Task<int> BulkAddAsync(List<StudentAcademicSession> sessions, string dbId);
        Task<int> BulkUpdateAsync(List<StudentAcademicSession> sessions, string dbId);
        Task<int> DeleteAsync(int Id, string dbId);
        #endregion

        #region Find
        Task<StudentAcademicSession> FindByIdAsync(int Id, string dbId, bool? active = null);
        Task<StudentAcademicSession> FindByStudentAdmissionIdAndSessionIdAsync(int studentAdmissionId, int sessionId, string dbId, bool? active = null);
        Task<IEnumerable<StudentAcademicSession>> FindBySessionIdAsync(int Id, string dbId, bool? active = null);
        Task<IEnumerable<StudentAcademicSession>> FindByStudentAdmissionIdAsync(int Id, string dbId, bool? active = null);
        Task<IEnumerable<StudentAcademicSession>> FindByClassIdAsync(int Id, string dbId, bool? active = null);
        Task<IEnumerable<StudentAcademicSession>> FindBySectionIdAsync(int Id, string dbId, bool? active = null);
        #endregion

        #region Get All
        Task<IEnumerable<StudentAcademicSession>> GetAllAsync(string dbId, bool? active = null);
        Task<IEnumerable<StudentAcademicSession>> GetAll_WithAddEditUserAsync(string dbId, bool? active = null);
        #endregion
    }
}
