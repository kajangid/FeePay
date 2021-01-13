CREATE PROCEDURE [dbo].[SP_Get_SchoolAdmin_User]
(
@Id INT = 0
,@UserName NVARCHAR(256) = NULL
,@NormalizedUserName NVARCHAR(256) = NULL
,@Email NVARCHAR(256) = NULL
,@NormalizedEmail NVARCHAR(256) = NULL
,@PhoneNumber NVARCHAR(50) = NULL
,@IsActive BIT = 1
)
AS
BEGIN
	IF(@Id != 0)
	BEGIN
		SELECT 
			[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
			, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
			, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
			, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive]
			,
			[au].[Id],[au].[FullName],[au].[Email]
			,
			[mu].[Id],[mu].[FullName],[mu].[Email]

			FROM [dbo].[SchoolAdmin_User] a 
			INNER JOIN [dbo].[SchoolAdmin_User] au 
			ON [a].[AddedBy] = [au].[Id]
			INNER JOIN [dbo].[SchoolAdmin_User] mu 
			ON [a].[ModifyBy] = [au].[Id]
			WHERE
			[a].[IsDelete] = 0 AND
			[au].[IsDelete] = 0 AND
			[mu].[IsDelete] = 0 AND
			[Id] = @id AND 
			[IsActive] = @IsActive
		--SELECT *FROM [dbo].[SchoolAdmin_User] WHERE [Id] = @id AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@UserName IS NOT NULL)
	BEGIN
		SELECT 
			[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
			, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
			, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
			, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive]
			,
			[au].[Id],[au].[FullName],[au].[Email]
			,
			[mu].[Id],[mu].[FullName],[mu].[Email]

			FROM [dbo].[SchoolAdmin_User] a 
			INNER JOIN [dbo].[SchoolAdmin_User] au 
			ON [a].[AddedBy] = [au].[Id]
			INNER JOIN [dbo].[SchoolAdmin_User] mu 
			ON [a].[ModifyBy] = [au].[Id]
			WHERE
			[a].[IsDelete] = 0 AND
			[au].[IsDelete] = 0 AND
			[mu].[IsDelete] = 0 AND
			[UserName] = @UserName AND 
			[IsActive] = @IsActive
		--SELECT *FROM [dbo].[SchoolAdmin_User] WHERE [UserName] = @UserName AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@NormalizedUserName IS NOT NULL)
	BEGIN
		SELECT 
			[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
			, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
			, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
			, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive]
			,
			[au].[Id],[au].[FullName],[au].[Email]
			,
			[mu].[Id],[mu].[FullName],[mu].[Email]

			FROM [dbo].[SchoolAdmin_User] a 
			INNER JOIN [dbo].[SchoolAdmin_User] au 
			ON [a].[AddedBy] = [au].[Id]
			INNER JOIN [dbo].[SchoolAdmin_User] mu 
			ON [a].[ModifyBy] = [au].[Id]
			WHERE
			[a].[IsDelete] = 0 AND
			[au].[IsDelete] = 0 AND
			[mu].[IsDelete] = 0 AND
			[NormalizedUserName] = @NormalizedUserName AND 
			[IsActive] = @IsActive
		--SELECT *FROM [dbo].[SchoolAdmin_User] WHERE [NormalizedUserName] = @NormalizedUserName AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@Email IS NOT NULL)
	BEGIN	
		SELECT 
			[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
			, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
			, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
			, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive]
			,
			[au].[Id],[au].[FullName],[au].[Email]
			,
			[mu].[Id],[mu].[FullName],[mu].[Email]

			FROM [dbo].[SchoolAdmin_User] a 
			INNER JOIN [dbo].[SchoolAdmin_User] au 
			ON [a].[AddedBy] = [au].[Id]
			INNER JOIN [dbo].[SchoolAdmin_User] mu 
			ON [a].[ModifyBy] = [au].[Id]
			WHERE
			[a].[IsDelete] = 0 AND
			[au].[IsDelete] = 0 AND
			[mu].[IsDelete] = 0 AND
			[Email] = @Email AND 
			[IsActive] = @IsActive
		--SELECT *FROM [dbo].[SchoolAdmin_User] WHERE [Email] = @Email AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@NormalizedEmail IS NOT NULL)
	BEGIN	
		SELECT 
			[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
			, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
			, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
			, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive]
			,
			[au].[Id],[au].[FullName],[au].[Email]
			,
			[mu].[Id],[mu].[FullName],[mu].[Email]

			FROM [dbo].[SchoolAdmin_User] a 
			INNER JOIN [dbo].[SchoolAdmin_User] au 
			ON [a].[AddedBy] = [au].[Id]
			INNER JOIN [dbo].[SchoolAdmin_User] mu 
			ON [a].[ModifyBy] = [au].[Id]
			WHERE
			[a].[IsDelete] = 0 AND
			[au].[IsDelete] = 0 AND
			[mu].[IsDelete] = 0 AND
			[NormalizedEmail] = @NormalizedEmail AND 
			[IsActive] = @IsActive
		--SELECT *FROM [dbo].[SchoolAdmin_User] WHERE [NormalizedEmail] = @NormalizedEmail AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@PhoneNumber IS NOT NULL)
	BEGIN	
		SELECT 
			[a].[Id], [a].[UserName], [a].[NormalizedUserName], [a].[Email], [a].[NormalizedEmail], [a].[EmailConfirmed]
			, [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[TwoFactorEnabled], [a].[LockoutEndDate], [a].[LockoutEnabled]
			, [a].[AccessFailedCount], [a].[FirstName], [a].[LastName], [a].[FullName], [a].[AddedDate], [a].[ModifyDate]
			, [a].[LastLoginIP], [a].[LastLoginDate], [a].[IsActive]
			,
			[au].[Id],[au].[FullName],[au].[Email]
			,
			[mu].[Id],[mu].[FullName],[mu].[Email]

			FROM [dbo].[SchoolAdmin_User] a 
			INNER JOIN [dbo].[SchoolAdmin_User] au 
			ON [a].[AddedBy] = [au].[Id]
			INNER JOIN [dbo].[SchoolAdmin_User] mu 
			ON [a].[ModifyBy] = [au].[Id]
			WHERE
			[a].[IsDelete] = 0 AND
			[au].[IsDelete] = 0 AND
			[mu].[IsDelete] = 0 AND
			[PhoneNumber] = @PhoneNumber AND 
			[IsActive] = @IsActive
		--SELECT *FROM [dbo].[SchoolAdmin_User] WHERE [PhoneNumber] = @PhoneNumber AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	--SELECT TOP(1) *FROM [dbo].[SchoolAdmin_User] WHERE [IsDelete] = 0 ORDER BY [Id] DESC
	--RETURN 
END