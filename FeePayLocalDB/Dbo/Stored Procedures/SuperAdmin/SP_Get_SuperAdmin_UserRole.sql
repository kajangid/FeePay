CREATE PROCEDURE [dbo].[SP_Get_SuperAdmin_UserRole]
(
@Id INT = 0
,@RoleId INT NULL
,@UserId INT NULL
,@IsActive BIT = 0
)
AS
BEGIN
	IF(@Id != 0)
	BEGIN
		SELECT *FROM [dbo].[SuperAdmin_UserRole] WHERE [Id] = @id AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@RoleId IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[SuperAdmin_UserRole] WHERE [RoleId] = @roleId AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@UserId IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[SuperAdmin_UserRole] WHERE [UserId] = @userId AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	-- SELECT TOP(1) *FROM [dbo].[SuperAdmin_UserRole] WHERE [IsDelete] = 0 ORDER BY [Id] DESC
	-- RETURN 
END