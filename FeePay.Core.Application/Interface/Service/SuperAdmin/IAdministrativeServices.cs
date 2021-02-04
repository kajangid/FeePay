using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Wrapper;

namespace FeePay.Core.Application.Interface.Service.SuperAdmin
{
    public interface IAdministrativeServices
    {


        #region SUPER USER
        Task<Response<List<SuperAdmin_UserViewModel>>> GetUserListAsync();
        Task<Response<SuperAdmin_UserViewModel>> GetUserByIdAsync(int id);
        Task<Response<bool>> AddOrEditUserAsync(SuperAdmin_UserViewModel model);
        Task<Response<bool>> DeleteUserAsync(int userId);
        Task<Response<bool>> ActiveUserAsync(int userId, bool isActive);
        Task<Response<UserPasswordViewModel>> GetUserCredetianlAsync(int userId);
        Task<Response<bool>> ChangeUserCredetianls_AdminAscync(ResetPasswordViewModel model, int userId);
        #endregion
        #region SUPER USER PROFILE 
        Task<Response<SuperAdmin_UserViewModel>> GetUserProfileAsync();
        #endregion
    }
}
