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
        Task<Response<FeeDepositSummeryViewModel>> GenerateFeeDepositSummery(SelectedFeeDepositViewModel model);
    }
}
