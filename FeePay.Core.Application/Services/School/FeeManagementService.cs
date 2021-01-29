using AutoMapper;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Core.Application.Interface.Service.Student;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Domain.Entities.School;
using FeePay.Core.Domain.Entities.Student;
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
                var feeTypeEntity = await _unitOfWork.FeeType.FindByIdAsync(model.Id, SchoolId);
                if (feeTypeEntity == null) return new Response<FeeTypeViewModel>($"Could not find data for id={model.Id}");
                feeTypeEntity.Name = feeType.Name?.Trim();
                feeTypeEntity.NormalizedName = feeType.Name?.ToUpper().Trim();
                feeTypeEntity.IsActive = feeType.IsActive;
                feeTypeEntity.Code = feeType.Code;
                feeTypeEntity.Description = feeType.Description;
                feeTypeEntity.ModifyBy = UserId;
                int res = await _unitOfWork.FeeType.UpdateAsync(feeTypeEntity, SchoolId);
                if (res <= 0) return new Response<FeeTypeViewModel>("Fee Type is already exist with same name or a error is accord while updating fee type.");
            }
            return new Response<FeeTypeViewModel>(model);
        }
        public Task<Response<bool>> deleteFeeTypeAsync(int feeTypeId)
        {
            //var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
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
                var feeGroupEntity = await _unitOfWork.FeeGroup.FindByIdAsync(model.Id, SchoolId);
                if (feeGroupEntity == null) return new Response<FeeGroupViewModel>($"Could not find data for id={model.Id}");
                feeGroupEntity.Name = feeGroup.Name?.Trim();
                feeGroupEntity.NormalizedName = feeGroup.Name?.ToUpper().Trim();
                feeGroupEntity.IsActive = feeGroup.IsActive;
                feeGroupEntity.Description = feeGroup.Description;
                feeGroupEntity.ModifyBy = UserId;
                int res = await _unitOfWork.FeeGroup.UpdateAsync(feeGroupEntity, SchoolId);
                if (res <= 0) return new Response<FeeGroupViewModel>("Fee Type is already exist with same name or a error is accord while updating fee type.");
            }
            return new Response<FeeGroupViewModel>(model);
        }
        public Task<Response<bool>> deleteFeeGroupAsync(int feeGroupId)
        {
            //var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
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
        public async Task<Response<List<FeeGroupViewModel>>> GetAllFeeGroupMasterAsync()
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var list = await _unitOfWork.FeeGroup.GetAllWithMasterAandTypeAsync(SchoolId);
            List<FeeGroupViewModel> tt = _mapper.Map<List<FeeGroupViewModel>>(list.ToList());
            return new Response<List<FeeGroupViewModel>>(_mapper.Map<List<FeeGroupViewModel>>(tt.ToList()));
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
                var feeMasterEntity = await _unitOfWork.FeeMaster.FindByIdAsync(model.Id, SchoolId);
                if (feeMasterEntity == null) return new Response<FeeMasterViewModel>($"Could not find data for id={model.Id}");
                feeMasterEntity.FeeGroupId = feeMaster.FeeGroupId;
                feeMasterEntity.FeeTypeId = feeMaster.FeeTypeId;
                feeMasterEntity.IsActive = feeMaster.IsActive;
                feeMasterEntity.DueDate = feeMaster.DueDate;
                feeMasterEntity.Amount = feeMaster.Amount;
                feeMasterEntity.Description = feeMaster.Description;
                feeMasterEntity.ModifyBy = UserId;
                int res = await _unitOfWork.FeeMaster.UpdateAsync(feeMasterEntity, SchoolId);
                if (res <= 0) return new Response<FeeMasterViewModel>("Fee Master is already exist with same name or a error is accord while updating fee master.");
            }
            return new Response<FeeMasterViewModel>(model);
        }
        public Task<Response<bool>> deleteFeeMasterAsync(int feeMasterId)
        {
            //var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
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


        #region Fee Assign

        public async Task<AssignFeesViewModel> SearchStudentAndBindAssignViewModel(AssignFeesViewModel data, int id)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var ress = await GetAllFeeGroupMasterAsync();
            data.FeeGroup = ress.Data?.SingleOrDefault(w => w.Id == id);

            var students = await _unitOfWork.StudentAdmision.SearchStudentAsync(classId: data.ClassId,
                sectionId: data.SectionId,
                seatchString: data.Search,
                gender: data.Gender,
                isActive: true,
                dbId: SchoolId);

            data.StudentAdmissionList = students != null && students.Count() > 0 ?
                _mapper.Map<List<StudentAdmissionViewModel>>(students) : new List<StudentAdmissionViewModel>();

            var studentsInFeeGroup = (await _unitOfWork.StudentFee.GetStudentsInFeesGroupAsync(id, dbId: SchoolId))?.ToList();
            var CheckBoxStudentList = data.StudentAdmissionList.Select(s => new CheckBoxItem
            {
                Id = s.Id,
                Name = s.FirstName,
                IsSelected = (studentsInFeeGroup?.ToList().Any(a => a.Id == s.Id) ?? false)
            }).ToList();
            data.CbStudents = CheckBoxStudentList;
            return data;
        }
        public async Task<Response<bool>> AssignFeesToStudents(AssignFeesViewModel data, int feeGroupId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            //var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            List<StudentFees> studentFees = new List<StudentFees>();
            if (data.CbStudents.Count > 0)
            {
                foreach (var CbStudent in data.CbStudents)
                {
                    var FeeMasterList = await _unitOfWork.FeeMaster.GetByFeeGroupIdAsync(feeGroupId, SchoolId);
                    foreach (var feeMaster in FeeMasterList)
                    {
                        bool NotAssignThisFee = await _unitOfWork.StudentFee.IsFeeAssignToStudentAsync(CbStudent.Id, feeMaster.Id, SchoolId);
                        if (CbStudent.IsSelected && !NotAssignThisFee)
                        {
                            studentFees.Add(new StudentFees()
                            {
                                StudentId = CbStudent.Id,
                                FeeMasterId = feeMaster.Id,
                                FeeGroupId = feeGroupId,
                            });
                        }
                    }
                }
            }
            if (studentFees.Count > 0)
            {
                var insertedId = await _unitOfWork.StudentFee.BulkAddAsync(studentFees: studentFees, dbId: SchoolId);
                if (insertedId <= 0) return new Response<bool>("error saving data");
            }
            return new Response<bool>(true, "data save successfully. ");
        }
        #endregion
    }
}
