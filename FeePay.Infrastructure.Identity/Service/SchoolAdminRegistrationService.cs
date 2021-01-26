using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using FeePay.Core.Application.UseCase;
using FeePay.Core.Application.Interface;

namespace FeePay.Infrastructure.Identity.Service
{
    public class SchoolAdminRegistrationService : ISchoolAdminRegistrationService
    {
        public SchoolAdminRegistrationService(IUnitOfWork unitOfWork, IAppContextAccessor appContextAccessor, ILoginService loginService,
            UserManager<SchoolAdminUser> userManager, IMapper mapper, ILogger<SchoolAdminRegistrationService> logger, 
            IPasswordHasher<SchoolAdminUser> passwordHasher, IPasswordGenerator passwordGenerator)
        {
            _unitOfWork = unitOfWork;
            _appContextAccessor = appContextAccessor;
            _loginService = loginService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _passwordGenerator = passwordGenerator;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly ILoginService _loginService;
        private readonly UserManager<SchoolAdminUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<SchoolAdminRegistrationService> _logger;
        public readonly IPasswordHasher<SchoolAdminUser> _passwordHasher;
        public readonly IPasswordGenerator _passwordGenerator;


        public async Task<bool> RegisterSchoolUserWithPhoneNumberAsync(StaffMemberViewModel model)
        {
            try
            {
                var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
                var User = _mapper.Map<SchoolAdminUser>(model);

                User.UserName = GeneratorToken.GenerateUserName(8);
                if (string.IsNullOrEmpty(model.Password?.Trim())) User.Password = _passwordGenerator.Generate();
                User.AddedBy = UserId;


                IdentityResult identityResult = await _userManager.CreateAsync(User, User.Password);
                _logger.LogInformation("new school admin user created.....");


                if (model.RoleList != null && model.RoleList.Any(a => a.IsSelected == true))
                {
                    SchoolAdminUser UserInserted = await _unitOfWork.SchoolAdminUser.FindByUserNameAsync(User.UserName, _appContextAccessor.ClaimSchoolUniqueId());
                    // _userManager.FindByNameAsync(User.UserName);
                    await AssignRolesToSchoolUserAsync(UserInserted, model.RoleList);
                }
                return identityResult.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding school admin user.....", ex);
                throw;
            }
        }
        public async Task<bool> UpdateSchoolUserWithPhoneNumberAsync(StaffMemberViewModel model)
        {
            try
            {
                var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
                var User = _mapper.Map<SchoolAdminUser>(model);

                //User.UserName = GeneratorToken.GenerateUserName(8);
                //User.Password = _passwordGenerator.Generate();
                //if (string.IsNullOrEmpty(model.Password?.Trim())) User.Password = model.PhoneNumber;
                User.ModifyBy = UserId;

                IdentityResult identityResult = await _userManager.UpdateAsync(User);
                _logger.LogInformation("school admin user updated.....");


                if (model.RoleList != null)
                {
                    SchoolAdminUser UserInserted = await _unitOfWork.SchoolAdminUser.FindByUserNameAsync(User.UserName, _appContextAccessor.ClaimSchoolUniqueId());
                    // _userManager.FindByNameAsync(User.UserName);
                    await AssignRolesToSchoolUserAsync(UserInserted, model.RoleList);
                }
                return identityResult.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating school admin user.....", ex);
                throw;
            }
        }



        public async Task ChangeSchoolUserPasswordAsync(SchoolAdminUser user, string newPassword)
        {
            await _userManager.RemovePasswordAsync(user);
            _logger.LogInformation($"Password is removed from school admin user with UserId = {user.Id}");
            await _userManager.AddPasswordAsync(user, newPassword);
            _logger.LogInformation($"New password is added to school admin user with UserId = {user.Id}");
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
                    _logger.LogInformation($"Role Added to school admin user with UserId = {user.Id} and RoleName = {f.Name}");
                }
                else if (!f.IsSelected && isInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, f.Name);
                    _logger.LogInformation($"Role Remove from school admin user with UserId = {user.Id} and RoleName = {f.Name}");
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
                    _logger.LogInformation($"Role Added to school admin user with UserId = {user.Id} and RoleName = {role.Name}");
                }
                else if (!f.IsSelected && isInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                    _logger.LogInformation($"Role Remove from school admin user with UserId = {user.Id} and RoleName = {role.Name}");
                }
            }
        }
    }
}
