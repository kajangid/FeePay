-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get all fee type list data with modifyby and addedby user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAll_FeeType_AddEditUser]
(
@Id INT = 0
,@Name NVARCHAR(256) = NULL
,@NormalizedName NVARCHAR(256) = NULL
,@Code NVARCHAR(256) = NULL
,@IsActive BIT = NULL
,@ModifyBy BIT = NULL
,@AddedBy INT = NULL
)
AS
BEGIN	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [ft].[Id], [ft].[Name], [ft].[NormalizedName], [ft].[Code], [ft].[Description], [ft].[IsActive], [ft].[IsDelete]
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
	AND(
	-- [ft].[Id] = CASE WHEN @Id IS NOT NULL AND @Id != 0 THEN  @Id ELSE [ft].[Id] END OR
	-- [ft].[Name] = CASE WHEN @Name IS NOT NULL THEN  @Name ELSE [ft].[Name] END OR
	-- [ft].[NormalizedName] = CASE WHEN @NormalizedName IS NOT NULL THEN  @NormalizedName ELSE [ft].[NormalizedName] END OR
	-- [ft].[Code] = CASE WHEN @Code IS NOT NULL THEN  @Code ELSE [ft].[Code] END OR
	[ft].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN  @IsActive ELSE [ft].[IsActive] END -- OR
	-- [ft].[ModifyBy] = CASE WHEN @ModifyBy IS NOT NULL AND @ModifyBy != 0 THEN  @ModifyBy ELSE [ft].[ModifyBy] END OR
	-- [ft].[AddedBy] = CASE WHEN @AddedBy IS NOT NULL AND @AddedBy != 0 THEN  @AddedBy ELSE [ft].[AddedBy] END 
	)
END
