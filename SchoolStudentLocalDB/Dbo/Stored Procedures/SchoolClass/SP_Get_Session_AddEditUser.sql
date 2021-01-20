-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get all Session list data with modifyby and addedby user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Session_AddEditUser]
(
	@Id INT = NULL,
	@Year NVARCHAR(20) = NULL,
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
			[s].[Id], [s].[Year], [s].[StartYear], [s].[EndYear], [s].[Description], [s].[IsActive], [s].[IsDelete],
			[s].[ModifyDate], [s].[AddedDate]
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
			FROM [dbo].[Session] [s] 
			OUTER APPLY (
				SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
				FROM [dbo].[SchoolAdmin_User] [ab]
				WHERE [ab].[Id] = [s].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]

			OUTER APPLY (
				SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
				FROM [dbo].[SchoolAdmin_User] [mb]
				WHERE [mb].[Id] = [s].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]
			WHERE
			[s].[IsDelete] = 0 
			AND [s].[Id] = @Id 
			AND [s].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [s].[IsActive] END

			RETURN 
	END
	ELSE IF(@Year IS NOT NULL)
	BEGIN
		SELECT 
			[s].[Id], [s].[Year], [s].[StartYear], [s].[EndYear], [s].[Description], [s].[IsActive], [s].[IsDelete],
			[s].[ModifyDate], [s].[AddedDate]
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
			FROM [dbo].[Session] [s] 
			OUTER APPLY (
				SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
				FROM [dbo].[SchoolAdmin_User] [ab]
				WHERE [ab].[Id] = [s].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]

			OUTER APPLY (
				SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
				FROM [dbo].[SchoolAdmin_User] [mb]
				WHERE [mb].[Id] = [s].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]
			WHERE
			[s].[IsDelete] = 0 
			AND [s].[Year] = @Year
			AND [s].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [s].[IsActive] END

			RETURN 
	END
	ELSE 
	BEGIN
		SELECT 
			[s].[Id], [s].[Year], [s].[StartYear], [s].[EndYear], [s].[Description], [s].[IsActive], [s].[IsDelete],
			[s].[ModifyDate], [s].[AddedDate]
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
			FROM [dbo].[Session] [s] 
			OUTER APPLY (
				SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
				FROM [dbo].[SchoolAdmin_User] [ab]
				WHERE [ab].[Id] = [s].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]

			OUTER APPLY (
				SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
				FROM [dbo].[SchoolAdmin_User] [mb]
				WHERE [mb].[Id] = [s].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]
			WHERE
			[s].[IsDelete] = 0 
			AND [s].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [s].[IsActive] END

			RETURN 
	END
		
END
