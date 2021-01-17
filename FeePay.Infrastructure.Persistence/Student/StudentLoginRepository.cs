using Dapper;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository.Student;
using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Infrastructure.Persistence.Student
{
    public class StudentLoginRepository : IStudentLoginRepository
    {
        public StudentLoginRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _ConnectionStringBuilder = connectionStringBuilder;
            _DBVariables = dBVariables;
        }
        private readonly IDBVariables _DBVariables;
        private readonly IConnectionStringBuilder _ConnectionStringBuilder;
        private string getConStr(string dbId = null)
        {
            return string.IsNullOrEmpty(dbId) ?
                _ConnectionStringBuilder.GetSchoolConnectionString() : // Demo DB
                _ConnectionStringBuilder.GetDynamicSchoolConnectionString(dbId); 
        }

        public async Task<int> AddUserAsync(StudentLogin user, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var SpRequiredParameters = new
                    {
                        AccessFailedCount = user.AccessFailedCount,
                        AddedBy = user.AddedBy,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        IsActive = user.IsActive,
                        IsLogin = user.IsLogin,
                        LastLoginDate = user.LastLoginDate,
                        LastLoginDevice = user.LastLoginDevice,
                        LastLoginIP = user.LastLoginIP,
                        LastLoginLocation = user.LastLoginLocation,
                        LockoutEnabled = user.LockoutEnabled,
                        LockoutEndDate = user.LockoutEndDate,
                        NormalizedEmail = user.NormalizedEmail,
                        NormalizedUserName = user.NormalizedUserName,
                        Password = user.Password,
                        PasswordHash = user.PasswordHash,
                        PhoneNumber = user.PhoneNumber,
                        PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                        SecurityStamp = user.SecurityStamp,
                        TwoFactorEnabled = user.TwoFactorEnabled,
                        UserName = user.UserName
                    };
                    return await connection.ExecuteAsync(_DBVariables.SP_Add_StudentLogin, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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
        public async Task<int> UpdateUserAsync(StudentLogin user, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var SpRequiredParameters = new
                    {
                        AccessFailedCount = user.AccessFailedCount,
                        AddedBy = user.AddedBy,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        IsActive = user.IsActive,
                        IsLogin = user.IsLogin,
                        LastLoginDate = user.LastLoginDate,
                        LastLoginDevice = user.LastLoginDevice,
                        LastLoginIP = user.LastLoginIP,
                        LastLoginLocation = user.LastLoginLocation,
                        LockoutEnabled = user.LockoutEnabled,
                        LockoutEndDate = user.LockoutEndDate,
                        NormalizedEmail = user.NormalizedEmail,
                        NormalizedUserName = user.NormalizedUserName,
                        Password = user.Password,
                        PasswordHash = user.PasswordHash,
                        PhoneNumber = user.PhoneNumber,
                        PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                        SecurityStamp = user.SecurityStamp,
                        TwoFactorEnabled = user.TwoFactorEnabled,
                        UserName = user.UserName
                    };
                    return await connection.ExecuteAsync(_DBVariables.SP_Update_StudentLogin, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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
        public async Task<int> DeleteUserAsync(int Id, string dbId = null)
        {
            try
            {
                await using (var connection = new SqlConnection(getConStr(dbId)))
                {
                    return await connection.ExecuteAsync(_DBVariables.SP_Delete_StudentLogin, new { Id = Id }, null, null, CommandType.StoredProcedure);
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
        public async Task<StudentLogin> FindUserByUserIdAsync(int userId, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var SpRequiredParameters = new { Id = userId };
                    return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_DBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<StudentLogin> FindUserByUserNameAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var SpRequiredParameters = new { NormalizedUserName = normalizedUserName };
                    return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_DBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<StudentLogin> FindUserByUserEmailAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var SpRequiredParameters = new { NormalizedEmail = normalizedEmail };
                    return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_DBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<StudentLogin> FindActiveUserByUserIdAsync(int userId, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var SpRequiredParameters = new { Id = userId, IsActive = true };
                    return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_DBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<StudentLogin> FindActiveUserByUserNameAsync(string normalizedUserName, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var SpRequiredParameters = new { NormalizedUserName = normalizedUserName, IsActive = true };
                    return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_DBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<StudentLogin> FindActiveUserByUserEmailAsync(string normalizedEmail, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var SpRequiredParameters = new { NormalizedEmail = normalizedEmail, IsActive = true };
                    return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_DBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
        public async Task<IList<StudentLogin>> FindAllActiveUserAsync(string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var SpRequiredParameters = new { IsActive = true };
                    return (await connection.QueryAsync<StudentLogin>(_DBVariables.SP_GetAll_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure)).ToList();
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
        public async Task UpdateLoginState(int userId, string Ip, string dbId = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(getConStr(dbId)))
                {
                    var SpRequiredParameters = new { Id = userId, LastLoginIP = Ip };
                    await connection.ExecuteScalarAsync<int>(_DBVariables.SP_Add_StudentLogin_LoginInfo, SpRequiredParameters, null, null, CommandType.StoredProcedure);
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
