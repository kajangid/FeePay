CREATE PROCEDURE [dbo].[SP_Get_SchoolAdmin_Role]
(
@Id INT = 0
,@Name NVARCHAR(256) = NULL
,@NormalizedName NVARCHAR(256) = NULL
,@IsActive BIT = 0
)
AS
BEGIN
	IF(@Id != 0)
	BEGIN
		SELECT 
		r.[Id],r.[Name],r.[NormalizedName],r.[IsActive],r.[AddedDate],r.[ModifyDate]
		,
		au.[Id],au.[FullName],au.[Email]
		,
		mu.[Id],mu.[FullName],mu.[Email]

		FROM [dbo].[SchoolAdmin_Role] r 
		INNER JOIN [dbo].[SchoolAdmin_User] au 
		ON r.[AddedBy] = au.[Id]
		Inner JOIN [dbo].[SchoolAdmin_User] mu 
		ON r.[ModifyBy] = au.[Id]
		WHERE
		(r.[IsDelete] = 0 AND au.[IsDelete] = 0 AND mu.[IsDelete] = 0)
		AND r.[Id] = @id AND r.[IsActive] = @IsActive

		--SELECT *FROM [dbo].[SchoolAdmin_Role] WHERE [Id] = @id AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@Name IS NOT NULL)
	BEGIN
	
		SELECT 
		r.[Id],r.[Name],r.[NormalizedName],r.[IsActive],r.[AddedDate],r.[ModifyDate]
		,
		au.[Id],au.[FullName],au.[Email]
		,
		mu.[Id],mu.[FullName],mu.[Email]

		FROM [dbo].[SchoolAdmin_Role] r 
		INNER JOIN [dbo].[SchoolAdmin_User] au 
		ON r.[AddedBy] = au.[Id]
		Inner JOIN [dbo].[SchoolAdmin_User] mu 
		ON r.[ModifyBy] = au.[Id]
		WHERE
		(r.[IsDelete] = 0 AND au.[IsDelete] = 0 AND mu.[IsDelete] = 0)
		AND r.[Name] = @Name AND r.[IsActive] = @IsActive

		--SELECT *FROM [dbo].[SchoolAdmin_Role] WHERE [Name] = @Name AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	IF(@NormalizedName IS NOT NULL)
	BEGIN
	
		SELECT 
		r.[Id],r.[Name],r.[NormalizedName],r.[IsActive],r.[AddedDate],r.[ModifyDate]
		,
		au.[Id],au.[FullName],au.[Email]
		,
		mu.[Id],mu.[FullName],mu.[Email]

		FROM [dbo].[SchoolAdmin_Role] r 
		INNER JOIN [dbo].[SchoolAdmin_User] au 
		ON r.[AddedBy] = au.[Id]
		Inner JOIN [dbo].[SchoolAdmin_User] mu 
		ON r.[ModifyBy] = au.[Id]
		WHERE
		(r.[IsDelete] = 0 AND au.[IsDelete] = 0 AND mu.[IsDelete] = 0)
		AND r.[NormalizedName] = @NormalizedName AND r.[IsActive] = @IsActive

		--SELECT *FROM [dbo].[SchoolAdmin_Role] WHERE [NormalizedName] = @NormalizedName AND [IsActive] = @IsActive AND [IsDelete] = 0
		RETURN 
	END
	-- SELECT TOP(1) *FROM [dbo].[SchoolAdmin_Role] WHERE [IsDelete] = 0 ORDER BY [Id] DESC
	-- RETURN 
END