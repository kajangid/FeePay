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
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Infrastructure.Persistence.School
{
    public class StudentAcademicSessionsRepository : IStudentAcademicSessionsRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public StudentAcademicSessionsRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }

        #region Execute
        public async Task<int> AddAsync(StudentAcademicSession session, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    session.StudentAdmissionId,
                    session.SessionId,
                    session.ClassId,
                    session.SectionId,
                    session.IsActive,
                    session.AddedBy
                };
                return await connection.ExecuteScalarAsync<int>(
                    sql: _dBVariables.SP_Add_Student_Academic_Sessions,
                    param: Parameters,
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<int> UpdateAsync(StudentAcademicSession session, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    session.Id,
                    session.StudentAdmissionId,
                    session.SessionId,
                    session.ClassId,
                    session.SectionId,
                    session.IsActive,
                    session.ModifyBy
                };
                return await connection.ExecuteAsync(
                    sql: _dBVariables.SP_Update_Student_Academic_Sessions,
                    param: Parameters,
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<int> BulkAddAsync(List<StudentAcademicSession> sessions, string dbId)
        {
            IDbConnection connection = new SqlConnection(GetConStr(dbId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var parameters = sessions.Select(s =>
                {
                    var tempParams = new DynamicParameters();
                    //tempParams.Add("@Id", s.Id, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@StudentAdmissionId", s.StudentAdmissionId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@SessionId", s.SessionId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@ClassId", s.ClassId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@SectionId", s.SectionId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@IsActive", s.IsActive, DbType.Boolean, ParameterDirection.Input);
                    tempParams.Add("@AddedBy", s.AddedBy, DbType.Int32, ParameterDirection.Input);
                    return tempParams;
                });
                var effectedRows = await connection.ExecuteAsync(
                    sql: _dBVariables.SP_Add_Student_Academic_Sessions,
                    param: parameters,
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure);

                if (effectedRows > 0 && effectedRows == sessions.Count)
                {
                    transaction?.Commit();
                    connection?.Close();
                    return effectedRows;
                }
                else
                {
                    transaction?.Rollback();
                    connection?.Close();
                    return 0;
                }
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<int> BulkUpdateAsync(List<StudentAcademicSession> sessions, string dbId)
        {
            IDbConnection connection = new SqlConnection(GetConStr(dbId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var parameters = sessions.Select(s =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@Id", s.Id, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@StudentAdmissionId", s.StudentAdmissionId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@SessionId", s.SessionId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@ClassId", s.ClassId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@SectionId", s.SectionId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@IsActive", s.IsActive, DbType.Boolean, ParameterDirection.Input);
                    tempParams.Add("@ModifyBy", s.ModifyBy, DbType.Int32, ParameterDirection.Input);
                    return tempParams;
                });
                var effectedRows = await connection.ExecuteAsync(
                    sql: _dBVariables.QUERY_BulkUpdate_Student_Academic_Sessions,
                    param: parameters,
                    transaction: transaction,
                    commandType: CommandType.Text);

                if (effectedRows > 0 && (effectedRows / 2) == sessions.Count)
                {
                    transaction?.Commit();
                    connection?.Close();
                    return effectedRows;
                }
                else
                {
                    transaction?.Rollback();
                    connection?.Close();
                    return 0;
                }
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
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
                return await connection.ExecuteAsync(
                    sql: _dBVariables.SP_Delete_Student_Academic_Sessions,
                    param: new { Id },
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        #endregion

        #region Find
        public async Task<StudentAcademicSession> FindByIdAsync(int Id, string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<StudentAcademicSession>(
                    sql: _dBVariables.SP_Get_Student_Academic_Sessions,
                    param: new { Id, IsActive = active },
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<StudentAcademicSession> FindByStudentAdmissionIdAndSessionIdAsync(int studentAdmissionId, int sessionId, string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<StudentAcademicSession>(
                    sql: _dBVariables.SP_Get_Student_Academic_Sessions,
                    param: new { StudentAdmissionId = studentAdmissionId, SessionId = sessionId, IsActive = active },
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<StudentAcademicSession>> FindBySessionIdAsync(int Id, string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<StudentAcademicSession>(
                    sql: _dBVariables.SP_Get_Student_Academic_Sessions,
                    param: new { SessionId = Id, IsActive = active },
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<StudentAcademicSession>> FindByStudentAdmissionIdAsync(int Id, string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<StudentAcademicSession>(
                    sql: _dBVariables.SP_Get_Student_Academic_Sessions,
                    param: new { StudentAdmissionId = Id, IsActive = active },
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<StudentAcademicSession>> FindByClassIdAsync(int Id, string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<StudentAcademicSession>(
                    sql: _dBVariables.SP_Get_Student_Academic_Sessions,
                    param: new { ClassId = Id, IsActive = active },
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<StudentAcademicSession>> FindBySectionIdAsync(int Id, string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<StudentAcademicSession>(
                    sql: _dBVariables.SP_Get_Student_Academic_Sessions,
                    param: new { SectionId = Id, IsActive = active },
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        #endregion

        #region Get All
        public async Task<IEnumerable<StudentAcademicSession>> GetAllAsync(string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<StudentAcademicSession>(
                    sql: _dBVariables.SP_Get_Student_Academic_Sessions,
                    param: new { IsActive = active },
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<StudentAcademicSession>> GetAll_WithAddEditUserAsync(string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var users = await connection.QueryAsync<SchoolAdminUser>(
                    sql: _dBVariables.SP_Get_SchoolAdmin_User,
                    param: new { IsActive = true },
                    commandType: CommandType.StoredProcedure);
                var list = await GetAllAsync(dbId, active);
                if (list != null && users != null && users.Any())
                {
                    var studentAcademicSessionList = list.ToList();
                    studentAcademicSessionList.ForEach(f =>
                    {
                        if (f.AddedBy != 0)
                            f.AddedByUser = users.Where(w => w.Id == f.AddedBy).SingleOrDefault();
                        if (f.ModifyBy != 0)
                            f.ModifyByUser = users.Where(w => w.Id == f.ModifyBy).SingleOrDefault();
                    });
                    return studentAcademicSessionList;
                }
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        #endregion


        // private methods
        private string GetConStr(string dbId = null)
        {
            return string.IsNullOrEmpty(dbId) ?
                _connectionStringBuilder.GetSchoolConnectionString() : // Demo DB
                _connectionStringBuilder.GetDynamicSchoolConnectionString(dbId);
        }
    }
}
