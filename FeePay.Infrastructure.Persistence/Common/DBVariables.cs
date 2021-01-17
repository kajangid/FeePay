namespace FeePay.Infrastructure.Persistence.Common
{
    using FeePay.Core.Application.Interface.Common;
    public class DBVariables : IDBVariables
    {
        #region SUPER ADMIN IDENTITY
        public string SP_Add_SuperAdmin_User { get; } = "SP_Add_SuperAdmin_User";
        public string SP_Delete_SuperAdmin_User { get; } = "SP_Delete_SuperAdmin_User";
        public string SP_Get_SuperAdmin_User { get; } = "SP_Get_SuperAdmin_User";
        public string SP_GetAll_SuperAdmin_User { get; } = "SP_GetAll_SuperAdmin_User";
        public string SP_Update_SuperAdmin_User { get; } = "SP_Update_SuperAdmin_User";
        public string SP_Add_SuperAdmin_Role { get; } = "SP_Add_SuperAdmin_Role";
        public string SP_Delete_SuperAdmin_Role { get; } = "SP_Delete_SuperAdmin_Role";
        public string SP_Get_SuperAdmin_Role { get; } = "SP_Get_SuperAdmin_Role";
        public string SP_GetAll_SuperAdmin_Role { get; } = "SP_GetAll_SuperAdmin_Role";
        public string SP_Update_SuperAdmin_Role { get; } = "SP_Update_SuperAdmin_Role";
        public string SP_Add_SuperAdmin_UserRole { get; } = "SP_Add_SuperAdmin_UserRole";
        public string SP_Delete_SuperAdmin_UserRole { get; } = "SP_Delete_SuperAdmin_UserRole";
        public string SP_Get_SuperAdmin_UserRole { get; } = "SP_Get_SuperAdmin_UserRole";
        public string SP_Update_SuperAdmin_UserRole { get; } = "SP_Update_SuperAdmin_UserRole";
        public string SP_GetUserRoles_SuperAdmin { get; } = "SP_GetUserRoles_SuperAdmin";
        public string SP_Get_SuperAdmin_UsersInRole { get; } = "SP_Get_SuperAdmin_UsersInRole";
        public string SP_GetAll_SuperAdmin_UserRole { get; } = "SP_GetAll_SuperAdmin_UserRole";
        public string SP_AddLoginInfo_SuperAdmin { get; } = "SP_AddLoginInfo_SuperAdmin";
        #endregion


        #region SCHOOL ADMIN IDENTITY
        public string SP_Add_SchoolAdmin_User { get; } = "SP_Add_SchoolAdmin_User";
        public string SP_Delete_SchoolAdmin_User { get; } = "SP_Delete_SchoolAdmin_User";
        public string SP_Get_SchoolAdmin_User { get; } = "SP_Get_SchoolAdmin_User";
        public string SP_GetAll_SchoolAdmin_User { get; } = "SP_GetAll_SchoolAdmin_User";
        public string SP_Get_SchoolAdmin_User_With_AddEditUser { get; } = "SP_Get_SchoolAdmin_User_AddEditUser";
        public string SP_GetAll_SchoolAdmin_User_With_AddEditUser { get; } = "SP_GetAll_SchoolAdmin_User_AddEditUser";
        public string SP_Update_SchoolAdmin_User { get; } = "SP_Update_SchoolAdmin_User";
        public string SP_Add_SchoolAdmin_Role { get; } = "SP_Add_SchoolAdmin_Role";
        public string SP_Delete_SchoolAdmin_Role { get; } = "SP_Delete_SchoolAdmin_Role";
        public string SP_Get_SchoolAdmin_Role { get; } = "SP_Get_SchoolAdmin_Role";
        public string SP_GetAll_SchoolAdmin_Role { get; } = "SP_GetAll_SchoolAdmin_Role";
        public string SP_Get_SchoolAdmin_Role_With_AddEditUser { get; } = "SP_Get_SchoolAdmin_Role_AddEditUser";
        public string SP_GetAll_SchoolAdmin_Role_With_AddEditUser { get; } = "SP_GetAll_SchoolAdmin_Role_AddEditUser";
        public string SP_Update_SchoolAdmin_Role { get; } = "SP_Update_SchoolAdmin_Role";
        public string SP_Add_SchoolAdmin_UserRole { get; } = "SP_Add_SchoolAdmin_UserRole";
        public string SP_Delete_SchoolAdmin_UserRole { get; } = "SP_Delete_SchoolAdmin_UserRole";
        public string SP_Get_SchoolAdmin_UserRole { get; } = "SP_Get_SchoolAdmin_UserRole";
        public string SP_Update_SchoolAdmin_UserRole { get; } = "SP_Update_SchoolAdmin_UserRole";
        public string SP_Get_SchoolAdmin_UserRoles { get; } = "SP_Get_SchoolAdmin_UserRoles";
        public string SP_Get_SchoolAdmin_UsersInRole { get; } = "SP_Get_SchoolAdmin_UsersInRole";
        public string SP_GetAll_SchoolAdmin_UserRole { get; } = "SP_GetAll_SchoolAdmin_UserRole";
        public string SP_Add_SchoolAdmin_User_LoginInfo { get; } = "SP_Add_SchoolAdmin_User_LoginInfo";
        #endregion


        #region STUDENT LOGIN IDENTITY
        public string SP_Add_StudentLogin { get; } = "SP_Add_StudentLogin";
        public string SP_Delete_StudentLogin { get; } = "SP_Delete_StudentLogin";
        public string SP_Get_StudentLogin { get; } = "SP_Get_StudentLogin";
        public string SP_GetAll_StudentLogin { get; } = "SP_GetAll_StudentLogin";
        public string SP_Update_StudentLogin { get; } = "SP_Update_StudentLogin";
        public string SP_Add_StudentLogin_LoginInfo { get; } = "SP_Add_StudentLogin_LoginInfo";
        #endregion


        #region REGISTER SCHOOL 
        public string SP_Add_RegisteredSchool { get; } = "SP_Add_RegisteredSchool";
        public string SP_Update_RegisteredSchool { get; } = "SP_Update_RegisteredSchool";
        public string SP_Delete_RegisteredSchool { get; } = "SP_Delete_RegisteredSchool";
        public string SP_Get_RegisteredSchool { get; } = "SP_Get_RegisteredSchool";
        public string SP_GetAll_RegisteredSchool { get; } = "SP_GetAll_RegisteredSchool";
        #endregion


        #region FEE TYPE 
        public string SP_Add_FeeType { get; } = "SP_Add_FeeType";
        public string SP_Update_FeeType { get; } = "SP_Update_FeeType";
        public string SP_Delete_FeeType { get; } = "SP_Delete_FeeType";
        public string SP_Get_FeeType { get; } = "SP_Get_FeeType";
        public string SP_GetAll_FeeType { get; } = "SP_GetAll_FeeType";
        public string SP_Get_FeeType_AddEditUser { get; } = "SP_Get_FeeType_AddEditUser";
        public string SP_GetAll_FeeType_AddEditUser { get; } = "SP_GetAll_FeeType_AddEditUser";
        #endregion


        #region FEE GROUP
        public string SP_Add_FeeGroup { get; } = "SP_Add_FeeGroup";
        public string SP_Update_FeeGroup { get; } = "SP_Update_FeeGroup";
        public string SP_Delete_FeeGroup { get; } = "SP_Delete_FeeGroup";
        public string SP_Get_FeeGroup { get; } = "SP_Get_FeeGroup";
        public string SP_GetAll_FeeGroup { get; } = "SP_GetAll_FeeGroup";
        public string SP_Get_FeeGroup_AddEditUser { get; } = "SP_Get_FeeGroup_AddEditUser";
        public string SP_GetAll_FeeGroup_AddEditUser { get; } = "SP_GetAll_FeeGroup_AddEditUser";
        #endregion


        #region FEE MASTER
        public string SP_Add_FeeMaster { get; } = "SP_Add_FeeMaster";
        public string SP_Update_FeeMaster { get; } = "SP_Update_FeeMaster";
        public string SP_Delete_FeeMaster { get; } = "SP_Delete_FeeMaster";
        public string SP_Get_FeeMaster { get; } = "SP_Get_FeeMaster";
        public string SP_GetAll_FeeMaster { get; } = "SP_GetAll_FeeMaster";
        public string SP_Get_FeeMaster_AddEditUser { get; } = "SP_Get_FeeMaster_AddEditUser";
        public string SP_GetAll_FeeMaster_AddEditUser { get; } = "SP_GetAll_FeeMaster_AddEditUser";
        #endregion



    }
}
