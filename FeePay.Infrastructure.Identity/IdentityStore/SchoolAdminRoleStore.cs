using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
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
    public class SchoolAdminRoleStore : IRoleStore<SchoolAdminRole>
    {
        public SchoolAdminRoleStore(IUnitOfWork unitOfWork, IAppContextAccessor AppContextAccessor)
        {
            _UnitOfWork = unitOfWork;
            _AppContextAccessor = AppContextAccessor;
        }
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IAppContextAccessor _AppContextAccessor;
        public async Task<IdentityResult> CreateAsync(SchoolAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.Id = await _UnitOfWork.SchoolAdminRole.AddAsync(role, _AppContextAccessor.ClaimSchoolUniqueId());
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(SchoolAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SchoolAdminRole.UpdateAsync(role, _AppContextAccessor.ClaimSchoolUniqueId());
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(SchoolAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SchoolAdminRole.DeleteAsync(role.Id, _AppContextAccessor.ClaimSchoolUniqueId());
            return IdentityResult.Success;
        }

        public async Task<SchoolAdminRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SchoolAdminRole.FindActiveByIdAsync(Convert.ToInt32(roleId), _AppContextAccessor.ClaimSchoolUniqueId());
        }

        public async Task<SchoolAdminRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SchoolAdminRole.FindActiveByNameAsync(normalizedRoleName, _AppContextAccessor.ClaimSchoolUniqueId());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(SchoolAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(SchoolAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(SchoolAdminRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(SchoolAdminRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(SchoolAdminRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            role.Name = roleName;
            return Task.CompletedTask;
        }
    }
}
