-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get school role data with modifyby and addedby user data
-- =============================================
CREATE PROCEDURE SP_Get_SchoolAdmin_Role_AddEditUser 
(
@Id INT = NULL
,@Name NVARCHAR(256) = NULL
,@NormalizedName NVARCHAR(256) = NULL
,@IsActive BIT = NULL)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
	
	IF(@Id IS NOT NULL AND @Id != 0)
	BEGIN	
	SELECT 
		[r].[Id],[r].[Name],[r].[NormalizedName],[r].[IsActive],[r].[AddedDate],[r].[ModifyDate]
		, [r].[AddedBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [r].[AddedBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [r].[AddedBy] AND [IsDelete] = 0) AS [Email]
		, [r].[ModifyBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [r].[ModifyBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [r].[ModifyBy] AND [IsDelete] = 0) AS [Email]
		FROM [dbo].[SchoolAdmin_Role] [r]
		WHERE
		[r].[IsDelete] = 0 
		AND [r].[Id] = @Id 
		AND [r].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [r].[IsActive] END
		RETURN 
	END
	IF(@Name IS NOT NULL)
	BEGIN	
	SELECT 
		[r].[Id],[r].[Name],[r].[NormalizedName],[r].[IsActive],[r].[AddedDate],[r].[ModifyDate]
		, [r].[AddedBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [r].[AddedBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [r].[AddedBy] AND [IsDelete] = 0) AS [Email]
		, [r].[ModifyBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [r].[ModifyBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [r].[ModifyBy] AND [IsDelete] = 0) AS [Email]
		FROM [dbo].[SchoolAdmin_Role] [r]
		WHERE
		[r].[IsDelete] = 0 
		AND [r].[Name] = @Name 
		AND [r].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [r].[IsActive] END
		RETURN 
	END
	IF(@NormalizedName IS NOT NULL)
	BEGIN	
	SELECT 
		[r].[Id],[r].[Name],[r].[NormalizedName],[r].[IsActive],[r].[AddedDate],[r].[ModifyDate]
		, [r].[AddedBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [r].[AddedBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [r].[AddedBy] AND [IsDelete] = 0) AS [Email]
		, [r].[ModifyBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [r].[ModifyBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [r].[ModifyBy] AND [IsDelete] = 0) AS [Email]
		FROM [dbo].[SchoolAdmin_Role] [r]
		WHERE
		[r].[IsDelete] = 0 
		AND [r].[NormalizedName] = @NormalizedName
		AND [r].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [r].[IsActive] END
		RETURN 
	END
END