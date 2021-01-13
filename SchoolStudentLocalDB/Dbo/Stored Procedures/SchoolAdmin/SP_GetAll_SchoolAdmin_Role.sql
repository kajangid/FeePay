CREATE PROCEDURE [dbo].[SP_GetAll_SchoolAdmin_Role]
(
@Id INT = 0
,@Name NVARCHAR(256) = NULL
,@NormalizedName NVARCHAR(256) = NULL
,@IsActive BIT = 0
,@ModifyBy BIT = 0
,@AddedBy INT  = 0
)
AS
BEGIN
	SELECT 
	r.[Id],r.[Name],r.[NormalizedName],r.[IsActive],r.[AddedDate],r.[ModifyDate]
	,
	au.[Id],au.[FullName],au.[Email]
	,
	mu.[Id],mu.[FullName],mu.[Email]

	FROM [dbo].[SchoolAdmin_Role] r 
	INNER JOIN [dbo].[SchoolAdmin_User] au 
	ON r.[AddedBy] = au.[Id]
	INNER JOIN [dbo].[SchoolAdmin_User] mu 
	ON r.[ModifyBy] = au.[Id]
	WHERE
	r.[IsDelete] = 0 AND
	au.[IsDelete] = 0 AND
	mu.[IsDelete] = 0 OR
	r.[Id] = CASE WHEN @Id IS NOT NULL AND @Id != 0 THEN  @Id ELSE r.[Id] END OR
	r.[Name] = CASE WHEN @Name IS NOT NULL THEN  @Name ELSE r.[Name] END OR
	r.[NormalizedName] = CASE WHEN @NormalizedName IS NOT NULL THEN  @NormalizedName ELSE r.[NormalizedName] END OR
	r.[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN  @IsActive ELSE r.[IsActive] END OR
	r.[ModifyBy] = CASE WHEN @ModifyBy IS NOT NULL THEN  @ModifyBy ELSE r.[ModifyBy] END OR
	r.[AddedBy] = CASE WHEN @AddedBy IS NOT NULL AND @AddedBy != 0 THEN  @AddedBy ELSE r.[AddedBy] END 


	--SELECT *FROM [dbo].[SchoolAdmin_Role] WHERE
	--		[IsDelete] = 0 AND
	--		([Id] != 0 AND [Id] = @Id) OR
	--		([Name] IS NOT NULL AND [Name] = @Name) OR
	--		([NormalizedName] IS NOT NULL AND [NormalizedName] = @NormalizedName) OR
	--		([IsActive] IS NOT NULL AND [IsActive] = @IsActive) OR
	--		([ModifyBy] != 0 AND [ModifyBy] = @ModifyBy) OR
	--		([AddedBy] !=0 AND [AddedBy] = @AddedBy)
	RETURN 
END