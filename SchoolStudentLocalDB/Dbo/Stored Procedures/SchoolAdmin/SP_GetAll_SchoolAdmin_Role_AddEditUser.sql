-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get all school role list data with modifyby and addedby user data
-- =============================================
CREATE PROCEDURE SP_GetAll_SchoolAdmin_Role_AddEditUser 
(
@Id INT = NULL
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
	AND(
	-- [r].[Id] = CASE WHEN @Id IS NOT NULL AND @Id != 0 THEN  @Id ELSE [r].[Id] END OR
	-- [r].[Name] = CASE WHEN @Name IS NOT NULL THEN  @Name ELSE [r].[Name] END OR
	-- [r].[NormalizedName] = CASE WHEN @NormalizedName IS NOT NULL THEN  @NormalizedName ELSE [r].[NormalizedName] END OR
	[r].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN  @IsActive ELSE [r].[IsActive] END -- OR
	-- [r].[ModifyBy] = CASE WHEN @ModifyBy IS NOT NULL AND @ModifyBy != 0 THEN  @ModifyBy ELSE [r].[ModifyBy] END OR
	-- [r].[AddedBy] = CASE WHEN @AddedBy IS NOT NULL AND @AddedBy != 0 THEN  @AddedBy ELSE [r].[AddedBy] END 
	)

END