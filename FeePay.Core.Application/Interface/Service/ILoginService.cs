using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Service
{
    public interface ILoginService
    {
        Task<Response<bool>> AuthenticateSchoolUserAsync(SchoolLoginViewModel model);
        Task<Response<bool>> AuthenticateStudentAsync(StudentLoginViewModel model);
        Task<Response<bool>> AuthenticateSuperAdminAsync(SuperAdminLoginViewModel model);


        Task<List<string>> GetUserAccessRights(string userName, string areaName);

        Task<Dictionary<string, string>> GetCurrentStudentClassSection();
        Task<string> GetCurrentStudentName();

        Task EnsureStudentLogoutAsync();
        Task EnsureSchoolUserLogoutAsync();
        Task EnsureSuperAdminLogoutAsync();

        Task LogoutAll();

        Task SchoolAdminLogout();
        Task SuperAdminLogout();
        Task StudentLogout();

        bool CheckUserIdentityClaim();

        string GetLogedInStudentId();
        string GetLogedInSchoolAdminId();
        string GetLogedInSuperAdminId();
    }
}
