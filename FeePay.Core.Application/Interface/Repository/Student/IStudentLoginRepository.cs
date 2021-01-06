using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Repository.Student
{
    public interface IStudentLoginRepository
    {
        Task<int> AddUserAsync(StudentLogin user, string dbId = null);
        Task<int> UpdateUserAsync(StudentLogin user, string dbId = null);
        Task<int> DeleteUserAsync(int Id, string dbId = null);
        Task<StudentLogin> FindUserByUserIdAsync(int userId, string dbId = null);
        Task<StudentLogin> FindUserByUserNameAsync(string normalizedUserName, string dbId = null);
        Task<StudentLogin> FindUserByUserEmailAsync(string normalizedEmail, string dbId = null);
        Task<StudentLogin> FindActiveUserByUserIdAsync(int userId, string dbId = null);
        Task<StudentLogin> FindActiveUserByUserNameAsync(string normalizedUserName, string dbId = null);
        Task<StudentLogin> FindActiveUserByUserEmailAsync(string normalizedEmail, string dbId = null);
        Task<IList<StudentLogin>> FindAllActiveUserAsync(string dbId = null);
        Task UpdateLoginState(int userId, string Ip, string dbId = null);

    }
}
