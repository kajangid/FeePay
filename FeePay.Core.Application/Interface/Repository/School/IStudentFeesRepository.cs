using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities;
using FeePay.Core.Domain.Entities.School;
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface IStudentFeesRepository
    {
        Task<int> AddAsync(int studentAdmissionId, int feeGroupId, string dbId, bool isActive = true);
        Task<int> BulkAddAsync(List<StudentFees> studentFees, string dbId);
        Task<int> DeleteAsync(int Id, string dbId);
        Task<int> DeleteAsync(int ClassId, int SectionId, string dbId);
        Task<IEnumerable<StudentAdmission>> GetStudentsInFeesGroupAsync(int studentId, string dbId);
        Task<IEnumerable<StudentAdmission>> GetStudentFeesAsync(string dbId);
        Task<bool> IsFeeAssignToStudentAsync(int studentAdmissionId, int feeGroupId, string dbId);
        Task<IEnumerable<StudentFees>> GetStudentFeeListAsync(int StudentAdmissionId, string dbId);
        Task<StudentFees> FindByIdAsync(int id, string dbId);
        Task<IEnumerable<StudentFees>> GetStudentFeeListByTransactionIdAsync(string transactionId, string dbId);

        /// <summary>
        /// Gets the Fees Assign to student classes
        /// </summary>
        /// <param name="dbId"> Database id </param>
        /// <returns>List of Comman_Sp_School With (ClassId,Amount,DueDate,IsPaid)</returns>
        Task<IEnumerable<Comman_Sp_School>> GetClasses_FeesAsync(string dbId);
        /// <summary>
        /// Gets the Fees Assign to student In class
        /// </summary>
        /// <param name="dbId"> Database id </param>
        /// <returns>List of Comman_Sp_School With (StudentAdmissionId,Name,Amount,DueDate,IsPaid)</returns>
        Task<IEnumerable<Comman_Sp_School>> GetClassStudents_FeesAsync(string dbId, int classId);
    }
}
