using AutoMapper;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.SuperAdmin;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Services.SuperAdmin
{
    public class SchoolsManagerServices : ISchoolsManagerServices
    {
        public SchoolsManagerServices(IUnitOfWork unitOfWork,
            ILoginService loginService,
            IAppContextAccessor appContextAccessor,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _loginService = loginService;
            _appContextAccessor = appContextAccessor;
            _mapper = mapper;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginService _loginService;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly IMapper _mapper;

        #region REGISTER SCHOOL
        public async Task<Response<List<RegisterSchoolViewModel>>> GetRegisterSchoolList()
        {
            var schools = await _unitOfWork.RegisteredSchool.GetAll_WithAddEditUserAsync();
            IEnumerable<RegisterSchoolViewModel> model = _mapper.Map<IEnumerable<RegisterSchoolViewModel>>(schools);
            return new Response<List<RegisterSchoolViewModel>>(model.ToList());
        }
        public async Task<Response<RegisterSchoolViewModel>> GetRegisterSchoolById(int id)
        {
            var school = await _unitOfWork.RegisteredSchool.FindByIdAsync(id);
            if (school == null) return new Response<RegisterSchoolViewModel>("No data Found.");
            RegisterSchoolViewModel model = _mapper.Map<RegisterSchoolViewModel>(school);
            return new Response<RegisterSchoolViewModel>(model);
        }
        public async Task<Response<bool>> AddOrEditRegisterSchool(RegisterSchoolViewModel model)
        {
            int UserId = Convert.ToInt32(_loginService.GetLogedInSuperAdminId());
            RegisteredSchool registerSchool = _mapper.Map<RegisteredSchool>(model);
            registerSchool.NormalizedName = registerSchool.Name?.ToUpper().Trim();
            registerSchool.IsActive = registerSchool.IsApproved;
            if (registerSchool.Id == 0)
            {
                registerSchool.AddedBy = UserId;
                registerSchool.UniqueId = CreateSchoolUniqueId(registerSchool.Name);

                var checkExist = await _unitOfWork.RegisteredSchool.FindByNameAsync(registerSchool.NormalizedName);
                if (checkExist != null && checkExist.UniqueId == registerSchool.UniqueId)
                    return new Response<bool>("School already register with this name..");

                var res = await _unitOfWork.RegisteredSchool.AddAsync(registerSchool);
                if (res <= 0) return new Response<bool>("School already register with this name...");
                return new Response<bool>(res > 0);
            }
            else
            {
                var checkExist = await _unitOfWork.RegisteredSchool.FindByIdAsync(registerSchool.Id);
                if (checkExist == null) return new Response<bool>("No data found to update.");
                if (registerSchool.NormalizedName != checkExist.NormalizedName)
                {
                    registerSchool.UniqueId = CreateSchoolUniqueId(registerSchool.Name);
                    var checkUniqueIdExist = await _unitOfWork.RegisteredSchool.FindByNameAsync(registerSchool.NormalizedName);
                    if (checkUniqueIdExist != null) return new Response<bool>("School already register with this name....");
                }

                registerSchool.ModifyBy = UserId;
                var res = await _unitOfWork.RegisteredSchool.UpdateAsync(registerSchool);
                if (res <= 0) return new Response<bool>("School already register with this name.....");
                return new Response<bool>(res > 0);
            }
        }
        public async Task<Response<bool>> DeleteRegisterSchool(int registeredSchoolId)
        {
            var res = await _unitOfWork.RegisteredSchool.DeleteAsync(registeredSchoolId);
            if (res <= 0) return new Response<bool>("No data found.");
            return new Response<bool>(res > 0);
        }
        public async Task<Response<bool>> ApproveRegisterSchool(int registeredSchoolId, bool IsApproved)
        {
            var checkExist = await _unitOfWork.RegisteredSchool.FindByIdAsync(registeredSchoolId);
            if (checkExist == null) return new Response<bool>("No data found.");
            int UserId = Convert.ToInt32(_loginService.GetLogedInSuperAdminId());
            checkExist.IsApproved = IsApproved;
            checkExist.ModifyBy = UserId;
            var res = await _unitOfWork.RegisteredSchool.UpdateAsync(checkExist);
            if (res <= 0) return new Response<bool>("Error approving school.");
            return new Response<bool>(res > 0);
        }
        #endregion

        #region PAYMENT GATEWAY DOCUMENT
        public async Task<Response<List<PaymentGatewayDocumentViewModel>>> GetPaymentGatewayDocumentList()
        {
            var list = await _unitOfWork.PaymentGatewayDocument.GetAll_WithSchoolDataAsync();
            List<PaymentGatewayDocumentViewModel> model = _mapper.Map<List<PaymentGatewayDocumentViewModel>>(list?.ToList());
            return new Response<List<PaymentGatewayDocumentViewModel>>(model);
        }
        public async Task<Response<PaymentGatewayDocumentViewModel>> GetPaymentGatewayDocumentById(int id)
        {
            var paymentGatewayDocument = await _unitOfWork.PaymentGatewayDocument.FindByIdAsync(id);
            if (paymentGatewayDocument == null) return new Response<PaymentGatewayDocumentViewModel>("No data found.");
            PaymentGatewayDocumentViewModel model = _mapper.Map<PaymentGatewayDocumentViewModel>(paymentGatewayDocument);
            var school = await _unitOfWork.RegisteredSchool.FindByIdAsync(paymentGatewayDocument.RegisteredSchoolId, true);
            if (school == null) return new Response<PaymentGatewayDocumentViewModel>("School data found.");
            model.RegisterSchool = _mapper.Map<RegisterSchoolViewModel>(school);
            return new Response<PaymentGatewayDocumentViewModel>(model);
        }
        public async Task<Response<bool>> ApprovePaymentGateway(IsActiveRequestViewModel model)
        {
            int userId = Convert.ToInt32(_loginService.GetLogedInSuperAdminId());
            var paymentGatewayDocument = await _unitOfWork.PaymentGatewayDocument.FindByIdAsync(model.DataId);
            if (paymentGatewayDocument == null) return new Response<bool>("No Data Found.");
            paymentGatewayDocument.IsApproved = model.IsApproved;
            paymentGatewayDocument.ModifyBy = userId;
            var res = await _unitOfWork.PaymentGatewayDocument.UpdateAsync(paymentGatewayDocument);
            return new Response<bool>(res > 0);
        }


        #endregion

        #region Private Methods
        private string CreateSchoolUniqueId(string schoolName)
        {
            var firstChars = schoolName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => s[0]);
            string preUniqueId = string.Join("", firstChars);
            var nowDate = DateTime.Now.ToString("ddMMyyyyHHmm");
            var newUniqueId = $"{preUniqueId}#{nowDate}";
            return newUniqueId.ToUpper();
        }
        #endregion
    }
}
