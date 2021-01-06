CREATE PROCEDURE [dbo].[SP_Get_SchoolAdmin_Role]
(@Id INT = 0,@Name NVARCHAR(256) = NULL,@NormalizedName NVARCHAR(256) = NULL,@IsActive BIT = 0)
AS
BEGIN
	IF(@Id != 0)
	BEGIN
		SELECT *FROM [dbo].[SchoolAdmin_Role] WHERE [Id] = @id AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@Name IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[SchoolAdmin_Role] WHERE [Name] = @Name AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@NormalizedName IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[SchoolAdmin_Role] WHERE [NormalizedName] = @NormalizedName AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	-- SELECT TOP(1) *FROM [dbo].[SchoolAdmin_Role] WHERE [IsDelete] = 0 ORDER BY [Id] DESC
	-- RETURN 
END
