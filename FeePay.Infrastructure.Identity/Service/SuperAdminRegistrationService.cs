using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.UseCase;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Application.Interface.Service.SuperAdmin;

namespace FeePay.Infrastructure.Identity.Service
{
    public class SuperAdminRegistrationService : ISuperAdminRegistrationService
    {
        public SuperAdminRegistrationService(IUnitOfWork unitOfWork,
            IAppContextAccessor appContextAccessor,
            ILoginService loginService,
            UserManager<SuperAdminUser> userManager,
            IMapper mapper,
            IPasswordHasher<SuperAdminUser> passwordHasher,
            IPasswordGenerator passwordGenerator)
        {
            _unitOfWork = unitOfWork;
            _appContextAccessor = appContextAccessor;
            _loginService = loginService;
            _userManager = userManager;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _passwordGenerator = passwordGenerator;
        }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginService _loginService;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly UserManager<SuperAdminUser> _userManager;
        public readonly IPasswordHasher<SuperAdminUser> _passwordHasher;
        public readonly IPasswordGenerator _passwordGenerator;


        public async Task<Response<bool>> RegisterUserAsync(SuperAdminUser model)
        {
            var CurrentUserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());

            model.UserName = GeneratorToken.GenerateUserName(8);
            model.Password = _passwordGenerator.Generate();
            model.AddedBy = CurrentUserId;

            // Check For Username Exist
            SuperAdminUser existUser = await _unitOfWork.SuperAdminUser.FindByUserNameAsync(model.UserName);
            if (existUser != null) return new Response<bool>("Username already exist");
            // Check For Email Exist If Email Unique Is Enabled
            if (!string.IsNullOrEmpty(model.Email))
            {
                existUser = await _unitOfWork.SuperAdminUser.FindByEmailAsync(model.Email);
                if (existUser != null) return new Response<bool>("Email already exist");
            }


            IdentityResult identityResult = await _userManager.CreateAsync(model, model.Password);
            if (!identityResult.Succeeded)
                return new Response<bool>(string.Join(",", identityResult.Errors.Select(s => s.Description).ToList()));
            return new Response<bool>(identityResult.Succeeded);
        }
        public async Task<Response<bool>> UpdateUserAsync(SuperAdminUser model)
        {
            var CurrentUserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());

            SuperAdminUser UserEntity = await _unitOfWork.SuperAdminUser.FindByIdAsync(model.Id);

            // Check For Email Exist If Email Unique Is Enabled And Change Accord
            if (!string.IsNullOrEmpty(model.Email) && model.Email != UserEntity.Email)
            {
                SuperAdminUser existUser = await _unitOfWork.SuperAdminUser.FindByEmailAsync(model.Email);
                if (existUser != null) return new Response<bool>("Email already exist");
            }
            UserEntity.ModifyBy = CurrentUserId;
            UserEntity.PhoneNumber = model.PhoneNumber;
            UserEntity.Email = model.Email;
            UserEntity.FirstName = model.FirstName;
            UserEntity.LastName = model.LastName;
            UserEntity.Photo = model.Photo;
            UserEntity.City = model.City;
            UserEntity.IsActive = model.IsActive;
            IdentityResult identityResult = await _userManager.UpdateAsync(UserEntity);
            if (!identityResult.Succeeded)
                return new Response<bool>(string.Join(",", identityResult.Errors.Select(s => s.Description).ToList()));
            return new Response<bool>(identityResult.Succeeded);
        }
        public async Task<Response<bool>> UpdateUserNameAsync(SuperAdminUser model)
        {
            if (string.IsNullOrEmpty(model.UserName)) return new Response<bool>("Username not provided.");
            var CurrentUserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());

            SuperAdminUser UserEntity = await _unitOfWork.SuperAdminUser.FindByIdAsync(model.Id);

            // Check For Username Exist If Change Accord
            if (model.UserName != UserEntity.UserName)
            {
                SuperAdminUser existUser = await _unitOfWork.SuperAdminUser.FindByUserNameAsync(model.UserName);
                if (existUser != null) return new Response<bool>("Username already exist");
            }
            UserEntity.ModifyBy = CurrentUserId;
            UserEntity.UserName = model.UserName;
            IdentityResult identityResult = await _userManager.UpdateAsync(UserEntity);
            if (!identityResult.Succeeded)
                return new Response<bool>(string.Join(",", identityResult.Errors.Select(s => s.Description).ToList()));
            return new Response<bool>(identityResult.Succeeded);
        }


        // Mics
        public async Task Remove_AddPasswordAsync(SuperAdminUser user, string newPassword)
        {
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, newPassword);
        }
        public SuperAdminUser GetNewHashPasswordAsync(SuperAdminUser user, string newPassword)
        {
            var hasedPassword = _passwordHasher.HashPassword(user, newPassword);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = hasedPassword;
            return user;
        }
    }
}
