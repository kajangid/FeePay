CREATE PROCEDURE [dbo].[SP_Update_SchoolAdmin_UserRole]
(
@Id INT
,@RoleId INT = NULL
,@UserId INT = NULL
,@IsActive BIT = NULL
,@ModifyBy INT = NULL
)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [dbo].[SchoolAdmin_UserRole] WHERE [UserId] = @UserId AND [RoleId] = @RoleId AND [IsDelete] = 0)
	BEGIN
		UPDATE [dbo].[SchoolAdmin_UserRole] SET
			[UserId] = CASE WHEN @UserId IS NOT NULL AND @UserId != 0 THEN @UserId ELSE [UserId] END --@UserId
			,[RoleId] = CASE WHEN @RoleId IS NOT NULL AND @RoleId != 0 THEN @RoleId ELSE [RoleId] END --@RoleId
			,[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END --@IsActive
			,[ModifyBy] = @ModifyBy
			,[ModifyDate] = GETDATE()
		WHERE 
			[Id] = @Id AND
			[IsDelete] = 0
		
		RETURN @Id
	END
	ELSE
	BEGIN
		RETURN 0
	END
END
