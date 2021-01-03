using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.SuperAdmin
{
    public interface ISuperAdminRoleRepository
    {
        Task<int> AddRoleAsync(SuperAdminRole Role, string dbId = null);
        Task<int> UpdateRoleAsync(SuperAdminRole Role, string dbId = null);
        Task<int> DeleteRoleAsync(int Id, string dbId = null);
        Task<SuperAdminRole> FindRoleByRoleIdAsync(int roleId, string dbId = null);
        Task<SuperAdminRole> FindRoleByRoleNameAsync(string normalizedName, string dbId = null);
        Task<SuperAdminRole> FindActiveRoleByRoleIdAsync(int roleId, string dbId = null);
        Task<SuperAdminRole> FindActiveRoleByRoleNameAsync(string normalizedName, string dbId = null);
    }
}
