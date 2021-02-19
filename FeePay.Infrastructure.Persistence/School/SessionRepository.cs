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
    public class SessionRepository : ISessionRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public SessionRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }


        #region Execute
        public async Task<int> AddAsync(Session session, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    session.Year,
                    session.StartYear,
                    session.EndYear,
                    session.Description,
                    session.IsActive,
                    session.AddedBy
                };
                return await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_Session,
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<int> UpdateAsync(Session session, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    session.Id,
                    session.Year,
                    session.StartYear,
                    session.EndYear,
                    session.Description,
                    //session.IsActive,
                    session.ModifyBy
                };
                return await connection.ExecuteAsync(_dBVariables.SP_Update_Session,
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<int> SetDefaultAsync(int Id, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_SetDefault_Session,
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        public async Task<int> DeleteAsync(int Id, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Delete_Session,
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception.", GetType().FullName), ex);
            }
        }
        #endregion

        #region Find
        public async Task<Session> FindByIdAsync(int Id, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<Session>(_dBVariables.SP_Get_Session,
                    new { Id, IsActive = isActive },
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
        public async Task<Session> FindByNameAsync(string year, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<Session>(_dBVariables.SP_Get_Session,
                    new { Year = year, IsActive = isActive },
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
        public async Task<Session> FetchActiveAcadmicSession(string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<Session>(_dBVariables.SP_Get_Session,
                    new { IsActive = true },
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
        public async Task<IEnumerable<Session>> GetAllAsync(string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<Session>(
                    sql: _dBVariables.SP_Get_Session,
                    param: new { IsActive = isActive },
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
        public async Task<IEnumerable<Session>> GetAll_WithAddEditUserAsync(string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var users = await connection.QueryAsync<SchoolAdminUser>(
                    sql: _dBVariables.SP_Get_SchoolAdmin_User,
                    param: new { IsActive = true },
                    commandType: CommandType.StoredProcedure);

                var list = await GetAllAsync(dbId, isActive);
                if (list != null && users != null && users.Any())
                {
                    var _classes = list.ToList();
                    _classes.ForEach(f =>
                    {
                        if (f.AddedBy != 0)
                            f.AddedByUser = users.Where(w => w.Id == f.AddedBy).SingleOrDefault();
                        if (f.ModifyBy != 0)
                            f.ModifyByUser = users.Where(w => w.Id == f.ModifyBy).SingleOrDefault();
                    });
                    return _classes;
                }
                return list;
                //return await connection.QueryAsync<Session, SchoolAdminUser, SchoolAdminUser, Session>(
                //    sql: _dBVariables.SP_Get_Session_AddEditUser,
                //    map: (session, addedby, modifyby) =>
                //    {
                //        session.AddedByUser = addedby;
                //        session.ModifyByUser = modifyby;
                //        return session;
                //    },
                //    param: new { IsActive = active },
                //    splitOn: "Id,Id,Id",
                //    commandType: CommandType.StoredProcedure);
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
