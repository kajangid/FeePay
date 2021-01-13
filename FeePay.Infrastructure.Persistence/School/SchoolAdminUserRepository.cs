using Dapper;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository.School;
using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Infrastructure.Persistence.School
{
    public class SchoolAdminUserRepository : ISchoolAdminUserRepository
    {
        public SchoolAdminUserRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _ConnectionStringBuilder = connectionStringBuilder;
            _DBVariables = dBVariables;
        }
        private readonly IDBVariables _DBVariables;
        private readonly IConnectionStringBuilder _ConnectionStringBuilder;
        private string GetConStr(string dbId = null)
        {
            return string.IsNullOrEmpty(dbId) ?
                _ConnectionStringBuilder.GetSchoolConnectionString() : // Demo DB
                _ConnectionStringBuilder.GetDynamicSchoolConnectionString(dbId);
        }

        public async Task<int> AddUserAsync(SchoolAdminUser user, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new
                {
                    AccessFailedCount = user.AccessFailedCount,
                    AddedBy = user.AddedBy,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    FirstName = user.FirstName,
                    FullName = user.FullName,
                    IsActive = user.IsActive,
                    LastName = user.LastName,
                    LockoutEnabled = user.LockoutEnabled,
                    LockoutEndDate = user.LockoutEndDate,
                    NormalizedEmail = user.NormalizedEmail,
                    NormalizedUserName = user.NormalizedUserName,
                    Password = user.Password,
                    PasswordHash = user.PasswordHash,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    SecurityStamp = user.SecurityStamp,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    UserName = user.UserName
                };
                return await connection.ExecuteAsync(_DBVariables.SP_Add_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
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
        public async Task<int> UpdateUserAsync(SchoolAdminUser user, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new
                {
                    Id = user.Id,
                    AccessFailedCount = user.AccessFailedCount,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    FirstName = user.FirstName,
                    FullName = user.FullName,
                    IsActive = user.IsActive,
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
                    SecurityStamp = user.SecurityStamp,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    UserName = user.UserName
                };
                return await connection.ExecuteAsync(_DBVariables.SP_Update_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
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
                using var connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_DBVariables.SP_Delete_SchoolAdmin_User, new { Id = Id }, commandType: CommandType.StoredProcedure);
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
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                    var SpRequiredParameters = new { Id = userId, LastLoginIP = Ip };
                    await connection.ExecuteScalarAsync<int>(_DBVariables.SP_AddLoginInfo_SchoolAdmin, SpRequiredParameters, commandType: CommandType.StoredProcedure);
                
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



        public async Task<SchoolAdminUser> FindUserByUserIdAsync(int userId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = userId };
                return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
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
        public async Task<SchoolAdminUser> FindUserByUserNameAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedUserName = normalizedUserName };
                return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
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
        public async Task<SchoolAdminUser> FindUserByUserEmailAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedEmail = normalizedEmail };
                return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
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
        public async Task<SchoolAdminUser> FindActiveUserByUserIdAsync(int userId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = userId, IsActive = true };
                return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
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
        public async Task<SchoolAdminUser> FindActiveUserByUserNameAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedUserName = normalizedUserName, IsActive = true };
                return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
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
        public async Task<SchoolAdminUser> FindActiveUserByUserEmailAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedEmail = normalizedEmail, IsActive = true };
                return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
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



        public async Task<IEnumerable<SchoolAdminUser>> FindAllActiveUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { IsActive = true };
                return await connection.QueryAsync<SchoolAdminUser>
                    (_DBVariables.SP_GetAll_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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
        public async Task<IEnumerable<SchoolAdminUser>> FindAllUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { };
                return await connection.QueryAsync<SchoolAdminUser>
                    (_DBVariables.SP_GetAll_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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


        public async Task<IList<SchoolAdminUser>> GetAllUsersWithRolesAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return (await connection.QueryAsync<SchoolAdminUser>(
                    _DBVariables.SP_GetAll_SchoolAdmin_User, commandType: CommandType.StoredProcedure)).ToList();

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




        public async Task<SchoolAdminUser> FindUserByUserId_WithAddEditUserAsync(int userId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = userId };
                var list = await connection.QueryAsync<SchoolAdminUser, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                (user, addedbyuser, modifybyuser) => { user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                , SpRequiredParameters, null, true
                , splitOn: "Id,Id,Id", null
                , CommandType.StoredProcedure);
                return list?.FirstOrDefault();

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
        public async Task<SchoolAdminUser> FindUserByUserName_WithAddEditUserAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedUserName = normalizedUserName };
                var list = await connection.QueryAsync<SchoolAdminUser, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                (user, addedbyuser, modifybyuser) => { user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                , SpRequiredParameters, null, true
                , splitOn: "Id,Id,Id", null
                , CommandType.StoredProcedure);
                return list?.FirstOrDefault();

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
        public async Task<SchoolAdminUser> FindUserByUserEmail_WithAddEditUserAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedEmail = normalizedEmail };
                var list = await connection.QueryAsync<SchoolAdminUser, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                (user, addedbyuser, modifybyuser) => { user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                , SpRequiredParameters, null, true
                , splitOn: "Id,Id,Id", null
                , CommandType.StoredProcedure);
                return list?.FirstOrDefault();

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
        public async Task<SchoolAdminUser> FindActiveUserByUserId_WithAddEditUserAsync(int userId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = userId, IsActive = true };
                var list = await connection.QueryAsync<SchoolAdminUser, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                    (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                    (user, addedbyuser, modifybyuser) => { user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                    , SpRequiredParameters, null, true
                    , splitOn: "Id,Id,Id", null
                    , CommandType.StoredProcedure);
                return list?.FirstOrDefault();

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
        public async Task<SchoolAdminUser> FindActiveUserByUserName_WithAddEditUserAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedUserName = normalizedUserName, IsActive = true };
                var list = await connection.QueryAsync<SchoolAdminUser, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                (user, addedbyuser, modifybyuser) => { user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                , SpRequiredParameters, null, true
                , splitOn: "Id,Id,Id", null
                , CommandType.StoredProcedure);
                return list?.FirstOrDefault();

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
        public async Task<SchoolAdminUser> FindActiveUserByUserEmail_WithAddEditUserAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedEmail = normalizedEmail, IsActive = true };
                var list = await connection.QueryAsync<SchoolAdminUser, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                (user, addedbyuser, modifybyuser) => { user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                , SpRequiredParameters, null, true
                , splitOn: "Id,Id,Id", null
                , CommandType.StoredProcedure);
                return list?.FirstOrDefault();

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



        public async Task<IEnumerable<SchoolAdminUser>> FindAllActiveUser_WithAddEditUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { IsActive = true };
                return await connection.QueryAsync<SchoolAdminUser, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                    (_DBVariables.SP_GetAll_SchoolAdmin_User_With_AddEditUser,
                    (user, addedbyuser, modifybyuser) => { user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                    , SpRequiredParameters, null, true
                    , splitOn: "Id,Id,Id", null
                    , CommandType.StoredProcedure);

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
        public async Task<IEnumerable<SchoolAdminUser>> FindAllUser_WithAddEditUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { };
                return await connection.QueryAsync<SchoolAdminUser, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                    (_DBVariables.SP_GetAll_SchoolAdmin_User_With_AddEditUser,
                    (user, addedbyuser, modifybyuser) => { user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                    , SpRequiredParameters, null, true
                    , splitOn: "Id,Id,Id", null
                    , CommandType.StoredProcedure);

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
