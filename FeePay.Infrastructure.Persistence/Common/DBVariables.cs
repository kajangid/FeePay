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
    }
}
