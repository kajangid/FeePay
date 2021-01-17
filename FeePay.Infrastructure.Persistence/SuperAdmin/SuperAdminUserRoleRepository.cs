using Dapper;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository.SuperAdmin;
using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Infrastructure.Persistence.SuperAdmin
{
    public class SuperAdminUserRoleRepository : ISuperAdminUserRoleRepository
    {
        public SuperAdminUserRoleRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _ConnectionStringBuilder = connectionStringBuilder;
            _DefaultConnectionString = connectionStringBuilder.GetDefaultConnectionString();
            _DBVariables = dBVariables;
        }
        private readonly IDBVariables _DBVariables;
        private readonly string _DefaultConnectionString;
        private readonly IConnectionStringBuilder _ConnectionStringBuilder;

        public async Task<int> AssignRoleToUserAsync(SuperAdminUser user, string roleName, string dbId = null)
        {
            var normalizedName = roleName.ToUpper();
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var roleId = await connection.ExecuteScalarAsync<int?>(_DBVariables.SP_Get_SuperAdmin_Role
                        , new { NormalizedName = normalizedName }
                        , commandType: CommandType.StoredProcedure);
                    if (!roleId.HasValue)
                        roleId = await connection.ExecuteAsync(_DBVariables.SP_Add_SuperAdmin_Role
                            , new { RoleName = roleName, NormalizedName = normalizedName }
                            , commandType: CommandType.StoredProcedure);
                    return await connection.ExecuteAsync(_DBVariables.SP_Add_SuperAdmin_UserRole
                        , new { UserId = user.Id, RoleId = roleId }
                        , commandType: CommandType.StoredProcedure);
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
        public async Task<int> UnassignUserFromRoleAsync(SuperAdminUser user, string roleName, string dbId = null)
        {
            var normalizedName = roleName.ToUpper();
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var role = await connection.QuerySingleOrDefaultAsync<SuperAdminRole>(_DBVariables.SP_Get_SuperAdmin_Role, new { NormalizedName = roleName.ToUpper() });
                    if (role != null && role.Id != 0)
                        return await connection.ExecuteAsync(_DBVariables.SP_Delete_SuperAdmin_UserRole
                            , new { UserId = user.Id, RoleId = role.Id }
                            , commandType: CommandType.StoredProcedure);
                    else
                        return 0;
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
        public async Task<IList<SuperAdminRole>> GetUserRolesAsync(SuperAdminUser user, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    return (await connection.QueryAsync<SuperAdminRole>(_DBVariables.SP_GetUserRoles_SuperAdmin
                        , new { UserId = user.Id }
                        , commandType: CommandType.StoredProcedure)).ToList();
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
        public async Task<int> UserInRoleAsync(SuperAdminUser user, string roleName, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    return (await connection.QueryAsync<SuperAdminRole>
                        (_DBVariables.SP_GetUserRoles_SuperAdmin
                        , new { UserId = user.Id }
                        , commandType: CommandType.StoredProcedure))
                        .Where(W => W.NormalizedName == roleName.ToUpper()).Count();
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
        public async Task<IList<SuperAdminUser>> GetUsersInRoleAsync(string roleName, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    return (await connection.QueryAsync<SuperAdminUser>
                        (_DBVariables.SP_Get_SuperAdmin_UsersInRole
                        , new { UserId = roleName.ToUpper() }
                        , commandType: CommandType.StoredProcedure)).ToList();
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
