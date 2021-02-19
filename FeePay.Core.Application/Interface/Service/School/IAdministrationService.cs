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
        #region USER  
        Task<StaffMemberViewModel> BindStaffMemberViewModel(StaffMemberViewModel model = null);
        Task<Response<List<RoleViewModel>>> GetAllStaffRolesAsync();
        Task<Response<RoleViewModel>> GetStaffRoleByIdAsync(int RoleId);
        Task<Response<RoleViewModel>> AddOrEditStaffRoleAsync(RoleViewModel model);
        Task<Response<bool>> DeleteStaffMemberAsync(int userId);
        Task<Response<UserPasswordViewModel>> GetStaffMemberPassword(int userId);
        #endregion

        #region USER ROLES 
        Task<RoleViewModel> BindRoleViewModel(RoleViewModel model = null);
        Task<Response<List<StaffMemberViewModel>>> GetAllStaffMemberAsync();
        Task<Response<StaffMemberViewModel>> GetStaffByIdAsync(int unserId);
        Task<Response<StaffMemberViewModel>> AddOrEditStaffMemberAsync(StaffMemberViewModel model);
        Task<Response<bool>> DeleteStaffRoleAsync(int roleId);
        #endregion

        #region USER PROFILE
        Task<Response<UserProfileViewModel>> GetUserProfileData();
        Task<Response<UserPasswordViewModel>> GetUserPassword();
        Task<Response<bool>> EditProfile(UserProfileViewModel model);
        Task<Response<bool>> ChangeUserPassword(ResetPasswordViewModel model);
        #endregion

        #region PAYMENT GATEWAY DOCUMENT
        Task<Response<PaymentGatewayDocumentViewModel>> GetPaymentGatewayDocument();
        Task<Response<bool>> AddOrEditPaymentGatewayDocument(PaymentGatewayDocumentViewModel model);
        #endregion
    }
}
