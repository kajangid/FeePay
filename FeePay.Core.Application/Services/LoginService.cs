using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Services
{
    public class LoginService : ILoginService
    {
        public LoginService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private IUnitOfWork _UnitOfWork;

        public async Task<StudentLoginViewModel> BindStudentLoginData()
        {
            var AllActiveSchool = await _UnitOfWork.RegisteredSchool.GetAllActiveSchoolAsync();
            List<DropDownItem> dropDownItems = AllActiveSchool.Select(s => new DropDownItem { Text = s.Name, Value = s.UniqueId }).ToList();
            return new StudentLoginViewModel() { ActiveSchools = dropDownItems };
        }

















    }
}
