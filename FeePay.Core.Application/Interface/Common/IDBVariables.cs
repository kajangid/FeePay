using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Common
{
    public interface IDBVariables
    {
        string SP_Add_SuperAdmin_User { get; }
        string SP_Delete_SuperAdmin_User { get; }
        string SP_Get_SuperAdmin_User { get; }
        string SP_GetAll_SuperAdmin_User { get; }
        string SP_Update_SuperAdmin_User { get; }
        string SP_Add_SuperAdmin_Role { get; }
        string SP_Delete_SuperAdmin_Role { get; }
        string SP_Get_SuperAdmin_Role { get; }
        string SP_GetAll_SuperAdmin_Role { get; }
        string SP_Update_SuperAdmin_Role { get; }
        string SP_Add_SuperAdmin_UserRole { get; }
        string SP_Delete_SuperAdmin_UserRole { get; }
        string SP_Get_SuperAdmin_UserRole { get; }
        string SP_Update_SuperAdmin_UserRole { get; }
        string SP_GetUserRoles_SuperAdmin { get; }
        string SP_Get_SuperAdmin_UsersInRole { get; }
        string SP_GetAll_SuperAdmin_UserRole { get; }
        string SP_AddLoginInfo_SuperAdmin { get; }
    }
}
