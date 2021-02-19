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
        #region Execute
        Task<int> AddAsync(StudentFees stuudentFees, string dbId);
        /// <summary>
        /// Insert Bulk data in db
        /// </summary>
        /// <param name="studentFees"> take only two properties StudentId,FeeMasterId,FeeGroupId </param>
        /// <param name="dbId"> SchoolId </param>
        /// <returns></returns>
        Task<int> BulkAddAsync(List<StudentFees> studentFees, string dbId);
        Task<int> DeleteAsync(int Id, string dbId);
        Task<int> DeleteAsync(int ClassId, int SectionId, string dbId);
        #endregion

        #region Find
        Task<StudentFees> FindByIdAsync(int id, string dbId, int? academicSessionId = null);
        #endregion

        #region Get Students
        Task<IEnumerable<StudentAdmission>> GetStudentsInFeesGroupAsync(string dbId, int feeGroupId, int academicSessionId);
        Task<IEnumerable<StudentAdmission>> GetStudentFeesAsync(string dbId, int academicSessionId);
        #endregion

        #region Check
        Task<bool> IsFeeAssignToStudentAsync(string dbId, int studentAdmissionId, int feeMasterId);
        #endregion

        #region Get StudentFees
        Task<IEnumerable<StudentFees>> GetStudentFeeListAsync(string dbId, int studentAdmissionId, int academicSessionId);
        Task<IEnumerable<StudentFees>> GetStudentFeeListByTransactionIdAsync(string dbId, string transactionId, int academicSessionId);
        #endregion

        #region Get According To Class
        /// <summary>
        /// Gets the Fees Assign to student classes
        /// </summary>
        /// <param name="dbId"> Database id </param>
        /// <returns>List of Comman_Sp_School With (ClassId,Amount,DueDate,IsPaid)</returns>
        Task<IEnumerable<Comman_Sp_School>> GetClasses_FeesAsync(string dbId, int academicSessionId);
        /// <summary>
        /// Gets the Fees Assign to student In class
        /// </summary>
        /// <param name="dbId"> Database id </param>
        /// <returns>List of Comman_Sp_School With (StudentAdmissionId,Name,Amount,DueDate,IsPaid)</returns>
        Task<IEnumerable<Comman_Sp_School>> GetClassStudents_FeesAsync(string dbId, int academicSessionId, int classId);
        #endregion

        #region Get StudentFees
        /// <summary>
        /// Gets the StudentFees list with join of FeesTransaction data
        /// </summary>
        /// <param name="dbId"> Database Id </param>
        /// <param name="fromDate"> From Date </param>
        /// <param name="toDate"> To Date </param>
        /// <param name="classId"> Class Id </param>
        /// <param name="sectionId"> Section Id </param>
        /// <param name="studentAdmissionId"> Student Admission Id </param>
        /// <returns> IEnumerable data list of StudentFees Object with FeeTransaction. </returns>
        Task<IEnumerable<StudentFees>> GetAllAsync(string dbId, int academicSessionId,
            DateTime? fromDate = null, DateTime? toDate = null, int? classId = null, int? sectionId = null,
            int? studentId = null, bool? isPaid = null, string studentSearchString = null);
        #endregion
    }
}
