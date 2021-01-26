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
            _connectionStringBuilder = connectionStringBuilder;
            _dBVariables = dBVariables;
        }
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        private string GetConStr(string dbId = null)
        {
            return string.IsNullOrEmpty(dbId) ?
                _connectionStringBuilder.GetSchoolConnectionString() : // Demo DB
                _connectionStringBuilder.GetDynamicSchoolConnectionString(dbId);
        }

        public async Task<int> AddUserAsync(StudentLogin user, string dbId = null)
        {
            try
            {
                IDbConnection connection = new SqlConnection(GetConStr(dbId));
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    var SpRequiredParameters = new
                    {
                        user.AccessFailedCount,
                        user.AddedBy,
                        user.Email,
                        user.EmailConfirmed,
                        user.IsActive,
                        user.IsLogin,
                        user.LastLoginDate,
                        user.LastLoginDevice,
                        user.LastLoginIP,
                        user.LastLoginLocation,
                        user.LockoutEnabled,
                        user.LockoutEndDate,
                        user.NormalizedEmail,
                        user.NormalizedUserName,
                        user.Password,
                        user.PasswordHash,
                        user.PhoneNumber,
                        user.PhoneNumberConfirmed,
                        user.SecurityStamp,
                        user.TwoFactorEnabled,
                        user.UserName
                    };
                    var loginId = await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_StudentLogin,
                        SpRequiredParameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                    if (loginId > 0)
                    {
                        var Parameters = new
                        {
                            user.StudentAdmission.Address,
                            user.StudentAdmission.AdmissionDate,
                            user.StudentAdmission.AlternateMobileNo,
                            user.StudentAdmission.Category,
                            user.StudentAdmission.Village,
                            user.StudentAdmission.ClassId,
                            user.StudentAdmission.DateOfBirth,
                            user.StudentAdmission.CityId,
                            user.StudentAdmission.EnrollNo,
                            user.StudentAdmission.FatherName,
                            user.StudentAdmission.FirstName,
                            user.StudentAdmission.LastName,
                            user.StudentAdmission.FormNo,
                            user.StudentAdmission.Gender,
                            user.StudentAdmission.GuardianEmail,
                            user.StudentAdmission.GuardianMobileNo,
                            user.StudentAdmission.Image,
                            user.StudentAdmission.MACHINEID,
                            user.StudentAdmission.Medium,
                            user.StudentAdmission.MobileNo,
                            user.StudentAdmission.MotherName,
                            user.StudentAdmission.PreviousClass,
                            user.StudentAdmission.PreviousInstituteName,
                            user.StudentAdmission.PreviousPercent,
                            user.StudentAdmission.PreviousRollNo,
                            user.StudentAdmission.Religion,
                            user.StudentAdmission.Remarks,
                            user.StudentAdmission.SectionId,
                            user.StudentAdmission.Sr_RegNo,
                            user.StudentAdmission.StateId,
                            user.StudentAdmission.StudentEmail,
                            StudentLoginId = loginId,
                            user.StudentAdmission.StudentType,
                            user.StudentAdmission.YearOfPassing,
                            user.StudentAdmission.IsActive,
                            user.StudentAdmission.AddedBy
                        };

                        var studentAdmissionId = await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_StudentAdmission,
                            Parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                        if (studentAdmissionId > 0)
                        {
                            transaction?.Commit();
                            connection?.Close();
                            return 1;
                        }
                    }
                    transaction?.Rollback();
                    connection?.Close();
                    return 0;
                }
                catch (TimeoutException ex)
                {
                    transaction?.Rollback();
                    throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();
                    throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    throw new Exception(String.Format("{0}", GetType().FullName), ex);
                }
                finally
                {
                    connection?.Close();
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
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new
                {
                    user.AccessFailedCount,
                    user.AddedBy,
                    user.Email,
                    user.EmailConfirmed,
                    user.IsActive,
                    user.IsLogin,
                    user.LastLoginDate,
                    user.LastLoginDevice,
                    user.LastLoginIP,
                    user.LastLoginLocation,
                    user.LockoutEnabled,
                    user.LockoutEndDate,
                    user.NormalizedEmail,
                    user.NormalizedUserName,
                    user.Password,
                    user.PasswordHash,
                    user.PhoneNumber,
                    user.PhoneNumberConfirmed,
                    user.SecurityStamp,
                    user.TwoFactorEnabled,
                    user.UserName
                };
                return await connection.ExecuteAsync(_dBVariables.SP_Update_StudentLogin, SpRequiredParameters, commandType: CommandType.StoredProcedure);

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
                using var connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Delete_StudentLogin, new { Id }, null, null, CommandType.StoredProcedure);

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
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = userId };
                return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_dBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);

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
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedUserName = normalizedUserName };
                return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_dBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);

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
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedEmail = normalizedEmail };
                return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_dBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);

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
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = userId, IsActive = true };
                return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_dBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);

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
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedUserName = normalizedUserName, IsActive = true };
                return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_dBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);

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
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { NormalizedEmail = normalizedEmail, IsActive = true };
                return await connection.QuerySingleOrDefaultAsync<StudentLogin>(_dBVariables.SP_Get_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure);

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
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { IsActive = true };
                return (await connection.QueryAsync<StudentLogin>(_dBVariables.SP_GetAll_StudentLogin, SpRequiredParameters, null, null, CommandType.StoredProcedure)).ToList();

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
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = userId, LastLoginIP = Ip };
                await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_StudentLogin_LoginInfo, SpRequiredParameters, null, null, CommandType.StoredProcedure);

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
