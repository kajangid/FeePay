-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get class data with add and edit user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Class_AddEditUser]
	(
		@Id INT = NULL,
		@Name NVARCHAR(50) = NULL,
		@NormalizedName NVARCHAR(50) = NULL,
		@IsActive BIT = NULL
	)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	IF(@Id IS NOT NULL AND @Id != 0)
	BEGIN	
		SELECT 
			[c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate],
			[c].[AddedDate]			
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
			FROM [dbo].[Class] [c] 
			OUTER APPLY (
				SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
				FROM [dbo].[SchoolAdmin_User] [ab]
				WHERE [ab].[Id] = [c].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]
			OUTER APPLY (
				SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
				FROM [dbo].[SchoolAdmin_User] [mb]
				WHERE [mb].[Id] = [c].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[Id] = @Id 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE IF(@Name IS NOT NULL)
	BEGIN	
		SELECT 
			[c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate],
			[c].[AddedDate]			
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
			FROM [dbo].[Class] [c] 
			OUTER APPLY (
				SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
				FROM [dbo].[SchoolAdmin_User] [ab]
				WHERE [ab].[Id] = [c].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]
			OUTER APPLY (
				SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
				FROM [dbo].[SchoolAdmin_User] [mb]
				WHERE [mb].[Id] = [c].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[Name] = @Name
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE IF(@NormalizedName IS NOT NULL)
	BEGIN	
		SELECT 
			[c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate],
			[c].[AddedDate]			
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
			FROM [dbo].[Class] [c] 
			OUTER APPLY (
				SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
				FROM [dbo].[SchoolAdmin_User] [ab]
				WHERE [ab].[Id] = [c].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]
			OUTER APPLY (
				SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
				FROM [dbo].[SchoolAdmin_User] [mb]
				WHERE [mb].[Id] = [c].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[NormalizedName] = @NormalizedName
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE 
	BEGIN	
		SELECT 
			[c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate],
			[c].[AddedDate]			
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
			FROM [dbo].[Class] [c] 
			OUTER APPLY (
				SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
				FROM [dbo].[SchoolAdmin_User] [ab]
				WHERE [ab].[Id] = [c].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]
			OUTER APPLY (
				SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
				FROM [dbo].[SchoolAdmin_User] [mb]
				WHERE [mb].[Id] = [c].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
END
