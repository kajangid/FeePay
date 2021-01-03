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
        IUserTwoFactorStore<SuperAdminUser>, IUserPasswordStore<SuperAdminUser>//, IUserRoleStore<SuperAdminUser>
    {
        public SuperAdminUserStore(IConnectionStringBuilder _connectionStringBuilder, IDBVariables dBVariables,
            IUnitOfWork unitOfWork)
        {
            _connectionString = _connectionStringBuilder.GetDefaultConnectionString();
            _DBVariables = dBVariables;
            _UnitOfWork = unitOfWork;
        }
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IDBVariables _DBVariables;
        private readonly string _connectionString;
        private readonly string SuperAdminUsertbl = "[SuperAdmin_User]";
        private readonly string SuperAdminRoletbl = "[SuperAdmin_Role]";
        private readonly string SuperAdminUserRoletbl = "[SuperAdmin_UserRole]";
        public async Task<IdentityResult> CreateAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.Id = await _UnitOfWork.SuperAdminUser.AddUserAsync(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SuperAdminUser.DeleteUserAsync(user.Id);
            return IdentityResult.Success;
        }

        public async Task<SuperAdminUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SuperAdminUser.FindActiveUserByUserIdAsync(Convert.ToInt32(userId));
        }

        public async Task<SuperAdminUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SuperAdminUser.FindActiveUserByUserNameAsync(normalizedUserName);
        }

        public async Task<SuperAdminUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _UnitOfWork.SuperAdminUser.FindActiveUserByUserEmailAsync(normalizedEmail);
        }

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

        public async Task<IdentityResult> UpdateAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _UnitOfWork.SuperAdminUser.UpdateUserAsync(user);
            return IdentityResult.Success;
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

        /*
        public async Task AddToRoleAsync(SuperAdminUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var normalizedName = roleName.ToUpper();
                var roleId = await connection.ExecuteScalarAsync<int?>($"SELECT [Id] FROM {SuperAdminRoletbl} WHERE [NormalizedName] = @{nameof(normalizedName)}", new { normalizedName });
                if (!roleId.HasValue)
                    roleId = await connection.ExecuteAsync($"INSERT INTO {SuperAdminRoletbl}([Name], [NormalizedName]) VALUES(@{nameof(roleName)}, @{nameof(normalizedName)})",
                        new { roleName, normalizedName });

                await connection.ExecuteAsync($"IF NOT EXISTS(SELECT 1 FROM {SuperAdminUserRoletbl} WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}) " +
                    $"INSERT INTO {SuperAdminUserRoletbl}([UserId], [RoleId]) VALUES(@userId, @{nameof(roleId)})",
                    new { userId = user.Id, roleId });
            }
        }

        public async Task RemoveFromRoleAsync(SuperAdminUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var roleId = await connection.ExecuteScalarAsync<int?>($"SELECT [Id] FROM {SuperAdminRoletbl} WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (!roleId.HasValue)
                    await connection.ExecuteAsync($"DELETE FROM {SuperAdminUserRoletbl} WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}", new { userId = user.Id, roleId });
            }
        }

        public async Task<IList<string>> GetRolesAsync(SuperAdminUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var queryResults = await connection.QueryAsync<string>($"SELECT r.[Name] FROM {SuperAdminRoletbl} r INNER JOIN {SuperAdminUserRoletbl} ur ON ur.[RoleId] = r.Id " +
                    "WHERE ur.UserId = @userId", new { userId = user.Id });

                return queryResults.ToList();
            }
        }

        public async Task<bool> IsInRoleAsync(SuperAdminUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var roleId = await connection.ExecuteScalarAsync<int?>($"SELECT [Id] FROM {SuperAdminRoletbl} WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (roleId == default(int)) return false;
                var matchingRoles = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {SuperAdminUserRoletbl} WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
                    new { userId = user.Id, roleId });

                return matchingRoles > 0;
            }
        }

        public async Task<IList<SuperAdminUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var queryResults = await connection.QueryAsync<SuperAdminUser>($@"SELECT u.* FROM {SuperAdminUsertbl} u 
                    INNER JOIN {SuperAdminUserRoletbl} ur ON ur.[UserId] = u.[Id] INNER JOIN {SuperAdminRoletbl} r ON r.[Id] = ur.[RoleId] WHERE r.[NormalizedName] = @normalizedName",
                    new { normalizedName = roleName.ToUpper() });

                return queryResults.ToList();
            }
        }
        */
        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
