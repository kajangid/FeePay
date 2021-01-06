CREATE PROCEDURE [dbo].[SP_Add_SchoolAdmin_UserRole]
(
@RoleId INT
,@UserId INT
,@IsActive BIT = 0
,@AddedBy INT = 0
)
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM [dbo].[SchoolAdmin_UserRole] WHERE [UserId] = @UserId AND [RoleId] = @RoleId AND [IsDelete] = 0)
	BEGIN
		INSERT INTO [dbo].[SchoolAdmin_UserRole] 
		([UserId], [RoleId],[IsActive],[IsDelete],[ModifyDate],[AddedDate],[AddedBy],[ModifyBy]) 
		VALUES
		(@UserId, @RoleId,@IsActive,0,GETDATE(),GETDATE(),@AddedBy,@AddedBy)
		
		RETURN CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN
		RETURN 0
	END
END
