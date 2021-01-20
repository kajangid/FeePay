-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to get classes Sections list data with add and update user info
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAll_ClassesSections_AddEditUser]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	

		SELECT 
		[c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description], [c].[IsActive], [c].[IsDelete],
		[c].[ModifyDate], [c].[AddedDate]

		, [t3].[Id], [t3].[Name], [t3].[NormalizedName] --  <-------- Sections 
		, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
		, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
		FROM [dbo].[Class] [c]
		OUTER APPLY (
			SELECT [s].[Id], [s].[Name], [s].[NormalizedName]
			FROM [dbo].[ClassSection] [cs]
			LEFT JOIN [dbo].[Section] [s] 
			ON [s].[Id] = [cs].[SectionId] AND [s].[IsDelete] = 0
			WHERE [cs].[ClassId] = [c].[Id] ) [t3]
		OUTER APPLY (
			SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
			FROM [dbo].[SchoolAdmin_User] [ab]
			WHERE [ab].[Id] = [c].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]
		OUTER APPLY (
			SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
			FROM [dbo].[SchoolAdmin_User] [mb]
			WHERE [mb].[Id] = [c].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]

		RETURN
END
