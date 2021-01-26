CREATE PROCEDURE [dbo].[SP_Update_StudentLogin]
(
@Id INT
,@AccessFailedCount INT = NULL
,@Email NVARCHAR(256) = NULL
,@EmailConfirmed BIT  = NULL
,@NormalizedEmail NVARCHAR(256) = NULL
,@NormalizedUserName NVARCHAR(256) = NULL
,@PasswordHash NVARCHAR(1000) = NULL
,@PhoneNumber NVARCHAR(50) = NULL
,@PhoneNumberConfirmed BIT = NULL
,@TwoFactorEnabled BIT = NULL
,@LockoutEndDate datetimeoffset(7) = NULL
,@LockoutEnabled BIT = NULL
,@SecurityStamp NVARCHAR(1000) = NULL
,@Password NVARCHAR(50) = NULL
,@IsActive  BIT = NULL
,@ModifyBy INT = NULL
,@UserName NVARCHAR(256) = NULL
)
AS
BEGIN
    IF EXISTS(SELECT *FROM [dbo].[StudentLogin] WHERE [Id] = @Id)
    BEGIN 
        UPDATE [dbo].[StudentLogin]
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
            [Password] = CASE WHEN @Password IS NOT NULL THEN @Password ELSE [Password] END ,
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
