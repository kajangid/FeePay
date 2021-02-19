CREATE PROCEDURE [dbo].[SP_GetAll_SchoolAdmin_UserRole]
(
@Id INT = 0
,@UserId INT = 0
,@RoleId INT = 0
,@IsActive BIT = 0
,@AddedBy INT = 1
,@ModifyBy INT = 0
)
AS	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

	SELECT 
	[ur].[Id],[ur].[UserId],[ur].[RoleId],[ur].[IsActive],[ur].[AddedBy],[ur].[AddedDate]
	,[ur].[ModifyBy],[ur].[ModifyDate]
	FROM [dbo].[SchoolAdmin_UserRole] [ur] 
	WHERE 
		[ur].[IsDelete] = 0  AND
		(
		[ur].[Id] = CASE WHEN @Id IS NOT NULL AND @Id != 0 THEN  @Id ELSE [ur].[Id] END OR
		[ur].[UserId] = CASE WHEN @UserId IS NOT NULL AND @UserId != 0 THEN  @UserId ELSE [ur].[UserId] END OR
		[ur].[RoleId] = CASE WHEN @RoleId IS NOT NULL AND @RoleId != 0 THEN  @RoleId ELSE [ur].[RoleId] END OR
		[ur].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN  @IsActive ELSE [ur].[IsActive] END OR
		[ur].[ModifyBy] = CASE WHEN @ModifyBy IS NOT NULL AND @ModifyBy != 0 THEN  @ModifyBy ELSE [ur].[ModifyBy] END OR
		[ur].[AddedBy] = CASE WHEN @AddedBy IS NOT NULL AND @AddedBy != 0 THEN  @AddedBy ELSE [ur].[AddedBy] END 
		)


RETURN 0
