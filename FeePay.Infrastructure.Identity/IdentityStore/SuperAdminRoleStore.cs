using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeePay.Infrastructure.Identity.IdentityStore
{
    public class SuperAdminRoleStore : IRoleStore<SuperAdminRole>
    {
        public SuperAdminRoleStore(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private readonly IUnitOfWork _UnitOfWork;
        public async Task<IdentityResult> CreateAsync(SuperAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.Id = await _UnitOfWork.SuperAdminRole.AddRoleAsync(role);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(SuperAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SuperAdminRole.DeleteRoleAsync(role.Id);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            // 
        }

        public async Task<SuperAdminRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SuperAdminRole.FindActiveRoleByRoleIdAsync(Convert.ToInt32(roleId));
        }

        public async Task<SuperAdminRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SuperAdminRole.FindActiveRoleByRoleNameAsync(normalizedRoleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(SuperAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(SuperAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(SuperAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(SuperAdminRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(SuperAdminRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(SuperAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SuperAdminRole.UpdateRoleAsync(role);
            return IdentityResult.Success;
        }
    }
}
