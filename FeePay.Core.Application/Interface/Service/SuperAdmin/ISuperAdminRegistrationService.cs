using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Identity;

namespace FeePay.Core.Application.Interface.Service.SuperAdmin
{
    public interface ISuperAdminRegistrationService
    {
        Task<Response<bool>> RegisterUserAsync(SuperAdminUser model);
        Task<Response<bool>> UpdateUserAsync(SuperAdminUser model);
        Task<Response<bool>> UpdateUserNameAsync(SuperAdminUser model);
        Task Remove_AddPasswordAsync(SuperAdminUser user, string newPassword);
        SuperAdminUser GetNewHashPasswordAsync(SuperAdminUser user, string newPassword);

    }
}
