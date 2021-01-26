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
using FeePay.Core.Application.Interface.Repository.SuperAdmin;
using FeePay.Core.Domain.Entities.Identity;

namespace FeePay.Infrastructure.Persistence.SuperAdmin
{
    public class SuperAdminRoleRepository : ISuperAdminRoleRepository
    { 
        public SuperAdminRoleRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _DefaultConnectionString = connectionStringBuilder.GetDefaultConnectionString();
            _DBVariables = dBVariables;
        }
        private readonly IDBVariables _DBVariables;
        private readonly string _DefaultConnectionString;

        public async Task<int> AddRoleAsync(SuperAdminRole role, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                    var SpRequiredParameters = new
                    {
                        role.AddedBy,
                        role.IsActive,
                        role.NormalizedName,
                        role.Name
                    };
                    return await connection.ExecuteAsync(_DBVariables.SP_Add_SuperAdmin_Role, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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
        public async Task<int> UpdateRoleAsync(SuperAdminRole role, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                    var SpRequiredParameters = new
                    {
                         role.Id,
                         role.IsActive,
                         role.NormalizedName,
                         role.Name,
                         role.ModifyBy
                    };
                    return await connection.ExecuteAsync(_DBVariables.SP_Update_SuperAdmin_Role, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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
        public async Task<int> DeleteRoleAsync(int Id, string dbId = null)
        {
            try
            {
                using var connection = new SqlConnection(_DefaultConnectionString);
                    return await connection.ExecuteAsync(_DBVariables.SP_Delete_SuperAdmin_Role, new { Id }, null, null, CommandType.StoredProcedure);
                
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
        public async Task<SuperAdminRole> FindRoleByRoleIdAsync(int roleId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                    var SpRequiredParameters = new { Id = roleId };
                    return await connection.QuerySingleOrDefaultAsync<SuperAdminRole>(_DBVariables.SP_Get_SuperAdmin_Role, SpRequiredParameters, null, null, CommandType.StoredProcedure);
                
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
        public async Task<SuperAdminRole> FindRoleByRoleNameAsync(string normalizedName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                    var SpRequiredParameters = new { NormalizedName = normalizedName };
                    return await connection.QuerySingleOrDefaultAsync<SuperAdminRole>(_DBVariables.SP_Get_SuperAdmin_Role, SpRequiredParameters, null, null, CommandType.StoredProcedure);
                
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
        public async Task<SuperAdminRole> FindActiveRoleByRoleIdAsync(int roleId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                    var SpRequiredParameters = new { Id = roleId, IsActive = true };
                    return await connection.QuerySingleOrDefaultAsync<SuperAdminRole>(_DBVariables.SP_Get_SuperAdmin_Role, SpRequiredParameters, null, null, CommandType.StoredProcedure);
                
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
        public async Task<SuperAdminRole> FindActiveRoleByRoleNameAsync(string normalizedName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                    var SpRequiredParameters = new { NormalizedName = normalizedName, IsActive = true };
                    return await connection.QuerySingleOrDefaultAsync<SuperAdminRole>(_DBVariables.SP_Get_SuperAdmin_Role, SpRequiredParameters, null, null, CommandType.StoredProcedure);
                
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
