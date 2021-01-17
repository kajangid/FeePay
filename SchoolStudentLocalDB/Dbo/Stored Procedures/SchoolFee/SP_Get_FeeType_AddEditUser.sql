-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get fee type data with modifyby and addedby user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_FeeType_AddEditUser]
(
@Id INT = 0
,@Name NVARCHAR(256) = NULL
,@NormalizedName NVARCHAR(256) = NULL
,@Code NVARCHAR(256) = NULL
,@IsActive BIT = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
		
	IF(@Id != 0)
	BEGIN	
	SELECT 
		[ft].[Id], [ft].[Name], [ft].[NormalizedName], [ft].[Code], [ft].[Description], [ft].[IsActive], [ft].[IsDelete]
		, [ft].[ModifyDate], [ft].[AddedDate]
		, [ft].[AddedBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [ft].[AddedBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [ft].[AddedBy] AND [IsDelete] = 0) AS [Email]
		, [ft].[ModifyBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [ft].[ModifyBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [ft].[ModifyBy] AND [IsDelete] = 0) AS [Email]
		FROM [dbo].[FeeType] [ft]
		WHERE
		[ft].[IsDelete] = 0 
		AND [ft].[Id] = @Id 
		AND [ft].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [ft].[IsActive] END

		RETURN 
	END
	IF(@Name IS NOT NULL)
	BEGIN	
	SELECT 
		[ft].[Id], [ft].[Name], [ft].[NormalizedName], [ft].[Code], [ft].[Description], [ft].[IsActive], [ft].[IsDelete]
		, [ft].[ModifyDate], [ft].[AddedDate]
		, [ft].[AddedBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [ft].[AddedBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [ft].[AddedBy] AND [IsDelete] = 0) AS [Email]
		, [ft].[ModifyBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [ft].[ModifyBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [ft].[ModifyBy] AND [IsDelete] = 0) AS [Email]
		FROM [dbo].[FeeType] [ft]
		WHERE
		[ft].[IsDelete] = 0 
		AND [ft].[Name] = @Name 
		AND [ft].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [ft].[IsActive] END

		RETURN 
	END
	IF(@NormalizedName IS NOT NULL)
	BEGIN	
	SELECT 
		[ft].[Id], [ft].[Name], [ft].[NormalizedName], [ft].[Code], [ft].[Description], [ft].[IsActive], [ft].[IsDelete]
		, [ft].[ModifyDate], [ft].[AddedDate]
		, [ft].[AddedBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [ft].[AddedBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [ft].[AddedBy] AND [IsDelete] = 0) AS [Email]
		, [ft].[ModifyBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [ft].[ModifyBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [ft].[ModifyBy] AND [IsDelete] = 0) AS [Email]
		FROM [dbo].[FeeType] [ft]
		WHERE
		[ft].[IsDelete] = 0 
		AND [ft].[NormalizedName] = @NormalizedName
		AND [ft].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [ft].[IsActive] END

		RETURN 
	END
	IF(@Code IS NOT NULL)
	BEGIN	
	SELECT 
		[ft].[Id], [ft].[Name], [ft].[NormalizedName], [ft].[Code], [ft].[Description], [ft].[IsActive], [ft].[IsDelete]
		, [ft].[ModifyDate], [ft].[AddedDate]
		, [ft].[AddedBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [ft].[AddedBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [ft].[AddedBy] AND [IsDelete] = 0) AS [Email]
		, [ft].[ModifyBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [ft].[ModifyBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [ft].[ModifyBy] AND [IsDelete] = 0) AS [Email]
		FROM [dbo].[FeeType] [ft]
		WHERE
		[ft].[IsDelete] = 0 
		AND [ft].[Code] = @Code
		AND [ft].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [ft].[IsActive] END

		RETURN 
	END

END
