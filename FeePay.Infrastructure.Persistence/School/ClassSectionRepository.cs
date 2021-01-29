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
using FeePay.Core.Application.UseCase;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Infrastructure.Persistence.School
{
    public class ClassSectionRepository : IClassSectionRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public ClassSectionRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }


        public async Task<int> AddAsync(ClassSection classSection, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var Parameters = new
                {
                    classSection.ClassId,
                    classSection.SectionId
                };
                return await connection.ExecuteScalarAsync<int>(_dBVariables.SP_Add_ClassSection,
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
        public async Task<int> DeleteAsync(int Id, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Remove_ClassSection,
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
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<int> DeleteAsync(int ClassId, int SectionId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                return await connection.ExecuteAsync(_dBVariables.SP_Remove_ClassSection,
                    new { ClassId, SectionId },
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


        public async Task<bool> IsSectionInClassAsync(int ClassId, int SectionId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<Classes, Section, Classes>(
                    _dBVariables.SP_Get_ClassSection,
                    (_class, section) => { _class.Sections.Add(section); return _class; },
                    new { ClassId, SectionId },
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = list.GroupBy(g => g.Id).
                    Select(s =>
                    {
                        var groupedPost = s.First();
                        groupedPost.Sections = s.Select(g => g.Sections.Single()).ToList();
                        groupedPost.Sections.RemoveAll(item => item == null);
                        return groupedPost;
                    });

                return (result.Any());
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


        public async Task<Classes> FindSectionsInClassByClassIdAsync(int ClassId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<Classes, Section, Classes>(
                    _dBVariables.SP_Get_ClassSection,
                    (_class, section) => { _class.Sections.Add(section); return _class; },
                    new { ClassId },
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = list.GroupBy(g => g.Id).
                    Select(s =>
                    {
                        var groupedPost = s.First();
                        groupedPost.Sections = s.Select(g => g.Sections.Single()).ToList();
                        groupedPost.Sections.RemoveAll(item => item == null);
                        return groupedPost;
                    });

                return result?.FirstOrDefault();
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
        public async Task<Section> FindClassesInSectionBySectionIdAsync(int SectionId, string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<Section, Classes, Section>(
                    _dBVariables.SP_Get_ClassSection,
                    (section, _class) => { section.Classes.Add(_class); return section; },
                    new { SectionId },
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = list.GroupBy(g => g.Id).
                    Select(s =>
                    {
                        var groupedPost = s.First();
                        groupedPost.Classes = s.Select(g => g.Classes.Single()).ToList();
                        return groupedPost;
                    });

                return result?.FirstOrDefault();
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
        public async Task<IEnumerable<Classes>> GetAll_Class_SectionAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var list = await connection.QueryAsync<Classes, Section, Classes>(
                    _dBVariables.SP_Get_ClassSection,
                    (_class, section) => { _class.Sections.Add(section); return _class; },
                    splitOn: "Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = list.GroupBy(g => g.Id).
                    Select(s =>
                    {
                        var groupedPost = s.First();
                        groupedPost.Sections = s.Select(g => g.Sections.Single()).ToList();
                        groupedPost.Sections.RemoveAll(item => item == null);
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


        public async Task<IEnumerable<Classes>> GetAll_Class_Section_WithAddEditUserAsync(string dbId = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var courseDictionary = new Dictionary<int, Section>();
                var list = await connection.QueryAsync<Classes, Section, SchoolAdminUser, SchoolAdminUser, Classes>(
                    _dBVariables.SP_GetAll_ClassesSections_AddEditUser,
                    (_class, section, addedby, modifyby) =>
                    {
                        _class.Sections.Add(section);
                        _class.AddedByUser = addedby;
                        _class.ModifyByUser = modifyby;
                        return _class;
                    },
                    splitOn: "Id,Id,Id,Id",
                    commandType: CommandType.StoredProcedure);

                var result = list.GroupBy(g => g.Id, new CustomEqualityComparer<int>()).
                    Select(s =>
                    {
                        var groupedPost = s.First();
                        groupedPost.Sections = s.Select(g => g.Sections.Single()).ToList();
                        groupedPost.Sections.RemoveAll(item => item == null);
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
