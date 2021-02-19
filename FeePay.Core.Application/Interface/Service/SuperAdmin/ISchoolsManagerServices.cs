using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Wrapper;

namespace FeePay.Core.Application.Interface.Service.SuperAdmin
{
    public interface ISchoolsManagerServices
    {
        #region REGISTER SCHOOL
        Task<Response<List<RegisterSchoolViewModel>>> GetRegisterSchoolList();
        Task<Response<RegisterSchoolViewModel>> GetRegisterSchoolById(int id);
        Task<Response<bool>> AddOrEditRegisterSchool(RegisterSchoolViewModel model);
        Task<Response<bool>> DeleteRegisterSchool(int registeredSchoolId);
        Task<Response<bool>> ApproveRegisterSchool(int registeredSchoolId, bool IsApproved);
        #endregion


        #region PAYMENT GATEWAY DOCUMENT
        Task<Response<List<PaymentGatewayDocumentViewModel>>> GetPaymentGatewayDocumentList();
        Task<Response<PaymentGatewayDocumentViewModel>> GetPaymentGatewayDocumentById(int id); 
        Task<Response<bool>> ApprovePaymentGateway(IsActiveRequestViewModel model);
        #endregion
    }
}
