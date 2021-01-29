using FeePay.Core.Application.DTOs;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Service.School
{
    public interface ISchoolAdminRegistrationService
    {
        Task<bool> RegisterSchoolUserWithPhoneNumberAsync(StaffMemberViewModel model);
        Task<bool> UpdateSchoolUserWithPhoneNumberAsync(StaffMemberViewModel model);


        Task AssignRolesToSchoolUserAsync(SchoolAdminUser user, List<CheckBoxItem> roleList);
        Task AssignRolesToSchoolUserAsync(SchoolAdminRole role, List<CheckBoxItem> userList);


        SchoolAdminUser GetNewHashSchoolUserPasswordAsync(SchoolAdminUser user, string newPassword);
    }
}
