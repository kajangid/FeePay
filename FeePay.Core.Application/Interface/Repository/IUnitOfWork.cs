using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Repository.SuperAdmin;

namespace FeePay.Core.Application.Interface.Repository
{
    public interface IUnitOfWork
    {
        ISuperAdminUserRepository SuperAdminUser { get; }
        ISuperAdminRoleRepository SuperAdminRole { get; }
        ISuperAdminUserRoleRepository SuperAdminUserRole { get; }
    }
}
