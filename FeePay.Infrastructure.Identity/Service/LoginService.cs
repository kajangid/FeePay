using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.SuperAdmin;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Domain.Entities.Common;
using AutoMapper;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace FeePay.Infrastructure.Identity.Service
{
    public class LoginService : ILoginService
    {
        public LoginService(SignInManager<SchoolAdminUser> SignInManagerSchool, SignInManager<StudentLogin> SignInManagerStudent,
            SignInManager<SuperAdminUser> SignInManagerSuperAdmin, IUnitOfWork UnitOfWork, IAppContextAccessor appContextAccessor,
            UserManager<SchoolAdminUser> UserManagerSchool, UserManager<StudentLogin> UserManagerStudent,
            UserManager<SuperAdminUser> UserManagerSuperAdmin)
        {
            _signInManagerSchool = SignInManagerSchool;
            _signInManagerStudent = SignInManagerStudent;
            _signInManagerSuperAdmin = SignInManagerSuperAdmin;
            _unitOfWork = UnitOfWork;
            _appContextAccessor = appContextAccessor;
            _userManagerSchool = UserManagerSchool;
            _userManagerStudent = UserManagerStudent;
            _userManagerSuperAdmin = UserManagerSuperAdmin;
        }
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<SchoolAdminUser> _signInManagerSchool;
        private readonly SignInManager<StudentLogin> _signInManagerStudent;
        private readonly SignInManager<SuperAdminUser> _signInManagerSuperAdmin;
        private readonly UserManager<SchoolAdminUser> _userManagerSchool;
        private readonly UserManager<StudentLogin> _userManagerStudent;
        private readonly UserManager<SuperAdminUser> _userManagerSuperAdmin;


        public async Task<Response<bool>> AuthenticateSchoolUserAsync(SchoolLoginViewModel model)
        {
            // Validate SchoolUniqueId

            RegisteredSchool t = await _unitOfWork.RegisteredSchool.FindByUniqueIdAsync(model.SchoolUniqueId, isActive: true);
            if (t == null) return new Response<bool>("Invalid School Id");

            // Get the User from School database
            SchoolAdminUser user = await _unitOfWork.SchoolAdminUser
                .FindActiveByUserNameAsync(model.UserName.ToUpper(), model.SchoolUniqueId);
            if (user == null) return new Response<bool>("Account does not exist with this username.");

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true

            user.SchoolUniqueId = model.SchoolUniqueId;
            user.SchoolName = t.Name;
            //user.RolesName = (await _UnitOfWork.SchoolAdminUserRole.GetUserRolesAsync(user.Id, model.SchoolUniqueId))?.Select(s => s.Name).ToList();
            // Authorize User
            var result = await _signInManagerSchool.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // TODO: Clear Claim Bug
                // Identity state does not update with claim factory in this request 
                // Check for new request to get all the claim
                //var clam = _AppContextAccessor.GetCurrentUserClaims();
                // TODO: Event will be create for this
                await _unitOfWork.SchoolAdminUser.UpdateLoginState(user.Id, _appContextAccessor.GetUserIP(), model.SchoolUniqueId);
                return new Response<bool>(true, user.FullName);
            }
            return new Response<bool>("Invalid Username or Password.");
        }

        public async Task<Response<bool>> AuthenticateStudentAsync(StudentLoginViewModel model)
        {

            // Validate SchoolUniqueId
            RegisteredSchool t = await _unitOfWork.RegisteredSchool.FindByUniqueIdAsync(model.SchoolUniqueId, isActive: true);
            if (t == null) return new Response<bool>("Invalid School Id");

            // Get the User from School database
            StudentLogin user = await _unitOfWork
                .StudentLogin
                .FindActiveUserByUserNameAsync(model.UserName.ToUpper(), model.SchoolUniqueId);
            if (user == null) return new Response<bool>("Account does not exist with this username.");


            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true

            user.SchoolUniqueId = model.SchoolUniqueId;
            user.SchoolName = t.Name;

            // Authorize User
            var result = await _signInManagerStudent.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                await _unitOfWork.StudentLogin.UpdateLoginState(user.Id, _appContextAccessor.GetUserIP(), model.SchoolUniqueId);
                return new Response<bool>(true, user.UserName);
            }
            return new Response<bool>("Invalid Username or Password.");
        }

        public async Task<Response<bool>> AuthenticateSuperAdminAsync(SuperAdminLoginViewModel model)
        {
            SuperAdminUser user = await _unitOfWork.SuperAdminUser
                .FindByUserNameAsync(model.UserName.ToUpper(), isActive: true);
            if (user == null) return new Response<bool>("Account does not exist with this email address.");
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            // var result = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            var result = await _signInManagerSuperAdmin.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                await _unitOfWork.SuperAdminUser.UpdateLoginState(user.Id, _appContextAccessor.GetUserIP());
                return new Response<bool>(true, user.FirstName);
            }
            return new Response<bool>("Invalid Email or Password.");
        }


        public async Task<List<string>> GetUserAccessRights(string userName, string areaName)
        {
            List<string> roles_Access = new List<string>();
            if (areaName == "SuperAdmin")
            {
                //var user = await _UnitOfWork.SuperAdminUser.FindActiveByUserNameAsync(userName);
                //roles_Access = user?.Roles?.Select(r => r.Access).ToList();
            }
            else if (areaName == "School")
            {
                var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
                var user = await _unitOfWork.SchoolAdminUser.FindActiveByUserNameAsync(userName, SchoolId);
                roles_Access = user?.Roles?.Select(r => r.Access).ToList();
            }
            else
            {
                throw new Exception("not supported area.");
            }
            return roles_Access;
        }

        public async Task<Dictionary<string, string>> GetCurrentStudentClassSection()
        {
            var dic = new Dictionary<string, string>();
            try
            {
                string SchoolUniqueId = _appContextAccessor.ClaimSchoolUniqueId();
                int UserId = Convert.ToInt32(GetLogedInStudentId());
                var studentProfile = await _unitOfWork.StudentAdmision.FindByStudentLoginIdAsync(UserId, SchoolUniqueId);
                if (studentProfile != null)
                {
                    if (studentProfile.ClassId > 0)
                    {
                        var _class = await _unitOfWork.ClassRepo.FindActiveByIdAsync(studentProfile.ClassId, SchoolUniqueId);
                        dic.Add("class", _class?.Name);
                    }
                    if (studentProfile.SectionId > 0)
                    {
                        var section = await _unitOfWork.SectionRepo.FindActiveByIdAsync(studentProfile.SectionId, SchoolUniqueId);
                        dic.Add("section", section?.Name);
                    }
                }

            }
            catch { }
            return dic;
        }

        public async Task<string> GetCurrentStudentName()
        {
            try
            {
                string SchoolUniqueId = _appContextAccessor.ClaimSchoolUniqueId();
                int UserId = Convert.ToInt32(GetLogedInStudentId());
                var studentProfile = await _unitOfWork.StudentAdmision.FindByStudentLoginIdAsync(UserId, SchoolUniqueId);
                if (studentProfile != null) return $"{studentProfile.FirstName} {studentProfile.LastName}";
            }
            catch { }
            return "";
        }
        public async Task<string> GetCurrentSchoolAdminUser_Name()
        {
            try
            {
                string SchoolUniqueId = _appContextAccessor.ClaimSchoolUniqueId();
                int UserId = Convert.ToInt32(GetLogedInSchoolAdminId());
                var school = await _unitOfWork.SchoolAdminUser.FindByIdAsync(UserId, SchoolUniqueId);
                if (school != null) return $"{school.FirstName} {school.LastName}";
            }
            catch { }
            return "";
        }
        public async Task<string> GetCurrentSuperAdminUser_Name()
        {
            try
            {
                string SchoolUniqueId = _appContextAccessor.ClaimSchoolUniqueId();
                int UserId = Convert.ToInt32(GetLogedInSuperAdminId());
                var sAdmin = await _unitOfWork.SuperAdminUser.FindByIdAsync(UserId);
                if (sAdmin != null) return $"{sAdmin.FirstName} {sAdmin.LastName}";
            }
            catch { }
            return "";
        }


        public async Task EnsureStudentLogoutAsync()
        {
            // Ensure all logout 
            await _signInManagerStudent.SignOutAsync();
        }
        public async Task EnsureSchoolUserLogoutAsync()
        {
            // Ensure all logout 
            await _signInManagerSchool.SignOutAsync();
        }
        public async Task EnsureSuperAdminLogoutAsync()
        {
            // Ensure all logout 
            await _signInManagerSuperAdmin.SignOutAsync();
        }

        public async Task LogoutAll()
        {
            await _signInManagerStudent.SignOutAsync();
            await _signInManagerSchool.SignOutAsync();
            await _signInManagerSuperAdmin.SignOutAsync();
        }
        public async Task SchoolAdminLogout() => await _signInManagerSchool.SignOutAsync();
        public async Task SuperAdminLogout() => await _signInManagerSuperAdmin.SignOutAsync();
        public async Task StudentLogout() => await _signInManagerStudent.SignOutAsync();

        public bool CheckUserIdentityClaim()
        {
            var allClaims = _appContextAccessor.GetCurrentUserClaims();
            var currentArea = _appContextAccessor.GetRequestPath();
            if (allClaims != null)
            {
                var SuperAdminIdentity = allClaims.FirstOrDefault(claim => claim.Type == "SuperAdminAuthRoute" && claim.Issuer.Equals("SuperAdmin", StringComparison.InvariantCultureIgnoreCase));
                if (SuperAdminIdentity != null && !currentArea.Contains("/SuperAdmin/")) { return false; }

                var SchoolAdminIdentity = allClaims.FirstOrDefault(claim => claim.Type == "SchoolAuthRoute" && claim.Issuer.Equals("SchoolAdmin", StringComparison.InvariantCultureIgnoreCase));
                if (SchoolAdminIdentity != null && !currentArea.Contains("/School/")) { return false; }

                var StudentLoginIdentity = allClaims.FirstOrDefault(claim => claim.Type == "StudentAuthRoute" && claim.Issuer.Equals("Student", StringComparison.InvariantCultureIgnoreCase));
                if (StudentLoginIdentity != null && !currentArea.Contains("/Student/")) { return false; }
            }
            return true;
        }

        public string GetLogedInStudentId()
        {
            return _userManagerStudent.GetUserId(_appContextAccessor.getCurrentUser());
        }
        public string GetLogedInSchoolAdminId()
        {
            return _userManagerSchool.GetUserId(_appContextAccessor.getCurrentUser());
        }
        public string GetLogedInSuperAdminId()
        {
            return _userManagerSuperAdmin.GetUserId(_appContextAccessor.getCurrentUser());
        }
    }
}
