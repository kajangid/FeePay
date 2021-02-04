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
using FeePay.Core.Domain.Entities.SuperAdmin;

namespace FeePay.Infrastructure.Persistence.SuperAdmin
{
    public class RegisteredSchoolRepository : IRegisteredSchoolRepository
    {
        public RegisteredSchoolRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _DefaultConnectionString = connectionStringBuilder.GetDefaultConnectionString();
            _DBVariables = dBVariables;
        }
        private readonly IDBVariables _DBVariables;
        private readonly string _DefaultConnectionString;

        public async Task<int> AddAsync(RegisteredSchool school)
        {
            try
            {
                IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                var SpRequiredParameters = new
                {
                    school.AddedBy,
                    school.IsActive,
                    school.NormalizedName,
                    school.Name,
                    school.UniqueId,
                    school.Address,
                    school.SchoolImage,
                    school.ContactNumber,
                    school.PrincipalName,
                    school.IsApproved
                };
                return await connection.ExecuteScalarAsync<int>(
                    _DBVariables.SP_Add_RegisteredSchool,
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
        }
        public async Task<int> UpdateAsync(RegisteredSchool school)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                var SpRequiredParameters = new
                {
                    school.Id,
                    school.ModifyBy,
                    school.IsActive,
                    school.NormalizedName,
                    school.Name,
                    school.UniqueId,
                    school.Address,
                    school.SchoolImage,
                    school.ContactNumber,
                    school.PrincipalName,
                    school.IsApproved
                };
                return await connection.ExecuteScalarAsync<int>(
                    _DBVariables.SP_Update_RegisteredSchool,
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
        public async Task<int> DeleteAsync(int Id)
        {
            try
            {
                IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                return await connection.ExecuteAsync(
                    _DBVariables.SP_Delete_RegisteredSchool,
                    new { Id },
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
        public async Task<RegisteredSchool> FindByNameAsync(string normalizedName, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                var SpRequiredParameters = new { NormalizedName = normalizedName.ToUpper(), IsActive = isActive };
                return await connection.QuerySingleOrDefaultAsync<RegisteredSchool>(
                    _DBVariables.SP_Get_RegisteredSchool,
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
        public async Task<RegisteredSchool> FindByIdAsync(int schoolId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                var SpRequiredParameters = new { Id = schoolId, IsActive = isActive };
                return await connection.QuerySingleOrDefaultAsync<RegisteredSchool>(
                    _DBVariables.SP_Get_RegisteredSchool,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<RegisteredSchool> FindByUniqueIdAsync(string schoolUniqueId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                var SpRequiredParameters = new { UniqueId = schoolUniqueId, IsActive = isActive };
                return await connection.QuerySingleOrDefaultAsync<RegisteredSchool>(
                    _DBVariables.SP_Get_RegisteredSchool,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<RegisteredSchool>> GetAllAsync(bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                var SpRequiredParameters = new { IsActive = isActive };
                var list = await connection.QueryAsync<RegisteredSchool, SuperAdminUser, SuperAdminUser, RegisteredSchool>(
                    _DBVariables.SP_GetAll_RegisteredSchool,
                    (rSchool, add, modify) =>
                    {
                        return rSchool;
                    },
                    SpRequiredParameters,
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);
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
        public async Task<IEnumerable<RegisteredSchool>> GetAll_WithAddEditUserAsync(bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
                var SpRequiredParameters = new { IsActive = isActive };
                var list = await connection.QueryAsync<RegisteredSchool, SuperAdminUser, SuperAdminUser, RegisteredSchool>(
                    _DBVariables.SP_GetAll_RegisteredSchool,
                    (rSchool, add, modify) =>
                    {
                        rSchool.AddedByUser = add;
                        rSchool.ModifyByUser = modify;
                        return rSchool;
                    },
                    SpRequiredParameters,
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);
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
    }
}
