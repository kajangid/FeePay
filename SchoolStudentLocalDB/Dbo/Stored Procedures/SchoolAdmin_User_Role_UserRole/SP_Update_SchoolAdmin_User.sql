CREATE PROCEDURE [dbo].[SP_Update_SchoolAdmin_User]
(
@Id INT
,@AccessFailedCount INT = NULL
,@Email NVARCHAR(256) = NULL
,@EmailConfirmed BIT  = NULL
,@FirstName NVARCHAR(50) = NULL
,@FullName NVARCHAR(110) = NULL
,@IsActive  BIT = NULL
,@LockoutEndDate datetimeoffset(7) = NULL
,@LockoutEnabled BIT = NULL
,@LastName NVARCHAR(50) = NULL
,@ModifyBy INT = NULL
,@NormalizedEmail NVARCHAR(256) = NULL
,@NormalizedUserName NVARCHAR(256) = NULL
,@Password NVARCHAR(50) = NULL
,@PasswordHash NVARCHAR(1000) = NULL
,@PhoneNumber NVARCHAR(50) = NULL
,@PhoneNumberConfirmed BIT = NULL
,@SecurityStamp NVARCHAR(1000) = NULL
,@TwoFactorEnabled BIT = NULL
,@UserName NVARCHAR(256) = NULL
)
AS
BEGIN
    IF EXISTS(SELECT *FROM [dbo].[SchoolAdmin_User] WHERE [Id] = @Id)
    BEGIN 
        UPDATE [dbo].[SchoolAdmin_User]
        SET 
            [UserName] = CASE WHEN @UserName IS NOT NULL THEN @UserName ELSE [UserName] END ,
            [NormalizedUserName] = CASE WHEN @NormalizedUserName IS NOT NULL THEN @NormalizedUserName ELSE [NormalizedUserName] END ,
            [Email] = CASE WHEN @Email IS NOT NULL THEN @Email ELSE [Email] END ,
            [NormalizedEmail] = CASE WHEN @NormalizedEmail IS NOT NULL THEN @NormalizedEmail ELSE [NormalizedEmail] END ,
            [EmailConfirmed] = CASE WHEN @EmailConfirmed IS NOT NULL THEN @EmailConfirmed ELSE [EmailConfirmed] END ,
            [PasswordHash] = CASE WHEN @PasswordHash IS NOT NULL THEN @PasswordHash ELSE [PasswordHash] END ,
            [PhoneNumber] = CASE WHEN @PhoneNumber IS NOT NULL THEN @PhoneNumber ELSE [PhoneNumber] END ,
            [PhoneNumberConfirmed] = CASE WHEN @PhoneNumberConfirmed IS NOT NULL THEN @PhoneNumberConfirmed ELSE [PhoneNumberConfirmed] END ,
            [TwoFactorEnabled] = CASE WHEN @TwoFactorEnabled IS NOT NULL THEN @TwoFactorEnabled ELSE [TwoFactorEnabled] END ,
            [LockoutEndDate] = CASE WHEN @LockoutEndDate IS NOT NULL THEN @LockoutEndDate ELSE [LockoutEndDate] END ,
            [LockoutEnabled] = CASE WHEN @LockoutEnabled IS NOT NULL THEN @LockoutEnabled ELSE [LockoutEnabled] END ,
            [AccessFailedCount] = CASE WHEN @AccessFailedCount IS NOT NULL THEN @AccessFailedCount ELSE [AccessFailedCount] END ,
            [SecurityStamp] = CASE WHEN @SecurityStamp IS NOT NULL THEN @SecurityStamp ELSE [SecurityStamp] END ,
            [FirstName] = CASE WHEN @FirstName IS NOT NULL THEN @FirstName ELSE [FirstName] END ,
            [LastName] = CASE WHEN @LastName IS NOT NULL THEN @LastName ELSE [LastName] END ,
            [Password] = CASE WHEN @Password IS NOT NULL THEN @Password ELSE [Password] END ,
            [FullName] = CASE WHEN @FullName IS NOT NULL THEN @FullName ELSE [FullName] END ,
            [IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END ,
            [ModifyDate] = GETDATE(),
            [ModifyBy] = @ModifyBy
        WHERE 
        Id = @Id AND
        [IsDelete] = 0

        RETURN @Id
    END
    ELSE
    BEGIN
        RETURN 0
    END
END
