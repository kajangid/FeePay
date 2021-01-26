﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Core.Application.Interface.Repository.Student
{
    public interface IStudentAdmisionRepository
    {
        Task<int> AddAsync(StudentAdmission studentAdmission, string dbId = null);
        Task<int> UpdateAsync(StudentAdmission studentAdmission, string dbId = null);
        Task<int> DeleteAsync(int Id, string dbId = null);


        // find
        Task<StudentAdmission> FindByIdAsync(int Id, string dbId = null);
        Task<StudentAdmission> FindByStudentLoginIdAsync(int Id, string dbId = null);
        Task<StudentAdmission> FindByFormNoAsync(string formno, string dbId = null);
        Task<StudentAdmission> FindBySr_RegNoAsync(string sr_regno, string dbId = null);
        Task<StudentAdmission> FindActiveByIdAsync(int Id, string dbId = null);
        Task<StudentAdmission> FindActiveByStudentLoginIdAsync(int Id, string dbId = null);
        Task<StudentAdmission> FindActiveByFormNoAsync(string formno, string dbId = null);
        Task<StudentAdmission> FindActiveBySr_RegNoAsync(string sr_regno, string dbId = null);


        // get all
        Task<IEnumerable<StudentAdmission>> GetAllAsync(string dbId = null);
        Task<IEnumerable<StudentAdmission>> GetAllActiveAsync(string dbId = null);
        Task<IEnumerable<StudentAdmission>> SearchStudentAsync(string dbId, string seatchString = null, int? classId = null,
            int? sectionId = null, bool isActive = true, string gender = null, int? studentId = null);
        Task<IEnumerable<StudentAdmission>> SearchStudent_WithAddEditUserAsync(string dbId, string seatchString = null, int? classId = null,
            int? sectionId = null, bool isActive = true, string gender = null, int? studentId = null);

        // get all with add edit user info
        Task<IEnumerable<StudentAdmission>> GetAll_WithAddEditUserAsync(string dbId = null);
        Task<IEnumerable<StudentAdmission>> GetAllActive_WithAddEditUserAsync(string dbId = null);
    }
}
