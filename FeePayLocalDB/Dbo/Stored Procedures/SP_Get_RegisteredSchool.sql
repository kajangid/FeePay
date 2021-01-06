CREATE PROCEDURE [dbo].[SP_Get_RegisteredSchool]
(
@Id INT = 0
,@Name NVARCHAR(256) = NULL
,@NormalizedName NVARCHAR(256) = NULL
,@IsActive BIT = 0
,@UniqueId NVARCHAR(40) NULL
)
AS
BEGIN
	IF(@Id != 0)
	BEGIN
		SELECT *FROM [dbo].[RegisteredSchool] WHERE [Id] = @id AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@Name IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[RegisteredSchool] WHERE [Name] = @Name AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@NormalizedName IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[RegisteredSchool] WHERE [NormalizedName] = @NormalizedName AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@UniqueId IS NOT NULL)
	BEGIN
		SELECT *FROM [dbo].[RegisteredSchool] WHERE [UniqueId] = @UniqueId AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	-- SELECT TOP(1) *FROM [dbo].[SuperAdmin_Role] WHERE [IsDelete] = 0 ORDER BY [Id] DESC 
	-- RETURN 
END