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



        //School 
        string SP_Add_SchoolAdmin_User { get; }
        string SP_Delete_SchoolAdmin_User { get; }
        string SP_Get_SchoolAdmin_User { get; }
        string SP_GetAll_SchoolAdmin_User { get; }
        string SP_Update_SchoolAdmin_User { get; }
        string SP_Add_SchoolAdmin_Role { get; }
        string SP_Delete_SchoolAdmin_Role { get; }
        string SP_Get_SchoolAdmin_Role { get; }
        string SP_GetAll_SchoolAdmin_Role { get; }
        string SP_Update_SchoolAdmin_Role { get; }
        string SP_Add_SchoolAdmin_UserRole { get; }
        string SP_Delete_SchoolAdmin_UserRole { get; }
        string SP_Get_SchoolAdmin_UserRole { get; }
        string SP_Update_SchoolAdmin_UserRole { get; }
        string SP_GetUserRoles_SchoolAdmin { get; }
        string SP_Get_SchoolAdmin_UsersInRole { get; }
        string SP_GetAll_SchoolAdmin_UserRole { get; }
        string SP_AddLoginInfo_SchoolAdmin { get; }
        string SP_Add_RegisteredSchool { get; }
        string SP_Update_RegisteredSchool { get; }
        string SP_Delete_RegisteredSchool { get; }
        string SP_Get_RegisteredSchool { get; }
        string SP_GetAll_RegisteredSchool { get; }

        //Student

        string SP_Add_StudentLogin { get; }
        string SP_Delete_StudentLogin { get; }
        string SP_Get_StudentLogin { get; }
        string SP_GetAll_StudentLogin { get; }
        string SP_Update_StudentLogin { get; }
        string SP_AddLoginInfo_StudentLogin { get; }
    }
}
