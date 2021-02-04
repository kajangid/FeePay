-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to get all and search super user data with add edit user
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAll_SuperAdmin_User]
(
@IsActive			BIT				= NULL,
@SearchParam		NVARCHAR(150)	= NUll
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF(@SearchParam IS NOT NULL AND @SearchParam <> N'')
		BEGIN
			SELECT [sa].[Id],[sa].[UserName],[sa].[NormalizedUserName],[sa].[Email],[sa].[NormalizedEmail],[sa].[EmailConfirmed],
				[sa].[PhoneNumber],[sa].[PhoneNumberConfirmed],[sa].[TwoFactorEnabled],[sa].[LockoutEndDate],[sa].[LockoutEnabled],
				[sa].[AccessFailedCount],[sa].[SecurityStamp],[sa].[FirstName],[sa].[LastName],[sa].[Photo],[sa].[City],
				[sa].[LastLoginIP],[sa].[LastLoginDate],[sa].[IsActive],[sa].[ModifyDate],[sa].[AddedDate] 
				, [t1].[Id], [t1].[FullName], [t1].[UserName] --<-------- Added By
				, [t2].[Id], [t2].[FullName], [t2].[UserName] --<-------- ModifyBy
			FROM [dbo].[SuperAdmin_User] [sa]
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
				[sa].[NormalizedUserName] LIKE '%'+@SearchParam+'%' OR
				[sa].[NormalizedEmail] LIKE '%'+@SearchParam+'%' OR
				[sa].[PhoneNumber] LIKE '%'+@SearchParam+'%' OR
				[sa].[FirstName] LIKE '%'+@SearchParam+'%' OR
				[sa].[LastName] LIKE '%'+@SearchParam+'%' 
				)
			RETURN
		END
	ELSE
		BEGIN
			SELECT [sa].[Id],[sa].[UserName],[sa].[NormalizedUserName],[sa].[Email],[sa].[NormalizedEmail],[sa].[EmailConfirmed],
				[sa].[PhoneNumber],[sa].[PhoneNumberConfirmed],[sa].[TwoFactorEnabled],[sa].[LockoutEndDate],[sa].[LockoutEnabled],
				[sa].[AccessFailedCount],[sa].[SecurityStamp],[sa].[FirstName],[sa].[LastName],[sa].[Photo],[sa].[City],
				[sa].[LastLoginIP],[sa].[LastLoginDate],[sa].[IsActive],[sa].[ModifyDate],[sa].[AddedDate] 
				, [t1].[Id], [t1].[FullName], [t1].[UserName] --<-------- Added By
				, [t2].[Id], [t2].[FullName], [t2].[UserName] --<-------- ModifyBy
			FROM [dbo].[SuperAdmin_User] [sa]
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
