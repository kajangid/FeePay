using AutoMapper;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Domain.Entities.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Services.School
{
    public class FeeManagementService : IFeeManagementService
    {
        public FeeManagementService(IUnitOfWork unitOfWork, IMapper mapper, IAppContextAccessor appContextAccessor,
            ILoginService loginService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appContextAccessor = appContextAccessor;
            _loginService = loginService;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly ILoginService _loginService;



        #region Fee Type 
        public async Task<Response<List<FeeTypeViewModel>>> GetAllFeeTypeAsync()
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var tt = await _unitOfWork.FeeType.GetAll_WithAddEditUserAsync(SchoolId);
            return new Response<List<FeeTypeViewModel>>(_mapper.Map<List<FeeTypeViewModel>>(tt.ToList()));
        }
        public async Task<Response<FeeTypeViewModel>> GetFeeTypeByIdAsync(int feeTypeId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            FeeType feeType = await _unitOfWork.FeeType.FindByIdAsync(feeTypeId, SchoolId);
            FeeTypeViewModel feeTypeViewModel = _mapper.Map<FeeTypeViewModel>(feeType);
            return new Response<FeeTypeViewModel>(feeTypeViewModel);
        }
        public async Task<Response<FeeTypeViewModel>> AddOrEditFeeTypeAsync(FeeTypeViewModel model)
        {
            var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            FeeType feeType = _mapper.Map<FeeType>(model);
            feeType.NormalizedName = feeType.Name.Trim().ToUpper();
            if (model.Id == 0)
            {
                feeType.AddedBy = UserId;
                int res = await _unitOfWork.FeeType.AddAsync(feeType, SchoolId);
                if (res <= 0) return new Response<FeeTypeViewModel>("Fee Type is already exist or a error is accord while creating Fee type.");
            }
            else
            {
                feeType.ModifyBy = UserId;
                int res = await _unitOfWork.FeeType.UpdateAsync(feeType, SchoolId);
                if (res <= 0) return new Response<FeeTypeViewModel>("Fee Type is already exist with same name or a error is accord while updating fee type.");
            }
            return new Response<FeeTypeViewModel>(model);
        }
        public Task<Response<bool>> deleteFeeTypeAsync(int feeTypeId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            //var roles = await _unitOfWork.SchoolAdminUserRole.GetUserRolesAsync(userId, SchoolId);
            //if (roles != null && roles.Count > 0)
            //{
            //    foreach (var role in roles) await _unitOfWork.SchoolAdminUserRole.delete(userId, role.Id, SchoolId);
            //}
            //int res = await _unitOfWork.SchoolAdminUser.DeleteUserAsync(userId, SchoolId);
            //if (res > 0) return new Response<bool>(true);
            return Task.FromResult(new Response<bool>("Error deleting school fee type"));
        }
        #endregion


        #region Fee Group
        public async Task<Response<List<FeeGroupViewModel>>> GetAllFeeGroupAsync()
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var tt = await _unitOfWork.FeeGroup.GetAll_WithAddEditUserAsync(SchoolId);
            return new Response<List<FeeGroupViewModel>>(_mapper.Map<List<FeeGroupViewModel>>(tt.ToList()));
        }
        public async Task<Response<FeeGroupViewModel>> GetFeeGroupByIdAsync(int feeGroupId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            FeeGroup feeGroup = await _unitOfWork.FeeGroup.FindByIdAsync(feeGroupId, SchoolId);
            FeeGroupViewModel feeGroupViewModel = _mapper.Map<FeeGroupViewModel>(feeGroup);
            return new Response<FeeGroupViewModel>(feeGroupViewModel);
        }
        public async Task<Response<FeeGroupViewModel>> AddOrEditFeeGroupAsync(FeeGroupViewModel model)
        {
            var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            FeeGroup feeGroup = _mapper.Map<FeeGroup>(model);
            feeGroup.NormalizedName = feeGroup.Name.Trim().ToUpper();
            if (model.Id == 0)
            {
                feeGroup.AddedBy = UserId;
                int res = await _unitOfWork.FeeGroup.AddAsync(feeGroup, SchoolId);
                if (res <= 0) return new Response<FeeGroupViewModel>("Fee Type is already exist or a error is accord while creating Fee type.");
            }
            else
            {
                feeGroup.ModifyBy = UserId;
                int res = await _unitOfWork.FeeGroup.UpdateAsync(feeGroup, SchoolId);
                if (res <= 0) return new Response<FeeGroupViewModel>("Fee Type is already exist with same name or a error is accord while updating fee type.");
            }
            return new Response<FeeGroupViewModel>(model);
        }
        public Task<Response<bool>> deleteFeeGroupAsync(int feeGroupId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            //var roles = await _unitOfWork.SchoolAdminUserRole.GetUserRolesAsync(userId, SchoolId);
            //if (roles != null && roles.Count > 0)
            //{
            //    foreach (var role in roles) await _unitOfWork.SchoolAdminUserRole.delete(userId, role.Id, SchoolId);
            //}
            //int res = await _unitOfWork.SchoolAdminUser.DeleteUserAsync(userId, SchoolId);
            //if (res > 0) return new Response<bool>(true);

            return Task.FromResult(new Response<bool>("Error deleting school fee group"));
        }
        #endregion


        #region Fee Manager
        public async Task<FeeMasterViewModel> BindFeeMasterViewModel(FeeMasterViewModel model = null)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var feetypelist = await _unitOfWork.FeeType.GetAllActiveAsync(SchoolId);
            var feegrouplist = await _unitOfWork.FeeGroup.GetAllActiveAsync(SchoolId);
            List<DropDownItem> feetypeDDL = feetypelist?.Select(s => new DropDownItem { Value = s.Id.ToString(), Text = $"{s.Name} | {s.Code}" }).ToList();
            List<DropDownItem> feegroupDDL = feegrouplist?.Select(s => new DropDownItem { Value = s.Id.ToString(), Text = s.Name }).ToList();
            if (model != null)
            {
                feetypeDDL.ForEach(f => { if (f.Value == model.FeeTypeId.ToString()) f.IsSelected = true; });
                model.FeeTypeList = feetypeDDL;
                feegroupDDL.ForEach(f => { if (f.Value == model.FeeGroupId.ToString()) f.IsSelected = true; });
                model.FeeGroupList = feegroupDDL;
            }
            else
            {
                model = new FeeMasterViewModel()
                {
                    FeeTypeList = feetypeDDL,
                    FeeGroupList = feegroupDDL
                };
            }
            return model;
        }
        public async Task<Response<List<FeeMasterViewModel>>> GetAllFeeMasterAsync()
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var tt = await _unitOfWork.FeeMaster.GetAll_WithAddEditUserAsync(SchoolId);
            return new Response<List<FeeMasterViewModel>>(_mapper.Map<List<FeeMasterViewModel>>(tt.ToList()));
        }
        public async Task<Response<FeeMasterViewModel>> GetFeeMasterByIdAsync(int feeMasterId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            FeeMaster feeMaster = await _unitOfWork.FeeMaster.FindByIdAsync(feeMasterId, SchoolId);
            FeeMasterViewModel feeMasterViewModel = _mapper.Map<FeeMasterViewModel>(feeMaster);
            return new Response<FeeMasterViewModel>(await BindFeeMasterViewModel(feeMasterViewModel));
        }
        public async Task<Response<FeeMasterViewModel>> AddOrEditFeeMasterAsync(FeeMasterViewModel model)
        {
            var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            FeeMaster feeMaster = _mapper.Map<FeeMaster>(model);
            if (model.Id == 0)
            {
                feeMaster.AddedBy = UserId;
                int res = await _unitOfWork.FeeMaster.AddAsync(feeMaster, SchoolId);
                if (res <= 0) return new Response<FeeMasterViewModel>("Fee Master is already exist or a error is accord while creating Fee master.");
            }
            else
            {
                feeMaster.ModifyBy = UserId;
                int res = await _unitOfWork.FeeMaster.UpdateAsync(feeMaster, SchoolId);
                if (res <= 0) return new Response<FeeMasterViewModel>("Fee Master is already exist with same name or a error is accord while updating fee master.");
            }
            return new Response<FeeMasterViewModel>(model);
        }
        public Task<Response<bool>> deleteFeeMasterAsync(int feeMasterId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            //var roles = await _unitOfWork.SchoolAdminUserRole.GetUserRolesAsync(userId, SchoolId);
            //if (roles != null && roles.Count > 0)
            //{
            //    foreach (var role in roles) await _unitOfWork.SchoolAdminUserRole.delete(userId, role.Id, SchoolId);
            //}
            //int res = await _unitOfWork.SchoolAdminUser.DeleteUserAsync(userId, SchoolId);
            //if (res > 0) return new Response<bool>(true);

            return Task.FromResult(new Response<bool>("Error deleting school fee master"));
        }

        #endregion
    }
}
