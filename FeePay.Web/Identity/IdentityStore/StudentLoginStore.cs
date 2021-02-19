using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Domain.Entities.Identity;

namespace FeePay.Infrastructure.Identity.IdentityStore
{
    public class StudentLoginStore : IUserStore<StudentLogin>, IUserEmailStore<StudentLogin>, IUserPhoneNumberStore<StudentLogin>,
        IUserTwoFactorStore<StudentLogin>, IUserPasswordStore<StudentLogin>
    {
        public StudentLoginStore(IUnitOfWork unitOfWork, IAppContextAccessor appContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _appContextAccessor = appContextAccessor;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppContextAccessor _appContextAccessor;
        public async Task<IdentityResult> CreateAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            user.Id = await _unitOfWork.StudentLogin.AddUserAsync(user, _appContextAccessor.ClaimSchoolUniqueId());
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _unitOfWork.StudentLogin.UpdateUserAsync(user, _appContextAccessor.ClaimSchoolUniqueId());
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _unitOfWork.StudentLogin.DeleteUserAsync(user.Id, _appContextAccessor.ClaimSchoolUniqueId());
            return IdentityResult.Success;
        }

        public async Task<StudentLogin> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _unitOfWork.StudentLogin.FindActiveUserByUserIdAsync(Convert.ToInt32(userId), _appContextAccessor.ClaimSchoolUniqueId());
        }

        public async Task<StudentLogin> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _unitOfWork.StudentLogin.FindActiveUserByUserNameAsync(normalizedUserName, _appContextAccessor.ClaimSchoolUniqueId());
        }

        public async Task<StudentLogin> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _unitOfWork.StudentLogin.FindActiveUserByUserEmailAsync(normalizedEmail, _appContextAccessor.ClaimSchoolUniqueId());
        }

        //

        public Task<string> GetNormalizedUserNameAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(StudentLogin user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(StudentLogin user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public Task SetEmailAsync(StudentLogin user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(StudentLogin user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedEmailAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(StudentLogin user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(StudentLogin user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(StudentLogin user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(StudentLogin user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetPasswordHashAsync(StudentLogin user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(StudentLogin user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public void Dispose()
        {
            //
        }
    }
}
