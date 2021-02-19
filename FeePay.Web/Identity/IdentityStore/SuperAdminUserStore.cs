using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace FeePay.Infrastructure.Identity.IdentityStore
{
    public class SuperAdminUserStore : IUserStore<SuperAdminUser>, IUserEmailStore<SuperAdminUser>, IUserPhoneNumberStore<SuperAdminUser>,
        IUserTwoFactorStore<SuperAdminUser>, IUserPasswordStore<SuperAdminUser>, IUserRoleStore<SuperAdminUser>
    {
        public SuperAdminUserStore(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private readonly IUnitOfWork _UnitOfWork;
        public async Task<IdentityResult> CreateAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.Id = await _UnitOfWork.SuperAdminUser.AddAsync(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SuperAdminUser.UpdateAsync(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SuperAdminUser.DeleteAsync(user.Id);
            return IdentityResult.Success;
        }

        public async Task<SuperAdminUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SuperAdminUser.FindByIdAsync(Convert.ToInt32(userId), isActive: true);
        }

        public async Task<SuperAdminUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SuperAdminUser.FindByUserNameAsync(normalizedUserName, isActive: true);
        }

        public async Task<SuperAdminUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SuperAdminUser.FindByEmailAsync(normalizedEmail, isActive: true);
        }

        // other fetch methods

        public Task<string> GetNormalizedUserNameAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(SuperAdminUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(SuperAdminUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public Task SetEmailAsync(SuperAdminUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(SuperAdminUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedEmailAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(SuperAdminUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(SuperAdminUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(SuperAdminUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(SuperAdminUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetPasswordHashAsync(SuperAdminUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        //role assigning

        public async Task AddToRoleAsync(SuperAdminUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SuperAdminUserRole.AssignRoleToUserAsync(user, roleName);
        }

        public async Task RemoveFromRoleAsync(SuperAdminUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SuperAdminUserRole.UnassignUserFromRoleAsync(user, roleName);
        }

        public async Task<IList<string>> GetRolesAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var Roles = await _UnitOfWork.SuperAdminUserRole.GetUserRolesAsync(user);
            return Roles.Select(s => s.Name).ToList();
        }

        public async Task<bool> IsInRoleAsync(SuperAdminUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            int matchingRoles = await _UnitOfWork.SuperAdminUserRole.UserInRoleAsync(user, roleName);
            return matchingRoles > 0;
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    var roleId = await connection.ExecuteScalarAsync<int?>($"SELECT [Id] FROM {SuperAdminRoletbl} WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
            //    if (roleId == default(int)) return false;
            //    var matchingRoles = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {SuperAdminUserRoletbl} WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
            //        new { userId = user.Id, roleId });
            //    return matchingRoles > 0;
            //}
        }

        public async Task<IList<SuperAdminUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return (await _UnitOfWork.SuperAdminUserRole.GetUsersInRoleAsync(roleName)).ToList();
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
