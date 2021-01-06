using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Repository.School;
using FeePay.Core.Application.Interface.Repository.Student;
using FeePay.Core.Application.Interface.Repository.SuperAdmin;

namespace FeePay.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            ISuperAdminUserRepository SuperAdminUserRepository, ISuperAdminRoleRepository SuperAdminRoleRepository,
            ISuperAdminUserRoleRepository SuperAdminUserRoleRepository,
            ISchoolAdminUserRepository SchoolAdminUserRepository, ISchoolAdminRoleRepository SchoolAdminRoleRepository,
            ISchoolAdminUserRoleRepository SchoolAdminUserRoleRepository,
            IStudentLoginRepository StudentLoginRepository, IRegisteredSchoolRepository RegisteredSchoolRepository
            )
        {
            SuperAdminUser = SuperAdminUserRepository;
            SuperAdminRole = SuperAdminRoleRepository;
            SuperAdminUserRole = SuperAdminUserRoleRepository;

            SchoolAdminUser = SchoolAdminUserRepository;
            SchoolAdminRole = SchoolAdminRoleRepository;
            SchoolAdminUserRole = SchoolAdminUserRoleRepository;

            StudentLogin = StudentLoginRepository;
            RegisteredSchool = RegisteredSchoolRepository;


        }
        public ISuperAdminUserRepository SuperAdminUser { get; }
        public ISuperAdminRoleRepository SuperAdminRole { get; }
        public ISuperAdminUserRoleRepository SuperAdminUserRole { get; }

        public ISchoolAdminUserRepository SchoolAdminUser { get; }
        public ISchoolAdminRoleRepository SchoolAdminRole { get; }
        public ISchoolAdminUserRoleRepository SchoolAdminUserRole { get; }
        public IStudentLoginRepository StudentLogin { get; }
        public IRegisteredSchoolRepository RegisteredSchool { get; }
    }
}
