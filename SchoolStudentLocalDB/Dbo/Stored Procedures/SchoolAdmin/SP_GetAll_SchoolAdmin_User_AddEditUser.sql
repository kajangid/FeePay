-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get all school user list data with modifyby and addedby user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAll_SchoolAdmin_User_AddEditUser] 
(
@Id INT = NULL
,@UserName NVARCHAR(256) = NULL
,@NormalizedUserName NVARCHAR(256) = NULL
,@Email NVARCHAR(256) = NULL
,@NormalizedEmail NVARCHAR(256) = NULL
,@PhoneNumber NVARCHAR(50) = NULL
,@IsActive BIT = NULL
,@ModifyBy INT = NULL
,@AddedBy INT = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
		, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
		, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
		, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive]

		, [t3].[Id],[t3].[Name],[t3].[NormalizedName],[t3].[Access] --  <-------- Role
		, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
		, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user

		FROM [dbo].[SchoolAdmin_User] [a] 

		OUTER APPLY (
			SELECT [r].[Id],[r].[Name],[r].[NormalizedName],[r].[Access]
			FROM [dbo].[SchoolAdmin_UserRole] [ur] 
			LEFT JOIN [dbo].[SchoolAdmin_Role] [r]
			ON [r].[Id] = [ur].[RoleId] AND [r].[IsDelete] = 0 AND [r].[IsActive] = 1
			WHERE [ur].[UserId] = [a].[Id] AND [ur].[IsDelete] = 0 AND [ur].[IsActive] = 1) [t3]

		OUTER APPLY (
			SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
			FROM [dbo].[SchoolAdmin_User] [ab]
			WHERE [ab].[Id] = [a].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]

		OUTER APPLY (
			SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
			FROM [dbo].[SchoolAdmin_User] [mb]
			WHERE [mb].[Id] = [a].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]

		WHERE
		[a].[IsDelete] = 0 AND
		[a].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN  @IsActive ELSE [a].[IsActive] END 




	
	--SELECT 
	--	[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
	--	, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
	--	, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
	--	, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive]
	--	, [a].[AddedBy] AS [Id]
	--	, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [a].[AddedBy] AND [IsDelete] = 0) AS [FullName]
	--	, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [a].[AddedBy] AND [IsDelete] = 0) AS [Email]
	--	, [a].[ModifyBy] AS [Id]
	--	, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [a].[ModifyBy] AND [IsDelete] = 0) AS [FullName]
	--	, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [a].[ModifyBy] AND [IsDelete] = 0) AS [Email]
	--	FROM [SchoolAdmin_User] [a]
	--	WHERE
	--	[a].[IsDelete] = 0 OR
	--	[a].[Id] = CASE WHEN @Id IS NOT NULL AND @Id != 0 THEN  @Id ELSE [a].[Id] END OR
	--	[a].[UserName] = CASE WHEN @UserName IS NOT NULL THEN  @UserName ELSE [a].[UserName] END OR
	--	[a].[NormalizedUserName] = CASE WHEN @NormalizedUserName IS NOT NULL THEN  @NormalizedUserName ELSE [a].[NormalizedUserName] END OR
	--	[a].[PhoneNumber] = CASE WHEN @PhoneNumber IS NOT NULL THEN  @PhoneNumber ELSE [a].[PhoneNumber] END OR
	--	[a].[NormalizedEmail] = CASE WHEN @NormalizedEmail IS NOT NULL THEN  @NormalizedEmail ELSE [a].[NormalizedEmail] END OR
	--	[a].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN  @IsActive ELSE [a].[IsActive] END OR
	--	[a].[ModifyBy] = CASE WHEN @ModifyBy IS NOT NULL AND @ModifyBy != 0 THEN  @ModifyBy ELSE [a].[ModifyBy] END OR
	--	[a].[AddedBy] = CASE WHEN @AddedBy IS NOT NULL AND @AddedBy != 0 THEN  @AddedBy ELSE [a].[AddedBy] END 

END