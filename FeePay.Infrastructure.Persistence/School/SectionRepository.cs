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
    public class SectionRepository : ISectionRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public SectionRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }


        #region Execute
        public async Task<int> AddAsync(Section section, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                section.NormalizedName = section.Name.ToUpper();
                var Parameters = new
                {
                    section.Name,
                    section.NormalizedName,
                    section.Description,
                    section.IsActive,
                    section.AddedBy
                };
                return await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_Section,
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
        public async Task<int> UpdateAsync(Section section, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                section.NormalizedName = section.Name.ToUpper();
                var Parameters = new
                {
                    section.Id,
                    section.Name,
                    section.NormalizedName,
                    section.Description,
                    section.IsActive,
                    section.ModifyBy
                };
                return await connection.ExecuteAsync(_dBVariables.SP_Update_Section,
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
        public async Task<int> DeleteAsync(int id, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Delete_Section,
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
        }
        #endregion

        #region Find
        public async Task<Section> FindByIdAsync(int id, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<Section>(_dBVariables.SP_Get_Section,
                    new { Id = id, IsActive = isActive },
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
        public async Task<Section> FindByNameAsync(string name, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<Section>(_dBVariables.SP_Get_Section,
                    new { NormalizedName = name.ToUpper().Trim(), IsActive = isActive },
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
        #endregion


        #region Get All
        public async Task<IEnumerable<Section>> GetAllAsync(string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<Section>(
                    sql: _dBVariables.SP_Get_Section,
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
        }
        public async Task<IEnumerable<Section>> GetAll_WithAddEditUserAsync(string dbId, bool? isActive = null)
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

                //return await connection.QueryAsync<Section, SchoolAdminUser, SchoolAdminUser, Section>(
                //    sql: _dBVariables.SP_Get_Section_AddEditUser,
                //    map: (section, addedby, modifyby) =>
                //     {
                //         section.AddedByUser = addedby;
                //         section.ModifyByUser = modifyby;
                //         return section;
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
