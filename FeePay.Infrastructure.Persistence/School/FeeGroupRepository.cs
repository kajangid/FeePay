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
    public class FeeGroupRepository : IFeeGroupRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public FeeGroupRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }

        #region Execute
        public async Task<int> AddAsync(FeeGroup feeGroup, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                feeGroup.NormalizedName = feeGroup.Name.ToUpper();
                var Parameters = new
                {
                    feeGroup.Name,
                    feeGroup.NormalizedName,
                    feeGroup.Description,
                    feeGroup.IsActive,
                    feeGroup.AddedBy
                };
                return await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_FeeGroup,
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
        public async Task<int> UpdateAsync(FeeGroup feeGroup, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                feeGroup.NormalizedName = feeGroup.Name.ToUpper();
                var Parameters = new
                {
                    feeGroup.Id,
                    feeGroup.Name,
                    feeGroup.NormalizedName,
                    feeGroup.Description,
                    feeGroup.IsActive,
                    feeGroup.ModifyBy
                };
                return await connection.ExecuteAsync(_dBVariables.SP_Update_FeeGroup,
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
                return await connection.ExecuteAsync(_dBVariables.SP_Delete_FeeGroup,
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
        public async Task<FeeGroup> FindByIdAsync(int id, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<FeeGroup>(
                    sql: _dBVariables.SP_Get_FeeGroup,
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
        public async Task<FeeGroup> FindByNameAsync(string name, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<FeeGroup>(
                    sql: _dBVariables.SP_Get_FeeGroup,
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
        #endregion

        #region Get All
        public async Task<IEnumerable<FeeGroup>> GetAllAsync(string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<FeeGroup>(
                    sql: _dBVariables.SP_GetAll_FeeGroup,
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
        public async Task<IEnumerable<FeeGroup>> GetAll_WithAddEditUserAsync(string dbId, bool? isActive = null)
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

                //return await connection.QueryAsync<FeeGroup, SchoolAdminUser, SchoolAdminUser, FeeGroup>(
                //    sql: _dBVariables.SP_GetAll_FeeGroup_AddEditUser,
                //    map: (feegroup, addeduser, modifyuser) =>
                //     {
                //         feegroup.AddedByUser = addeduser;
                //         feegroup.ModifyByUser = modifyuser;
                //         return feegroup;
                //     },
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
                throw new Exception(String.Format("{0}.WithConnection() an exception", GetType().FullName), ex);
            }
        }
        #endregion


        public async Task<IEnumerable<FeeGroup>> GetAllWithMasterAandTypeAsync(string dbId, int academicSessionId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<FeeGroup, FeeMaster, FeeType, FeeGroup>(
                    sql: _dBVariables.SP_GetAll_FeeGroup_MasterAndType,
                    map: (feegroup, master, type) =>
                    {
                        feegroup.FeeMasterList.Add(master);
                        feegroup.FeeTypeList.Add(type);
                        return feegroup;
                    },
                    param: new { AcademicSessionId = academicSessionId },
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = list.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.FeeMasterList = g.Select(p => p.FeeMasterList?.Single()).ToList();
                    groupedPost.FeeTypeList = g.Select(p => p.FeeTypeList?.Single()).ToList();
                    return groupedPost;
                });

                return result;
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





        // private methods
        private string GetConStr(string dbId = null)
        {
            return string.IsNullOrEmpty(dbId) ?
                _connectionStringBuilder.GetSchoolConnectionString() : // Demo DB
                _connectionStringBuilder.GetDynamicSchoolConnectionString(dbId);
        }
    }
}
