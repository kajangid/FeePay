using AutoMapper;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Core.Application.Interface.Service.Student;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities;
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
            var feetypelist = await _unitOfWork.FeeType.GetAllAsync(dbId: SchoolId, isActive: true);
            var feegrouplist = await _unitOfWork.FeeGroup.GetAllAsync(dbId: SchoolId, isActive: true);
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
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<List<FeeMasterViewModel>>("Select Academic Session.");
            var tt = await _unitOfWork.FeeMaster.GetAll_WithAddEditUserAsync(dbId: SchoolId, academicSessionId: currentSession.Id);
            return new Response<List<FeeMasterViewModel>>(_mapper.Map<List<FeeMasterViewModel>>(tt.ToList()));
        }
        public async Task<Response<List<FeeGroupViewModel>>> GetAllFeeGroupMasterAsync()
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<List<FeeGroupViewModel>>("Select Academic Session.");
            var list = await _unitOfWork.FeeGroup.GetAllWithMasterAandTypeAsync(dbId: SchoolId, academicSessionId: currentSession.Id);
            List<FeeGroupViewModel> tt = _mapper.Map<List<FeeGroupViewModel>>(list.ToList());
            return new Response<List<FeeGroupViewModel>>(tt.ToList());
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
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<FeeMasterViewModel>("Select Academic Session.");
            FeeMaster feeMaster = _mapper.Map<FeeMaster>(model);
            if (model.Id == 0)
            {
                feeMaster.AddedBy = UserId;
                feeMaster.IsActive = true;
                feeMaster.AcademicSessionId = currentSession.Id;
                int res = await _unitOfWork.FeeMaster.AddAsync(feeMaster, SchoolId);
                if (res <= 0) return new Response<FeeMasterViewModel>("Fee Master is already exist or a error is accord while creating Fee master.");
            }
            else
            {
                var feeMasterEntity = await _unitOfWork.FeeMaster.FindByIdAsync(model.Id, SchoolId);
                if (feeMasterEntity == null) return new Response<FeeMasterViewModel>($"Could not find data for id={model.Id}");
                feeMasterEntity.FeeGroupId = feeMaster.FeeGroupId;
                feeMasterEntity.FeeTypeId = feeMaster.FeeTypeId;
                feeMasterEntity.IsActive = true;
                feeMasterEntity.DueDate = feeMaster.DueDate;
                feeMasterEntity.Amount = feeMaster.Amount;
                feeMasterEntity.Description = feeMaster.Description;
                feeMasterEntity.ModifyBy = UserId;
                feeMasterEntity.AcademicSessionId = currentSession.Id;
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
        public async Task<Response<AssignFeesViewModel>> BindAssignViewModelAsync(int feeGroupId, AssignFeesViewModel model = null)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<AssignFeesViewModel>("Select Academic Session.");
            var feeGroupWithFeeMasterAndFeeType = await _unitOfWork.FeeGroup.GetAllWithMasterAandTypeAsync(dbId: SchoolId, academicSessionId: currentSession.Id);
            List<FeeGroupViewModel> tt = _mapper.Map<List<FeeGroupViewModel>>(feeGroupWithFeeMasterAndFeeType.ToList());
            if (tt == null) return new Response<AssignFeesViewModel>("No data found.");
            var _classes = (await _unitOfWork
                .ClassRepo
                .GetAllAsync(dbId: SchoolId, isActive: true))
                .Select(s => new DropDownItem { Value = s.Id.ToString(), Text = s.NormalizedName }).ToList();
            if (model == null)
            {
                model = new AssignFeesViewModel()
                {
                    FeeGroup = tt?.SingleOrDefault(w => w.Id == feeGroupId),
                    Classes = _classes ?? new List<DropDownItem>()
                };
            }
            else
            {
                model.FeeGroup = tt?.SingleOrDefault(w => w.Id == feeGroupId);
                model.Classes = _classes ?? new List<DropDownItem>();
            }
            return new Response<AssignFeesViewModel>(model);
        }
        public async Task<Response<AssignFeesViewModel>> SearchStudentAndAddToAssignViewModelAsync(AssignFeesViewModel data)
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<AssignFeesViewModel>("Select Academic Session.");

            var students = await _unitOfWork.StudentAdmision.SearchStudentAsync(
                dbId: schoolId,
                academicSessionId: currentSession.Id,
                classId: data.ClassId,
                sectionId: data.SectionId,
                seatchString: data.Search,
                gender: data.Gender,
                isActive: true);

            data.StudentAdmissionList = students != null && students.Count() > 0 ?
                _mapper.Map<List<StudentAdmissionViewModel>>(students) : new List<StudentAdmissionViewModel>();

            var studentsInFeeGroup = (await _unitOfWork.StudentFee.GetStudentsInFeesGroupAsync(
                feeGroupId: data.FeeGroup.Id, academicSessionId: currentSession.Id, dbId: schoolId))?.ToList();
            var CheckBoxStudentList = data.StudentAdmissionList.Select(s => new CheckBoxItem
            {
                Id = s.Id,
                Name = s.FirstName,
                IsSelected = (studentsInFeeGroup?.ToList().Any(a => a.Id == s.Id) ?? false)
            }).ToList();
            data.CbStudents = CheckBoxStudentList;
            return new Response<AssignFeesViewModel>(data);
        }
        public async Task<Response<bool>> AssignFeesToStudents(AssignFeesViewModel data, int feeGroupId)
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            //var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<bool>("Select Academic Session.");
            List<StudentFees> studentFees = new List<StudentFees>();
            if (data.CbStudents.Count > 0)
            {
                foreach (var CbStudent in data.CbStudents)
                {
                    var FeeMasterList = await _unitOfWork.FeeMaster.FindByFeeGroupIdAsync(id: feeGroupId, dbId: schoolId, academicSessionId: currentSession.Id, isActive: true);
                    foreach (var feeMaster in FeeMasterList)
                    {
                        bool NotAssignThisFee = await _unitOfWork.StudentFee.IsFeeAssignToStudentAsync(
                            studentAdmissionId: CbStudent.Id, feeMasterId: feeMaster.Id, dbId: schoolId);
                        if (CbStudent.IsSelected && !NotAssignThisFee)
                        {
                            studentFees.Add(new StudentFees()
                            {
                                StudentAdmissionId = CbStudent.Id,
                                FeeMasterId = feeMaster.Id,
                                FeeGroupId = feeGroupId,
                                AcademicSessionId = ((await _unitOfWork.StudentAcademicSessions
                                .FindByStudentAdmissionIdAndSessionIdAsync(CbStudent.Id, currentSession.Id, schoolId))?.Id) ?? 0
                            });
                        }
                    }
                }
            }
            if (studentFees.Count > 0)
            {
                var insertedId = await _unitOfWork.StudentFee.BulkAddAsync(studentFees: studentFees, dbId: schoolId);
                if (insertedId <= 0) return new Response<bool>("error saving data");
            }
            return new Response<bool>(true, "data save successfully. ");
        }
        #endregion


        #region Fee Summary Report
        public async Task<Response<List<AllFeeSummaryViewModel>>> GetAllFeeSummaryAsync()
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var userId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<List<AllFeeSummaryViewModel>>("Select Academic Session.");
            var classesWithFeesAmount = await _unitOfWork.StudentFee.GetClasses_FeesAsync(
                dbId: schoolId, academicSessionId: currentSession.Id);
            if (classesWithFeesAmount == null) return new Response<List<AllFeeSummaryViewModel>>("No Data found.");
            var classes = await _unitOfWork.ClassRepo.GetAllAsync(dbId: schoolId, isActive: true);
            List<AllFeeSummaryViewModel> model = classes.Select(s =>
                new AllFeeSummaryViewModel
                {
                    ClassId = s.Id,
                    ClassName = s.Name,
                    TotalFees = classesWithFeesAmount.Where(w => w.ClassId == s.Id).Sum(s => s.Amount),
                    TotalPaid = classesWithFeesAmount.Where(w => w.ClassId == s.Id && w.IsPaid == true).Sum(s => s.Amount),
                    TotalBalance = (classesWithFeesAmount.Where(w => w.ClassId == s.Id).Sum(s => s.Amount) -
                    classesWithFeesAmount.Where(w => w.ClassId == s.Id && w.IsPaid == true).Sum(s => s.Amount)),
                }).ToList();
            return new Response<List<AllFeeSummaryViewModel>>(model);
        }
        public async Task<Response<List<ClassFeeSummaryViewModel>>> GetClassFeeSummaryAsync(int id)
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var userId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<List<ClassFeeSummaryViewModel>>("Select Academic Session.");

            var classStudentsWithFeesAmount = await _unitOfWork.StudentFee.GetClassStudents_FeesAsync(
                dbId: schoolId, academicSessionId: currentSession.Id, classId: id);
            if (classStudentsWithFeesAmount == null) return new Response<List<ClassFeeSummaryViewModel>>("No Data found.");


            var studentsInClass = await _unitOfWork.StudentAdmision.SearchStudentAsync(
                dbId: schoolId,
                academicSessionId: currentSession.Id,
                classId: id);
            List<ClassFeeSummaryViewModel> model = studentsInClass.Select(s =>
                new ClassFeeSummaryViewModel
                {
                    StudentId = s.Id,
                    StudentName = s.FirstName + " " + s.LastName,
                    ClassSectionName = $"{s.StudentClass?.Name}({s.StudentSection?.Name})",
                    TotalFees = classStudentsWithFeesAmount.Where(w => w.StudentAdmissionId == s.Id).Sum(s => s.Amount),
                    TotalPaid = classStudentsWithFeesAmount.Where(w => w.StudentAdmissionId == s.Id && w.IsPaid == true).Sum(s => s.Amount),
                    TotalBalance = (classStudentsWithFeesAmount.Where(w => w.StudentAdmissionId == s.Id).Sum(s => s.Amount) -
                    classStudentsWithFeesAmount.Where(w => w.StudentAdmissionId == s.Id && w.IsPaid == true).Sum(s => s.Amount)),
                }).ToList();
            return new Response<List<ClassFeeSummaryViewModel>>(model);
        }
        #endregion


        #region Fee Collection Report
        public async Task<Response<FeesCollerctionReportViewModel>> GetFeeCollectionReport(FeeCollectionSearchModel searchModel = null)
        {
            if (searchModel == null) { searchModel = new FeeCollectionSearchModel() { FromDate = DateTime.Now, ToDate = DateTime.Now }; }
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var _classes = await _unitOfWork.ClassRepo.GetAllAsync(dbId: schoolId, isActive: true);
            var section = await _unitOfWork.SectionRepo.GetAllAsync(dbId: schoolId, isActive: true);
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<FeesCollerctionReportViewModel>("Select Academic Session.");
            var studentFees = await _unitOfWork.StudentFee.GetAllAsync(
                dbId: schoolId,
                academicSessionId: currentSession.Id,
                isPaid: true,
                fromDate: searchModel.FromDate,
                toDate: searchModel.ToDate);
            if (studentFees == null) return new Response<FeesCollerctionReportViewModel>("NO Data Found.");

            List<StudentFeesViewModel> studentfeemodal = _mapper.Map<List<StudentFeesViewModel>>(studentFees.ToList());

            studentfeemodal.ForEach(f =>
            {
                if (f.StudentAdmission != null)
                {
                    f.StudentAdmission.StudentClass = _classes?.Where(w => w.Id == f.StudentAdmission.ClassId).SingleOrDefault();
                    f.StudentAdmission.StudentSection = section?.Where(w => w.Id == f.StudentAdmission.SectionId).SingleOrDefault();
                }
            });
            FeesCollerctionReportViewModel model = new FeesCollerctionReportViewModel()
            {
                StudentFees = studentfeemodal,
                SearchModel = searchModel
            };
            return new Response<FeesCollerctionReportViewModel>(model);
        }
        #endregion


        #region Fee Transaction Report
        public async Task<Response<List<FeesTransactionReportViewModel>>> GetFeeTransactionReport()
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var _classes = await _unitOfWork.ClassRepo.GetAllAsync(dbId: schoolId, isActive: true);
            var section = await _unitOfWork.SectionRepo.GetAllAsync(dbId: schoolId, isActive: true);
            var transcations = await _unitOfWork.FeesTranscation.GetAll_WithStudentAdmissionAsync(schoolId,
                isComplated: true);
            if (transcations == null) return new Response<List<FeesTransactionReportViewModel>>("NO Data Found.");
            List<FeesTransactionReportViewModel> model = transcations.Select(s => new FeesTransactionReportViewModel
            {
                ClassName = _classes?.Where(w => w.Id == s.StudentAdmission?.ClassId).SingleOrDefault()?.Name,
                SectionName = section?.Where(w => w.Id == s.StudentAdmission?.SectionId).SingleOrDefault()?.Name,
                Deposit = s.Amount,
                FeeTransactionId = s.TransactionId,
                FatherName = s.StudentAdmission?.FatherName,
                MobileNo = s.StudentAdmission?.MobileNo,
                Sr_RegNo = s.StudentAdmission?.Sr_RegNo,
                StudentName = $"{s.StudentAdmission?.FirstName} {s.StudentAdmission?.LastName}",
                StudentAdmissionId = s.StudentAdmission?.Id ?? 0,

            }).ToList();
            return new Response<List<FeesTransactionReportViewModel>>(model);
        }
        #endregion


        #region Pending Fess
        public async Task<Response<FeePendingViewModel>> GetPendingFeesAsync(StudentSearchViewModel searchModel = null)
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var _classes = await _unitOfWork.ClassRepo.GetAllAsync(dbId: schoolId, isActive: true);
            var section = await _unitOfWork.SectionRepo.GetAllAsync(dbId: schoolId, isActive: true);
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<FeePendingViewModel>("Select Academic Session.");
            FeePendingViewModel model = new FeePendingViewModel();
            if (searchModel == null)
            {
                searchModel = new StudentSearchViewModel()
                {
                    Classes = _classes.Select(s => new DropDownItem { Text = s.Name, Value = s.Id.ToString() }).ToList(),
                };
            }
            else
            {
                searchModel.Classes = _classes.Select(s => new DropDownItem { Text = s.Name, Value = s.Id.ToString() }).ToList();

                var studentFees = await _unitOfWork.StudentFee.GetAllAsync(
                    dbId: schoolId,
                    academicSessionId: currentSession.Id,
                    classId: searchModel.ClassId,
                    sectionId: searchModel.SectionId,
                    studentSearchString: searchModel.Search,
                    isPaid: false);
                if (studentFees == null) return new Response<FeePendingViewModel>(model);

                var dueFees = studentFees.Where(w => w.DueDate < DateTime.Now).ToList();
                dueFees.ForEach(f =>
                {
                    if (f.StudentAdmission != null)
                    {
                        f.StudentAdmission.StudentClass = _classes?.Where(w => w.Id == f.StudentAdmission.ClassId).SingleOrDefault();
                        f.StudentAdmission.StudentSection = section?.Where(w => w.Id == f.StudentAdmission.SectionId).SingleOrDefault();
                    }
                });

                List<StudentFeesViewModel> StudentFeeModel = _mapper.Map<List<StudentFeesViewModel>>(dueFees);
                model.StudentFeesList = StudentFeeModel;
                model.CbStudents = StudentFeeModel.Select(s => new CheckBoxItem { Id = s.StudentAdmissionId }).ToList();
            }
            model.SearchModel = searchModel;
            return new Response<FeePendingViewModel>(model);
        }
        #endregion
    }
}
