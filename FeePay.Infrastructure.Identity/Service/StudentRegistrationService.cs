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

namespace FeePay.Infrastructure.Identity.Service
{
    public class StudentRegistrationService : IStudentRegistrationService
    {
        public StudentRegistrationService(UserManager<StudentLogin> userManager, ILogger<StudentRegistrationService> logger
            //,IUnitOfWork unitOfWork, IAppContextAccessor appContextAccessor, ILoginService loginService, IMapper mapper
            )
        {
            //_appContextAccessor = appContextAccessor;
            //_loginService = loginService;
            //_unitOfWork = unitOfWork;
            //_mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }
        //private readonly IAppContextAccessor _appContextAccessor;
        //private readonly ILoginService _loginService;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;
        private readonly UserManager<StudentLogin> _userManager;
        private readonly ILogger<StudentRegistrationService> _logger;





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



    }
}
