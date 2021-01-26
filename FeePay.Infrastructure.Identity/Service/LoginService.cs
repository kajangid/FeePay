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
            _SignInManagerSchool = SignInManagerSchool;
            _SignInManagerStudent = SignInManagerStudent;
            _SignInManagerSuperAdmin = SignInManagerSuperAdmin;
            _UnitOfWork = UnitOfWork;
            _AppContextAccessor = appContextAccessor;
            _UserManagerSchool = UserManagerSchool;
            _UserManagerStudent = UserManagerStudent;
            _UserManagerSuperAdmin = UserManagerSuperAdmin;
        }
        private readonly IAppContextAccessor _AppContextAccessor;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly SignInManager<SchoolAdminUser> _SignInManagerSchool;
        private readonly SignInManager<StudentLogin> _SignInManagerStudent;
        private readonly SignInManager<SuperAdminUser> _SignInManagerSuperAdmin;
        private readonly UserManager<SchoolAdminUser> _UserManagerSchool;
        private readonly UserManager<StudentLogin> _UserManagerStudent;
        private readonly UserManager<SuperAdminUser> _UserManagerSuperAdmin;


        public async Task<Response<bool>> AuthenticateSchoolUserAsync(SchoolLoginViewModel model)
        {
            // Validate SchoolUniqueId
            RegisteredSchool t = await _UnitOfWork.RegisteredSchool.GetActiveByUniqueIdAsync(model.SchoolUniqueId);
            if (t == null) return new Response<bool>("Invalid School Id");

            // Get the User from School database
            SchoolAdminUser user = await _UnitOfWork.SchoolAdminUser
                .FindActiveByUserNameAsync(model.UserName.ToUpper(), model.SchoolUniqueId);

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true

            user.SchoolUniqueId = model.SchoolUniqueId;
            user.SchoolName = t.Name;
            //user.RolesName = (await _UnitOfWork.SchoolAdminUserRole.GetUserRolesAsync(user.Id, model.SchoolUniqueId))?.Select(s => s.Name).ToList();
            // Authorize User
            var result = await _SignInManagerSchool.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // TODO: Clear Claim Bug
                // Identity state does not update with claim factory in this request 
                // Check for new request to get all the claim
                //var clam = _AppContextAccessor.GetCurrentUserClaims();
                // TODO: Event will be create for this
                await _UnitOfWork.SchoolAdminUser.UpdateLoginState(user.Id, _AppContextAccessor.GetUserIP(), model.SchoolUniqueId);
                return new Response<bool>(true, user.FullName);
            }
            return new Response<bool>("Invalid Email or Password.");
        }

        public async Task<Response<bool>> AuthenticateStudentAsync(StudentLoginViewModel model)
        {

            // Validate SchoolUniqueId
            RegisteredSchool t = await _UnitOfWork.RegisteredSchool.GetActiveByUniqueIdAsync(model.SchoolUniqueId);
            if (t == null) return new Response<bool>("Invalid School Id");

            // Get the User from School database
            StudentLogin user = await _UnitOfWork.StudentLogin
                .FindActiveUserByUserEmailAsync(model.Email.ToUpper(), model.SchoolUniqueId);
            if (user == null) return new Response<bool>("Account does not exist with this email address.");


            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true

            user.SchoolUniqueId = model.SchoolUniqueId;
            user.SchoolName = t.Name;

            // Authorize User
            var result = await _SignInManagerStudent.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                await _UnitOfWork.StudentLogin.UpdateLoginState(user.Id, _AppContextAccessor.GetUserIP(), model.SchoolUniqueId);
                return new Response<bool>(true, user.UserName);
            }
            return new Response<bool>("Invalid Email or Password.");
        }

        public async Task<Response<bool>> AuthenticateSuperAdminAsync(SuperAdminLoginViewModel model)
        {
            SuperAdminUser user = await _UnitOfWork.SuperAdminUser
                .FindActiveUserByUserEmailAsync(model.Email.ToUpper());
            if (user == null) return new Response<bool>("Account does not exist with this email address.");
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            // var result = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            var result = await _SignInManagerSuperAdmin.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                await _UnitOfWork.SuperAdminUser.UpdateLoginState(user.Id, _AppContextAccessor.GetUserIP());
                return new Response<bool>(true, user.FirstName);
            }
            return new Response<bool>("Invalid Email or Password.");
        }


        public async Task<StudentLoginViewModel> BindStudentLoginModelAsync()
        {
            var allschool = await _UnitOfWork.RegisteredSchool.GetAllActiveAsync();

            return new StudentLoginViewModel()
            {
                ActiveSchools = allschool.Select(s => new DropDownItem { Text = s.Name, Value = s.UniqueId }).ToList()
            };
        }

        public async Task<StudentLoginViewModel> BindStudentLoginModelAsync(StudentLoginViewModel model)
        {
            var allschool = await _UnitOfWork.RegisteredSchool.GetAllActiveAsync();

            return new StudentLoginViewModel()
            {
                Email = model.Email,
                SchoolUniqueId = model.SchoolUniqueId,
                ActiveSchools = allschool.Select(s => new DropDownItem { Text = s.Name, Value = s.UniqueId }).ToList()
            };
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
                var SchoolId = _AppContextAccessor.ClaimSchoolUniqueId();
                var user = await _UnitOfWork.SchoolAdminUser.FindActiveByUserNameAsync(userName, SchoolId);
                roles_Access = user?.Roles?.Select(r => r.Access).ToList();
            }
            else
            {
                throw new Exception("not supported area.");
            }
            return roles_Access;
        }


        public async Task EnsureStudentLogoutAsync()
        {
            // Ensure all logout 
            await _SignInManagerStudent.SignOutAsync();
        }
        public async Task EnsureSchoolUserLogoutAsync()
        {
            // Ensure all logout 
            await _SignInManagerSchool.SignOutAsync();
        }
        public async Task EnsureSuperAdminLogoutAsync()
        {
            // Ensure all logout 
            await _SignInManagerSuperAdmin.SignOutAsync();
        }

        public async Task LogoutAll()
        {
            await _SignInManagerStudent.SignOutAsync();
            await _SignInManagerSchool.SignOutAsync();
            await _SignInManagerSuperAdmin.SignOutAsync();
        }
        public async Task SchoolAdminLogout() => await _SignInManagerSchool.SignOutAsync();
        public async Task SuperAdminLogout() => await _SignInManagerSuperAdmin.SignOutAsync();
        public async Task StudentLogout() => await _SignInManagerStudent.SignOutAsync();

        public bool CheckUserIdentityClaim()
        {
            var allClaims = _AppContextAccessor.GetCurrentUserClaims();
            var currentArea = _AppContextAccessor.GetRequestPath();
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
            return _UserManagerStudent.GetUserId(_AppContextAccessor.getCurrentUser());
        }
        public string GetLogedInSchoolAdminId()
        {
            return _UserManagerSchool.GetUserId(_AppContextAccessor.getCurrentUser());
        }
        public string GetLogedInSuperAdminId()
        {
            return _UserManagerSuperAdmin.GetUserId(_AppContextAccessor.getCurrentUser());
        }
    }
}
