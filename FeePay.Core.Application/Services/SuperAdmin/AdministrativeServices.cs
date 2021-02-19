using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.SuperAdmin;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Identity;

namespace FeePay.Core.Application.Services.SuperAdmin
{
    public class AdministrativeServices : IAdministrativeServices
    {
        public AdministrativeServices(IUnitOfWork unitOfWork,
            ILoginService loginService,
            IAppContextAccessor appContextAccessor,
            IMapper mapper,
            ISuperAdminRegistrationService superAdminRegistrationService
            )
        {
            _unitOfWork = unitOfWork;
            _loginService = loginService;
            _appContextAccessor = appContextAccessor;
            _mapper = mapper;
            _superAdminRegistrationService = superAdminRegistrationService;
        }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginService _loginService;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly ISuperAdminRegistrationService _superAdminRegistrationService;



        #region SUPER USER
        public async Task<Response<List<SuperAdmin_UserViewModel>>> GetUserListAsync()
        {
            var users = await _unitOfWork.SuperAdminUser.GetAll_WithAddEditUserAsync();
            IEnumerable<SuperAdmin_UserViewModel> model = _mapper.Map<IEnumerable<SuperAdmin_UserViewModel>>(users);
            return new Response<List<SuperAdmin_UserViewModel>>(model.ToList());
        }
        public async Task<Response<SuperAdmin_UserViewModel>> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.SuperAdminUser.FindByIdAsync(id);
            if (user == null) return new Response<SuperAdmin_UserViewModel>("No data Found.");
            SuperAdmin_UserViewModel model = _mapper.Map<SuperAdmin_UserViewModel>(user);
            return new Response<SuperAdmin_UserViewModel>(model);
        }
        public async Task<Response<bool>> AddOrEditUserAsync(SuperAdmin_UserViewModel model)
        {
            SuperAdminUser superUser = _mapper.Map<SuperAdminUser>(model);
            superUser.NormalizedUserName = superUser.UserName?.ToUpper().Trim();
            if (superUser.Id == 0)
            {
                return await _superAdminRegistrationService.RegisterUserAsync(superUser);
            }
            else
            {
                return await _superAdminRegistrationService.UpdateUserAsync(superUser);
            }
        }
        public async Task<Response<bool>> DeleteUserAsync(int userId)
        {
            int identityUserId = Convert.ToInt32(_loginService.GetLogedInSuperAdminId());
            if (userId == identityUserId) return new Response<bool>("Cannot delete user which you are logged in.");
            var res = await _unitOfWork.SuperAdminUser.DeleteAsync(userId);
            if (res <= 0) return new Response<bool>("No data found.");
            return new Response<bool>(res > 0);
        }
        public async Task<Response<bool>> ActiveUserAsync(int userId, bool isActive)
        {
            var checkExist = await _unitOfWork.SuperAdminUser.FindByIdAsync(userId);
            if (checkExist == null) return new Response<bool>("No data found.");
            int UserId = Convert.ToInt32(_loginService.GetLogedInSuperAdminId());
            checkExist.IsActive = isActive;
            checkExist.ModifyBy = UserId;
            var res = await _unitOfWork.SuperAdminUser.UpdateAsync(checkExist);
            if (res <= 0) return new Response<bool>($"Error {(isActive ? "activating" : "inactivating")} school.");
            return new Response<bool>(res > 0);
        }
        public async Task<Response<UserPasswordViewModel>> GetUserCredetianlAsync(int userId)
        {
            SuperAdminUser user = await _unitOfWork.SuperAdminUser.FindPasswordByIdAsync(userId);
            if (user == null) return new Response<UserPasswordViewModel>("No data found.");
            UserPasswordViewModel userNamePass = _mapper.Map<UserPasswordViewModel>(user);
            return new Response<UserPasswordViewModel>(userNamePass);
        }
        public async Task<Response<bool>> ChangeUserCredetianls_AdminAscync(ResetPasswordViewModel model, int userId)
        {
            SuperAdminUser user = await _unitOfWork.SuperAdminUser.FindByIdAsync(userId);
            if (user == null) return new Response<bool>("No data found.");
            user = _superAdminRegistrationService.GetNewHashPasswordAsync(user, model.NewPassword);
            var res = await _unitOfWork.SuperAdminUser.UpdateAsync(user);
            if (res <= 0) return new Response<bool>("Error changing password.");
            return new Response<bool>(res > 0);
        }
        #endregion

        #region SUPER USER PROFILE 
        public async Task<Response<SuperAdmin_UserViewModel>> GetUserProfileAsync()
        {
            var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var user = await _unitOfWork.SuperAdminUser.FindByIdAsync(UserId, isActive: true);
            if (user == null) return new Response<SuperAdmin_UserViewModel>("No data Found.");
            SuperAdmin_UserViewModel model = _mapper.Map<SuperAdmin_UserViewModel>(user);
            return new Response<SuperAdmin_UserViewModel>(model);
        }
        #endregion



    }
}
