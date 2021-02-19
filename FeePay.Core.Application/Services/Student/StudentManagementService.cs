using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.Student;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Domain.Entities.School;
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Core.Application.Services.Student
{
    public class StudentManagementService : IStudentManagementService
    {
        public StudentManagementService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAppContextAccessor appContextAccessor,
            ILoginService loginService,
            IStudentRegistrationService studentRegistrationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appContextAccessor = appContextAccessor;
            _loginService = loginService;
            _studentRegistrationService = studentRegistrationService;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly ILoginService _loginService;
        private readonly IStudentRegistrationService _studentRegistrationService;


        #region STUDENT_ADMISSION CURD
        public async Task<StudentAdmissionViewModel> BindStudentAdmissionViewModelAsync(StudentAdmissionViewModel model = null)
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var ClassesDropDownItemList = (await _unitOfWork.ClassRepo.GetAllAsync(dbId: schoolId, isActive: true))
                .Select(s => new DropDownItem { Value = s.Id.ToString(), Text = s.NormalizedName }).ToList();
            var StatesDropDownItemList = (await _unitOfWork.CityState.GetAllActiveStatesAsync(schoolId))
                .Select(s => new DropDownItem { Value = s.Id.ToString(), Text = s.Name }).ToList();
            if (model != null)
            {
                model.AvaliableClasses = ClassesDropDownItemList;
                model.StatesDDL = StatesDropDownItemList;
            }
            else
            {
                model = new StudentAdmissionViewModel()
                {
                    AvaliableClasses = ClassesDropDownItemList,
                    StatesDDL = StatesDropDownItemList
                };
            }
            return model;
        }
        public async Task<Response<StudentSearchViewModel>> SearchStudentAsync(StudentSearchViewModel searchModel = null)
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<StudentSearchViewModel>("Select Academic Session.");
            var _classes = await _unitOfWork.ClassRepo.GetAllAsync(dbId: schoolId, isActive: true);
            if (searchModel == null)
            {
                searchModel = new StudentSearchViewModel()
                {
                    Classes = _classes.Select(s => new DropDownItem { Text = s.Name, Value = s.Id.ToString() }).ToList()
                };
                var students = await _unitOfWork.StudentAdmision.GetAll_WithAddEditUserAsync(schoolId, currentSession.Id);
                searchModel.Students = _mapper.Map<List<StudentAdmissionViewModel>>(students);
                return new Response<StudentSearchViewModel>(searchModel);
            }
            else
            {
                searchModel.Classes = _classes.Select(s => new DropDownItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                var students = await _unitOfWork.StudentAdmision.SearchStudent_WithAddEditUserAsync(
                    dbId: schoolId,
                    academicSessionId: currentSession.Id,
                    classId: searchModel.ClassId,
                    sectionId: searchModel.SectionId,
                    seatchString: searchModel.Search);
                searchModel.Students = _mapper.Map<List<StudentAdmissionViewModel>>(students);
                return new Response<StudentSearchViewModel>(searchModel);
            }
        }
        public async Task<Response<List<StudentAdmissionViewModel>>> GetListOfStudentsAsync()
        {
            string schoolKey = _appContextAccessor.ClaimSchoolUniqueId();
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<List<StudentAdmissionViewModel>>("Select Academic Session.");
            var students = await _unitOfWork.StudentAdmision.GetAll_WithAddEditUserAsync(schoolKey, currentSession.Id);
            var model = _mapper.Map<List<StudentAdmissionViewModel>>(students);
            return new Response<List<StudentAdmissionViewModel>>(model);
        }
        public async Task<Response<StudentAdmissionViewModel>> FindStudentByIdAsync(int Id)
        {
            string schoolKey = _appContextAccessor.ClaimSchoolUniqueId();
            var student = await _unitOfWork.StudentAdmision.FindByIdAsync(Id, schoolKey);
            var model = _mapper.Map<StudentAdmissionViewModel>(student);
            model = await BindStudentAdmissionViewModelAsync(model);
            return new Response<StudentAdmissionViewModel>(model);
        }
        public async Task<Response<bool>> AddOrEditStudentAsync(StudentAdmissionViewModel model)
        {
            var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            StudentAdmission studentAdmission = _mapper.Map<StudentAdmission>(model);
            studentAdmission.DateOfBirth = null;
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<bool>("Select Academic Session.");
            bool res;
            if (model.Id == 0)
            {
                studentAdmission.AddedBy = UserId;
                studentAdmission.AcademicSessionId = currentSession?.Id ?? 0;
                res = await _studentRegistrationService.AddStudentAsync(studentAdmission);
                if (res)
                {
                    StudentAdmission modifyedStudent = await _unitOfWork.StudentAdmision
                        .FindBySr_RegNoAsync(studentAdmission.Sr_RegNo, SchoolId);
                    StudentAcademicSession studentAS = new StudentAcademicSession()
                    {
                        AddedBy = UserId,
                        StudentAdmissionId = modifyedStudent.Id,
                        SessionId = currentSession.Id,
                        ClassId = modifyedStudent.ClassId,
                        SectionId = modifyedStudent.SectionId
                    };
                    var studentASResult = await _unitOfWork.StudentAcademicSessions.AddAsync(studentAS, SchoolId);
                    if (studentASResult <= 0) return new Response<bool>("Error Adding Student Academic Session.");
                }
            }
            else
            {
                var studentAdmissionEntity = await _unitOfWork.StudentAdmision.FindByIdAsync(model.Id, SchoolId);
                if (studentAdmissionEntity == null) return new Response<bool>($"Could not find data for id={model.Id}");
                studentAdmissionEntity.AdmissionDate = studentAdmission.AdmissionDate;
                studentAdmissionEntity.FirstName = studentAdmission.FirstName;
                studentAdmissionEntity.LastName = studentAdmission.LastName;
                studentAdmissionEntity.FatherName = studentAdmission.FatherName;
                studentAdmissionEntity.MotherName = studentAdmission.MotherName;
                studentAdmissionEntity.Gender = studentAdmission.Gender;
                studentAdmissionEntity.MobileNo = studentAdmission.MobileNo;
                studentAdmissionEntity.Address = studentAdmission.Address;
                studentAdmissionEntity.FormNo = studentAdmission.FormNo;
                studentAdmissionEntity.Sr_RegNo = studentAdmission.Sr_RegNo;
                studentAdmissionEntity.AcademicSessionId = currentSession.Id;
                studentAdmissionEntity.ClassId = studentAdmission.ClassId;
                studentAdmissionEntity.SectionId = studentAdmission.SectionId;
                studentAdmissionEntity.IsActive = studentAdmission.IsActive;
                studentAdmission.ModifyBy = UserId;
                var updatedId = await _unitOfWork.StudentAdmision.UpdateAsync(studentAdmissionEntity, SchoolId);
                res = (updatedId > 0);
                // TODO: Check this logic
                if (res)
                {
                    var studentASList = await _unitOfWork.StudentAcademicSessions.FindByStudentAdmissionIdAndSessionIdAsync(studentAdmissionEntity.Id, currentSession.Id, SchoolId);
                    if (studentASList == null)
                    {
                        StudentAcademicSession studentAS = new StudentAcademicSession()
                        {
                            AddedBy = UserId,
                            StudentAdmissionId = studentAdmissionEntity.Id,
                            SessionId = currentSession.Id,
                            ClassId = studentAdmissionEntity.ClassId,
                            SectionId = studentAdmissionEntity.SectionId
                        };
                        var studentASResult = await _unitOfWork.StudentAcademicSessions.AddAsync(studentAS, SchoolId);
                        if (studentASResult <= 0) return new Response<bool>("Error Adding Student Academic Session On Student Update.");
                    }
                    else
                    {
                        studentASList.ClassId = studentAdmissionEntity.ClassId;
                        studentASList.SectionId = studentAdmissionEntity.SectionId;
                        studentASList.ModifyBy = UserId;
                        var studentASResult = await _unitOfWork.StudentAcademicSessions.UpdateAsync(studentASList, SchoolId);
                        if (studentASResult <= 0) return new Response<bool>("Error Updating Student Academic Session On Student Update.");
                    }
                }
            }
            if (!res) return new Response<bool>("student is already present with same formNo.");
            return new Response<bool>(res);
        }
        #endregion

        #region STUDENT PROFILE 
        public async Task<Response<StudentAdmissionViewModel>> StudentProfileAsync()
        {
            int studentId = Convert.ToInt32(_loginService.GetLogedInStudentId());
            string SchoolUniqueId = _appContextAccessor.ClaimSchoolUniqueId();
            var studentProfile = await _unitOfWork.StudentAdmision.FindByStudentLoginIdAsync(studentId, SchoolUniqueId);
            if (studentProfile == null) return new Response<StudentAdmissionViewModel>
                    ("Student Profile not found. Please Talk to the school administration.");

            var studentProfileModel = _mapper.Map<StudentAdmissionViewModel>(studentProfile);
            studentProfileModel.StudentClass = await _unitOfWork.ClassRepo.FindByIdAsync(studentProfileModel.ClassId, SchoolUniqueId);
            studentProfileModel.StudentSection = await _unitOfWork.SectionRepo.FindByIdAsync(studentProfileModel.SectionId, SchoolUniqueId);

            return new Response<StudentAdmissionViewModel>(studentProfileModel);
        }
        #endregion

        public async Task<Response<StudentLedgerViewModel>> StudentLedgerAsync(int studentId)
        {
            string SchoolKey = _appContextAccessor.ClaimSchoolUniqueId();
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<StudentLedgerViewModel>("Select Academic Session.");
            var student = await _unitOfWork.StudentAdmision.SearchStudentAsync(
                    dbId: SchoolKey,
                    academicSessionId: currentSession.Id,
                    studentAdmissionId: studentId);
            if (student == null && student.Count() < 0) return new Response<StudentLedgerViewModel>("No data found.");
            var singleStudent = student.SingleOrDefault();
            singleStudent.StudentClass = await _unitOfWork.ClassRepo.FindByIdAsync(id: singleStudent.ClassId, dbId: SchoolKey, isActive: true);
            singleStudent.StudentSection = await _unitOfWork.SectionRepo.FindByIdAsync(id: singleStudent.SectionId, dbId: SchoolKey, isActive: true);
            var fees = await _unitOfWork.StudentFee.GetStudentFeeListAsync(
                studentAdmissionId: studentId, dbId: SchoolKey, academicSessionId: currentSession.Id);
            StudentLedgerViewModel model = new StudentLedgerViewModel
            {
                StudentAdmissionViewModel = _mapper.Map<StudentAdmissionViewModel>(singleStudent),
                FeeList = fees.ToList()
            };
            return new Response<StudentLedgerViewModel>(model);
        }
        public async Task<Response<UserPasswordViewModel>> GetStudentPasswordAsync(int studentId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            StudentLogin Student = await _unitOfWork.StudentLogin.FindPasswordByIdAsync(id: studentId, dbId: SchoolId);
            UserPasswordViewModel StudentNamePass = _mapper.Map<UserPasswordViewModel>(Student);
            return new Response<UserPasswordViewModel>(StudentNamePass);
        }


        /// <summary>
        /// Method to change Student login password when request is send by schoolAdmin user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>s
        public async Task<Response<bool>> ChangePassword_FromAdminAsync(ResetPasswordViewModel model, int userId)
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var currentLoginUserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var studentadmission = await _unitOfWork.StudentAdmision.FindByIdAsync(userId, dbId: schoolId);
            StudentLogin user = await _unitOfWork.StudentLogin.FindUserByUserIdAsync(studentadmission.StudentLoginId, dbId: schoolId);
            user = _studentRegistrationService.GetNewHashStudentLoginPasswordAsync(user, model.NewPassword);
            user.Password = model.NewPassword;
            user.ModifyBy = currentLoginUserId;
            var res = await _unitOfWork.StudentLogin.UpdateUserAsync(user, schoolId);
            if (res <= 0) return new Response<bool>("Error Changing password.");

            return new Response<bool>(res > 0);
        }

        /// <summary>
        /// Method to change Student login password when request is send by Student Panel
        /// </summary>
        /// <param name="model"> Model with Password </param>
        /// <returns> boolen response object </returns>
        public async Task<Response<bool>> ChangePassword_FromStudentAsync(ResetPasswordViewModel model)
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var userId = Convert.ToInt32(_loginService.GetLogedInStudentId());
            StudentLogin student_NamePass = await _unitOfWork.StudentLogin.FindPasswordByIdAsync(id: userId, dbId: schoolId);
            UserPasswordViewModel userNamePass = _mapper.Map<UserPasswordViewModel>(student_NamePass);
            if (model.CurrentPassword == userNamePass.Password)
            {
                StudentLogin user = await _unitOfWork.StudentLogin.FindActiveUserByUserIdAsync(userId, dbId: schoolId);
                user = _studentRegistrationService.GetNewHashStudentLoginPasswordAsync(user, model.NewPassword);
                user.Password = model.NewPassword;
                user.ModifyBy = userId;
                var res = await _unitOfWork.StudentLogin.UpdateUserAsync(user, schoolId);
                if (res <= 0) return new Response<bool>("Error Changing password.");
                return new Response<bool>(res > 0);
            }
            return new Response<bool>("Current password doesn't match.");
        }


        public async Task<Response<StudentPromotionViewModel>> StudentPromotion_SearchStudentAndBindModel(StudentPromotionViewModel model = null)
        {
            string schoolKey = _appContextAccessor.ClaimSchoolUniqueId();
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<StudentPromotionViewModel>("Select Academic Session.");
            var _classes = await _unitOfWork.ClassRepo.GetAllAsync(dbId: schoolKey, isActive: true);
            var _sections = await _unitOfWork.SectionRepo.GetAllAsync(dbId: schoolKey, isActive: true);
            var _sessions = await _unitOfWork.Session.GetAllAsync(dbId: schoolKey);

            if (model == null)
            {
                model = new StudentPromotionViewModel()
                {
                    Classes = _classes.Select(s => new DropDownItem { Text = s.Name, Value = s.Id.ToString() }).ToList(),
                    Sections = _sections.Select(s => new DropDownItem { Text = s.Name, Value = s.Id.ToString() }).ToList(),
                    Sessions = _sessions.Select(s => new DropDownItem { Text = s.Year, Value = s.Id.ToString() }).ToList(),
                };
            }
            else
            {
                model.Classes = _classes.Select(s => new DropDownItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                model.Sections = _sections.Select(s => new DropDownItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                model.Sessions = _sessions.Select(s => new DropDownItem { Text = s.Year, Value = s.Id.ToString() }).ToList();

                var students = await _unitOfWork.StudentAdmision.SearchStudent_WithAddEditUserAsync(
                    dbId: schoolKey,
                    academicSessionId: currentSession.Id,
                    classId: model.ClassId_Post,
                    sectionId: model.SectionId_Post);

                model.StudentAdmission = students?.ToList();
            }
            return new Response<StudentPromotionViewModel>(model);
        }
        public async Task<Response<bool>> StudentPromotion_Promote(StudentPromotionViewModel model)
        {
            string schoolKey = _appContextAccessor.ClaimSchoolUniqueId();
            int userId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var currentSession = await _loginService.GetCurrentAcademicSession();
            if (currentSession == null) return new Response<bool>("Select Academic Session.");
            if (model.StudentAdmission == null && !model.StudentAdmission.Any()) return new Response<bool>("No Student Selected.");
            var students_Post = await _unitOfWork.StudentAdmision.SearchStudent_WithAddEditUserAsync(
                dbId: schoolKey,
                academicSessionId: currentSession.Id,
                classId: model.ClassId_Post,
                sectionId: model.SectionId_Post);
            List<StudentAdmission> bulkUpdateStudentAdmissionList = new List<StudentAdmission>();
            List<StudentAcademicSession> bulkInsertStudentAcademicSessionList = new List<StudentAcademicSession>();
            List<StudentAcademicSession> bulkUpdateStudentAcademicSessionList = new List<StudentAcademicSession>();
            foreach (var student in model.StudentAdmission)
            {
                // studentadmission 
                var stu = students_Post.Where(w => w.Id == student.Id && w.Sr_RegNo == student.Sr_RegNo).SingleOrDefault();
                if (stu != null)
                {
                    stu.AcademicSessionId = model.SessionId_Promo;
                    stu.ClassId = model.ClassId_Promo;
                    stu.SectionId = model.SectionId_Promo;
                    stu.ModifyBy = userId;
                    bulkUpdateStudentAdmissionList.Add(stu);
                }
                //studentacademicsession
                var studentASList = await _unitOfWork.StudentAcademicSessions.
                    FindByStudentAdmissionIdAndSessionIdAsync(student.Id, currentSession.Id, schoolKey);
                if (studentASList == null)
                {
                    bulkInsertStudentAcademicSessionList.Add(new StudentAcademicSession()
                    {
                        AddedBy = userId,
                        StudentAdmissionId = student.Id,
                        SessionId = model.SessionId_Promo,
                        ClassId = model.ClassId_Promo,
                        SectionId = model.SectionId_Promo
                    });
                }
                else
                {
                    studentASList.ClassId = model.ClassId_Promo;
                    studentASList.SectionId = model.SectionId_Promo;
                    studentASList.ModifyBy = userId;
                    bulkUpdateStudentAcademicSessionList.Add(studentASList);
                }
            }
            var res = await _unitOfWork.StudentAdmision.BulkUpdateAsync(studentAdmission: bulkUpdateStudentAdmissionList, dbId: schoolKey);
            if (res <= 0) return new Response<bool>("Error Promoting students.");
            if (bulkInsertStudentAcademicSessionList.Any())
            {
                res = await _unitOfWork.StudentAcademicSessions.BulkAddAsync(sessions: bulkInsertStudentAcademicSessionList, dbId: schoolKey);
                if (res <= 0) return new Response<bool>("Error Promoting students.");
            }
            if (bulkUpdateStudentAcademicSessionList.Any())
            {
                res = await _unitOfWork.StudentAcademicSessions.BulkUpdateAsync(sessions: bulkUpdateStudentAcademicSessionList, dbId: schoolKey);
                if (res <= 0) return new Response<bool>("Error Promoting students.");
            }
            return new Response<bool>(true);
        }
    }
}
