using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Core.Application.Interface.Repository.Student
{
    public interface IStudentAdmisionRepository
    {
        Task<int> AddAsync(StudentAdmission studentAdmission, string dbId);
        Task<int> UpdateAsync(StudentAdmission studentAdmission, string dbId);
        Task<int> BulkUpdateAsync(List<StudentAdmission> studentAdmission, string dbId);
        Task<int> DeleteAsync(int Id, string dbId);


        // find
        Task<StudentAdmission> FindByIdAsync(int Id, string dbId, bool? active = null);
        Task<StudentAdmission> FindByStudentLoginIdAsync(int Id, string dbId, bool? active = null);
        Task<StudentAdmission> FindByFormNoAsync(string formno, string dbId, bool? active = null);
        Task<StudentAdmission> FindBySr_RegNoAsync(string sr_regno, string dbId, bool? active = null);


        // get all
        Task<IEnumerable<StudentAdmission>> GetAllAsync(string dbId, int academicSessionId, bool? active = null);
        Task<IEnumerable<StudentAdmission>> GetAll_WithAddEditUserAsync(string dbId, int academicSessionId, bool? active = null);

        // Search
        Task<IEnumerable<StudentAdmission>> SearchStudentAsync(string dbId,
            int academicSessionId, int? classId = null, int? sectionId = null,
            string seatchString = null, string gender = null, int? studentAdmissionId = null,
            bool isActive = true);
        Task<IEnumerable<StudentAdmission>> SearchStudent_WithAddEditUserAsync(string dbId,
            int academicSessionId, int? classId = null, int? sectionId = null,
            string seatchString = null, string gender = null, int? studentAdmissionId = null,
            bool isActive = true);
    }
}
