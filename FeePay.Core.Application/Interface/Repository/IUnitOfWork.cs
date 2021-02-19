using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Repository.School;
using FeePay.Core.Application.Interface.Repository.Student;
using FeePay.Core.Application.Interface.Repository.SuperAdmin;

namespace FeePay.Core.Application.Interface.Repository
{
    public interface IUnitOfWork
    {
        ISuperAdminUserRepository SuperAdminUser { get; }
        ISuperAdminRoleRepository SuperAdminRole { get; }
        ISuperAdminUserRoleRepository SuperAdminUserRole { get; }
        ISchoolAdminUserRepository SchoolAdminUser { get; }
        ISchoolAdminRoleRepository SchoolAdminRole { get; }
        ISchoolAdminUserRoleRepository SchoolAdminUserRole { get; }
        IStudentLoginRepository StudentLogin { get; }
        IRegisteredSchoolRepository RegisteredSchool { get; }
        IFeeTypeRepository FeeType { get; }
        IFeeGroupRepository FeeGroup { get; }
        IFeeMasterRespository FeeMaster { get; }
        IClassRepository ClassRepo { get; }
        ISectionRepository SectionRepo { get; }
        IClassSectionRepository ClassSection { get; }
        ISessionRepository Session { get; }
        IStudentAdmisionRepository StudentAdmision { get; }
        ICityStateRepository CityState { get; }
        IStudentFeesRepository StudentFee { get; }
        IFeesTranscationRepository FeesTranscation { get; }
        IStudentAcademicSessionsRepository StudentAcademicSessions { get; }
        IDocumentsRepository Documents { get; }
        IPaymentGatewayDocumentRepository PaymentGatewayDocument { get; }
    }
}
