using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Service.School
{
    public interface IFeeManagementService
    {
        #region Fee Type 
        Task<Response<List<FeeTypeViewModel>>> GetAllFeeTypeAsync();
        Task<Response<FeeTypeViewModel>> GetFeeTypeByIdAsync(int feeTypeId);
        Task<Response<FeeTypeViewModel>> AddOrEditFeeTypeAsync(FeeTypeViewModel model);
        Task<Response<bool>> deleteFeeTypeAsync(int feeTypeId);
        #endregion

        #region Fee Group 
        Task<Response<List<FeeGroupViewModel>>> GetAllFeeGroupAsync();
        Task<Response<FeeGroupViewModel>> GetFeeGroupByIdAsync(int feeGroupId);
        Task<Response<FeeGroupViewModel>> AddOrEditFeeGroupAsync(FeeGroupViewModel model);
        Task<Response<bool>> deleteFeeGroupAsync(int feeGroupId);
        #endregion

        #region Fee Master
        Task<FeeMasterViewModel> BindFeeMasterViewModel(FeeMasterViewModel model = null);
        Task<Response<List<FeeMasterViewModel>>> GetAllFeeMasterAsync();
        Task<Response<List<FeeGroupViewModel>>> GetAllFeeGroupMasterAsync();
        Task<Response<FeeMasterViewModel>> GetFeeMasterByIdAsync(int feeMasterId);
        Task<Response<FeeMasterViewModel>> AddOrEditFeeMasterAsync(FeeMasterViewModel model);
        Task<Response<bool>> deleteFeeMasterAsync(int feeMasterId);
        #endregion

        #region Fee Assign
        Task<Response<AssignFeesViewModel>> BindAssignViewModelAsync(int feeGroupId, AssignFeesViewModel model = null);
        Task<Response<AssignFeesViewModel>> SearchStudentAndAddToAssignViewModelAsync(AssignFeesViewModel data);
        Task<Response<bool>> AssignFeesToStudents(AssignFeesViewModel data, int feeGroupId);
        #endregion

        #region Fee Summary 
        Task<Response<List<AllFeeSummaryViewModel>>> GetAllFeeSummaryAsync();
        Task<Response<List<ClassFeeSummaryViewModel>>> GetClassFeeSummaryAsync(int id);
        #endregion

        #region Fee Collection
        Task<Response<FeesCollerctionReportViewModel>> GetFeeCollectionReport(FeeCollectionSearchModel searchModel = null);
        #endregion

        #region Fee Transaction Report
        Task<Response<List<FeesTransactionReportViewModel>>> GetFeeTransactionReport();
        #endregion

        #region Pending Fess
        Task<Response<FeePendingViewModel>> GetPendingFeesAsync(StudentSearchViewModel searchModel = null);
        #endregion
    }
}
