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


        public async Task<int> AddAsync(int studentAdmissionId, int feeMasterId, string dbId, bool isActive = true)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    StudentAdmissionId = studentAdmissionId,
                    FeeMasterId = feeMasterId,
                    IsActive = isActive
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
        public async Task<int> UpdateAsync(int studentAdmissionId, int feeMasterId, string dbId, bool isActive = true)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    StudentAdmissionId = studentAdmissionId,
                    FeeMasterId = feeMasterId,
                    IsActive = isActive
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
        public async Task<StudentFees> FindByIdAsync(int id, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var q = await connection.QuerySingleAsync<StudentFees>(
                    _dBVariables.QUERY_Find_StudentFee,
                    new { Id = id },
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


        public async Task<IEnumerable<StudentAdmission>> GetStudentsInFeesGroupAsync(int feeGroupId, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var q = await connection.QueryAsync<StudentAdmission, FeeGroup, StudentAdmission>(_dBVariables.SP_Get_StudentFees,
                    (stu, fg) =>
                    {
                        stu.FeesGroupList.Add(fg);
                        return stu;
                    },
                    new { FeeGroupId = feeGroupId },
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

        public async Task<IEnumerable<StudentAdmission>> GetStudentFeesAsync(string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var q = await connection.QueryAsync<StudentAdmission, FeeGroup, StudentAdmission>(_dBVariables.SP_Get_StudentFees,
                    (stu, fg) =>
                    {
                        stu.FeesGroupList.Add(fg);
                        return stu;
                    },
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
        public async Task<bool> IsFeeAssignToStudentAsync(int studentAdmissionId, int feeMasterId, string dbId)
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

        public async Task<IEnumerable<StudentFees>> GetStudentFeeListAsync(int studentAdmissionId, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<StudentFees>(_dBVariables.QUERY_StudentFeeList,
                    new { StudentAdmissionId = studentAdmissionId },
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


        // private methods
        private string GetConStr(string dbId = null)
        {
            return string.IsNullOrEmpty(dbId) ?
                _connectionStringBuilder.GetSchoolConnectionString() : // Demo DB
                _connectionStringBuilder.GetDynamicSchoolConnectionString(dbId);
        }
    }
}
