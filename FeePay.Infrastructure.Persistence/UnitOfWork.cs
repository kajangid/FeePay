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
            IStudentLoginRepository StudentLoginRepository, IRegisteredSchoolRepository RegisteredSchoolRepository,
            IFeeTypeRepository feeTypeRepository, IFeeGroupRepository feeGroupRepository, IFeeMasterRespository feeMasterRespository,
            IClassRepository classRepository, ISectionRepository sectionRepository, IClassSectionRepository classSectionRepository,
            ISessionRepository sessionRepository, IStudentAdmisionRepository studentAdmisionRepository, ICityStateRepository cityStateRepository,
            IStudentFeesRepository studentFeeRepository, IFeesTranscationRepository feesTranscationRepository)
        {
            SuperAdminUser = SuperAdminUserRepository;
            SuperAdminRole = SuperAdminRoleRepository;
            SuperAdminUserRole = SuperAdminUserRoleRepository;

            SchoolAdminUser = SchoolAdminUserRepository;
            SchoolAdminRole = SchoolAdminRoleRepository;
            SchoolAdminUserRole = SchoolAdminUserRoleRepository;

            StudentLogin = StudentLoginRepository;
            RegisteredSchool = RegisteredSchoolRepository;

            FeeType = feeTypeRepository;
            FeeGroup = feeGroupRepository;
            FeeMaster = feeMasterRespository;

            ClassRepo = classRepository;
            SectionRepo = sectionRepository;
            ClassSection = classSectionRepository;
            Session = sessionRepository;

            StudentAdmision = studentAdmisionRepository;
            CityState = cityStateRepository;
            StudentFee = studentFeeRepository;
            FeesTranscation = feesTranscationRepository;
        }
        public ISuperAdminUserRepository SuperAdminUser { get; }
        public ISuperAdminRoleRepository SuperAdminRole { get; }
        public ISuperAdminUserRoleRepository SuperAdminUserRole { get; }

        public ISchoolAdminUserRepository SchoolAdminUser { get; }
        public ISchoolAdminRoleRepository SchoolAdminRole { get; }
        public ISchoolAdminUserRoleRepository SchoolAdminUserRole { get; }
        public IStudentLoginRepository StudentLogin { get; }
        public IRegisteredSchoolRepository RegisteredSchool { get; }

        public IFeeTypeRepository FeeType { get; }
        public IFeeGroupRepository FeeGroup { get; }
        public IFeeMasterRespository FeeMaster { get; }

        public IClassRepository ClassRepo { get; }
        public ISectionRepository SectionRepo { get; }
        public IClassSectionRepository ClassSection { get; }
        public ISessionRepository Session { get; }

        public IStudentAdmisionRepository StudentAdmision { get; }
        public ICityStateRepository CityState { get; }
        public IStudentFeesRepository StudentFee { get; }
        public IFeesTranscationRepository FeesTranscation { get; }
    }
}
