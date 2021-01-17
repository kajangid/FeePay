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
    public class SchoolAdminRoleRepository : ISchoolAdminRoleRepository
    {
        public SchoolAdminRoleRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
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



        public async Task<int> AddAsync(SchoolAdminRole role, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(GetConStr(dbId)))
                {
                    var SpRequiredParameters = new
                    {
                        AddedBy = role.AddedBy,
                        IsActive = role.IsActive,
                        NormalizedName = role.NormalizedName,
                        Name = role.Name
                    };
                    int id = await connection.ExecuteScalarAsync<int>(_DBVariables.SP_Add_SchoolAdmin_Role
                        , SpRequiredParameters, commandType: CommandType.StoredProcedure);
                    return id;
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

        public async Task<int> UpdateAsync(SchoolAdminRole role, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(GetConStr(dbId)))
                {
                    var SpRequiredParameters = new
                    {
                        Id = role.Id,
                        IsActive = role.IsActive,
                        NormalizedName = role.NormalizedName,
                        Name = role.Name,
                        ModifyBy = role.ModifyBy
                    };
                    return await connection.ExecuteScalarAsync<int>(_DBVariables.SP_Update_SchoolAdmin_Role, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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

        public async Task<int> DeleteAsync(int Id, string dbId = null)
        {
            try
            {
                using (var connection = new SqlConnection(GetConStr(dbId)))
                {
                    return await connection.ExecuteAsync(_DBVariables.SP_Delete_SchoolAdmin_Role, new { Id = Id }, null, null, CommandType.StoredProcedure);
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




        public async Task<SchoolAdminRole> FindByIdAsync(int roleId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = roleId };
                return await connection.QuerySingleOrDefaultAsync<SchoolAdminRole>(
                    _DBVariables.SP_Get_SchoolAdmin_Role, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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

        public async Task<SchoolAdminRole> FindByNameAsync(string normalizedName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedName = normalizedName };
                return await connection.QuerySingleOrDefaultAsync<SchoolAdminRole>(
                    _DBVariables.SP_Get_SchoolAdmin_Role, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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

        public async Task<SchoolAdminRole> FindActiveByIdAsync(int roleId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = roleId, IsActive = true };
                return await connection.QuerySingleOrDefaultAsync<SchoolAdminRole>(
                            _DBVariables.SP_Get_SchoolAdmin_Role, SpRequiredParameters, commandType: CommandType.StoredProcedure);


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

        public async Task<SchoolAdminRole> FindActiveByNameAsync(string normalizedName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedName = normalizedName, IsActive = true };
                return await connection.QuerySingleOrDefaultAsync<SchoolAdminRole>(
                    _DBVariables.SP_Get_SchoolAdmin_Role, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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

        public async Task<IEnumerable<SchoolAdminRole>> GetAllActiveAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { IsActive = true };
                return await connection.QueryAsync<SchoolAdminRole>
                    (_DBVariables.SP_GetAll_SchoolAdmin_Role, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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

        public async Task<IEnumerable<SchoolAdminRole>> GetAllAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { };
                return await connection.QueryAsync<SchoolAdminRole>
                    (_DBVariables.SP_GetAll_SchoolAdmin_Role, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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





        public async Task<SchoolAdminRole> FindById_WithAddEditUserAsync(int roleId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = roleId };
                var list = await connection.QueryAsync<SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminRole>
                    (_DBVariables.SP_Get_SchoolAdmin_Role_With_AddEditUser,
                    (role, addeduser, modifyuser) => { role.AddedByUser = addeduser; role.ModifyByUser = modifyuser; return role; }
                    , SpRequiredParameters, splitOn: "Id,Id,Id", commandType: CommandType.StoredProcedure);

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

        public async Task<SchoolAdminRole> FindByName_WithAddEditUserAsync(string normalizedName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedName = normalizedName };
                var list = await connection.QueryAsync<SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminRole>
                    (_DBVariables.SP_Get_SchoolAdmin_Role_With_AddEditUser,
                    (role, addeduser, modifyuser) => { role.AddedByUser = addeduser; role.ModifyByUser = modifyuser; return role; }
                    , SpRequiredParameters, splitOn: "Id,Id,Id", commandType: CommandType.StoredProcedure);

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

        public async Task<SchoolAdminRole> FindActiveById_WithAddEditUserAsync(int roleId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = roleId, IsActive = true };
                var list = await connection.QueryAsync<SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminRole>
                    (_DBVariables.SP_Get_SchoolAdmin_Role_With_AddEditUser,
                    (role, addeduser, modifyuser) => { role.AddedByUser = addeduser; role.ModifyByUser = modifyuser; return role; }
                    , SpRequiredParameters, splitOn: "Id,Id,Id", commandType: CommandType.StoredProcedure);

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

        public async Task<SchoolAdminRole> FindActiveByName_WithAddEditUserAsync(string normalizedName, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedName = normalizedName, IsActive = true };
                var list = await connection.QueryAsync<SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminRole>
                    (_DBVariables.SP_Get_SchoolAdmin_Role_With_AddEditUser,
                    (role, addeduser, modifyuser) => { role.AddedByUser = addeduser; role.ModifyByUser = modifyuser; return role; }
                    , SpRequiredParameters, splitOn: "Id,Id,Id", commandType: CommandType.StoredProcedure);

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

        public async Task<IEnumerable<SchoolAdminRole>> GetAllActive_WithAddEditUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { IsActive = true };
                return await connection.QueryAsync<SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminRole>
                    (_DBVariables.SP_GetAll_SchoolAdmin_Role_With_AddEditUser,
                    (role, addeduser, modifyuser) => { role.AddedByUser = addeduser; role.ModifyByUser = modifyuser; return role; }
                    , SpRequiredParameters, splitOn: "Id,Id,Id", commandType: CommandType.StoredProcedure);

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

        public async Task<IEnumerable<SchoolAdminRole>> GetAll_WithAddEditUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { };
                return await connection.QueryAsync<SchoolAdminRole, SchoolAdminUser, SchoolAdminUser, SchoolAdminRole>
                    (_DBVariables.SP_GetAll_SchoolAdmin_Role_With_AddEditUser,
                    (role, addeduser, modifyuser) => { role.AddedByUser = addeduser; role.ModifyByUser = modifyuser; return role; }
                    , SpRequiredParameters, splitOn: "Id,Id,Id", commandType: CommandType.StoredProcedure);

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
