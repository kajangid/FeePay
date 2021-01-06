﻿using FeePay.Core.Domain.Entities.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface IRegisteredSchoolRepository
    {
        Task<int> AddRegisteredSchoolAsync(RegisteredSchool school);
        Task<int> UpdateRegisteredSchoolAsync(RegisteredSchool school);
        Task<int> DeleteRegisteredSchoolAsync(int Id);
        Task<RegisteredSchool> GetByNameAsync(string normalizedName);
        Task<RegisteredSchool> GetByIdAsync(int schoolId);
        Task<RegisteredSchool> GetByUniqueIdAsync(string schoolUniqueID);
        Task<RegisteredSchool> GetActiveByNameAsync(string normalizedName);
        Task<RegisteredSchool> GetActiveByIdAsync(int schoolId);
        Task<RegisteredSchool> GetActiveByUniqueIdAsync(string schoolUniqueID);
        Task<IList<RegisteredSchool>> GetAllActiveSchoolAsync();

    }
}
