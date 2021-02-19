-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get school user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_SchoolAdmin_User]
(
@Id INT = NULL
,@UserName NVARCHAR(256) = NULL
,@NormalizedUserName NVARCHAR(256) = NULL
,@Email NVARCHAR(256) = NULL
,@NormalizedEmail NVARCHAR(256) = NULL
,@PhoneNumber NVARCHAR(50) = NULL
,@IsActive BIT = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
	IF(@Id IS NOT NULL AND @Id != 0)
	BEGIN
		SELECT 
		[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
		, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
		, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
		, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive], [a].[PasswordHash]
		, [a].[AddedBy], [a].[ModifyBy]

		, [t3].[Id],[t3].[Name],[t3].[NormalizedName],[t3].[Access]

		FROM [dbo].[SchoolAdmin_User] [a] 

		OUTER APPLY (
			SELECT [r].[Id],[r].[Name],[r].[NormalizedName] ,[r].[Access] 
			FROM [dbo].[SchoolAdmin_UserRole] [ur] 
			LEFT JOIN [dbo].[SchoolAdmin_Role] [r]
			ON [r].[Id] = [ur].[RoleId] AND [r].[IsDelete] = 0 AND [r].[IsActive] = 1
			WHERE [ur].[UserId] = [a].[Id] AND [ur].[IsDelete] = 0 AND [ur].[IsActive] = 1) [t3]

		WHERE
		[a].[IsDelete] = 0
		AND [a].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [a].[IsActive] END
		AND [a].[Id] = @Id 

		RETURN 
	END
	IF(@UserName IS NOT NULL)
	BEGIN
		SELECT 
		[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
		, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
		, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
		, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive], [a].[PasswordHash]
		, [a].[AddedBy], [a].[ModifyBy]

		, [t3].[Id],[t3].[Name],[t3].[NormalizedName],[t3].[Access]

		FROM [dbo].[SchoolAdmin_User] [a] 

		OUTER APPLY (
			SELECT [r].[Id],[r].[Name],[r].[NormalizedName] ,[r].[Access] 
			FROM [dbo].[SchoolAdmin_UserRole] [ur] 
			LEFT JOIN [dbo].[SchoolAdmin_Role] [r]
			ON [r].[Id] = [ur].[RoleId] AND [r].[IsDelete] = 0 AND [r].[IsActive] = 1
			WHERE [ur].[UserId] = [a].[Id] AND [ur].[IsDelete] = 0 AND [ur].[IsActive] = 1) [t3]

		WHERE
		[a].[IsDelete] = 0
		AND [a].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [a].[IsActive] END
		AND [a].[UserName] = @UserName 

		RETURN 
	END
	IF(@NormalizedUserName IS NOT NULL)
	BEGIN
		SELECT 
		[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
		, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
		, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
		, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive], [a].[PasswordHash]
		, [a].[AddedBy], [a].[ModifyBy]

		, [t3].[Id],[t3].[Name],[t3].[NormalizedName],[t3].[Access]

		FROM [dbo].[SchoolAdmin_User] [a] 

		OUTER APPLY (
			SELECT [r].[Id],[r].[Name],[r].[NormalizedName] ,[r].[Access] 
			FROM [dbo].[SchoolAdmin_UserRole] [ur] 
			LEFT JOIN [dbo].[SchoolAdmin_Role] [r]
			ON [r].[Id] = [ur].[RoleId] AND [r].[IsDelete] = 0 AND [r].[IsActive] = 1
			WHERE [ur].[UserId] = [a].[Id] AND [ur].[IsDelete] = 0 AND [ur].[IsActive] = 1) [t3]

		WHERE
		[a].[IsDelete] = 0
		AND [a].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [a].[IsActive] END
		AND [a].[NormalizedUserName] = @NormalizedUserName  

		RETURN 
	END
	IF(@Email IS NOT NULL)
	BEGIN
		SELECT 
		[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
		, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
		, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
		, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive], [a].[PasswordHash]
		, [a].[AddedBy], [a].[ModifyBy]

		, [t3].[Id],[t3].[Name],[t3].[NormalizedName],[t3].[Access]

		FROM [dbo].[SchoolAdmin_User] [a] 

		OUTER APPLY (
			SELECT [r].[Id],[r].[Name],[r].[NormalizedName] ,[r].[Access] 
			FROM [dbo].[SchoolAdmin_UserRole] [ur] 
			LEFT JOIN [dbo].[SchoolAdmin_Role] [r]
			ON [r].[Id] = [ur].[RoleId] AND [r].[IsDelete] = 0 AND [r].[IsActive] = 1
			WHERE [ur].[UserId] = [a].[Id] AND [ur].[IsDelete] = 0 AND [ur].[IsActive] = 1) [t3]

		WHERE
		[a].[IsDelete] = 0
		AND [a].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [a].[IsActive] END
		AND [a].[Email] = @Email 

		RETURN 
	END
	IF(@NormalizedEmail IS NOT NULL)
	BEGIN
		SELECT 
		[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
		, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
		, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
		, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive], [a].[PasswordHash]
		, [a].[AddedBy], [a].[ModifyBy]

		, [t3].[Id],[t3].[Name],[t3].[NormalizedName],[t3].[Access]

		FROM [dbo].[SchoolAdmin_User] [a] 

		OUTER APPLY (
			SELECT [r].[Id],[r].[Name],[r].[NormalizedName],[r].[Access] 
			FROM [dbo].[SchoolAdmin_UserRole] [ur] 
			LEFT JOIN [dbo].[SchoolAdmin_Role] [r]
			ON [r].[Id] = [ur].[RoleId] AND [r].[IsDelete] = 0 AND [r].[IsActive] = 1
			WHERE [ur].[UserId] = [a].[Id] AND [ur].[IsDelete] = 0 AND [ur].[IsActive] = 1) [t3]

		WHERE
		[a].[IsDelete] = 0
		AND [a].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [a].[IsActive] END
		AND [a].[NormalizedEmail] = @NormalizedEmail 

		RETURN 
	END
	IF(@PhoneNumber IS NOT NULL)
	BEGIN
		SELECT 
		[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
		, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
		, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
		, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive], [a].[PasswordHash]
		, [a].[AddedBy], [a].[ModifyBy]

		, [t3].[Id],[t3].[Name],[t3].[NormalizedName],[t3].[Access]

		FROM [dbo].[SchoolAdmin_User] [a] 

		OUTER APPLY (
			SELECT [r].[Id],[r].[Name],[r].[NormalizedName],[r].[Access] 
			FROM [dbo].[SchoolAdmin_UserRole] [ur] 
			LEFT JOIN [dbo].[SchoolAdmin_Role] [r]
			ON [r].[Id] = [ur].[RoleId] AND [r].[IsDelete] = 0 AND [r].[IsActive] = 1
			WHERE [ur].[UserId] = [a].[Id] AND [ur].[IsDelete] = 0 AND [ur].[IsActive] = 1) [t3]

		WHERE
		[a].[IsDelete] = 0
		AND [a].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [a].[IsActive] END
		AND [a].[PhoneNumber] = @PhoneNumber 

		RETURN 
	END
END