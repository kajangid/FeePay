-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get all fee group list with add and edit user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAll_FeeGroup_AddEditUser]
(
@Id INT = 0
,@Name NVARCHAR(256) = NULL
,@NormalizedName NVARCHAR(256) = NULL
,@IsActive BIT = NULL
,@ModifyBy BIT = NULL
,@AddedBy INT = NULL
)
AS
BEGIN	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
		

	SELECT 
		[fg].[Id], [fg].[Name], [fg].[NormalizedName], [fg].[Description], [fg].[IsActive], [fg].[IsDelete]
		, [fg].[ModifyDate], [fg].[AddedDate]
		, [fg].[AddedBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [fg].[AddedBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [fg].[AddedBy] AND [IsDelete] = 0) AS [Email]
		, [fg].[ModifyBy] AS [Id]
		, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [fg].[ModifyBy] AND [IsDelete] = 0) AS [FullName]
		, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [fg].[ModifyBy] AND [IsDelete] = 0) AS [Email]
	FROM [dbo].[FeeGroup] [fg]
	WHERE
		[fg].[IsDelete] = 0 
	AND(
	-- [fg].[Id] = CASE WHEN @Id IS NOT NULL AND @Id != 0 THEN  @Id ELSE [fg].[Id] END OR
	-- [fg].[Name] = CASE WHEN @Name IS NOT NULL THEN  @Name ELSE [fg].[Name] END OR
	-- [fg].[NormalizedName] = CASE WHEN @NormalizedName IS NOT NULL THEN  @NormalizedName ELSE [fg].[NormalizedName] END OR
	[fg].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN  @IsActive ELSE [fg].[IsActive] END -- OR
	-- [fg].[ModifyBy] = CASE WHEN @ModifyBy IS NOT NULL AND @ModifyBy != 0 THEN  @ModifyBy ELSE [fg].[ModifyBy] END OR
	-- [fg].[AddedBy] = CASE WHEN @AddedBy IS NOT NULL AND @AddedBy != 0 THEN  @AddedBy ELSE [fg].[AddedBy] END 
	)
END