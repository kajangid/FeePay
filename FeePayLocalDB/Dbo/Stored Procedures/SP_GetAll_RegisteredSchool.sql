-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to get all and search Registered Schools data with add edit user
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAll_RegisteredSchool]
(
@SearchParam		NVARCHAR(50)	= NULL,
@IsActive			BIT				= NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF(@SearchParam IS NOT NULL AND @SearchParam <> N'')
		BEGIN
		SELECT [sa].[Id], [sa].[Name], [sa].[NormalizedName], [sa].[UniqueId], [sa].[Address], [sa].[PrincipalName],
			[sa].[ContactNumber], [sa].[IsApproved], [sa].[SchoolImage], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate],
			[sa].[ModifyBy], [sa].[AddedDate], [sa].[AddedBy]
		
	
			, [t1].[Id], [t1].[FullName], [t1].[UserName] --<-------- Added By
			, [t2].[Id], [t2].[FullName], [t2].[UserName] --<-------- ModifyBy

		FROM [dbo].[RegisteredSchool] [sa]
			OUTER APPLY (
						SELECT [ab].[Id],[ab].[FullName],[ab].[Email],[ab].[UserName]
						FROM   [dbo].[SuperAdmin_User] [ab]
						WHERE  [ab].[Id] = [sa].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t1]

			OUTER APPLY (
						SELECT [mb].[Id],[mb].[FullName],[mb].[Email],[mb].[UserName]
						FROM   [dbo].[SuperAdmin_User] [mb]
						WHERE  [mb].[Id] = [sa].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t2]
		WHERE
			[sa].[IsDelete] = 0 AND
			[sa].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEn @IsActive ELSE [sa].[IsActive] END AND
			(
			[sa].[Name] LIKE '%'+@SearchParam+'%' OR
			[sa].[NormalizedName] LIKE '%'+@SearchParam+'%' OR
			[sa].[UniqueId] LIKE '%'+@SearchParam+'%' OR
			[sa].[PrincipalName] LIKE '%'+@SearchParam+'%' OR
			[sa].[ContactNumber] LIKE '%'+@SearchParam+'%' OR
			[sa].[IsApproved] LIKE '%'+@SearchParam+'%'
			)
		RETURN
		END
	ELSE
	BEGIN
		SELECT [sa].[Id], [sa].[Name], [sa].[NormalizedName], [sa].[UniqueId], [sa].[Address], [sa].[PrincipalName],
			[sa].[ContactNumber], [sa].[IsApproved], [sa].[SchoolImage], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate],
			[sa].[ModifyBy], [sa].[AddedDate], [sa].[AddedBy]
		
	
			, [t1].[Id], [t1].[FullName], [t1].[UserName] --<-------- Added By
			, [t2].[Id], [t2].[FullName], [t2].[UserName] --<-------- ModifyBy

		FROM [dbo].[RegisteredSchool] [sa]
			OUTER APPLY (
						SELECT [ab].[Id],[ab].[FullName],[ab].[Email],[ab].[UserName]
						FROM   [dbo].[SuperAdmin_User] [ab]
						WHERE  [ab].[Id] = [sa].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t1]

			OUTER APPLY (
						SELECT [mb].[Id],[mb].[FullName],[mb].[Email],[mb].[UserName]
						FROM   [dbo].[SuperAdmin_User] [mb]
						WHERE  [mb].[Id] = [sa].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t2]
		WHERE
			[sa].[IsDelete] = 0 AND
			[sa].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEn @IsActive ELSE [sa].[IsActive] END
		RETURN
	END
END