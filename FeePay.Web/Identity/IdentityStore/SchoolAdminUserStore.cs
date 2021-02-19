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
    public class SchoolAdminUserStore : IUserStore<SchoolAdminUser>, IUserEmailStore<SchoolAdminUser>, IUserPhoneNumberStore<SchoolAdminUser>,
        IUserTwoFactorStore<SchoolAdminUser>, IUserPasswordStore<SchoolAdminUser>, IUserRoleStore<SchoolAdminUser>
    {
        public SchoolAdminUserStore(IUnitOfWork unitOfWork, IAppContextAccessor AppContextAccessor)
        {
            _UnitOfWork = unitOfWork;
            _AppContextAccessor = AppContextAccessor;
        }
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IAppContextAccessor _AppContextAccessor;

        public async Task<IdentityResult> CreateAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.Id = await _UnitOfWork.SchoolAdminUser.AddAsync(user, _AppContextAccessor.ClaimSchoolUniqueId());
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SchoolAdminUser.UpdateAsync(user, _AppContextAccessor.ClaimSchoolUniqueId());
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SchoolAdminUser.DeleteAsync(user.Id, _AppContextAccessor.ClaimSchoolUniqueId());
            return IdentityResult.Success;
        }

        public async Task<SchoolAdminUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SchoolAdminUser.FindActiveByIdAsync(Convert.ToInt32(userId), _AppContextAccessor.ClaimSchoolUniqueId());
        }

        public async Task<SchoolAdminUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SchoolAdminUser.FindActiveByUserNameAsync(normalizedUserName, _AppContextAccessor.ClaimSchoolUniqueId());
        }

        public async Task<SchoolAdminUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SchoolAdminUser.FindActiveByEmailAsync(normalizedEmail, _AppContextAccessor.ClaimSchoolUniqueId());
        }

        // other fetch methods

        public Task<string> GetNormalizedUserNameAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(SchoolAdminUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(SchoolAdminUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public Task SetEmailAsync(SchoolAdminUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(SchoolAdminUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedEmailAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(SchoolAdminUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(SchoolAdminUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(SchoolAdminUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(SchoolAdminUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetPasswordHashAsync(SchoolAdminUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        // role assigning

        public async Task AddToRoleAsync(SchoolAdminUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SchoolAdminUserRole.AssignRoleToUserAsync(user, roleName, _AppContextAccessor.ClaimSchoolUniqueId());
        }

        public async Task RemoveFromRoleAsync(SchoolAdminUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SchoolAdminUserRole.UnassignUserFromRoleAsync(user, roleName, _AppContextAccessor.ClaimSchoolUniqueId());
        }

        public async Task<IList<string>> GetRolesAsync(SchoolAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var Roles = await _UnitOfWork.SchoolAdminUserRole.GetUserRolesAsync(user.Id, _AppContextAccessor.ClaimSchoolUniqueId());
            return Roles.Select(s => s.Name).ToList();
        }

        public async Task<bool> IsInRoleAsync(SchoolAdminUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            int matchingRoles = await _UnitOfWork.SchoolAdminUserRole.UserInRoleAsync(user.Id, roleName, _AppContextAccessor.ClaimSchoolUniqueId());
            return matchingRoles > 0;
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    var roleId = await connection.ExecuteScalarAsync<int?>($"SELECT [Id] FROM {SchoolAdminRoletbl} WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
            //    if (roleId == default(int)) return false;
            //    var matchingRoles = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {SchoolAdminUserRoletbl} WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
            //        new { userId = user.Id, roleId });
            //    return matchingRoles > 0;
            //}
        }

        public async Task<IList<SchoolAdminUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return (await _UnitOfWork.SchoolAdminUserRole.GetUsersInRoleAsync(roleName, _AppContextAccessor.ClaimSchoolUniqueId())).ToList();
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
