using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository.School;
using FeePay.Core.Domain.Entities;
using FeePay.Core.Domain.Entities.School;
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Infrastructure.Persistence.School
{
    public class StudentFeesRepository : IStudentFeesRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public StudentFeesRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }

        #region Execute
        public async Task<int> AddAsync(StudentFees studentFees, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    studentFees.StudentAdmissionId,
                    studentFees.FeeMasterId,
                    studentFees.AcademicSessionId,
                    studentFees.IsActive
                };
                return await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_StudentFees,
                    Parameters, commandType: CommandType.StoredProcedure);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        public async Task<int> UpdateAsync(StudentFees studentFees, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    studentFees.Id,
                    studentFees.StudentAdmissionId,
                    studentFees.FeeMasterId,
                    studentFees.AcademicSessionId,
                    studentFees.IsActive
                };
                return await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_StudentFees,
                    Parameters, commandType: CommandType.StoredProcedure);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        /// <summary>
        /// Insert Bulk data in db
        /// </summary>
        /// <param name="studentFees"> take only two properties StudentId,FeeMasterId,FeeGroupId </param>
        /// <param name="dbId"> SchoolId </param>
        /// <returns></returns>
        public async Task<int> BulkAddAsync(List<StudentFees> studentFees, string dbId)
        {
            IDbConnection connection = new SqlConnection(GetConStr(dbId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var parameters = studentFees.Select(s =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@StudentAdmissionId", s.StudentAdmissionId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@FeeMasterId", s.FeeMasterId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@FeeGroupId", s.FeeGroupId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@AcademicSessionId", s.AcademicSessionId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@IsActive", true, DbType.Boolean, ParameterDirection.Input);
                    return tempParams;
                });
                int affectedRows = await connection.ExecuteAsync(
                    sql: _dBVariables.SP_Add_StudentFees,
                    param: parameters,
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure);

                if (affectedRows <= 0)
                {
                    transaction?.Rollback();
                    connection?.Close();
                    return 0;
                }
                transaction?.Commit();
                connection?.Close();
                return 1;
            }
            catch (TimeoutException ex)
            {
                transaction?.Rollback();
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                transaction?.Rollback();
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw new Exception(String.Format("{0}", GetType().FullName), ex);
            }
            finally
            {
                connection?.Close();
            }
        }
        public async Task<int> DeleteAsync(int Id, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Remove_StudentFees,
                    new { Id },
                    commandType: CommandType.StoredProcedure);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        public async Task<int> DeleteAsync(int ClassId, int SectionId, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Remove_StudentFees,
                    new { ClassId, SectionId },
                    commandType: CommandType.StoredProcedure);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
        #endregion

        #region Find 
        public async Task<StudentFees> FindByIdAsync(int id, string dbId, int? academicSessionId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var q = await connection.QuerySingleAsync<StudentFees>(
                    sql: _dBVariables.QUERY_Find_StudentFee,
                    param: new { Id = id },
                    commandType: CommandType.Text);
                return q;
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }

        }
        #endregion

        #region Get Students
        public async Task<IEnumerable<StudentAdmission>> GetStudentsInFeesGroupAsync(string dbId, int feeGroupId, int academicSessionId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var q = await connection.QueryAsync<StudentAdmission, FeeGroup, StudentAdmission>(
                    sql: _dBVariables.SP_Get_StudentFees,
                    map: (stu, fg) =>
                     {
                         stu.FeesGroupList.Add(fg);
                         return stu;
                     },
                    param: new { FeeGroupId = feeGroupId, AcademicSessionId = academicSessionId },
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = q.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.FeesGroupList = g.Select(p => p.FeesGroupList?.Single()).ToList();
                    return groupedPost;
                });
                return result;
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }

        }
        public async Task<IEnumerable<StudentAdmission>> GetStudentFeesAsync(string dbId, int academicSessionId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var q = await connection.QueryAsync<StudentAdmission, FeeGroup, StudentAdmission>(
                    sql: _dBVariables.SP_Get_StudentFees,
                    map: (stu, fg) =>
                    {
                        stu.FeesGroupList.Add(fg);
                        return stu;
                    },
                    param: new { AcademicSessionId = academicSessionId },
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = q.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.FeesGroupList = g.Select(p => p.FeesGroupList?.Single()).ToList();
                    return groupedPost;
                });
                return result;
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }

        }
        #endregion

        #region Check
        public async Task<bool> IsFeeAssignToStudentAsync(string dbId, int studentAdmissionId, int feeMasterId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var q = await connection.ExecuteScalarAsync<int>(_dBVariables.QUERY_IsFeeAssignToStudent,
                    new { StudentAdmissionId = studentAdmissionId, FeeMasterId = feeMasterId },
                    commandType: CommandType.Text);
                return (q > 0);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }

        }
        #endregion

        #region Get StudentFees
        public async Task<IEnumerable<StudentFees>> GetStudentFeeListAsync(string dbId, int studentAdmissionId, int academicSessionId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<StudentFees>(
                    sql: _dBVariables.QUERY_StudentFeeList,
                    param: new { StudentAdmissionId = studentAdmissionId, AcademicSessionId = academicSessionId },
                    commandType: CommandType.Text);
                return list;
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }

        }
        public async Task<IEnumerable<StudentFees>> GetStudentFeeListByTransactionIdAsync(string dbId, string transactionId, int academicSessionId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<StudentFees>(
                    sql: _dBVariables.QUERY_StudentFeeList_ByTranscationId,
                    param: new { PaymentId = transactionId, AcademicSessionId = academicSessionId },
                    commandType: CommandType.Text);
                return list;
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }

        }
        #endregion

        #region Get According To Class
        /// <summary>
        /// Gets the Fees Assign to student classes
        /// </summary>
        /// <param name="dbId"> Database id </param>
        /// <returns>List of Comman_Sp_School With (ClassId,Amount,DueDate,IsPaid)</returns>
        public async Task<IEnumerable<Comman_Sp_School>> GetClasses_FeesAsync(string dbId, int academicSessionId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var q = await connection.QueryAsync<Comman_Sp_School>(
                    sql: _dBVariables.QUERY_GetClasses_Fees,
                    param: new { AcademicSessionId = academicSessionId },
                    commandType: CommandType.Text);
                return q;
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} experienced an exception", nameof(GetClasses_FeesAsync)), ex);
            }
        }
        /// <summary>
        /// Gets the Fees Assign to student classes
        /// </summary>
        /// <param name="dbId"> Database id </param>
        /// <returns>List of Comman_Sp_School With (StudentAdmissionId,Name,Amount,DueDate,IsPaid)</returns>
        public async Task<IEnumerable<Comman_Sp_School>> GetClassStudents_FeesAsync(string dbId, int academicSessionId, int classId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var q = await connection.QueryAsync<Comman_Sp_School>(
                    sql: _dBVariables.QUERY_GetClassStudents_Fees,
                    param: new { ClassId = classId, AcademicSessionId = academicSessionId },
                    commandType: CommandType.Text);
                return q;
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} experienced an exception", nameof(GetClasses_FeesAsync)), ex);
            }
        }
        #endregion

        #region Get StudentFees
        public async Task<IEnumerable<StudentFees>> GetAllAsync(string dbId, int academicSessionId,
            DateTime? fromDate = null, DateTime? toDate = null, int? classId = null, int? sectionId = null,
            int? studentId = null, bool? isPaid = null, string studentSearchString = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                StringBuilder Query = new StringBuilder(_dBVariables.QUERY_STUDENTFEES_GetAll);
                var parameters = new DynamicParameters();
                Query.Append(_dBVariables.QUERY_STUDENTFEES_GetAll_Where_AcademicSessionId);
                parameters.Add("@AcademicSessionId", fromDate, DbType.DateTime, ParameterDirection.Input);
                if (fromDate is not null)
                {
                    Query.Append(_dBVariables.QUERY_STUDENTFEES_GetAll_Where_FromDate);
                    parameters.Add("@FromDate", fromDate, DbType.DateTime, ParameterDirection.Input);
                }
                if (toDate is not null)
                {
                    Query.Append(_dBVariables.QUERY_STUDENTFEES_GetAll_Where_ToDate);
                    parameters.Add("@ToDate", toDate, DbType.DateTime, ParameterDirection.Input);
                }
                if (classId is not null and not 0)
                {
                    Query.Append(_dBVariables.QUERY_STUDENTFEES_GetAll_Where_ClassId);
                    parameters.Add("@ClassId", classId, DbType.Int32, ParameterDirection.Input);
                }
                if (sectionId is not null and not 0)
                {
                    Query.Append(_dBVariables.QUERY_STUDENTFEES_GetAll_Where_SectionId);
                    parameters.Add("@SectionId", sectionId, DbType.Int32, ParameterDirection.Input);
                }
                if (studentId is not null and not 0)
                {
                    Query.Append(_dBVariables.QUERY_STUDENTFEES_GetAll_Where_StudentAdmissionId);
                    parameters.Add("@StudentAdmissionId", studentId, DbType.Int32, ParameterDirection.Input);
                }
                if (isPaid is not null)
                {
                    Query.Append(_dBVariables.QUERY_STUDENTFEES_GetAll_Where_IsPaid);
                    parameters.Add("@IsPaid", isPaid, DbType.Boolean, ParameterDirection.Input);
                }
                if (!string.IsNullOrEmpty(studentSearchString) && !string.IsNullOrWhiteSpace(studentSearchString))
                {
                    Query.Append(_dBVariables.QUERY_STUDENTFEES_GetAll_Where_SearchParam);
                    parameters.Add("@SearchParam", studentSearchString, DbType.String, ParameterDirection.Input);
                }

                var q = await connection.QueryAsync<StudentFees, StudentAdmission, StudentFees>(
                    sql: Query.ToString(),
                    map: (studentfees, StudentAdmission) =>
                    {
                        studentfees.StudentAdmission = StudentAdmission;
                        return studentfees;
                    },
                    param: parameters,
                    splitOn: "Id,Id",
                    commandType: CommandType.Text);
                return q;
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} experienced an exception", nameof(GetClasses_FeesAsync)), ex);
            }
        }
        #endregion

        private string GetConStr(string dbId = null)
        {
            return string.IsNullOrEmpty(dbId) ?
                _connectionStringBuilder.GetSchoolConnectionString() : // Demo DB
                _connectionStringBuilder.GetDynamicSchoolConnectionString(dbId);
        }
    }
}
