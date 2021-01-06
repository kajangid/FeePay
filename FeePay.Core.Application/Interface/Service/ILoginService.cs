using FeePay.Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Service
{
    public interface ILoginService
    {
        Task<StudentLoginViewModel> BindStudentLoginData();
    }
}
