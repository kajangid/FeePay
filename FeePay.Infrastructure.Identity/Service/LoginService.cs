using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.School;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Application.Interface.Service;
using Microsoft.AspNetCore.Http;
using FeePay.Core.Domain.Entities.Common;

namespace FeePay.Infrastructure.Identity.Service
{
    public class LoginService : ILoginService
    {
        public LoginService(SignInManager<SchoolAdminUser> SignInManagerSchool, UserManager<SchoolAdminUser> UserManagerSchool,
             IHttpContextAccessor httpContentAccessor, IUnitOfWork UnitOfWork, IAppContextAccessor appContextAccessor,
            SignInManager<StudentLogin> SignInManagerStudent, UserManager<StudentLogin> UserManagerStudent,
            SignInManager<SuperAdminUser> SignInManagerSuperAdmin, UserManager<SuperAdminUser> UserManagerSuperAdmin)
        {
            _SignInManagerSchool = SignInManagerSchool;
            _UserManagerSchool = UserManagerSchool;
            _SignInManagerStudent = SignInManagerStudent;
            _UserManagerStudent = UserManagerStudent;
            _SignInManagerSuperAdmin = SignInManagerSuperAdmin;
            _UserManagerSuperAdmin = UserManagerSuperAdmin;
            _UnitOfWork = UnitOfWork;
            _HttpContext = httpContentAccessor.HttpContext;
            _AppContextAccessor = appContextAccessor;
        }
        private readonly IAppContextAccessor _AppContextAccessor;
        private readonly SignInManager<SchoolAdminUser> _SignInManagerSchool;
        private readonly UserManager<SchoolAdminUser> _UserManagerSchool;
        private readonly SignInManager<StudentLogin> _SignInManagerStudent;
        private readonly UserManager<StudentLogin> _UserManagerStudent;
        private readonly SignInManager<SuperAdminUser> _SignInManagerSuperAdmin;
        private readonly UserManager<SuperAdminUser> _UserManagerSuperAdmin;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly HttpContext _HttpContext;


        public async Task<Response<bool>> AuthenticateSchoolUserAsync(SchoolLoginViewModel model)
        {
            // Validate SchoolUniqueId
            RegisteredSchool t = await _UnitOfWork.RegisteredSchool.GetActiveByUniqueIdAsync(model.SchoolUniqueId);
            if (t == null) return new Response<bool>("Invalid School Id");

            // Get the User from School database
            SchoolAdminUser user = await _UnitOfWork.SchoolAdminUser
                .FindActiveUserByUserEmailAsync(model.Email.ToUpper(), model.SchoolUniqueId);

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true

            user.SchoolUniqueId = model.SchoolUniqueId;
            user.SchoolName = t.Name;

            // Authorize User
            var result = await _SignInManagerSchool.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // TODO: Clear Claim Bug
                // Identity state does not update with claim factory in this request 
                // Check for new request to get all the claim
                var clam = _AppContextAccessor.GetCurrentUserClaims();
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
                return new Response<bool>(true, user.FirstName);
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
            //var result = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
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
            var allschool = await _UnitOfWork.RegisteredSchool.GetAllActiveSchoolAsync();

            return new StudentLoginViewModel()
            {
                ActiveSchools = allschool.Select(s => new DropDownItem { Text = s.Name, Value = s.UniqueId }).ToList()
            };
        }
        public async Task<StudentLoginViewModel> BindStudentLoginModelAsync(StudentLoginViewModel model)
        {
            var allschool = await _UnitOfWork.RegisteredSchool.GetAllActiveSchoolAsync();

            return new StudentLoginViewModel()
            {
                Email = model.Email,
                SchoolUniqueId = model.SchoolUniqueId,
                ActiveSchools = allschool.Select(s => new DropDownItem { Text = s.Name, Value = s.UniqueId }).ToList()
            };
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
    }
}
