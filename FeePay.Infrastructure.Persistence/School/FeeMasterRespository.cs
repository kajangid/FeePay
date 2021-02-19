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


        #region Execute
        public async Task<int> AddAsync(FeeMaster feeMaster, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    feeMaster.FeeGroupId,
                    feeMaster.FeeTypeId,
                    feeMaster.AcademicSessionId,
                    feeMaster.Amount,
                    feeMaster.DueDate,
                    feeMaster.Description,
                    IsActive = true,
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<int> UpdateAsync(FeeMaster feeMaster, string dbId)
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
                    feeMaster.AcademicSessionId,
                    feeMaster.DueDate,
                    feeMaster.Description,
                    IsActive = true,
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<int> DeleteAsync(int id, int? changeBy, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Delete_FeeMaster,
                    new { Id = id, ModifyBy = changeBy },
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
        #endregion

        #region Find
        public async Task<FeeMaster> FindByIdAsync(int id, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeMaster, FeeType, FeeGroup, FeeMaster>(
                    sql: _dBVariables.SP_Get_FeeMaster,
                    map: (feemaster, feetype, feegroup) =>
                    {
                        feemaster.FeeType = feetype;
                        feemaster.FeeGroup = feegroup;
                        return feemaster;
                    },
                    param: new { Id = id, IsActive = isActive },
                    splitOn: "Id,Id,Id",
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<FeeMaster>> FindByFeeGroupIdAsync(int id, string dbId, int academicSessionId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeMaster, FeeType, FeeGroup, FeeMaster>(
                    sql: _dBVariables.SP_Get_FeeMaster,
                    map: (feemaster, feetype, feegroup) =>
                    {
                        feemaster.FeeType = feetype;
                        feemaster.FeeGroup = feegroup;
                        return feemaster;
                    },
                    param: new { FeeGroupId = id, AcademicSessionId = academicSessionId, IsActive = isActive },
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
        #endregion

        #region Get All
        public async Task<IEnumerable<FeeMaster>> GetAllAsync(string dbId, int academicSessionId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<FeeMaster, FeeType, FeeGroup, FeeMaster>(
                    sql: _dBVariables.SP_GetAll_FeeMaster,
                    map: (feemaster, feetype, feegroup) =>
                    {
                        feemaster.FeeType = feetype;
                        feemaster.FeeGroup = feegroup;
                        return feemaster;
                    },
                    param: new { AcademicSessionId = academicSessionId, IsActive = isActive },
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<FeeMaster>> GetAll_WithAddEditUserAsync(string dbId, int academicSessionId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var users = await connection.QueryAsync<SchoolAdminUser>(
                    sql: _dBVariables.SP_Get_SchoolAdmin_User,
                    param: new { IsActive = true },
                    commandType: CommandType.StoredProcedure);
                var list = await GetAllAsync(dbId, academicSessionId, isActive);
                if (list != null && users != null && users.Any())
                {
                    var studentAcademicSessionList = list.ToList();
                    studentAcademicSessionList.ForEach(f =>
                    {
                        if (f.AddedBy != 0)
                            f.AddedByUser = users.Where(w => w.Id == f.AddedBy).SingleOrDefault();
                        if (f.ModifyBy != 0)
                            f.ModifyByUser = users.Where(w => w.Id == f.ModifyBy).SingleOrDefault();
                    });
                    return studentAcademicSessionList;
                }
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
        #endregion

        // private methods
        private string GetConStr(string dbId = null)
        {
            return string.IsNullOrEmpty(dbId) ?
                _connectionStringBuilder.GetSchoolConnectionString() : // Demo DB
                _connectionStringBuilder.GetDynamicSchoolConnectionString(dbId);
        }
    }
}
