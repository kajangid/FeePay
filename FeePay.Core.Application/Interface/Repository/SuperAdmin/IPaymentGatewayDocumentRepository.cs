using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.SuperAdmin;

namespace FeePay.Core.Application.Interface.Repository.SuperAdmin
{
    public interface IPaymentGatewayDocumentRepository
    {
        #region Execute
        Task<int> AddAsync(PaymentGatewayDocument paymentGatewayDocument);
        Task<int> UpdateAsync(PaymentGatewayDocument paymentGatewayDocument);
        Task<int> DeleteAsync(int id, int userId);
        #endregion

        #region Find
        Task<PaymentGatewayDocument> FindByIdAsync(int id, bool? isActive = null);
        Task<PaymentGatewayDocument> FindByRegisteredSchoolIdAsync(int registeredSchoolId, bool? isActive = null);
        Task<IEnumerable<PaymentGatewayDocument>> GetByIsApprovedAsync(bool isApproved, bool? isActive = null);
        #endregion


        #region Get All
        Task<IEnumerable<PaymentGatewayDocument>> GetAllAsync(bool? isActive = null);
        Task<IEnumerable<PaymentGatewayDocument>> GetAll_WithSchoolDataAsync(bool? isActive = null);
        Task<IEnumerable<PaymentGatewayDocument>> GetAll_WithAddEditUserAsync(bool? isActive = null);
        #endregion

    }
}
