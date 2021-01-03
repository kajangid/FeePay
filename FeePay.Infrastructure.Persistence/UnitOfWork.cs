using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Repository.SuperAdmin;

namespace FeePay.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ISuperAdminUserRepository SuperAdminUserRepository, ISuperAdminRoleRepository SuperAdminRoleRepository,
            ISuperAdminUserRoleRepository superAdminUserRoleRepository)
        {
            SuperAdminUser = SuperAdminUserRepository;
            SuperAdminRole = SuperAdminRoleRepository;
            SuperAdminUserRole = superAdminUserRoleRepository;
        }
        public ISuperAdminUserRepository SuperAdminUser { get; }
        public ISuperAdminRoleRepository SuperAdminRole { get; }
        public ISuperAdminUserRoleRepository SuperAdminUserRole { get; }
    }
}
