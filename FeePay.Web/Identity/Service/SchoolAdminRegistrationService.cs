using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.UseCase;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;

namespace FeePay.Infrastructure.Identity.Service
{
    public class SchoolAdminRegistrationService : ISchoolAdminRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly ILoginService _loginService;
        private readonly UserManager<SchoolAdminUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<SchoolAdminUser> _passwordHasher;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IUtilityService _utilityService;
        public SchoolAdminRegistrationService(
            IUnitOfWork unitOfWork,
            IAppContextAccessor appContextAccessor,
            ILoginService loginService,
            UserManager<SchoolAdminUser> userManager,
            IMapper mapper,
            IPasswordHasher<SchoolAdminUser> passwordHasher,
            IPasswordGenerator passwordGenerator,
            IUtilityService utilityService
            )
        {
            _unitOfWork = unitOfWork;
            _appContextAccessor = appContextAccessor;
            _loginService = loginService;
            _userManager = userManager;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _passwordGenerator = passwordGenerator;
            _utilityService = utilityService;
        }

        /// <summary>
        /// Generate a EmailVerification Token/Code
        /// </summary>
        /// <param name="user"> EmailVarification Token/Code for User Data </param>
        /// <returns> Confirmation Link </returns>
        public async Task<string> GenarateEmailVarificationCodeAsync(SchoolAdminUser user)
        {
            try
            {
                var userId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
                var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
                //var user = await _unitOfWork.SchoolAdminUser.FindByIdAsync(userId, schoolId);
                var token = await _userManager
                    .GenerateUserTokenAsync(user, "SchoolAdminEmailConfirmTokenProvider", "emailconfirmation-auth");
                var confirmationLink = _appContextAccessor
                    .AbsoluteUriByPage("School/Authentication/ConfirmEmail", new
                    {
                        token,
                        email = _utilityService.EncodeUrl(user.Email)
                    });

                // Save token and expire time in db
                return confirmationLink;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Verify EmailVarification Token/Code
        /// </summary>
        /// <param name="user"> Verify for User Data </param>
        /// <param name="token"> Verification Token/Code </param>
        /// <returns></returns>
        public async Task<bool> VerifyEmailVarificationCodeAsync(SchoolAdminUser user, string token)
        {
            try
            {
                var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
                var res = await _userManager.ConfirmEmailAsync(user, _utilityService.DecodeUrl(token));
                return (res.Succeeded);
            }
            catch (Exception ex)
            {
                return (ex.Message == null);
            }
        }








        public async Task<bool> RegisterSchoolUserWithPhoneNumberAsync(StaffMemberViewModel model)
        {
            var CurrentUserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var User = _mapper.Map<SchoolAdminUser>(model);

            User.UserName = GeneratorToken.GenerateUserName(8);
            if (string.IsNullOrEmpty(model.Password?.Trim())) User.Password = _passwordGenerator.Generate();
            User.AddedBy = CurrentUserId;

            IdentityResult identityResult = await _userManager.CreateAsync(User, User.Password);

            if (identityResult.Succeeded && model.RoleList != null && model.RoleList.Any(a => a.IsSelected == true))
            {
                SchoolAdminUser UserInserted = await _unitOfWork.SchoolAdminUser.FindByUserNameAsync(User.UserName, SchoolId);
                await AssignRolesToSchoolUserAsync(UserInserted, model.RoleList);
            }
            return identityResult.Succeeded;
        }
        public async Task<bool> UpdateSchoolUserWithPhoneNumberAsync(StaffMemberViewModel model)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var CurrentUserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            //var User = _mapper.Map<SchoolAdminUser>(model);

            SchoolAdminUser UserEntity = await _unitOfWork.SchoolAdminUser.FindByIdAsync(model.Id, SchoolId);
            UserEntity.ModifyBy = CurrentUserId;
            UserEntity.PhoneNumber = model.PhoneNumber;
            UserEntity.Email = model.Email;
            UserEntity.FirstName = model.FirstName;
            UserEntity.LastName = model.LastName;
            IdentityResult identityResult = await _userManager.UpdateAsync(UserEntity);

            if (model.RoleList != null)
            {
                await AssignRolesToSchoolUserAsync(UserEntity, model.RoleList);
            }
            return identityResult.Succeeded;
        }



        public async Task ChangeSchoolUserPasswordAsync(SchoolAdminUser user, string newPassword)
        {
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, newPassword);
        }
        public SchoolAdminUser GetNewHashSchoolUserPasswordAsync(SchoolAdminUser user, string newPassword)
        {
            var hasedPassword = _passwordHasher.HashPassword(user, newPassword);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = hasedPassword;
            return user;
        }


        public async Task AssignRolesToSchoolUserAsync(SchoolAdminUser user, List<CheckBoxItem> roleList)
        {
            foreach (var f in roleList)
            {
                bool isInRole = await _userManager.IsInRoleAsync(user, f.Name);
                if (f.IsSelected && !isInRole)
                {
                    await _userManager.AddToRoleAsync(user, f.Name);
                }
                else if (!f.IsSelected && isInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, f.Name);
                }
            }
        }
        public async Task AssignRolesToSchoolUserAsync(SchoolAdminRole role, List<CheckBoxItem> userList)
        {
            foreach (var f in userList)
            {
                SchoolAdminUser user = await _userManager.FindByIdAsync(f.Id.ToString());
                bool isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                if (f.IsSelected && !isInRole)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!f.IsSelected && isInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }
        }
    }
}
