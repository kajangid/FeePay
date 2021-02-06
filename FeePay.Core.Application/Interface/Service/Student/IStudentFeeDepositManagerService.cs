using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Wrapper;

namespace FeePay.Core.Application.Interface.Service.Student
{
    public interface IStudentFeeDepositManagerService
    {
        Task<Response<IEnumerable<StudentFeesViewModel>>> GetStudentFees();
        Task<Response<FeeDepositSummeryViewModel>> GetSelectedFeeSummary(SelectedFeeDepositViewModel model);
        Task<Response<FeeDepositSummeryViewModel>> GenerateFeeDeposit(SelectedFeeDepositViewModel model);
        Task<Response<bool>> GenerateFeesTransaction(string transactionId, string status,
            string mode, decimal amountPay, List<StudentFeesViewModel> feesModel);
        Task<Response<bool>> ComplateFeesTransaction(string transactionId, string status, DateTime complateDate);
        Task<Response<bool>> FailFeesTransaction(string transactionId, string status);
    }
}
