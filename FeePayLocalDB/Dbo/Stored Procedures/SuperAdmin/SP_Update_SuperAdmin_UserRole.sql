CREATE PROCEDURE [dbo].[SP_Update_SuperAdmin_UserRole]
(
@Id INT
,@RoleId INT NULL
,@UserId INT NULL
,@IsActive BIT = 0
,@ModifyBy INT NULL
)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [dbo].[SuperAdmin_UserRole] WHERE [UserId] = @UserId AND [RoleId] = @RoleId AND [IsDelete] = 0)
	BEGIN
		UPDATE [dbo].[SuperAdmin_UserRole] SET
			[UserId] = @UserId
			,[RoleId] = @RoleId
			,[IsActive] = @IsActive
			,[ModifyDate] = GETDATE()
			,[ModifyBy] = @ModifyBy
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
