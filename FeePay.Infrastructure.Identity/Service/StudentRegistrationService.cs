using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Domain.Entities.Identity;
using System.Transactions;
using FeePay.Core.Domain.Entities.Student;
using FeePay.Core.Application.UseCase;
using FeePay.Core.Application.Interface.Service.Student;
using FeePay.Core.Application.Exceptions;
using FeePay.Core.Application.Interface;

namespace FeePay.Infrastructure.Identity.Service
{
    public class StudentRegistrationService : IStudentRegistrationService
    {
        public StudentRegistrationService(
            ILogger<StudentRegistrationService> logger,
            UserManager<StudentLogin> userManager,
            IUnitOfWork unitOfWork,
            IAppContextAccessor appContextAccessor,
            ILoginService loginService,
            IMapper mapper,
            IPasswordHasher<StudentLogin> passwordHasher,
            IPasswordGenerator passwordGenerator
            )
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _appContextAccessor = appContextAccessor;
            _loginService = loginService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _passwordGenerator = passwordGenerator;
        }
        private readonly ILogger<StudentRegistrationService> _logger;
        private readonly UserManager<StudentLogin> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;
        public readonly IPasswordHasher<StudentLogin> _passwordHasher;
        public readonly IPasswordGenerator _passwordGenerator;





        public async Task<bool> AddStudentAsync(StudentAdmission studentAdmission)
        {
            var username = GeneratorToken.GenerateUserName(8);
            StudentLogin user = new StudentLogin()
            {
                NormalizedUserName = username.ToUpper(),
                UserName = username,
                Password = GeneratorToken.GeneratePassword(10),
                PhoneNumber = studentAdmission.MobileNo,
                Email = studentAdmission.StudentEmail,
                IsActive = true,
                StudentAdmission = studentAdmission
            };
            var result = await _userManager.CreateAsync(user, user.Password);
            if (!result.Succeeded)
            {
                _logger.LogError("Cannot create student login identity");
                return false;
            }
            return true;
        }



        public async Task ChangeStudentLoginPasswordAsync(StudentLogin user, string newPassword)
        {
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, newPassword);
        }
        public StudentLogin GetNewHashStudentLoginPasswordAsync(StudentLogin user, string newPassword)
        {
            var hasedPassword = _passwordHasher.HashPassword(user, newPassword);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.PasswordHash = hasedPassword;
            return user;
        }
    }
}
