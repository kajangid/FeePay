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
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Core.Application.Services.Student
{
    public class StudentFeeDepositManagerService : IStudentFeeDepositManagerService
    {
        public StudentFeeDepositManagerService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IAppContextAccessor appContextAccessor,
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

        public async Task<Response<IEnumerable<StudentFeesViewModel>>> GetStudentFees()
        {
            string SchoolUniqueId = _appContextAccessor.ClaimSchoolUniqueId();
            int UserId = Convert.ToInt32(_loginService.GetLogedInStudentId());

            var studentProfile = await _unitOfWork.StudentAdmision.FindByStudentLoginIdAsync(UserId, SchoolUniqueId);
            if (studentProfile == null) return new Response<IEnumerable<StudentFeesViewModel>>
                    ("Student Profile not found. Please Talk to the school administration.");

            var studentFees = await _unitOfWork.StudentFee.GetStudentFeeListAsync(studentProfile.Id, SchoolUniqueId);
            if (studentFees == null) return new Response<IEnumerable<StudentFeesViewModel>>("No fee Assign");

            IEnumerable<StudentFeesViewModel> model = _mapper.Map<IEnumerable<StudentFeesViewModel>>(studentFees);

            return new Response<IEnumerable<StudentFeesViewModel>>(model.ToList());
        }

        public async Task<Response<FeeDepositSummeryViewModel>> GenerateFeeDepositSummery(SelectedFeeDepositViewModel model)
        {
            string SchoolUniqueId = _appContextAccessor.ClaimSchoolUniqueId();
            int UserId = Convert.ToInt32(_loginService.GetLogedInStudentId());

            if (model != null && model.FeeDeposit.Any(a => a.IsSelected == true))
            {
                var studentProfile = await _unitOfWork.StudentAdmision.FindByStudentLoginIdAsync(UserId, SchoolUniqueId);
                if (studentProfile == null) return new Response<FeeDepositSummeryViewModel>
                        ("Student Profile not found. Please Talk to the school administration.");

                var studentProfileModel = _mapper.Map<StudentAdmissionViewModel>(studentProfile);
                studentProfileModel.StudentClass = await _unitOfWork.ClassRepo.FindByIdAsync(studentProfileModel.ClassId, SchoolUniqueId);
                studentProfileModel.StudentSection = await _unitOfWork.SectionRepo.FindByIdAsync(studentProfileModel.SectionId, SchoolUniqueId);


                List<StudentFees> fees = new List<StudentFees>();
                foreach (var selectedFee in model.FeeDeposit)
                {
                    if (selectedFee.IsSelected)
                    {
                        var stuFee = await _unitOfWork.StudentFee.FindByIdAsync(selectedFee.StudentFeeId, SchoolUniqueId);
                        if (stuFee != null) fees.Add(stuFee);
                    }
                }
                FeeDepositSummeryViewModel resModel = new FeeDepositSummeryViewModel()
                {
                    Student = studentProfileModel,
                    Fees = _mapper.Map<List<StudentFeesViewModel>>(fees)
                };
                return new Response<FeeDepositSummeryViewModel>(resModel);
            }
            return new Response<FeeDepositSummeryViewModel>();

        }
    }
}
