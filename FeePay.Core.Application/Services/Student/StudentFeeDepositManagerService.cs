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
using FeePay.Core.Application.IoC;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.School;
using FeePay.Core.Domain.Entities.Student;
using FeePay.Core.Application.Enums;

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

            var session = await _unitOfWork.Session.FetchActiveAcadmicSession(dbId: SchoolUniqueId);

            var studentFees = await _unitOfWork.StudentFee.GetStudentFeeListAsync(
                studentAdmissionId: studentProfile.Id, dbId: SchoolUniqueId, academicSessionId: session.Id);
            if (studentFees == null) return new Response<IEnumerable<StudentFeesViewModel>>("No fee Assign");

            IEnumerable<StudentFeesViewModel> model = _mapper.Map<IEnumerable<StudentFeesViewModel>>(studentFees);

            return new Response<IEnumerable<StudentFeesViewModel>>(model.ToList());
        }

        public async Task<Response<FeeDepositSummeryViewModel>> GetSelectedFeeSummary(SelectedFeeDepositViewModel model)
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
                        if (!selectedFee.StudentFeeToken.IsValidToken()) return new Response<FeeDepositSummeryViewModel>("Invalid Data.");
                        var studentFeeId = selectedFee.StudentFeeToken.DecryptID();
                        var stuFee = await _unitOfWork.StudentFee.FindByIdAsync(studentFeeId, SchoolUniqueId);
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

        #region Deposit Request 
        public async Task<Response<FeeDepositSummeryViewModel>> GenerateFeeDeposit(SelectedFeeDepositViewModel model)
        {
            string SchoolUniqueId = _appContextAccessor.ClaimSchoolUniqueId();
            int UserId = Convert.ToInt32(_loginService.GetLogedInStudentId());

            if (model != null && model.FeeDeposit != null && model.FeeDeposit.Count > 0)
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
                    if (!selectedFee.StudentFeeToken.IsValidToken()) return new Response<FeeDepositSummeryViewModel>("Invalid Data.");
                    var studentFeeId = selectedFee.StudentFeeToken.DecryptID();
                    var stuFee = await _unitOfWork.StudentFee.FindByIdAsync(studentFeeId, SchoolUniqueId);
                    if (stuFee != null) fees.Add(stuFee);
                }

                //Check If Paid Fee Exist in Deposit list 
                if (fees.Any(a => a.IsPaid == true)) return new Response<FeeDepositSummeryViewModel>
                                    ("Invalid Data. Cannot Select Already Deposit Fee/Fees.");

                FeeDepositSummeryViewModel resModel = new FeeDepositSummeryViewModel()
                {
                    Student = studentProfileModel,
                    Fees = _mapper.Map<List<StudentFeesViewModel>>(fees)
                };
                return new Response<FeeDepositSummeryViewModel>(resModel);
            }
            return new Response<FeeDepositSummeryViewModel>();

        }
        public async Task<Response<bool>> GenerateFeesTransaction(string transactionId, string status,
            string mode, decimal amountPay, List<StudentFeesViewModel> feesModel)
        {
            string SchoolUniqueId = _appContextAccessor.ClaimSchoolUniqueId();
            int UserId = Convert.ToInt32(_loginService.GetLogedInStudentId());
            FeesTranscation feesTranscation = new FeesTranscation()
            {
                UserId = UserId,
                Amount = amountPay,
                IsComplete = false,
                TransactionId = transactionId,
                TransactionMode = mode,
                State = status,
                Receipt = "",
            };
            List<StudentFees> fees = _mapper.Map<List<StudentFees>>(feesModel);
            fees.ForEach(f =>
            {
                f.Status = nameof(PaymentStatus.Not_Paied);
                f.PaymentId = transactionId;
                f.IsPaid = false;
            });

            var res = await _unitOfWork.FeesTranscation.AddAsync(feesTranscation, fees, SchoolUniqueId);
            if (res < 0) return new Response<bool>("Error Adding FeesTranscation Data.");
            return new Response<bool>(res > 0);
        }

        public async Task<Response<bool>> ComplateFeesTransaction(string transactionId, string status, DateTime complateDate)
        {
            string SchoolUniqueId = _appContextAccessor.ClaimSchoolUniqueId();
            var session = await _unitOfWork.Session.FetchActiveAcadmicSession(dbId: SchoolUniqueId);
            FeesTranscation feesTranscation = await _unitOfWork.FeesTranscation.FindByTranscationIdAsync(transactionId, SchoolUniqueId);
            if (feesTranscation == null) return new Response<bool>("NO Transaction Found.");
            feesTranscation.State = status;
            feesTranscation.IsComplete = true;
            feesTranscation.Date = complateDate;
            var fees = await _unitOfWork.StudentFee.GetStudentFeeListByTransactionIdAsync(
                transactionId: transactionId, dbId: SchoolUniqueId, academicSessionId: session.Id);
            if (fees == null || !fees.Any()) return new Response<bool>("NO Transaction Fees Found.");
            fees.ToList().ForEach(f =>
            {
                f.Status = nameof(PaymentStatus.Paied);
                f.PaymentDate = complateDate;
                f.Mode = nameof(PaymentMode.ONLINE);
                f.IsPaid = true;
            });

            var res = await _unitOfWork.FeesTranscation.UpdateAsync(feesTranscation, fees.ToList(), SchoolUniqueId);
            if (res < 0) return new Response<bool>("Error Adding FeesTranscation Data.");
            return new Response<bool>(res > 0);
        }

        public async Task<Response<bool>> FailFeesTransaction(string transactionId, string status)
        {
            string SchoolUniqueId = _appContextAccessor.ClaimSchoolUniqueId();
            var session = await _unitOfWork.Session.FetchActiveAcadmicSession(dbId: SchoolUniqueId);
            FeesTranscation feesTranscation = await _unitOfWork.FeesTranscation.FindByTranscationIdAsync(transactionId, SchoolUniqueId);
            if (feesTranscation == null) return new Response<bool>("NO Transaction Found.");
            feesTranscation.State = status;
            feesTranscation.IsComplete = false;
            feesTranscation.Date = null;
            var fees = await _unitOfWork.StudentFee.GetStudentFeeListByTransactionIdAsync(
                transactionId: transactionId, dbId: SchoolUniqueId, academicSessionId: session.Id);
            if (fees == null || !fees.Any()) return new Response<bool>("NO Transaction Fees Found.");
            fees.ToList().ForEach(f =>
            {
                f.Status = nameof(PaymentStatus.Not_Paied);
                f.PaymentDate = null;
                f.Mode = string.Empty;
                f.PaymentId = string.Empty;
                f.IsPaid = false;
            });

            var res = await _unitOfWork.FeesTranscation.UpdateAsync(feesTranscation, fees.ToList(), SchoolUniqueId);
            if (res < 0) return new Response<bool>("Error Adding FeesTranscation Data.");
            return new Response<bool>(res > 0);

        }
        #endregion
    }
}
