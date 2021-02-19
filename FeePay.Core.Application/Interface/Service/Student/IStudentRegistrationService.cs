using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Core.Application.Interface.Service.Student
{
    public interface IStudentRegistrationService
    {
        Task<bool> AddStudentAsync(StudentAdmission studentAdmission);
        Task ChangeStudentLoginPasswordAsync(StudentLogin user, string newPassword);
        StudentLogin GetNewHashStudentLoginPasswordAsync(StudentLogin user, string newPassword);
    }
}
