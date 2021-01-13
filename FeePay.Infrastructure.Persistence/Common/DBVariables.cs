namespace FeePay.Infrastructure.Persistence.Common
{
    using FeePay.Core.Application.Interface.Common;
    public class DBVariables : IDBVariables
    {
        // super 
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



        // School
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
        public string SP_GetUserRoles_SchoolAdmin { get; } = "SP_GetUserRoles_SchoolAdmin";
        public string SP_Get_SchoolAdmin_UsersInRole { get; } = "SP_Get_SchoolAdmin_UsersInRole";
        public string SP_GetAll_SchoolAdmin_UserRole { get; } = "SP_GetAll_SchoolAdmin_UserRole";
        public string SP_AddLoginInfo_SchoolAdmin { get; } = "SP_AddLoginInfo_SchoolAdmin";
        
        public string SP_Add_RegisteredSchool { get; } = "SP_Add_RegisteredSchool";
        public string SP_Update_RegisteredSchool { get; } = "SP_Update_RegisteredSchool";
        public string SP_Delete_RegisteredSchool { get; } = "SP_Delete_RegisteredSchool";
        public string SP_Get_RegisteredSchool { get; } = "SP_Get_RegisteredSchool";
        public string SP_GetAll_RegisteredSchool { get; } = "SP_GetAll_RegisteredSchool";





        //Student
        public string SP_Add_StudentLogin { get; } = "SP_Add_StudentLogin";
        public string SP_Delete_StudentLogin { get; } = "SP_Delete_StudentLogin";
        public string SP_Get_StudentLogin { get; } = "SP_Get_StudentLogin";
        public string SP_GetAll_StudentLogin { get; } = "SP_GetAll_StudentLogin";
        public string SP_Update_StudentLogin { get; } = "SP_Update_StudentLogin";
        public string SP_AddLoginInfo_StudentLogin { get; } = "SP_AddLoginInfo_StudentLogin";

    }
}
