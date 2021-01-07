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
        private string getConStr(string dbId = null)
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
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var roleId = await connection.ExecuteScalarAsync<int?>(_DBVariables.SP_Get_SchoolAdmin_Role, new { NormalizedName = normalizedName });
                    if (!roleId.HasValue)
                        roleId = await connection.ExecuteAsync(_DBVariables.SP_Add_SchoolAdmin_Role, new { RoleName = roleName, NormalizedName = normalizedName });
                    return await connection.ExecuteAsync(_DBVariables.SP_Add_SchoolAdmin_UserRole, new { UserId = user.Id, RoleId = roleId });
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }

        }
        public async Task<int> UnassignUserFromRoleAsync(SchoolAdminUser user, string roleName, string dbId = null)
        {
            var normalizedName = roleName.ToUpper();
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var role = await connection.QuerySingleOrDefaultAsync<SchoolAdminRole>(_DBVariables.SP_Get_SchoolAdmin_Role, new { NormalizedName = roleName.ToUpper() });
                    if (role != null && role.Id != 0)
                        return await connection.ExecuteAsync(_DBVariables.SP_Delete_SchoolAdmin_UserRole, new { UserId = user.Id, RoleId = role.Id });
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }

        }
        public async Task<IList<SchoolAdminRole>> GetUserRolesAsync(SchoolAdminUser user, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    return (await connection.QueryAsync<SchoolAdminRole>(_DBVariables.SP_GetUserRoles_SchoolAdmin, new { UserId = user.Id })).ToList();
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }

        }
        public async Task<int> UserInRoleAsync(SchoolAdminUser user, string roleName, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    return (await connection.QueryAsync<SchoolAdminRole>
                        (_DBVariables.SP_GetUserRoles_SchoolAdmin, new { UserId = user.Id }))
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }
        }
        public async Task<IList<SchoolAdminUser>> GetUsersInRoleAsync(string roleName, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    return (await connection.QueryAsync<SchoolAdminUser>
                        (_DBVariables.SP_Get_SchoolAdmin_UsersInRole, new { UserId = roleName.ToUpper() })).ToList();
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} exception", GetType().FullName), ex);
            }
        }

    }
}
