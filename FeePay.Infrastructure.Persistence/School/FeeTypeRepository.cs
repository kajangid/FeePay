using Dapper;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository.School;
using FeePay.Core.Domain.Entities.Identity;
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
    public class FeeTypeRepository : IFeeTypeRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public FeeTypeRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }

        public async Task<int> AddAsync(FeeType feeType, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                feeType.NormalizedName = feeType.Name.ToUpper();
                var Parameters = new
                {
                    feeType.Name,
                    feeType.NormalizedName,
                    feeType.Code,
                    feeType.Description,
                    feeType.IsActive,
                    feeType.AddedBy
                };
                return await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_FeeType,
                    Parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<int> UpdateAsync(FeeType feeType, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                feeType.NormalizedName = feeType.Name.ToUpper();
                var Parameters = new
                {
                    feeType.Id,
                    feeType.Name,
                    feeType.NormalizedName,
                    feeType.Code,
                    feeType.Description,
                    feeType.IsActive,
                    feeType.ModifyBy
                };
                return await connection.ExecuteAsync(_dBVariables.SP_Update_FeeType,
                    Parameters, commandType: CommandType.StoredProcedure);
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
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Delete_FeeType,
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
        }
        public async Task<IEnumerable<FeeType>> GetAllActiveAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<FeeType>(_dBVariables.SP_GetAll_FeeType,
                    new { IsActive = true },
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
        public async Task<FeeType> FindActiveByIdAsync(int Id, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<FeeType>(_dBVariables.SP_Get_FeeType,
                    new { Id, IsActive = true },
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
        public async Task<FeeType> FindActiveByNameAsync(string Name, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<FeeType>(_dBVariables.SP_Get_FeeType,
                    new { NormalizeName = Name.ToUpper(), IsActive = true },
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
        public async Task<FeeType> FindActiveByCodeAsync(string Code, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<FeeType>(_dBVariables.SP_Get_FeeType,
                    new { Code, IsActive = true },
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
        public async Task<IEnumerable<FeeType>> GetAllAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<FeeType>(_dBVariables.SP_GetAll_FeeType,
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
        public async Task<FeeType> FindByIdAsync(int Id, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<FeeType>(_dBVariables.SP_Get_FeeType,
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
        }
        public async Task<FeeType> FindByNameAsync(string Name, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<FeeType>(_dBVariables.SP_Get_FeeType,
                    new { NormalizeName = Name.ToUpper() },
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
        public async Task<FeeType> FindByCodeAsync(string Code, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<FeeType>(_dBVariables.SP_Get_FeeType,
                    new { Code },
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



        public async Task<IEnumerable<FeeType>> GetAllActive_WithAddEditUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<FeeType, SchoolAdminUser, SchoolAdminUser, FeeType>(_dBVariables.SP_GetAll_FeeType_AddEditUser,
                    (feetype, addeduser, modifyuser) => { feetype.AddedByUser = addeduser; feetype.ModifyByUser = modifyuser; return feetype; },
                    new { IsActive = true },
                    splitOn: "Id,Id,Id",
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
        public async Task<FeeType> FindActiveById_WithAddEditUserAsync(int Id, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeType, SchoolAdminUser, SchoolAdminUser, FeeType>(_dBVariables.SP_Get_FeeType_AddEditUser,
                    (feetype, addeduser, modifyuser) => { feetype.AddedByUser = addeduser; feetype.ModifyByUser = modifyuser; return feetype; },
                    new { Id, IsActive = true },
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);

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
        public async Task<FeeType> FindActiveByName_WithAddEditUserAsync(string Name, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeType, SchoolAdminUser, SchoolAdminUser, FeeType>(_dBVariables.SP_Get_FeeType_AddEditUser,
                    (feetype, addeduser, modifyuser) => { feetype.AddedByUser = addeduser; feetype.ModifyByUser = modifyuser; return feetype; },
                    new { NormalizeName = Name.ToUpper(), IsActive = true },
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);

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
        public async Task<FeeType> FindActiveByCode_WithAddEditUserAsync(string Code, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeType, SchoolAdminUser, SchoolAdminUser, FeeType>(_dBVariables.SP_Get_FeeType_AddEditUser,
                    (feetype, addeduser, modifyuser) => { feetype.AddedByUser = addeduser; feetype.ModifyByUser = modifyuser; return feetype; },
                    new { Code, IsActive = true },
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);

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
        public async Task<IEnumerable<FeeType>> GetAll_WithAddEditUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<FeeType, SchoolAdminUser, SchoolAdminUser, FeeType>(_dBVariables.SP_GetAll_FeeType_AddEditUser,
                    (feetype, addeduser, modifyuser) => { feetype.AddedByUser = addeduser; feetype.ModifyByUser = modifyuser; return feetype; },
                    splitOn: "Id,Id,Id",
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
        public async Task<FeeType> FindById_WithAddEditUserAsync(int Id, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeType, SchoolAdminUser, SchoolAdminUser, FeeType>(_dBVariables.SP_Get_FeeType_AddEditUser,
                    (feetype, addeduser, modifyuser) => { feetype.AddedByUser = addeduser; feetype.ModifyByUser = modifyuser; return feetype; },
                    new { Id },
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);

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
        public async Task<FeeType> FindByName_WithAddEditUserAsync(string Name, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeType, SchoolAdminUser, SchoolAdminUser, FeeType>(_dBVariables.SP_Get_FeeType_AddEditUser,
                    (feetype, addeduser, modifyuser) => { feetype.AddedByUser = addeduser; feetype.ModifyByUser = modifyuser; return feetype; },
                    new { NormalizeName = Name.ToUpper() },
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);

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
        public async Task<FeeType> FindByCode_WithAddEditUserAsync(string Code, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeType, SchoolAdminUser, SchoolAdminUser, FeeType>(_dBVariables.SP_Get_FeeType_AddEditUser,
                    (feetype, addeduser, modifyuser) => { feetype.AddedByUser = addeduser; feetype.ModifyByUser = modifyuser; return feetype; },
                    new { Code },
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);

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



        // private methods
        private string GetConStr(string dbId = null)
        {
            return string.IsNullOrEmpty(dbId) ?
                _connectionStringBuilder.GetSchoolConnectionString() : // Demo DB
                _connectionStringBuilder.GetDynamicSchoolConnectionString(dbId);
        }
    }
}
