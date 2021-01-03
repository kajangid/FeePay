CREATE PROCEDURE [dbo].[SP_Get_SuperAdmin_UsersInRole]
	@NormalizedRoleName NVARCHAR 
AS
BEGIN

	SELECT [u].[Id], [u].[UserName], [u].[NormalizedUserName], [u].[Email], [u].[NormalizedEmail], [u].[EmailConfirmed],
	[u].[PasswordHash], [u].[PhoneNumber], [u].[PhoneNumberConfirmed], [u].[TwoFactorEnabled], [u].[LockoutEndDate],
	[u].[LockoutEnabled], [u].[AccessFailedCount], [u].[SecurityStamp], [u].[FirstName], [u].[LastName],
	[u].[FullName], [u].[Photo], [u].[City], [u].[LastLoginIP], [u].[LastLoginDate], [u].[IsActive], [u].[IsDelete],
	[u].[ModifyDate], [u].[ModifyBy], [u].[AddedDate], [u].[AddedBy] FROM [dbo].[SuperAdmin_User] u 
	INNER JOIN [dbo].[SuperAdmin_UserRole] ur 
	ON ur.[UserId] = u.[Id] 
	INNER JOIN [dbo].[SuperAdmin_Role] r 
	ON r.[Id] = ur.[RoleId] 
	WHERE 
	r.[NormalizedName] = @NormalizedRoleName

END
RETURN 0
