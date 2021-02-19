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
    public class ClassRepository : IClassRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public ClassRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }


        #region Execute
        /// <summary>
        /// Add New Class
        /// </summary>
        /// <param name="classes"> Class Object To Update </param>
        /// <param name="dbId"> School Db Key </param>
        /// <returns> Id Of Added Class If 0 Class Is Already Exists. </returns>
        public async Task<int> AddAsync(Classes classes, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                classes.NormalizedName = classes.Name.ToUpper();
                var Parameters = new
                {
                    classes.Name,
                    classes.NormalizedName,
                    classes.Description,
                    classes.IsActive,
                    classes.AddedBy
                };
                return await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_Class,
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

        /// <summary>
        /// Update Class
        /// </summary>
        /// <param name="classes"> Class Object To Update </param>
        /// <param name="dbId"> School Db Key </param>
        /// <returns> Id Of Updated Class If 0 Class Is Not Already. </returns>
        public async Task<int> UpdateAsync(Classes classes, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                classes.NormalizedName = classes.Name.ToUpper();
                var Parameters = new
                {
                    classes.Id,
                    classes.Name,
                    classes.NormalizedName,
                    classes.Description,
                    classes.IsActive,
                    classes.ModifyBy
                };
                return await connection.ExecuteAsync(_dBVariables.SP_Update_Class,
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

        /// <summary>
        /// Delete Class
        /// </summary>
        /// <param name="id"> Id Of The Class </param>
        /// <param name="dbId"> School Db Key </param>
        /// <returns> Number Of Row Effected </returns>
        public async Task<int> DeleteAsync(int id, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Delete_Class,
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
        /// <summary>
        /// Fetch the Class Data
        /// </summary>
        /// <param name="id"> Id Of The Class </param>
        /// <param name="dbId"> School Db Key </param>
        /// <param name="isActive"> Class Active Condition </param>
        /// <returns> Class Object Data </returns>
        public async Task<Classes> FindByIdAsync(int id, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<Classes>(_dBVariables.SP_Get_Class,
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }

        /// <summary>
        /// Fetch the Class Data
        /// </summary>
        /// <param name="name"> Name Of The Class </param>
        /// <param name="dbId"> School Db Key </param>
        /// <param name="isActive"> Class Active Condition </param>
        /// <returns> Class Object Data </returns>
        public async Task<Classes> FindByNameAsync(string name, string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<Classes>(_dBVariables.SP_Get_Class,
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        #endregion


        #region Get All
        /// <summary>
        /// Get All Classes
        /// </summary>
        /// <param name="dbId"> School Db Key </param>
        /// <param name="isActive"> Class Active Condition </param>
        /// <returns> IEnumerable Classes Object </returns>
        public async Task<IEnumerable<Classes>> GetAllAsync(string dbId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<Classes>(
                    sql: _dBVariables.SP_Get_Class,
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

        /// <summary>
        /// Get All Classes with Audit data
        /// </summary>
        /// <param name="dbId"> School Db Key </param>
        /// <param name="isActive"> Class Active Condition </param>
        /// <returns> IEnumerable Classes Object </returns>
        public async Task<IEnumerable<Classes>> GetAll_WithAddEditUserAsync(string dbId, bool? isActive = null)
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

                //return await connection.QueryAsync<Classes, SchoolAdminUser, SchoolAdminUser, Classes>(
                //      sql: _dBVariables.SP_Get_Class_AddEditUser,
                //      map: (_class, addedby, modifyby) =>
                //      {
                //          _class.AddedByUser = addedby;
                //          _class.ModifyByUser = modifyby;
                //          return _class;
                //      },
                //      param: new { IsActive = isActive },
                //      splitOn: "Id,Id,Id",
                //      commandType: CommandType.StoredProcedure);
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
