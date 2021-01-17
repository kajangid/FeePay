using Dapper;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository.School;
using FeePay.Core.Domain.Entities.School;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Infrastructure.Persistence.School
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
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new
                    {
                        AddedBy = school.AddedBy,
                        IsActive = school.IsActive,
                        NormalizedName = school.NormalizedName,
                        Name = school.Name,
                        UniqueId = school.UniqueId,
                        Address = school.Address,
                        SchoolImage = school.SchoolImage,
                        ContactNumber = school.ContactNumber,
                        PrincipalName = school.PrincipalName,
                        IsApproved = school.IsApproved
                    };
                    return await connection.ExecuteAsync(_DBVariables.SP_Add_RegisteredSchool, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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
        public async Task<int> UpdateAsync(RegisteredSchool school)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new
                    {
                        Id = school.Id,
                        ModifyBy = school.ModifyBy,
                        IsActive = school.IsActive,
                        NormalizedName = school.NormalizedName,
                        Name = school.Name,
                        UniqueId = school.UniqueId,
                        Address = school.Address,
                        SchoolImage = school.SchoolImage,
                        ContactNumber = school.ContactNumber,
                        PrincipalName = school.PrincipalName,
                        IsApproved = school.IsApproved
                    };
                    return await connection.ExecuteAsync(_DBVariables.SP_Update_RegisteredSchool, SpRequiredParameters, commandType: CommandType.StoredProcedure);
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
        public async Task<int> DeleteAsync(int Id)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    return await connection.ExecuteAsync(_DBVariables.SP_Delete_RegisteredSchool, new { Id = Id }, null, null, CommandType.StoredProcedure);
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
        public async Task<RegisteredSchool> GetByNameAsync(string normalizedName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { NormalizedName = normalizedName };
                    return await connection.QuerySingleOrDefaultAsync<RegisteredSchool>(_DBVariables.SP_Get_RegisteredSchool, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<RegisteredSchool> GetByIdAsync(int schoolId)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { Id = schoolId };
                    return await connection.QuerySingleOrDefaultAsync<RegisteredSchool>(_DBVariables.SP_Get_RegisteredSchool, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<RegisteredSchool> GetByUniqueIdAsync(string schoolUniqueId)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { UniqueId = schoolUniqueId };
                    return await connection.QuerySingleOrDefaultAsync<RegisteredSchool>(_DBVariables.SP_Get_RegisteredSchool, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<RegisteredSchool> GetActiveByNameAsync(string normalizedName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { NormalizedName = normalizedName, IsActive = true };
                    return await connection.QuerySingleOrDefaultAsync<RegisteredSchool>(_DBVariables.SP_Get_RegisteredSchool, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<RegisteredSchool> GetActiveByIdAsync(int schoolId)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { Id = schoolId, IsActive = true };
                    return await connection.QuerySingleOrDefaultAsync<RegisteredSchool>(_DBVariables.SP_Get_RegisteredSchool, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<RegisteredSchool> GetActiveByUniqueIdAsync(string schoolUniqueId)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { UniqueId = schoolUniqueId, IsActive = true };
                    return await connection.QuerySingleOrDefaultAsync<RegisteredSchool>(_DBVariables.SP_Get_RegisteredSchool, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<IList<RegisteredSchool>> GetAllActiveAsync()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_DefaultConnectionString))
                {
                    var SpRequiredParameters = new { IsActive = true };
                    return (await connection.QueryAsync<RegisteredSchool>(_DBVariables.SP_GetAll_RegisteredSchool, SpRequiredParameters, null, null, CommandType.StoredProcedure)).ToList();
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
