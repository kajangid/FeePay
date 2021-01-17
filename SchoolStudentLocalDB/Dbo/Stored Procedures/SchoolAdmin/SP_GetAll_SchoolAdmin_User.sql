-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get all school user list data
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAll_SchoolAdmin_User]
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
		, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive], [a].[AddedBy], [a].[ModifyBy]

		, [t3].[Id],[t3].[Name],[t3].[NormalizedName]

		FROM [dbo].[SchoolAdmin_User] [a] 

		OUTER APPLY (
		SELECT [r].[Id],[r].[Name],[r].[NormalizedName] 
		FROM [dbo].[SchoolAdmin_UserRole] [ur] 
		LEFT JOIN [dbo].[SchoolAdmin_Role] [r]
		ON [r].[Id] = [ur].[RoleId] AND [r].[IsDelete] = 0 AND [r].[IsActive] = 1
		WHERE [ur].[UserId] = [a].[Id] AND [ur].[IsDelete] = 0 AND [ur].[IsActive] = 1) [t3]

		WHERE
		[a].[IsDelete] = 0 AND 
		[a].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN  @IsActive ELSE [a].[IsActive] END


	--SELECT 
	--	[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
	--	, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
	--	, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
	--	, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive], [a].[AddedBy], [a].[ModifyBy]
	--	FROM [SchoolAdmin_User] [a]
	--	WHERE
	--	[a].[IsDelete] = 0 OR
	--	[a].[Id] = CASE WHEN @Id IS NOT NULL AND @Id != 0 THEN  @Id ELSE [a].[Id] END OR
	--	[a].[UserName] = CASE WHEN @UserName IS NOT NULL THEN  @UserName ELSE [a].[UserName] END OR
	--	[a].[NormalizedUserName] = CASE WHEN @NormalizedUserName IS NOT NULL THEN  @NormalizedUserName ELSE [a].[NormalizedUserName] END OR
	--	[a].[PhoneNumber] = CASE WHEN @PhoneNumber IS NOT NULL THEN  @PhoneNumber ELSE [a].[PhoneNumber] END OR
	--	[a].[NormalizedEmail] = CASE WHEN @NormalizedEmail IS NOT NULL THEN  @NormalizedEmail ELSE [a].[NormalizedEmail] END OR
	--	[a].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN  @IsActive ELSE [a].[IsActive] END OR
	--	[a].[ModifyBy] = CASE WHEN @ModifyBy IS NOT NULL THEN  @ModifyBy ELSE [a].[ModifyBy] END OR
	--	[a].[AddedBy] = CASE WHEN @AddedBy IS NOT NULL AND @AddedBy != 0 THEN  @AddedBy ELSE [a].[AddedBy] END 


		
	--SELECT  [Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PhoneNumber],
	--		[PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDate], [LockoutEnabled], [AccessFailedCount], [SecurityStamp],
	--		[FirstName], [LastName], [FullName], [LastLoginIP], [LastLoginDate], [IsActive], [ModifyDate],
	--		[ModifyBy], [AddedDate], [AddedBy]
	--FROM [dbo].[SchoolAdmin_User] WHERE
	--		[IsDelete] = 0 AND
	--		([Id] != 0 AND [Id] = @Id) OR
	--		([UserName] IS NOT NULL AND [UserName] = @UserName) OR
	--		([NormalizedUserName] IS NOT NULL AND [NormalizedUserName] = @NormalizedUserName) OR
	--		([Email] IS NOT NULL AND [Email] = @Email) OR
	--		([NormalizedEmail] IS NOT NULL AND [NormalizedEmail] = @NormalizedEmail) OR
	--		([PhoneNumber] IS NOT NULL AND [PhoneNumber] = @PhoneNumber) OR			
	--		([IsActive] IS NOT NULL AND [IsActive] = @IsActive) OR
	--		([ModifyBy] != 0 AND [ModifyBy] = @ModifyBy) OR
	--		([AddedBy] !=0 AND [AddedBy] = @AddedBy)
	
END