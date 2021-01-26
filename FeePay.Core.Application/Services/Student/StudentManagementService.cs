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
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Core.Application.Services.Student
{
    public class StudentManagementService : IStudentManagementService
    {
        public StudentManagementService(IUnitOfWork unitOfWork, IMapper mapper, IAppContextAccessor appContextAccessor,
            ILoginService loginService, IStudentRegistrationService studentRegistrationService)
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



        public async Task<StudentAdmissionViewModel> BindStudentAdmissionViewModelAsync(StudentAdmissionViewModel model = null)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var ClassesDropDownItemList = (await _unitOfWork.ClassRepo.GetAllActiveAsync(SchoolId))
                .Select(s => new DropDownItem { Value = s.Id.ToString(), Text = s.NormalizedName }).ToList();
            var StatesDropDownItemList = (await _unitOfWork.CityState.GetAllActiveStatesAsync(SchoolId))
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
        public async Task<Response<List<StudentAdmissionViewModel>>> GetListOfStudentsAsync()
        {
            string schoolKey = _appContextAccessor.ClaimSchoolUniqueId();
            var students = await _unitOfWork.StudentAdmision.GetAll_WithAddEditUserAsync(schoolKey);
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
            bool res;
            if (model.Id != 0)
            {
                studentAdmission.ModifyBy = UserId;
                var updatedId = await _unitOfWork.StudentAdmision.UpdateAsync(studentAdmission, SchoolId);
                res = (updatedId > 0);
            }
            else
            {
                studentAdmission.AddedBy = UserId;
                res = await _studentRegistrationService.AddStudentAsync(studentAdmission);
            }
            if (res) return new Response<bool>(res);
            return new Response<bool>("student is already present with same formNo.");
        }
        public async Task<Response<List<DropDownItem>>> ClassSectionsAsync(int classId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var _class = await _unitOfWork.ClassSection.FindSectionsInClassByClassIdAsync(classId, SchoolId);
            List<DropDownItem> ddl = _class.Sections?.Select(s => new DropDownItem { Text = s.NormalizedName, Value = s.Id.ToString() }).ToList();
            return new Response<List<DropDownItem>>(ddl ?? new List<DropDownItem>());
        }
        public async Task<Response<List<DropDownItem>>> StateCitiesAsync(int stateId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var cities = await _unitOfWork.CityState.FindActiveCitiesByStateIdAsync(stateId, SchoolId);
            List<DropDownItem> ddl = cities?.Select(s => new DropDownItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
            return new Response<List<DropDownItem>>(ddl ?? new List<DropDownItem>());
        }
        public async Task<Response<List<StudentAdmissionViewModel>>> GetListOfActiveStudentsByClassIdAsync(int classId)
        {
            string schoolKey = _appContextAccessor.ClaimSchoolUniqueId();
            var students = await _unitOfWork.StudentAdmision.SearchStudentAsync(classId: classId, isActive: true, dbId: schoolKey);
            var model = _mapper.Map<List<StudentAdmissionViewModel>>(students);
            return new Response<List<StudentAdmissionViewModel>>(model);
        }
        public async Task<Response<List<StudentAdmissionViewModel>>> SearchStudentAsync(string searchParam)
        {
            string SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var students = await _unitOfWork.StudentAdmision.SearchStudent_WithAddEditUserAsync(seatchString: searchParam, dbId: SchoolId);
            var model = _mapper.Map<List<StudentAdmissionViewModel>>(students);
            return new Response<List<StudentAdmissionViewModel>>(model);
        }

        public async Task<Response<StudentLedgerViewModel>> StudentLedgerAsync(int id)
        {
            string SchoolKey = _appContextAccessor.ClaimSchoolUniqueId();
            var student = await _unitOfWork.StudentAdmision.SearchStudentAsync(dbId: SchoolKey, studentId: id);
            var fees = await _unitOfWork.StudentFee.GetStudentFeeListAsync(id, SchoolKey);
            StudentLedgerViewModel model = new StudentLedgerViewModel
            {
                StudentAdmissionViewModel = _mapper.Map<StudentAdmissionViewModel>(student.SingleOrDefault()),
                FeeList = fees.ToList()
            };
            return new Response<StudentLedgerViewModel>(model);
        }
    }
}
