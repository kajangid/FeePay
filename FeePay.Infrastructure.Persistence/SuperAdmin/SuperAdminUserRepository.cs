using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository.SuperAdmin;
using System.Threading;

namespace FeePay.Infrastructure.Persistence.SuperAdmin
{
    public class SuperAdminUserRepository : ISuperAdminUserRepository
    {
        public SuperAdminUserRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _defaultConnectionString = connectionStringBuilder.GetDefaultConnectionString();
            _dbVariables = dBVariables;
        }
        private readonly IDBVariables _dbVariables;
        private readonly string _defaultConnectionString;

        public async Task<int> AddAsync(SuperAdminUser user)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var SpRequiredParameters = new
                {
                    user.AccessFailedCount,
                    user.AddedBy,
                    user.City,
                    user.Email,
                    user.EmailConfirmed,
                    user.FirstName,
                    user.FullName,
                    user.IsActive,
                    user.LastLoginDate,
                    user.LastLoginIP,
                    user.LastName,
                    user.LockoutEnabled,
                    user.LockoutEndDate,
                    user.NormalizedEmail,
                    user.NormalizedUserName,
                    user.Password,
                    user.PasswordHash,
                    user.PhoneNumber,
                    user.PhoneNumberConfirmed,
                    user.Photo,
                    user.SecurityStamp,
                    user.TwoFactorEnabled,
                    user.UserName
                };
                return await connection.ExecuteScalarAsync<int>(
                    _dbVariables.SP_Add_SuperAdmin_User,
                    SpRequiredParameters,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }
        }
        public async Task<int> UpdateAsync(SuperAdminUser user)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var SpRequiredParameters = new
                {
                    user.Id,
                    user.AccessFailedCount,
                    user.City,
                    user.Email,
                    user.EmailConfirmed,
                    user.FirstName,
                    user.FullName,
                    user.IsActive,
                    user.LastName,
                    user.LockoutEnabled,
                    user.LockoutEndDate,
                    user.ModifyBy,
                    user.NormalizedEmail,
                    user.NormalizedUserName,
                    user.Password,
                    user.PasswordHash,
                    user.PhoneNumber,
                    user.PhoneNumberConfirmed,
                    user.Photo,
                    user.SecurityStamp,
                    user.TwoFactorEnabled,
                    user.UserName
                };
                return await connection.ExecuteScalarAsync<int>(
                    _dbVariables.SP_Update_SuperAdmin_User,
                    SpRequiredParameters,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }
        }
        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                using var connection = new SqlConnection(_defaultConnectionString);
                return await connection.ExecuteScalarAsync<int>(
                    _dbVariables.SP_Delete_SuperAdmin_User,
                    new { Id = id },
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }

        }

        // Find
        public async Task<SuperAdminUser> FindByIdAsync(int userId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var SpRequiredParameters = new { Id = userId, IsActive = isActive };
                return await connection.QuerySingleOrDefaultAsync<SuperAdminUser>(
                    _dbVariables.SP_Get_SuperAdmin_User,
                    SpRequiredParameters,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }
        }
        public async Task<SuperAdminUser> FindByUserNameAsync(string normalizedUserName, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var SpRequiredParameters = new { NormalizedUserName = normalizedUserName, IsActive = isActive };
                return await connection.QuerySingleOrDefaultAsync<SuperAdminUser>(
                    _dbVariables.SP_Get_SuperAdmin_User,
                    SpRequiredParameters,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }
        }
        public async Task<SuperAdminUser> FindByEmailAsync(string normalizedEmail, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var SpRequiredParameters = new { NormalizedEmail = normalizedEmail, IsActive = isActive };
                return await connection.QuerySingleOrDefaultAsync<SuperAdminUser>(
                    _dbVariables.SP_Get_SuperAdmin_User,
                    SpRequiredParameters,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }
        }

        // Get
        public async Task<IEnumerable<SuperAdminUser>> GetAllAsync(bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var SpRequiredParameters = new { IsActive = isActive };
                var list = (await connection.QueryAsync<SuperAdminUser>(
                    _dbVariables.SP_Get_SuperAdmin_User,
                    SpRequiredParameters,
                    commandType: CommandType.StoredProcedure));
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<SuperAdminUser>> GetAll_WithAddEditUserAsync(bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var SpRequiredParameters = new { IsActive = isActive };
                var list = (await connection.QueryAsync<SuperAdminUser, SuperAdminUser, SuperAdminUser, SuperAdminUser>(
                    _dbVariables.SP_GetAll_SuperAdmin_User,
                    (user, addedby, modifyby) =>
                    {
                        user.AddedByUser = addedby;
                        user.ModifyByUser = modifyby;
                        return user;
                    },
                    SpRequiredParameters,
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure));
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }
        }

        // Search
        /// <summary>
        /// Search data in NormalizedUserName,NormalizedEmail,PhoneNumber,FirstName,LastName
        /// </summary>
        /// <param name="searchParam"> search string </param>
        /// <param name="isActive">active[true]/inactive[false]/all[null]</param>
        /// <returns> List of Super admin user data</returns>
        public async Task<IEnumerable<SuperAdminUser>> Search_WithAddEdirUserAsync(string searchParam, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var SpRequiredParameters = new { SearchParam = searchParam, IsActive = isActive };
                var list = (await connection.QueryAsync<SuperAdminUser, SuperAdminUser, SuperAdminUser, SuperAdminUser>(
                    _dbVariables.SP_GetAll_SuperAdmin_User,
                    (user, addedby, modifyby) =>
                    {
                        user.AddedByUser = addedby;
                        user.ModifyByUser = modifyby;
                        return user;
                    },
                    SpRequiredParameters,
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure));
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }
        }

        // Mics
        public async Task UpdateLoginState(int userId, string Ip)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var SpRequiredParameters = new { Id = userId, LastLoginIP = Ip };
                await connection.QueryAsync<SuperAdminUser>(
                    _dbVariables.SP_AddLoginInfo_SuperAdmin,
                    SpRequiredParameters,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }
        }
        public async Task<SuperAdminUser> FindPasswordByIdAsync(int id, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                DynamicParameters tempParams = new DynamicParameters();
                string sql = _dbVariables.QUERY_Password_SuperAdmin + @" AND [Id] = @UserId";
                tempParams.Add("@UserId", id, DbType.Int32, ParameterDirection.Input);
                tempParams.Add("@IsActive", (object)isActive ?? DBNull.Value, DbType.Boolean, ParameterDirection.Input);
                var user = await connection.QuerySingleAsync<SuperAdminUser>
                    (sql,
                     tempParams,
                     commandType: CommandType.Text);
                return user;
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }

        }
        public async Task<IEnumerable<SuperAdminUser>> GetAll_WithPasswordAsync(bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                DynamicParameters tempParams = new DynamicParameters();
                tempParams.Add("@IsActive", (object)isActive ?? DBNull.Value, DbType.Boolean, ParameterDirection.Input);
                var user = await connection.QueryAsync<SuperAdminUser>
                    (_dbVariables.QUERY_Password_SuperAdmin,
                     tempParams,
                     commandType: CommandType.Text);
                return user;
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }

        }
    }
}
