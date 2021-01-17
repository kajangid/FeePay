using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Common
{
    public interface IDBVariables
    {
        #region SUPER ADMIN IDENTITY
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
        #endregion


        #region SCHOOL ADMIN IDENTITY
        string SP_Add_SchoolAdmin_User { get; }
        string SP_Delete_SchoolAdmin_User { get; }
        string SP_Get_SchoolAdmin_User { get; }
        string SP_GetAll_SchoolAdmin_User { get; }
        string SP_Get_SchoolAdmin_User_With_AddEditUser { get; }
        string SP_GetAll_SchoolAdmin_User_With_AddEditUser { get; }
        string SP_Update_SchoolAdmin_User { get; }
        string SP_Add_SchoolAdmin_Role { get; }
        string SP_Delete_SchoolAdmin_Role { get; }
        string SP_Get_SchoolAdmin_Role { get; }
        string SP_GetAll_SchoolAdmin_Role { get; }
        string SP_Get_SchoolAdmin_Role_With_AddEditUser { get; }
        string SP_GetAll_SchoolAdmin_Role_With_AddEditUser { get; }
        string SP_Update_SchoolAdmin_Role { get; }
        string SP_Add_SchoolAdmin_UserRole { get; }
        string SP_Delete_SchoolAdmin_UserRole { get; }
        string SP_Get_SchoolAdmin_UserRole { get; }
        string SP_Update_SchoolAdmin_UserRole { get; }
        string SP_Get_SchoolAdmin_UserRoles { get; }
        string SP_Get_SchoolAdmin_UsersInRole { get; }
        string SP_GetAll_SchoolAdmin_UserRole { get; }
        string SP_Add_SchoolAdmin_User_LoginInfo { get; }
        #endregion


        #region STUDENT LOGIN IDENTITY
        string SP_Add_StudentLogin { get; }
        string SP_Update_StudentLogin { get; }
        string SP_Delete_StudentLogin { get; }
        string SP_Get_StudentLogin { get; }
        string SP_GetAll_StudentLogin { get; }
        string SP_Add_StudentLogin_LoginInfo { get; }
        #endregion


        #region REGISTER SCHOOL 
        string SP_Add_RegisteredSchool { get; }
        string SP_Update_RegisteredSchool { get; }
        string SP_Delete_RegisteredSchool { get; }
        string SP_Get_RegisteredSchool { get; }
        string SP_GetAll_RegisteredSchool { get; }
        #endregion


        #region FEE TYPE 
        public string SP_Add_FeeType { get; }
        public string SP_Update_FeeType { get; }
        public string SP_Delete_FeeType { get; }
        public string SP_Get_FeeType { get; }
        public string SP_GetAll_FeeType { get; }
        public string SP_Get_FeeType_AddEditUser { get; }
        public string SP_GetAll_FeeType_AddEditUser { get; }
        #endregion


        #region FEE GROUP
        public string SP_Add_FeeGroup { get; }
        public string SP_Update_FeeGroup { get; }
        public string SP_Delete_FeeGroup { get; }
        public string SP_Get_FeeGroup { get; }
        public string SP_GetAll_FeeGroup { get; }
        public string SP_Get_FeeGroup_AddEditUser { get; }
        public string SP_GetAll_FeeGroup_AddEditUser { get; }
        #endregion


        #region FEE MASTER
        public string SP_Add_FeeMaster { get; }
        public string SP_Update_FeeMaster { get; }
        public string SP_Delete_FeeMaster { get; }
        public string SP_Get_FeeMaster { get; }
        public string SP_GetAll_FeeMaster { get; }
        public string SP_Get_FeeMaster_AddEditUser { get; }
        public string SP_GetAll_FeeMaster_AddEditUser { get; }
        #endregion



    }
}
