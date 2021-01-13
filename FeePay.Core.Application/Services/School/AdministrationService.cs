﻿using AutoMapper;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Services.School
{
    public class AdministrationService : IAdministrationService
    {
        public AdministrationService(IUnitOfWork unitOfWork, IAppContextAccessor appContextAccessor, ILoginService loginService,
            IMapper mapper, ISchoolAdminRegistrationService schoolAdminRegistrationService)
        {
            _unitOfWork = unitOfWork;
            _appContextAccessor = appContextAccessor;
            _loginService = loginService;
            _mapper = mapper;
            _schoolAdminRegistrationService = schoolAdminRegistrationService;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;
        private readonly ISchoolAdminRegistrationService _schoolAdminRegistrationService;



        public async Task<StaffMemberViewModel> BindStaffMemberViewModel(StaffMemberViewModel model = null)
        {
            if (model != null && model.Id != 0)
                model.RoleList = await GetCheckBoxRoleListAsync(model.Id);
            else
                model = new StaffMemberViewModel() { RoleList = await GetCheckBoxRoleListAsync() };
            return model;
        }
        public async Task<Response<List<StaffMemberViewModel>>> GetAllStaffMemberAsync()
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var tt = await _unitOfWork.SchoolAdminUser.FindAllActiveUser_WithAddEditUserAsync(SchoolId);
            return new Response<List<StaffMemberViewModel>>(_mapper.Map<List<StaffMemberViewModel>>(tt));
        }
        public async Task<Response<StaffMemberViewModel>> GetStaffByIdAsync(int unserId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            SchoolAdminUser schoolAdminUser = await _unitOfWork.SchoolAdminUser.FindActiveUserByUserIdAsync(unserId, SchoolId);
            StaffMemberViewModel StaffModel = _mapper.Map<StaffMemberViewModel>(schoolAdminUser);
            StaffModel.RoleList = await GetCheckBoxRoleListAsync(unserId);
            return new Response<StaffMemberViewModel>(StaffModel);
        }
        public async Task<Response<StaffMemberViewModel>> AddOrEditStaffMemberAsync(StaffMemberViewModel model)
        {
            if (model.Id == 0)
            {
                bool success = await _schoolAdminRegistrationService.RegisterSchoolUserWithPhoneNumberAsync(model);
                if (!success) new Response<StaffMemberViewModel>("User is already exist or a error is accord while creating staff member.");
            }
            else
            {
                bool success = await _schoolAdminRegistrationService.UpdateSchoolUserWithPhoneNumberAsync(model);
                if (!success) new Response<StaffMemberViewModel>("User is already exist with same phone number or a error is accord while updating staff member.");
            }
            return new Response<StaffMemberViewModel>(model);
        }
        public async Task<Response<bool>> deleteStaffMemberAsync(int userId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            try
            {
                var roles = await _unitOfWork.SchoolAdminUserRole.GetUserRolesAsync(userId, SchoolId);
                if (roles != null && roles.Count > 0)
                {
                    foreach (var role in roles)
                    {
                        await _unitOfWork.SchoolAdminUserRole.delete(userId, role.Id, SchoolId);
                    }
                }
                int res = await _unitOfWork.SchoolAdminUser.DeleteUserAsync(userId, SchoolId);
                if (res > 0) return new Response<bool>(true); 
                
                //_logger.LogInformation($@"Error deleting schoolAdminUser user Method={nameof(deleteStaffMemberAsync)} | 
                //            UserId={userId} | LoginUserId={_loginService.GetLogedInSchoolAdminId()} | DBsetId={SchoolId}");
                return new Response<bool>("Error deleting school staff member");
            }
            catch //(Exception ex)
            {
                //_logger.LogError(ex, $@"Error deleting schoolAdminUser user Method={nameof(deleteStaffMemberAsync)} | 
                //            UserId={userId} | LoginUserId={_loginService.GetLogedInSchoolAdminId()} | DBsetId={SchoolId}");
                return new Response<bool>();
            }
        }


        public async Task<RoleViewModel> BindRoleViewModel(RoleViewModel model = null)
        {
            if (model != null && model.Id != 0)
                model.UserList = await GetCheckBoxStaffListAsync(model.Id);
            else
                model = new RoleViewModel() { UserList = await GetCheckBoxStaffListAsync() };
            return model;
        }
        public async Task<Response<List<RoleViewModel>>> GetAllStaffRolesAsync()
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var tt = await _unitOfWork.SchoolAdminRole.GetAllActiveRoles_WithAddEditUserAsync(SchoolId);
            return new Response<List<RoleViewModel>>(_mapper.Map<List<RoleViewModel>>(tt.ToList()));
        }
        public async Task<Response<RoleViewModel>> GetStaffRoleByIdAsync(int roleId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            SchoolAdminRole schoolAdminRole = await _unitOfWork.SchoolAdminRole.FindActiveRoleByRoleIdAsync(roleId, SchoolId);
            RoleViewModel roleModel = _mapper.Map<RoleViewModel>(schoolAdminRole);
            roleModel.UserList = await GetCheckBoxStaffListAsync(roleId);
            return new Response<RoleViewModel>(roleModel);
        }
        public async Task<Response<RoleViewModel>> AddOrEditStaffRoleAsync(RoleViewModel model)
        {
            var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            SchoolAdminRole role = _mapper.Map<SchoolAdminRole>(model);
            int RoleId = model.Id;
            if (RoleId == 0)
            {
                role.AddedBy = UserId;
                RoleId = await _unitOfWork.SchoolAdminRole.AddRoleAsync(role, SchoolId);
                if (RoleId <= 0) return new Response<RoleViewModel>($"Role is already present with the same name {model.Name}");
            }
            else
            {
                role.ModifyBy = UserId;
                RoleId = await _unitOfWork.SchoolAdminRole.UpdateRoleAsync(role, SchoolId);
                if (RoleId <= 0) return new Response<RoleViewModel>($"Role is already present with the same name {model.Name}");
            }
            await _schoolAdminRegistrationService.AssignRolesToSchoolUserAsync(role, model.UserList);

            return new Response<RoleViewModel>(model);
        }
        public async Task<Response<bool>> deleteStaffRoleAsync(int roleId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            try
            {
                var staffs = await _unitOfWork.SchoolAdminUserRole.GetUsersInRoleAsync(roleId, SchoolId);
                if (staffs != null && staffs.Count > 0)
                {
                    foreach (var staff in staffs)
                    {
                        await _unitOfWork.SchoolAdminUserRole.delete(staff.Id, roleId, SchoolId);
                    }
                }
                int res = await _unitOfWork.SchoolAdminRole.DeleteRoleAsync(roleId, SchoolId);
                if (res > 0) return new Response<bool>(true);

                //_logger.LogInformation($@"Error deleting schoolAdminRole role Method={nameof(deleteStaffMemberAsync)} | 
                //            RoleId={roleId} | LoginUserId={_loginService.GetLogedInSchoolAdminId()} | DBsetId={SchoolId}");
                return new Response<bool>("Error deleting school staff member");
            }
            catch //(Exception ex)
            {
                //_logger.LogError(ex, $@"Error deleting schoolAdminRole role Method={nameof(deleteStaffMemberAsync)} | 
                //            RoleId={roleId} | LoginUserId={_loginService.GetLogedInSchoolAdminId()} | DBsetId={SchoolId}");
                return new Response<bool>();
            }
        }


        private async Task<List<CheckBoxItem>> GetCheckBoxRoleListAsync(int unserId = 0)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            IEnumerable<SchoolAdminRole> roles = await _unitOfWork.SchoolAdminRole.GetAllActiveRolesAsync(SchoolId);
            //List<RoleViewModel> RoleList = _mapper.Map<IList<RoleViewModel>>(allStaffRole).ToList();
            List<CheckBoxItem> cbList = new List<CheckBoxItem>();
            if (unserId != 0)
            {
                IList<SchoolAdminRole> rolesInStaffMember = await _unitOfWork.SchoolAdminUserRole.GetUserRolesAsync(unserId, SchoolId);
                roles.ToList().ForEach(f =>
                {
                    cbList.Add(new CheckBoxItem()
                    {

                        Id = f.Id,
                        Name = f.Name,
                        IsSelected = (rolesInStaffMember.Any(a => a.Id == f.Id)) ? true : false
                    });
                    //if (userRoles.Any(a => a.Id == f.Id)) f.IsSelected = true;
                    //else f.IsSelected = false;
                });
                //cbList = RoleList.Select(s => new CheckBoxItem { Id = s.Id, Name = s.Name, IsSelected = s.IsSelected }).ToList();
            }
            return cbList;
        }
        private async Task<List<CheckBoxItem>> GetCheckBoxStaffListAsync(int roleId = 0)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            IEnumerable<SchoolAdminUser> staff = await _unitOfWork.SchoolAdminUser.FindAllActiveUserAsync(SchoolId);
            List<CheckBoxItem> cbList = new List<CheckBoxItem>();
            string dash = "----";
            if (roleId != 0)
            {
                IEnumerable<SchoolAdminUser> staffMembersInRole = await _unitOfWork.SchoolAdminUserRole.GetUsersInRoleAsync(roleId, SchoolId);
                staff.ToList().ForEach(f =>
                {
                    cbList.Add(new CheckBoxItem()
                    {

                        Id = f.Id,
                        Name = $"Name : {f.FullName} | Email : {(string.IsNullOrEmpty(f.Email) ? dash : f.Email)} | Phone Number : {(string.IsNullOrEmpty(f.PhoneNumber) ? dash : f.PhoneNumber)}",
                        IsSelected = (staffMembersInRole.Any(a => a.Id == f.Id)) ? true : false
                    });
                });
            }
            return cbList;
        }
    }
}