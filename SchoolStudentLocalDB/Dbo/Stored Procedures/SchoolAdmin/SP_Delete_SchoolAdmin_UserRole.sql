CREATE PROCEDURE [dbo].[SP_Delete_SchoolAdmin_UserRole]
	(@UserId INT
	, @RoleId INT 
	, @ModifyBy INT = NULL)
AS
BEGIN
	--IF EXISTS(SELECT 1 FROM [dbo].[SuperAdmin_UserRole] WHERE [Id] = @Id AND [IsDelete] = 0)
	IF EXISTS(SELECT 1 FROM [dbo].[SchoolAdmin_UserRole] WHERE [UserId] = @UserId AND [RoleId] = @RoleId AND [IsDelete] = 0)
	BEGIN
		UPDATE [dbo].[SchoolAdmin_UserRole] SET
			[IsDelete] = 1
			,[ModifyDate] = GETDATE()
			,[ModifyBy] = @ModifyBy
		WHERE 
			[UserId] = @UserId AND 
			[RoleId] = ISNULL(@RoleId,[RoleId])
		
		RETURN 1
	END
	ELSE
	BEGIN
		RETURN 0
	END
END
RETURN 0
