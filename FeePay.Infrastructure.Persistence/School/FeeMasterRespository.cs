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
using FeePay.Core.Application.Interface.Repository.School;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Infrastructure.Persistence.School
{
    public class FeeMasterRespository : IFeeMasterRespository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public FeeMasterRespository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }

        public async Task<int> AddAsync(FeeMaster feeMaster, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    feeMaster.FeeGroupId,
                    feeMaster.FeeTypeId,
                    feeMaster.Amount,
                    feeMaster.DueDate,
                    feeMaster.Description,
                    feeMaster.IsActive,
                    feeMaster.AddedBy
                };
                return await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_FeeMaster,
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
        public async Task<int> UpdateAsync(FeeMaster feeMaster, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    feeMaster.Id,
                    feeMaster.FeeGroupId,
                    feeMaster.FeeTypeId,
                    feeMaster.Amount,
                    feeMaster.DueDate,
                    feeMaster.Description,
                    feeMaster.IsActive,
                    feeMaster.ModifyBy
                };
                return await connection.ExecuteAsync(_dBVariables.SP_Update_FeeMaster,
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
        public async Task<int> DeleteAsync(int Id, int? changeBy, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Delete_FeeMaster,
                    new { Id, ModifyBy = changeBy },
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


        public async Task<IEnumerable<FeeMaster>> GetAllAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<FeeMaster, FeeType, FeeGroup, FeeMaster>(_dBVariables.SP_GetAll_FeeMaster,
                    (feemaster, feetype, feegroup) => { feemaster.FeeType = feetype; feemaster.FeeGroup = feegroup; return feemaster; },
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
        public async Task<FeeMaster> FindByIdAsync(int Id, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeMaster, FeeType, FeeGroup, FeeMaster>(_dBVariables.SP_Get_FeeMaster,
                    (feemaster, feetype, feegroup) => { feemaster.FeeType = feetype; feemaster.FeeGroup = feegroup; return feemaster; },
                    new { Id }, splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);

                return list.FirstOrDefault();
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

        public async Task<IEnumerable<FeeMaster>> GetAllActiveAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<FeeMaster, FeeType, FeeGroup, FeeMaster>(_dBVariables.SP_GetAll_FeeMaster,
                    (feemaster, feetype, feegroup) => { feemaster.FeeType = feetype; feemaster.FeeGroup = feegroup; return feemaster; },
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
        public async Task<FeeMaster> FindActiveByIdAsync(int Id, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeMaster, FeeType, FeeGroup, FeeMaster>(_dBVariables.SP_Get_FeeMaster,
                    (feemaster, feetype, feegroup) => { feemaster.FeeType = feetype; feemaster.FeeGroup = feegroup; return feemaster; },
                    new { Id, IsActive = true }, splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);

                return list.FirstOrDefault();
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


        public async Task<IEnumerable<FeeMaster>> GetAllActive_WithAddEditUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<FeeMaster, SchoolAdminUser, SchoolAdminUser, FeeType, FeeGroup, FeeMaster>(
                    _dBVariables.SP_GetAll_FeeMaster_AddEditUser,
                    (feemaster, addeduser, modifyuser, feetype, feegroup) =>
                    {
                        feemaster.AddedByUser = addeduser;
                        feemaster.ModifyByUser = modifyuser;
                        feemaster.FeeType = feetype;
                        feemaster.FeeGroup = feegroup;
                        return feemaster;
                    },
                    new { IsActive = true },
                    splitOn: "Id,Id,Id,Id,Id",
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
        public async Task<FeeMaster> FindActiveById_WithAddEditUserAsync(int Id, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeMaster, SchoolAdminUser, SchoolAdminUser, FeeType, FeeGroup, FeeMaster>(
                    _dBVariables.SP_Get_FeeMaster_AddEditUser,
                    (feemaster, addeduser, modifyuser, feetype, feegroup) =>
                    {
                        feemaster.AddedByUser = addeduser;
                        feemaster.ModifyByUser = modifyuser;
                        feemaster.FeeType = feetype;
                        feemaster.FeeGroup = feegroup;
                        return feemaster;
                    },
                    new { Id, IsActive = true },
                    splitOn: "Id,Id,Id,Id,Id",
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

        public async Task<IEnumerable<FeeMaster>> GetAll_WithAddEditUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<FeeMaster, SchoolAdminUser, SchoolAdminUser, FeeType, FeeGroup, FeeMaster>(
                    _dBVariables.SP_GetAll_FeeMaster_AddEditUser,
                    (feemaster, addeduser, modifyuser, feetype, feegroup) =>
                    {
                        feemaster.AddedByUser = addeduser;
                        feemaster.ModifyByUser = modifyuser;
                        feemaster.FeeType = feetype;
                        feemaster.FeeGroup = feegroup;
                        return feemaster;
                    },
                    splitOn: "Id,Id,Id,Id,Id",
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
        public async Task<FeeMaster> FindById_WithAddEditUserAsync(int Id, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeMaster, SchoolAdminUser, SchoolAdminUser, FeeType, FeeGroup, FeeMaster>(
                    _dBVariables.SP_Get_FeeMaster_AddEditUser,
                    (feemaster, addeduser, modifyuser, feetype, feegroup) =>
                    {
                        feemaster.AddedByUser = addeduser;
                        feemaster.ModifyByUser = modifyuser;
                        feemaster.FeeType = feetype;
                        feemaster.FeeGroup = feegroup;
                        return feemaster;
                    },
                    new { Id },
                    splitOn: "Id,Id,Id,Id,Id",
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
