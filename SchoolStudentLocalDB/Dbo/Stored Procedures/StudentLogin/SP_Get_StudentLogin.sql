CREATE PROCEDURE [dbo].[SP_Get_StudentLogin]
(
@Id INT = 0
,@UserName NVARCHAR(256) = NULL
,@NormalizedUserName NVARCHAR(256) = NULL
,@Email NVARCHAR(256) = NULL
,@NormalizedEmail NVARCHAR(256) = NULL
,@PhoneNumber NVARCHAR(50) = NULL
,@IsActive BIT = 1
)
AS
BEGIN
	IF(@Id != 0)
	BEGIN
		SELECT *FROM [dbo].[StudentLogin] WHERE [Id] = @id AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@UserName IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[StudentLogin] WHERE [UserName] = @UserName AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@NormalizedUserName IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[StudentLogin] WHERE [NormalizedUserName] = @NormalizedUserName AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@Email IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[StudentLogin] WHERE [Email] = @Email AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@NormalizedEmail IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[StudentLogin] WHERE [NormalizedEmail] = @NormalizedEmail AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@PhoneNumber IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[StudentLogin] WHERE [PhoneNumber] = @PhoneNumber AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	--SELECT TOP(1) *FROM [dbo].[StudentLogin] WHERE [IsDelete] = 0 ORDER BY [Id] DESC
	--RETURN 
END
