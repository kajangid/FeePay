-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to find super user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_SuperAdmin_User]
@Id							INT					= NULL
,@NormalizedUserName		NVARCHAR(256)		= NULL
,@NormalizedEmail			NVARCHAR(256)		= NULL
,@PhoneNumber				NVARCHAR(50)		= NULL
,@IsActive					BIT					= NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF(@Id IS NOT NULL AND @Id <> 0)
	BEGIN
		SELECT [Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash],
		[PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDate], [LockoutEnabled], [AccessFailedCount],
		[SecurityStamp], [FirstName], [LastName], [FullName], [Photo], [City], [LastLoginIP], [LastLoginDate],
		[IsActive], [IsDelete], [ModifyDate], [ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[SuperAdmin_User] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND
		[Id] = @Id
		RETURN
	END
	ELSE IF(@NormalizedUserName IS NOT NULL)
	BEGIN
		SELECT [Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash],
		[PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDate], [LockoutEnabled], [AccessFailedCount],
		[SecurityStamp], [FirstName], [LastName], [FullName], [Photo], [City], [LastLoginIP], [LastLoginDate],
		[IsActive], [IsDelete], [ModifyDate], [ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[SuperAdmin_User] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND		
		[NormalizedUserName] = @NormalizedUserName
		RETURN 
	END
	ELSE IF(@NormalizedEmail IS NOT NULL)
	BEGIN
		SELECT [Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash],
		[PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDate], [LockoutEnabled], [AccessFailedCount],
		[SecurityStamp], [FirstName], [LastName], [FullName], [Photo], [City], [LastLoginIP], [LastLoginDate],
		[IsActive], [IsDelete], [ModifyDate], [ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[SuperAdmin_User] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND		
		[NormalizedEmail] = @NormalizedEmail
		RETURN 
	END
	ELSE IF(@PhoneNumber IS NOT NULL)
	BEGIN
		SELECT [Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash],
		[PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDate], [LockoutEnabled], [AccessFailedCount],
		[SecurityStamp], [FirstName], [LastName], [FullName], [Photo], [City], [LastLoginIP], [LastLoginDate],
		[IsActive], [IsDelete], [ModifyDate], [ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[SuperAdmin_User] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND		
		[PhoneNumber] = @PhoneNumber
		RETURN 
	END
	ELSE 
	BEGIN
		SELECT [Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash],
		[PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDate], [LockoutEnabled], [AccessFailedCount],
		[SecurityStamp], [FirstName], [LastName], [FullName], [Photo], [City], [LastLoginIP], [LastLoginDate],
		[IsActive], [IsDelete], [ModifyDate], [ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[SuperAdmin_User] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END 
		RETURN 		
	END
END