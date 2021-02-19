CREATE PROCEDURE [dbo].[SP_Get_SchoolAdmin_UsersInRole]
	@NormalizedRoleName NVARCHAR = NULL,
	@RoleId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF(@NormalizedRoleName IS NOT NULL)
	BEGIN
		SELECT [u].[Id], [u].[UserName], [u].[NormalizedUserName], [u].[Email], [u].[NormalizedEmail], [u].[EmailConfirmed],
			[u].[PasswordHash], [u].[PhoneNumber], [u].[PhoneNumberConfirmed], [u].[TwoFactorEnabled], [u].[LockoutEndDate],
			[u].[LockoutEnabled], [u].[AccessFailedCount], [u].[SecurityStamp], [u].[FirstName], [u].[LastName],
			[u].[FullName], [u].[LastLoginIP], [u].[LastLoginDate], [u].[IsActive], [u].[IsDelete],
			[u].[ModifyDate], [u].[ModifyBy], [u].[AddedDate], [u].[AddedBy] 
		FROM [dbo].[SchoolAdmin_User] [u] 
			INNER JOIN [dbo].[SchoolAdmin_UserRole] [ur] 
		ON [ur].[UserId] = [u].[Id] AND [ur].[IsDelete] = 0 AND [ur].[IsActive] = 1
			INNER JOIN [dbo].[SchoolAdmin_Role] [r] 
		ON [r].[Id] = [ur].[RoleId] AND [r].[IsDelete] = 0 AND [r].[IsActive] = 1
		WHERE 
			[r].[NormalizedName] = @NormalizedRoleName AND 
			[u].[IsDelete] = 0 AND
			[u].[IsActive] = 1

		RETURN
	END
	IF(@RoleId IS NOT NULL AND @RoleId != 0)
	BEGIN
		SELECT [u].[Id], [u].[UserName], [u].[NormalizedUserName], [u].[Email], [u].[NormalizedEmail], [u].[EmailConfirmed],
			[u].[PasswordHash], [u].[PhoneNumber], [u].[PhoneNumberConfirmed], [u].[TwoFactorEnabled], [u].[LockoutEndDate],
			[u].[LockoutEnabled], [u].[AccessFailedCount], [u].[SecurityStamp], [u].[FirstName], [u].[LastName],
			[u].[FullName], [u].[LastLoginIP], [u].[LastLoginDate], [u].[IsActive], [u].[IsDelete],
			[u].[ModifyDate], [u].[ModifyBy], [u].[AddedDate], [u].[AddedBy] 
		FROM [dbo].[SchoolAdmin_User] [u] 
			INNER JOIN [dbo].[SchoolAdmin_UserRole] [ur] 
		ON [ur].[UserId] = [u].[Id] AND [ur].[IsDelete] = 0 AND [ur].[IsActive] = 1
			INNER JOIN [dbo].[SchoolAdmin_Role] [r] 
		ON [r].[Id] = [ur].[RoleId] AND [r].[IsDelete] = 0 AND [r].[IsActive] = 1
		WHERE 
			[ur].[RoleId] = @RoleId AND 
			[u].[IsDelete] = 0 AND
			[u].[IsActive] = 1
		
	END	
RETURN 0
END
