﻿using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.School
{
    public interface ISchoolAdminRoleRepository
    {
        Task<int> AddRoleAsync(SchoolAdminRole Role, string dbId = null);
        Task<int> UpdateRoleAsync(SchoolAdminRole Role, string dbId = null);
        Task<int> DeleteRoleAsync(int Id, string dbId = null);
        Task<SchoolAdminRole> FindRoleByRoleIdAsync(int roleId, string dbId = null);
        Task<SchoolAdminRole> FindRoleByRoleNameAsync(string normalizedName, string dbId = null);
        Task<SchoolAdminRole> FindActiveRoleByRoleIdAsync(int roleId, string dbId = null);
        Task<SchoolAdminRole> FindActiveRoleByRoleNameAsync(string normalizedName, string dbId = null);
    }
}
