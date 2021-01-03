CREATE PROCEDURE [dbo].[SP_Delete_SuperAdmin_UserRole]
	--@Id INT
	(@UserId INT, @RoleId INT , @ModifyBy INT NULL)
AS
BEGIN
	--IF EXISTS(SELECT 1 FROM [dbo].[SuperAdmin_UserRole] WHERE [Id] = @Id AND [IsDelete] = 0)
	IF EXISTS(SELECT 1 FROM [dbo].[SuperAdmin_UserRole] WHERE [UserId] = @UserId AND [RoleId] = @RoleId)
	BEGIN
		UPDATE [dbo].[SuperAdmin_UserRole] SET
			--[Id] = @Id
			[IsDelete] = 0
			,[ModifyDate] = GETDATE()
			,[ModifyBy] = @ModifyBy
		WHERE 
			[UserId] = @UserId AND 
			[RoleId] = @RoleId
		
		RETURN 1
	END
	ELSE
	BEGIN
		RETURN 0
	END
END
RETURN 0
