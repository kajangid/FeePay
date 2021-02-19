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
using FeePay.Core.Application.Interface.Repository.Student;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Domain.Entities.School;
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Infrastructure.Persistence.Student
{
    public class StudentAdmisionRepository : IStudentAdmisionRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public StudentAdmisionRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }


        /// <summary>
        /// this repository method will  not execute form this instance 
        /// send StudentAdmission class instance with StudentLogin Data to StudentRegisterService viva 
        /// Student Identity UserManager
        /// </summary>
        /// <param name="studentAdmission"> StudentAdmission data </param>
        /// <param name="dbId"> connection string key </param>
        /// <returns></returns>
        public Task<int> AddAsync(StudentAdmission studentAdmission, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    studentAdmission.Address,
                    studentAdmission.AdmissionDate,
                    studentAdmission.AcademicSessionId,
                    studentAdmission.AlternateMobileNo,
                    studentAdmission.Category,
                    studentAdmission.Village,
                    studentAdmission.ClassId,
                    studentAdmission.DateOfBirth,
                    studentAdmission.CityId,
                    studentAdmission.EnrollNo,
                    studentAdmission.FatherName,
                    studentAdmission.FirstName,
                    studentAdmission.LastName,
                    studentAdmission.FormNo,
                    studentAdmission.Gender,
                    studentAdmission.GuardianEmail,
                    studentAdmission.GuardianMobileNo,
                    studentAdmission.Image,
                    studentAdmission.MACHINEID,
                    studentAdmission.Medium,
                    studentAdmission.MobileNo,
                    studentAdmission.MotherName,
                    studentAdmission.PreviousClass,
                    studentAdmission.PreviousInstituteName,
                    studentAdmission.PreviousPercent,
                    studentAdmission.PreviousRollNo,
                    studentAdmission.Religion,
                    studentAdmission.Remarks,
                    studentAdmission.SectionId,
                    studentAdmission.Sr_RegNo,
                    studentAdmission.StateId,
                    studentAdmission.StudentEmail,
                    studentAdmission.StudentLoginId,
                    studentAdmission.StudentType,
                    studentAdmission.YearOfPassing,
                    studentAdmission.IsActive,
                    studentAdmission.AddedBy
                };
                return Task.FromResult(0);
                //return await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_StudentAdmission,
                //    Parameters, commandType: CommandType.StoredProcedure);
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
        public async Task<int> UpdateAsync(StudentAdmission studentAdmission, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    studentAdmission.Id,
                    studentAdmission.Address,
                    //studentAdmission.AdmissionDate,
                    studentAdmission.AcademicSessionId,
                    studentAdmission.AlternateMobileNo,
                    studentAdmission.Category,
                    studentAdmission.Village,
                    studentAdmission.ClassId,
                    studentAdmission.DateOfBirth,
                    studentAdmission.CityId,
                    studentAdmission.EnrollNo,
                    studentAdmission.FatherName,
                    studentAdmission.FirstName,
                    studentAdmission.LastName,
                    studentAdmission.FormNo,
                    studentAdmission.Gender,
                    studentAdmission.GuardianEmail,
                    studentAdmission.GuardianMobileNo,
                    studentAdmission.Image,
                    studentAdmission.MACHINEID,
                    studentAdmission.Medium,
                    studentAdmission.MobileNo,
                    studentAdmission.MotherName,
                    studentAdmission.PreviousClass,
                    studentAdmission.PreviousInstituteName,
                    studentAdmission.PreviousPercent,
                    studentAdmission.PreviousRollNo,
                    studentAdmission.Religion,
                    studentAdmission.Remarks,
                    studentAdmission.SectionId,
                    studentAdmission.Sr_RegNo,
                    studentAdmission.StateId,
                    studentAdmission.StudentEmail,
                    studentAdmission.StudentLoginId,
                    studentAdmission.StudentType,
                    studentAdmission.YearOfPassing,
                    studentAdmission.IsActive,
                    studentAdmission.ModifyBy
                };
                return await connection.ExecuteAsync(_dBVariables.SP_Update_StudentAdmission,
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
        public async Task<int> BulkUpdateAsync(List<StudentAdmission> studentAdmission, string dbId)
        {
            IDbConnection connection = new SqlConnection(GetConStr(dbId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var parameters = studentAdmission.Select(s =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@Id", s.Id, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@AcademicSessionId", s.AcademicSessionId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@ClassId", s.ClassId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@SectionId", s.SectionId, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@IsActive", s.IsActive, DbType.Boolean, ParameterDirection.Input);
                    tempParams.Add("@ModifyBy", s.ModifyBy, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@EnrollNo", s.EnrollNo, DbType.String, ParameterDirection.Input);
                    return tempParams;
                });

                var effectedRows = await connection.ExecuteAsync(
                    sql: _dBVariables.QUERY_BulkUpdate_StudentAdmission,
                    param: parameters,
                    transaction: transaction,
                    commandType: CommandType.Text);

                if (effectedRows > 0 && (effectedRows / 2) == studentAdmission.Count)
                {
                    transaction?.Commit();
                    connection?.Close();
                    return effectedRows;
                }
                else
                {
                    transaction?.Rollback();
                    connection?.Close();
                    return 0;
                }
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
        public async Task<int> DeleteAsync(int Id, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Delete_StudentAdmission,
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


        // find
        public async Task<StudentAdmission> FindByIdAsync(int Id, string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<StudentAdmission>(_dBVariables.SP_Get_StudentAdmission,
                    new { Id, IsActive = active },
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
        public async Task<StudentAdmission> FindByStudentLoginIdAsync(int Id, string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<StudentAdmission>(_dBVariables.SP_Get_StudentAdmission,
                    new { StudentLoginId = Id, IsActive = active },
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
        public async Task<StudentAdmission> FindByFormNoAsync(string formno, string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<StudentAdmission>(_dBVariables.SP_Get_StudentAdmission,
                    new { FormNo = formno, IsActive = active },
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
        public async Task<StudentAdmission> FindBySr_RegNoAsync(string sr_regno, string dbId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QuerySingleOrDefaultAsync<StudentAdmission>(_dBVariables.SP_Get_StudentAdmission,
                    new { Sr_RegNo = sr_regno, IsActive = active },
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


        // get all
        public async Task<IEnumerable<StudentAdmission>> GetAllAsync(string dbId, int academicSessionId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.QueryAsync<StudentAdmission>(
                    sql: _dBVariables.SP_Get_StudentAdmission,
                    param: new { IsActive = active, AcademicSessionId = academicSessionId },
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
        public async Task<IEnumerable<StudentAdmission>> GetAll_WithAddEditUserAsync(string dbId, int academicSessionId, bool? active = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var students = await connection.QueryAsync<StudentAdmission>(
                    sql: _dBVariables.SP_Get_StudentAdmission,
                    param: new { IsActive = active, AcademicSessionId = academicSessionId },
                    commandType: CommandType.StoredProcedure);
                if (students != null && students.Any())
                {
                    var users = await connection.QueryAsync<SchoolAdminUser>(
                        sql: _dBVariables.SP_Get_SchoolAdmin_User,
                        param: new { IsActive = true },
                        commandType: CommandType.StoredProcedure);
                    var classes = await connection.QueryAsync<Classes>(
                        sql: _dBVariables.SP_Get_Class,
                        param: new { IsActive = true },
                        commandType: CommandType.StoredProcedure);
                    var sections = await connection.QueryAsync<Section>(
                        sql: _dBVariables.SP_Get_Section,
                        param: new { IsActive = true },
                        commandType: CommandType.StoredProcedure);
                    var studentList = students.ToList();
                    studentList.ForEach(f =>
                    {
                        if (users != null && users.Any())
                            f.AddedByUser = users.Where(w => w.Id == f.AddedBy).SingleOrDefault();
                        if (users != null && users.Any())
                            f.ModifyByUser = users.Where(w => w.Id == f.ModifyBy).SingleOrDefault();
                        if (classes != null && classes.Any())
                            f.StudentClass = classes.Where(w => w.Id == f.ClassId).SingleOrDefault();
                        if (sections != null && sections.Any())
                            f.StudentSection = sections.Where(w => w.Id == f.SectionId).SingleOrDefault();
                    });
                    return studentList;
                }
                return students;
                //return await connection.QueryAsync<StudentAdmission, Classes, Section, SchoolAdminUser, SchoolAdminUser, StudentAdmission>(
                //    sql: _dBVariables.SP_Get_StudentAdmission_AddEditUser,
                //    map: (studentadmission, _class, section, addedby, modifyby) =>
                //    {
                //        studentadmission.AddedByUser = addedby;
                //        studentadmission.ModifyByUser = modifyby;
                //        studentadmission.StudentClass = _class;
                //        studentadmission.StudentSection = section;
                //        return studentadmission;
                //    },
                //    param: new { IsActive = active },
                //    splitOn: "Id,Id,Id,Id,Id",
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }
        }

        // Search
        public async Task<IEnumerable<StudentAdmission>> SearchStudentAsync(string dbId, int academicSessionId,
            int? classId = null, int? sectionId = null,
            string seatchString = null, string gender = null, int? studentAdmissionId = null,
            bool isActive = true)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                StringBuilder Query = new StringBuilder();
                Query.Append(_dBVariables.QUERY_StudentSearch_Select);
                Query.Append(_dBVariables.QUERY_StudentSearch_Where_IsActive);
                Query.Append(_dBVariables.QUERY_StudentSearch_Where_AcademicSessionId);
                var parameters = new DynamicParameters();
                parameters.Add("@IsActive", isActive, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@AcademicSessionId", academicSessionId, DbType.Int32, ParameterDirection.Input);
                if (string.IsNullOrEmpty(seatchString))
                {
                    if (classId is not null and not 0)
                    {
                        Query.Append(_dBVariables.QUERY_StudentSearch_Where_ClassId);
                        parameters.Add("@ClassId", classId, DbType.Int32, ParameterDirection.Input);
                    }
                    if (sectionId is not null and not 0)
                    {
                        Query.Append(_dBVariables.QUERY_StudentSearch_Where_SectionId);
                        parameters.Add("@SectionId", sectionId, DbType.Int32, ParameterDirection.Input);
                    }
                    if (!string.IsNullOrEmpty(gender))
                    {
                        Query.Append(_dBVariables.QUERY_StudentSearch_Where_Gender);
                        parameters.Add("@Gender", gender, DbType.String, ParameterDirection.Input, gender.Length);
                    }
                    if (studentAdmissionId is not null and not 0)
                    {
                        Query.Append(_dBVariables.QUERY_StudentSearch_Where_StudentId);
                        parameters.Add("@StudentId", studentAdmissionId, DbType.Int32, ParameterDirection.Input);
                    }
                }
                else
                {
                    Query.Append(_dBVariables.QUERY_StudentSearch_Where_SearchIn);
                    parameters.Add("@SearchParam", seatchString, DbType.String, ParameterDirection.Input, seatchString.Length);
                }
                return await connection.QueryAsync<StudentAdmission, Classes, Section, StudentAdmission>
                    (Query.ToString(), (studentadmission, _class, section) =>
                    {
                        studentadmission.StudentClass = _class;
                        studentadmission.StudentSection = section;
                        return studentadmission;
                    },
                    parameters,
                    splitOn: "Id,Id,Id",
                    commandType: CommandType.Text);
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
                throw new Exception(String.Format("{0}", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<StudentAdmission>> SearchStudent_WithAddEditUserAsync(string dbId, int academicSessionId,
            int? classId = null, int? sectionId = null,
            string seatchString = null, string gender = null, int? studentAdmissionId = null,
            bool isActive = true)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                StringBuilder Query = new StringBuilder();
                Query.Append(_dBVariables.QUERY_StudentSearch_Select_AddEditUser);
                Query.Append(_dBVariables.QUERY_StudentSearch_Where_IsActive);
                Query.Append(_dBVariables.QUERY_StudentSearch_Where_AcademicSessionId);
                var parameters = new DynamicParameters();
                parameters.Add("@IsActive", isActive, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@AcademicSessionId", academicSessionId, DbType.Int32, ParameterDirection.Input);
                if (string.IsNullOrEmpty(seatchString))
                {
                    if (classId is not null and not 0)
                    {
                        Query.Append(_dBVariables.QUERY_StudentSearch_Where_ClassId);
                        parameters.Add("@ClassId", classId, DbType.Int32, ParameterDirection.Input);
                    }
                    if (sectionId is not null and not 0)
                    {
                        Query.Append(_dBVariables.QUERY_StudentSearch_Where_SectionId);
                        parameters.Add("@SectionId", sectionId, DbType.Int32, ParameterDirection.Input);
                    }
                    if (!string.IsNullOrEmpty(gender))
                    {
                        Query.Append(_dBVariables.QUERY_StudentSearch_Where_Gender);
                        parameters.Add("@Gender", gender, DbType.String, ParameterDirection.Input, gender.Length);
                    }
                    if (studentAdmissionId is not null and not 0)
                    {
                        Query.Append(_dBVariables.QUERY_StudentSearch_Where_StudentId);
                        parameters.Add("@StudentId", studentAdmissionId, DbType.Int32, ParameterDirection.Input);
                    }
                }
                else
                {
                    Query.Append(_dBVariables.QUERY_StudentSearch_Where_SearchIn);
                    parameters.Add("@SearchParam", seatchString, DbType.String, ParameterDirection.Input, seatchString.Length);
                }
                return await connection.QueryAsync<StudentAdmission, Classes, Section, SchoolAdminUser, SchoolAdminUser, StudentAdmission>
                    (Query.ToString(), (studentadmission, _class, section, addedby, modifyby) =>
                    {
                        studentadmission.AddedByUser = addedby;
                        studentadmission.ModifyByUser = modifyby;
                        studentadmission.StudentClass = _class;
                        studentadmission.StudentSection = section;
                        return studentadmission;
                    },
                    parameters,
                    splitOn: "Id,Id,Id,Id,Id",
                    commandType: CommandType.Text);
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
                throw new Exception(String.Format("{0}", GetType().FullName), ex);
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
