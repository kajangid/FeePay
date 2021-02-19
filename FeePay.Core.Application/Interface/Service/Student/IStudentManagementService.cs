using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Service.Student
{
    public interface IStudentManagementService
    { 
        #region STUDENT_ADMISSION CURD
        Task<StudentAdmissionViewModel> BindStudentAdmissionViewModelAsync(StudentAdmissionViewModel model = null);
        Task<Response<StudentSearchViewModel>> SearchStudentAsync(StudentSearchViewModel searchModel = null);
        Task<Response<List<StudentAdmissionViewModel>>> GetListOfStudentsAsync();
        Task<Response<StudentAdmissionViewModel>> FindStudentByIdAsync(int Id);
        Task<Response<bool>> AddOrEditStudentAsync(StudentAdmissionViewModel model);
        #endregion

        #region STUDENT PROFILE
        Task<Response<StudentAdmissionViewModel>> StudentProfileAsync();
        #endregion

        Task<Response<StudentLedgerViewModel>> StudentLedgerAsync(int id);
        Task<Response<UserPasswordViewModel>> GetStudentPasswordAsync(int studentId);

        /// <summary>
        /// Method to change Student login password when request is send by schoolAdmin user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>s
        Task<Response<bool>> ChangePassword_FromAdminAsync(ResetPasswordViewModel model, int userId);

        /// <summary>
        /// Method to change Student login password when request is send by Student Panel
        /// </summary>
        /// <param name="model"> Model with Password </param>
        /// <returns> boolen response object </returns>
        Task<Response<bool>> ChangePassword_FromStudentAsync(ResetPasswordViewModel model);


        Task<Response<StudentPromotionViewModel>> StudentPromotion_SearchStudentAndBindModel(StudentPromotionViewModel model = null);
        Task<Response<bool>> StudentPromotion_Promote(StudentPromotionViewModel model);
    }
}
