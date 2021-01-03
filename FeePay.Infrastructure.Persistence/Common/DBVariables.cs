namespace FeePay.Infrastructure.Persistence.Common
{
    using FeePay.Core.Application.Interface.Common;
    public class DBVariables : IDBVariables
    {
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
    }
}
