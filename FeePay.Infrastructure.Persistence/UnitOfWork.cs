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
        public UnitOfWork(ISuperAdminUserRepository SuperAdminUserRepository)
        {
            SuperAdminUser = SuperAdminUserRepository;
        }
        public ISuperAdminUserRepository SuperAdminUser { get; }
    }
}
