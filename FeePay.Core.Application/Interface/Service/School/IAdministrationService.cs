using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Service.School
{
    public interface IAdministrationService
    {
        Task<StaffMemberViewModel> BindStaffMemberViewModel(StaffMemberViewModel model = null);
        Task<Response<List<RoleViewModel>>> GetAllStaffRolesAsync();
        Task<Response<RoleViewModel>> GetStaffRoleByIdAsync(int RoleId);
        Task<Response<RoleViewModel>> AddOrEditStaffRoleAsync(RoleViewModel model);
        Task<Response<bool>> DeleteStaffMemberAsync(int userId);
        Task<Response<UserPasswordViewModel>> GetStaffMemberPassword(int userId);


        Task<RoleViewModel> BindRoleViewModel(RoleViewModel model = null);
        Task<Response<List<StaffMemberViewModel>>> GetAllStaffMemberAsync();
        Task<Response<StaffMemberViewModel>> GetStaffByIdAsync(int unserId);
        Task<Response<StaffMemberViewModel>> AddOrEditStaffMemberAsync(StaffMemberViewModel model);
        Task<Response<bool>> DeleteStaffRoleAsync(int roleId);


        Task<Response<UserProfileViewModel>> GetUserProfileData();
        Task<Response<UserPasswordViewModel>> GetUserPassword();
        Task<Response<bool>> EditProfile(UserProfileViewModel model);
        Task<Response<bool>> ChangeUserPassword(ResetPasswordViewModel model);
    }
}
