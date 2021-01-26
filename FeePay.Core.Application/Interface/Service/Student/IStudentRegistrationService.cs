using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Core.Application.Interface.Service.Student
{
    public interface IStudentRegistrationService
    {
        Task<bool> AddStudentAsync(StudentAdmission studentAdmission);
    }
}
