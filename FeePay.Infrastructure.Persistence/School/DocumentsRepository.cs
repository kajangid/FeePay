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
    public class DocumentsRepository : IDocumentsRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public DocumentsRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }

        #region Execute
        public async Task<int> AddAsync(string dbId, Documents document)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    document.Name,
                    NormalizedName = document.Name.ToUpper(),
                    document.Description,
                    document.UserId,
                    document.UserType,
                    document.Type,
                    document.FileName,
                    document.FileExtension,
                    document.FileType,
                    document.DownloadUrl,
                    document.HtmlAlt,
                    document.HtmlTitle,
                    document.IsActive,
                    document.AddedBy
                };
                return await connection.ExecuteScalarAsync<int>(
                    sql: _dBVariables.SP_Add_Document,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<int> BulkAddAsync(List<Documents> documents, string dbId)
        {
            IDbConnection connection = new SqlConnection(GetConStr(dbId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var parameters = documents.Select(s =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@UserId", s.UserId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@UserType", s.UserType, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@Name", s.Name, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@NormalizedName", s.Name.ToUpper(), DbType.String, ParameterDirection.Input);
                    tempParams.Add("@Description", s.Description, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@Type", s.Type, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@FileName", s.FileName, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@FileExtension", s.FileExtension, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@FileType", s.FileType, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@DownloadUrl", s.DownloadUrl, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@HtmlAlt", s.HtmlAlt, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@HtmlTitle", s.HtmlTitle, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@UserType", s.UserType, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@IsActive", s.IsActive, DbType.Boolean, ParameterDirection.Input);
                    tempParams.Add("@AddedBy", s.AddedBy, DbType.Int32, ParameterDirection.Input);
                    return tempParams;
                });
                var effectedRows = await connection.ExecuteAsync(
                    sql: _dBVariables.SP_Add_Document,
                    param: parameters,
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure);

                if (effectedRows > 0 && effectedRows == documents.Count)
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
        public async Task<int> UpdateAsync(string dbId, Documents document)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    document.Id,
                    document.Name,
                    NormalizedName = document.Name?.ToUpper(),
                    document.UserId,
                    document.UserType,
                    document.Type,
                    document.FileName,
                    document.FileExtension,
                    document.FileType,
                    document.DownloadUrl,
                    document.HtmlAlt,
                    document.HtmlTitle,
                    document.Description,
                    document.IsActive,
                    document.ModifyBy
                };
                return await connection.ExecuteAsync(
                    sql: _dBVariables.SP_Update_Document,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<int> DeleteAsync(string dbId, int id, int userId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(
                    sql: _dBVariables.SP_Delete_Document,
                    param: new { Id = id, ModifyBy = userId },
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
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        #endregion

        #region Find
        public async Task<Documents> FindByIdAsync(string dbId, int id, int userId, string userType, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));

                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
                dynamicParams.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);
                dynamicParams.Add("@UserType", userType, DbType.String, ParameterDirection.Input);
                dynamicParams.Add("@IsActive", isActive ?? (object)DBNull.Value, DbType.Boolean, ParameterDirection.Input);

                return await connection.QuerySingleOrDefaultAsync<Documents>(
                    sql: _dBVariables.SP_Get_Document,
                    param: dynamicParams,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<Documents> FindByNameAsync(string dbId, string name, int userId, string userType, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));

                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@Name", name, DbType.String, ParameterDirection.Input);
                dynamicParams.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);
                dynamicParams.Add("@UserType", userType, DbType.String, ParameterDirection.Input);
                dynamicParams.Add("@IsActive", isActive ?? (object)DBNull.Value, DbType.Boolean, ParameterDirection.Input);

                return await connection.QuerySingleOrDefaultAsync<Documents>(
                    sql: _dBVariables.SP_Get_Document,
                    param: dynamicParams,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        #endregion


        #region Get All
        public async Task<IEnumerable<Documents>> GetAllAsync(string dbId, int userId, string userType, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));

                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);
                dynamicParams.Add("@UserType", userType, DbType.String, ParameterDirection.Input);
                dynamicParams.Add("@IsActive", isActive ?? (object)DBNull.Value, DbType.Boolean, ParameterDirection.Input);

                return await connection.QueryAsync<Documents>(
                    sql: _dBVariables.SP_Get_Document,
                    param: dynamicParams,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<Documents>> GetAll_WithAddEditUserAsync(string dbId, int userId, string userType, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var users = await connection.QueryAsync<SchoolAdminUser>(
                    sql: _dBVariables.SP_Get_SchoolAdmin_User,
                    param: new { IsActive = true },
                    commandType: CommandType.StoredProcedure);

                var list = await GetAllAsync(dbId, userId, userType, isActive);
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
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
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
