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
            _ConnectionStringBuilder = connectionStringBuilder;
            _DefaultConnectionString = connectionStringBuilder.GetDefaultConnectionString();
            _DBVariables = dBVariables;
        }
        private readonly IDBVariables _DBVariables;
        private readonly string _DefaultConnectionString;
        private readonly IConnectionStringBuilder _ConnectionStringBuilder;

        public async Task<int> AddUserAsync(SuperAdminUser user, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new
                    {
                        AccessFailedCount = user.AccessFailedCount,
                        AddedBy = user.AddedBy,
                        City = user.City,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        FirstName = user.FirstName,
                        FullName = user.FullName,
                        IsActive = user.IsActive,
                        LastLoginDate = user.LastLoginDate,
                        LastLoginIP = user.LastLoginIP,
                        LastName = user.LastName,
                        LockoutEnabled = user.LockoutEnabled,
                        LockoutEndDate = user.LockoutEndDate,
                        NormalizedEmail = user.NormalizedEmail,
                        NormalizedUserName = user.NormalizedUserName,
                        Password = user.Password,
                        PasswordHash = user.PasswordHash,
                        PhoneNumber = user.PhoneNumber,
                        PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                        Photo = user.Photo,
                        SecurityStamp = user.SecurityStamp,
                        TwoFactorEnabled = user.TwoFactorEnabled,
                        UserName = user.UserName
                    };
                    return await connection.ExecuteAsync(_DBVariables.SP_Add_SuperAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);

                }
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
        public async Task<int> UpdateUserAsync(SuperAdminUser user, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new
                    {
                        Id = user.Id,
                        AccessFailedCount = user.AccessFailedCount,
                        City = user.City,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        FirstName = user.FirstName,
                        FullName = user.FullName,
                        IsActive = user.IsActive,
                        LastLoginDate = user.LastLoginDate,
                        LastLoginIP = user.LastLoginIP,
                        LastName = user.LastName,
                        LockoutEnabled = user.LockoutEnabled,
                        LockoutEndDate = user.LockoutEndDate,
                        ModifyBy = user.ModifyBy,
                        NormalizedEmail = user.NormalizedEmail,
                        NormalizedUserName = user.NormalizedUserName,
                        Password = user.Password,
                        PasswordHash = user.PasswordHash,
                        PhoneNumber = user.PhoneNumber,
                        PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                        Photo = user.Photo,
                        SecurityStamp = user.SecurityStamp,
                        TwoFactorEnabled = user.TwoFactorEnabled,
                        UserName = user.UserName
                    };
                    return await connection.ExecuteAsync(_DBVariables.SP_Update_SuperAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);

                }
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
        public async Task<int> DeleteUserAsync(int Id, string dbId = null)
        {
            try
            {
                await using (var connection = new SqlConnection(_DefaultConnectionString))
                {
                    return await connection.ExecuteAsync(_DBVariables.SP_Delete_SuperAdmin_User, new { Id = Id }, null, null, CommandType.StoredProcedure);
                }
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
        public async Task<SuperAdminUser> FindUserByUserIdAsync(int userId, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { Id = userId };
                    return await connection.QuerySingleOrDefaultAsync<SuperAdminUser>(_DBVariables.SP_Get_SuperAdmin_User, SpRequiredParameters, null, null, CommandType.StoredProcedure);
                }
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
        public async Task<SuperAdminUser> FindUserByUserNameAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { NormalizedUserName = normalizedUserName };
                    return await connection.QuerySingleOrDefaultAsync<SuperAdminUser>(_DBVariables.SP_Get_SuperAdmin_User, SpRequiredParameters, null, null, CommandType.StoredProcedure);
                }
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
        public async Task<SuperAdminUser> FindUserByUserEmailAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { NormalizedEmail = normalizedEmail };
                    return await connection.QuerySingleOrDefaultAsync<SuperAdminUser>(_DBVariables.SP_Get_SuperAdmin_User, SpRequiredParameters, null, null, CommandType.StoredProcedure);
                }
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
        public async Task<SuperAdminUser> FindActiveUserByUserIdAsync(int userId, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { Id = userId, IsActive = true };
                    return await connection.QuerySingleOrDefaultAsync<SuperAdminUser>(_DBVariables.SP_Get_SuperAdmin_User, SpRequiredParameters, null, null, CommandType.StoredProcedure);
                }
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
        public async Task<SuperAdminUser> FindActiveUserByUserNameAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { NormalizedUserName = normalizedUserName, IsActive = true };
                    return await connection.QuerySingleOrDefaultAsync<SuperAdminUser>(_DBVariables.SP_Get_SuperAdmin_User, SpRequiredParameters, null, null, CommandType.StoredProcedure);
                }
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
        public async Task<SuperAdminUser> FindActiveUserByUserEmailAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { NormalizedEmail = normalizedEmail, IsActive = true };
                    return await connection.QuerySingleOrDefaultAsync<SuperAdminUser>(_DBVariables.SP_Get_SuperAdmin_User, SpRequiredParameters, null, null, CommandType.StoredProcedure);
                }
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
        public async Task<IList<SuperAdminUser>> FindAllActiveUserAsync(string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { IsActive = true };
                    return (await connection.QueryAsync<SuperAdminUser>(_DBVariables.SP_GetAll_SuperAdmin_User, SpRequiredParameters, null, null, CommandType.StoredProcedure)).ToList();
                }
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
        public async Task UpdateLoginState(int userId, string Ip, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { Id = userId, LastLoginIP = Ip };
                    await connection.QueryAsync<SuperAdminUser>(_DBVariables.SP_AddLoginInfo_SuperAdmin, SpRequiredParameters, null, null, CommandType.StoredProcedure);
                }
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
    }
}
