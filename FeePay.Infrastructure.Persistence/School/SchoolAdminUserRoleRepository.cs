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
    public class SchoolAdminUserRoleRepository : ISchoolAdminUserRoleRepository
    {
        public SchoolAdminUserRoleRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
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

        public async Task<int> AssignRoleToUserAsync(SchoolAdminUser user, string roleName, string dbId = null)
        {
            var normalizedName = roleName.ToUpper();
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                // Check if role Exists 
                int? roleId = (await connection.QuerySingleOrDefaultAsync<SchoolAdminRole>(_DBVariables.SP_Get_SchoolAdmin_Role
                    , new { NormalizedName = normalizedName, IsActive = true }, commandType: CommandType.StoredProcedure))?.Id;
                // If role Does not exist create new    
                if (!roleId.HasValue)
                    roleId = await connection.ExecuteAsync(_DBVariables.SP_Add_SchoolAdmin_Role
                        , new { RoleName = roleName, NormalizedName = normalizedName, IsActive = true }, commandType: CommandType.StoredProcedure);
                // Assign role to user
                return await connection.ExecuteAsync(_DBVariables.SP_Add_SchoolAdmin_UserRole, new { UserId = user.Id, RoleId = roleId, IsActive = true }
                , commandType: CommandType.StoredProcedure);
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
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }

        }
        public async Task<int> UnassignUserFromRoleAsync(SchoolAdminUser user, string roleName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                // Check if role Exists 
                var role = await connection.QuerySingleOrDefaultAsync<SchoolAdminRole>(_DBVariables.SP_Get_SchoolAdmin_Role
                        , new { NormalizedName = roleName.ToUpper(), IsActive = true }, commandType: CommandType.StoredProcedure);
                // Check if role Exists remove
                if (role != null && role.Id != 0)
                    return await connection.ExecuteAsync(_DBVariables.SP_Delete_SchoolAdmin_UserRole
                        , new { UserId = user.Id, RoleId = role.Id }, commandType: CommandType.StoredProcedure);
                else
                    return 0;
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
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }

        }
        public async Task<IList<SchoolAdminRole>> GetUserRolesAsync(int userId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return (await connection.QueryAsync<SchoolAdminRole>(_DBVariables.SP_Get_SchoolAdmin_UserRoles
                    , new { UserId = userId, IsActive = true }, commandType: CommandType.StoredProcedure)).ToList();
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
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }

        }

        /// <summary>
        /// Check If user is present in role
        /// </summary>
        /// <param name="userId"> user id which we will check </param>
        /// <param name="roleName"> role name in which we check user </param>
        /// <param name="dbId"> database id to get connection string </param>
        /// <returns></returns>
        public async Task<int> UserInRoleAsync(int userId, string roleName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return (await connection.QueryAsync<SchoolAdminRole>(_DBVariables.SP_Get_SchoolAdmin_UserRoles
                    , new { UserId = userId, IsActive = true }, commandType: CommandType.StoredProcedure))
                    .Where(W => W.NormalizedName == roleName.ToUpper()).Count();
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
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }
        }
        public async Task<IList<SchoolAdminUser>> GetUsersInRoleAsync(string roleName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return (await connection.QueryAsync<SchoolAdminUser>
                    (_DBVariables.SP_Get_SchoolAdmin_UsersInRole, new { NormalizedRoleName = roleName.ToUpper() }
                    , commandType: CommandType.StoredProcedure)).ToList();
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
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }
        }


        public async Task<IList<SchoolAdminUser>> GetUsersInRoleAsync(int roleID, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return (await connection.QueryAsync<SchoolAdminUser>
                    (_DBVariables.SP_Get_SchoolAdmin_UsersInRole, new { RoleId = roleID }
                    , commandType: CommandType.StoredProcedure)).ToList();
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
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }
        }


        public async Task<bool> delete(int Id, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                int res = await connection.ExecuteAsync(
                     _DBVariables.SP_Delete_SchoolAdmin_UserRole,
                     new { Id },
                     commandType: CommandType.StoredProcedure);

                return (res > 0);
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
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }
        }
        public async Task<bool> delete(int userId, int roleId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                int res = await connection.ExecuteAsync(
                     _DBVariables.SP_Delete_SchoolAdmin_UserRole,
                     new { UserId = userId, RoleId = roleId },
                     commandType: CommandType.StoredProcedure);

                return (res > 0);
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
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }
        }
        public async Task<bool> delete(string userName, int roleId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                // Check if role Exists 
                var role = await connection.QuerySingleOrDefaultAsync<SchoolAdminRole>(
                    _DBVariables.SP_Get_SchoolAdmin_Role,
                    new { Id = roleId, IsActive = true },
                    commandType: CommandType.StoredProcedure);

                // Get user Id 
                var user = await connection.QuerySingleOrDefaultAsync<SchoolAdminUser>(
                    _DBVariables.SP_Get_SchoolAdmin_User,
                    new { NormalizedName = userName.ToUpper(), IsActive = true },
                    commandType: CommandType.StoredProcedure);

                int res = 0;
                // Check if role Exists remove
                if (role != null && user != null)
                    res = await connection.ExecuteAsync(
                         _DBVariables.SP_Delete_SchoolAdmin_UserRole,
                         new { UserId = user.Id, RoleId = role.Id },
                         commandType: CommandType.StoredProcedure);

                return (res > 0);
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
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }
        }
        public async Task<bool> delete(int userId, string roleName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                // Check if role Exists 
                var role = await connection.QuerySingleOrDefaultAsync<SchoolAdminRole>(
                    _DBVariables.SP_Get_SchoolAdmin_Role,
                    new { NormalizedName = roleName.ToUpper(), IsActive = true },
                    commandType: CommandType.StoredProcedure);

                int res = 0;
                // Check if role Exists remove
                if (role != null && role.Id != 0)
                    res = await connection.ExecuteAsync(
                         _DBVariables.SP_Delete_SchoolAdmin_UserRole,
                         new { UserId = userId, RoleId = role.Id },
                         commandType: CommandType.StoredProcedure);

                return (res > 0);
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
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }
        }
    }
}
