-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to update super user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_SuperAdmin_User]
(
@Id INT
,@AccessFailedCount             INT             = NULL
,@Email                         NVARCHAR(256)   = NULL
,@EmailConfirmed                BIT             = NULL
,@NormalizedEmail               NVARCHAR(256)   = NULL
,@NormalizedUserName            NVARCHAR(256)   = NULL
,@PasswordHash                  NVARCHAR(1000)  = NULL
,@PhoneNumber                   NVARCHAR(50)    = NULL
,@PhoneNumberConfirmed          BIT             = NULL
,@TwoFactorEnabled              BIT             = NULL
,@LockoutEndDate                DATETIME        = NULL
,@LockoutEnabled                BIT             = NULL
,@SecurityStamp                 NVARCHAR(1000)  = NULL
,@FirstName                     NVARCHAR(50)    = NULL
,@LastName                      NVARCHAR(50)    = NULL
,@Password                      NVARCHAR(50)    = NULL
,@FullName                      NVARCHAR(110)   = NULL
,@Photo                         NVARCHAR(500)   = NULL
,@City                          NVARCHAR(50)    = NULL
,@IsActive                      BIT             = NULL
,@ModifyBy                      INT             = NULL
,@UserName                      NVARCHAR(256)   = NULL
)
AS
BEGIN
    IF EXISTS(SELECT *FROM [dbo].[SuperAdmin_User] WHERE [Id] = @Id AND [IsDelete] = 0)
    BEGIN 
        UPDATE [dbo].[SuperAdmin_User]
        SET
            [UserName]               =   CASE WHEN @UserName IS NOT NULL THEN @UserName ELSE [UserName] END ,
            [NormalizedUserName]     =   CASE WHEN @NormalizedUserName IS NOT NULL THEN @NormalizedUserName ELSE [NormalizedUserName] END ,
            [Email]                  =   CASE WHEN @Email IS NOT NULL THEN @Email ELSE [Email] END ,
            [NormalizedEmail]        =   CASE WHEN @NormalizedEmail IS NOT NULL THEN @NormalizedEmail ELSE [NormalizedEmail] END ,
            [EmailConfirmed]         =   CASE WHEN @EmailConfirmed IS NOT NULL THEN @EmailConfirmed ELSE [EmailConfirmed] END ,
            [PasswordHash]           =   CASE WHEN @PasswordHash IS NOT NULL THEN @PasswordHash ELSE [PasswordHash] END ,
            [PhoneNumber]            =   CASE WHEN @PhoneNumber IS NOT NULL THEN @PhoneNumber ELSE [PhoneNumber] END ,
            [PhoneNumberConfirmed]   =   CASE WHEN @PhoneNumberConfirmed IS NOT NULL THEN @PhoneNumberConfirmed ELSE [PhoneNumberConfirmed] END ,
            [TwoFactorEnabled]       =   CASE WHEN @TwoFactorEnabled IS NOT NULL THEN @TwoFactorEnabled ELSE [TwoFactorEnabled] END ,
            [LockoutEndDate]         =   CASE WHEN @LockoutEndDate IS NOT NULL THEN @LockoutEndDate ELSE [LockoutEndDate] END ,
            [LockoutEnabled]         =   CASE WHEN @LockoutEnabled IS NOT NULL THEN @LockoutEnabled ELSE [LockoutEnabled] END ,
            [AccessFailedCount]      =   CASE WHEN @AccessFailedCount IS NOT NULL THEN @AccessFailedCount ELSE [AccessFailedCount] END ,
            [SecurityStamp]          =   CASE WHEN @SecurityStamp IS NOT NULL THEN @SecurityStamp ELSE [SecurityStamp] END ,
            [FirstName]              =   CASE WHEN @FirstName IS NOT NULL THEN @FirstName ELSE [FirstName] END ,
            [LastName]               =   CASE WHEN @LastName IS NOT NULL THEN @LastName ELSE [LastName] END ,
            [Password]               =   CASE WHEN @Password IS NOT NULL THEN @Password ELSE [Password] END ,
            [FullName]               =   CASE WHEN @FullName IS NOT NULL THEN @FullName ELSE [FullName] END ,
            [Photo]                  =   CASE WHEN @Photo IS NOT NULL THEN @Photo ELSE [Photo] END ,
            [City]                   =   CASE WHEN @City IS NOT NULL THEN @City ELSE [City] END ,
            [IsActive]               =   CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END ,
            [ModifyDate]             =   GETDATE(),
            [ModifyBy]               =   @ModifyBy
        WHERE 
            Id          = @Id   AND
            [IsDelete]  = 0

        SELECT @Id
    END
    ELSE
    BEGIN
        RETURN 0
    END
END
