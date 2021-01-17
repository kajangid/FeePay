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

        public async Task<int> AddAsync(SchoolAdminUser user, string dbId = null)
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
        public async Task<int> UpdateAsync(SchoolAdminUser user, string dbId = null)
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
        public async Task<int> DeleteAsync(int Id, string dbId = null)
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
                await connection.ExecuteScalarAsync<int>(_DBVariables.SP_Add_SchoolAdmin_User_LoginInfo, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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



        public async Task<SchoolAdminUser> FindByIdAsync(int userId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = userId };
                //return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                //    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);

                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User,
                    (user, role) => { user.Roles.Add(role); return user; },
                    SpRequiredParameters,
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });
                return result?.FirstOrDefault();
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
        public async Task<SchoolAdminUser> FindByUserNameAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedUserName = normalizedUserName };
                //return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                //    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User,
                    (user, role) => { user.Roles.Add(role); return user; },
                    SpRequiredParameters,
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });
                return result?.FirstOrDefault();
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
        public async Task<SchoolAdminUser> FindByEmailAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedEmail = normalizedEmail };
                //return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                //    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User,
                    (user, role) => { user.Roles.Add(role); return user; },
                    SpRequiredParameters,
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });
                return result?.FirstOrDefault();
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
        public async Task<SchoolAdminUser> FindActiveByIdAsync(int userId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = userId, IsActive = true };
                //return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                //    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User,
                    (user, role) => { user.Roles.Add(role); return user; },
                    SpRequiredParameters,
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });
                return result?.FirstOrDefault();
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
        public async Task<SchoolAdminUser> FindActiveByUserNameAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedUserName = normalizedUserName, IsActive = true };
                //return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                //    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User,
                    (user, role) => { user.Roles.Add(role); return user; },
                    SpRequiredParameters,
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });
                return result?.FirstOrDefault();
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
        public async Task<SchoolAdminUser> FindActiveByEmailAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedEmail = normalizedEmail, IsActive = true };
                //return await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                //    _DBVariables.SP_Get_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User,
                    (user, role) => { user.Roles.Add(role); return user; },
                    SpRequiredParameters,
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });
                return result?.FirstOrDefault();
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



        public async Task<IEnumerable<SchoolAdminUser>> FindAllActiveAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { IsActive = true };
                //return await connection.QueryAsync<SchoolAdminUser>
                //    (_DBVariables.SP_GetAll_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser>(
                    _DBVariables.SP_GetAll_SchoolAdmin_User,
                    (user, role) => { user.Roles.Add(role); return user; },
                    SpRequiredParameters,
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
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
        public async Task<IEnumerable<SchoolAdminUser>> FindAllAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { };
                //return await connection.QueryAsync<SchoolAdminUser>
                //    (_DBVariables.SP_GetAll_SchoolAdmin_User, SpRequiredParameters, commandType: CommandType.StoredProcedure);
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser>(
                    _DBVariables.SP_GetAll_SchoolAdmin_User,
                    (user, role) => { user.Roles.Add(role); return user; },
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
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



        public async Task<SchoolAdminUser> FindById_WithAddEditUserAsync(int userId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = userId };
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                (user, role, addedbyuser, modifybyuser) => { user.Roles.Add(role); user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                , SpRequiredParameters
                , splitOn: "Id,Id,Id,Id"
                , commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });

                return result?.FirstOrDefault();
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
        public async Task<SchoolAdminUser> FindByUserName_WithAddEditUserAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedUserName = normalizedUserName };
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                (user, role, addedbyuser, modifybyuser) => { user.Roles.Add(role); user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                , SpRequiredParameters
                , splitOn: "Id,Id,Id,Id"
                , commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });

                return result?.FirstOrDefault();
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
        public async Task<SchoolAdminUser> FindByEmail_WithAddEditUserAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedEmail = normalizedEmail };
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                (user, role, addedbyuser, modifybyuser) => { user.Roles.Add(role); user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                , SpRequiredParameters
                , splitOn: "Id,Id,Id,Id"
                , commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });

                return result?.FirstOrDefault();

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
        public async Task<SchoolAdminUser> FindActiveById_WithAddEditUserAsync(int userId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = userId, IsActive = true };
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                    (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                    (user, role, addedbyuser, modifybyuser) => { user.Roles.Add(role); user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                    , SpRequiredParameters
                    , splitOn: "Id,Id,Id,Id"
                    , commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });

                return result?.FirstOrDefault();

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
        public async Task<SchoolAdminUser> FindActiveByUserName_WithAddEditUserAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedUserName = normalizedUserName, IsActive = true };
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                (user, role, addedbyuser, modifybyuser) => { user.Roles.Add(role); user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                , SpRequiredParameters
                , splitOn: "Id,Id,Id,Id"
                , commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });

                return result?.FirstOrDefault();

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
        public async Task<SchoolAdminUser> FindActiveByEmail_WithAddEditUserAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedEmail = normalizedEmail, IsActive = true };
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                (_DBVariables.SP_Get_SchoolAdmin_User_With_AddEditUser,
                (user, role, addedbyuser, modifybyuser) => { user.Roles.Add(role); user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                , SpRequiredParameters
                , splitOn: "Id,Id,Id,Id"
                , commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                    return groupedPost;
                });

                return result?.FirstOrDefault();

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



        public async Task<IEnumerable<SchoolAdminUser>> FindAllActive_WithAddEditUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { IsActive = true };
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                    (_DBVariables.SP_GetAll_SchoolAdmin_User_With_AddEditUser,
                    (user, role, addedbyuser, modifybyuser) => { user.Roles.Add(role); user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                    , SpRequiredParameters
                    , splitOn: "Id,Id,Id,Id"
                    , commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
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
        public async Task<IEnumerable<SchoolAdminUser>> FindAll_WithAddEditUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { };
                var users = await connection.QueryAsync<SchoolAdminUser, SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminUser>
                    (_DBVariables.SP_GetAll_SchoolAdmin_User_With_AddEditUser,
                    (user, role, addedbyuser, modifybyuser) => { user.Roles.Add(role); user.AddedByUser = addedbyuser; user.ModifyByUser = modifybyuser; return user; }
                    , SpRequiredParameters
                    , splitOn: "Id,Id,Id,Id"
                    , commandType: CommandType.StoredProcedure);

                var result = users.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
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
    }
}
