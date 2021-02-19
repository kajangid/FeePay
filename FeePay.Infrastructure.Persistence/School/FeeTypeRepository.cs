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

        #region Execute
        public async Task<int> AddAsync(FeeType feeType, string dbId)
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<int> UpdateAsync(FeeType feeType, string dbId)
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<int> DeleteAsync(int id, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Delete_FeeType,
                    new { Id = id },
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
        public async Task<FeeType> FindByIdAsync(int id, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<FeeType>(
                    sql: _dBVariables.SP_Get_FeeType,
                    param: new { Id = id, IsActive = isActive },
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
        public async Task<FeeType> FindByNameAsync(string name, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<FeeType>(
                    sql: _dBVariables.SP_Get_FeeType,
                    param: new { NormalizeName = name.ToUpper(), IsActive = isActive },
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
        public async Task<FeeType> FindByCodeAsync(string code, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<FeeType>(
                    sql: _dBVariables.SP_Get_FeeType,
                    param: new { Code = code, IsActive = isActive },
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

        #region Get All
        public async Task<IEnumerable<FeeType>> GetAllAsync(string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<FeeType>(
                    sql: _dBVariables.SP_GetAll_FeeType,
                    param: new { IsActive = isActive },
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
        public async Task<IEnumerable<FeeType>> GetAll_WithAddEditUserAsync(string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var users = await connection.QueryAsync<SchoolAdminUser>(
                    sql: _dBVariables.SP_Get_SchoolAdmin_User,
                    param: new { IsActive = true },
                    commandType: CommandType.StoredProcedure);

                var list = await GetAllAsync(dbId, isActive);
                if (list != null && users != null && users.Any())
                {
                    var _classes = list.ToList();
                    _classes.ForEach(f =>
                    {
                        if (f.AddedBy != 0)
                            f.AddedByUser = users.Where(w => w.Id == f.AddedBy).SingleOrDefault();
                        if (f.ModifyBy != 0)
                            f.ModifyByUser = users.Where(w => w.Id == f.ModifyBy).SingleOrDefault();
                    });
                    return _classes;
                }
                return list;
                //return await connection.QueryAsync<FeeType, SchoolAdminUser, SchoolAdminUser, FeeType>(
                //    sql: _dBVariables.SP_GetAll_FeeType_AddEditUser,
                //    map: (feetype, addeduser, modifyuser) =>
                //    {
                //        feetype.AddedByUser = addeduser;
                //        feetype.ModifyByUser = modifyuser;
                //        return feetype;
                //    },
                //    param: new { IsActive = isActive },
                //    splitOn: "Id,Id,Id",
                //    commandType: CommandType.StoredProcedure);
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
