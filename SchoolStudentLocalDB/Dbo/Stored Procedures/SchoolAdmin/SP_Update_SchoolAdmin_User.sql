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
            [UserName] = @UserName,
            [NormalizedUserName] = @NormalizedUserName,
            [Email] = @Email,
            [NormalizedEmail] = @NormalizedEmail,
            [EmailConfirmed] = @EmailConfirmed,
            [PasswordHash] = @PasswordHash,
            [PhoneNumber] = @PhoneNumber,
            [PhoneNumberConfirmed] = @PhoneNumberConfirmed,
            [TwoFactorEnabled] = @TwoFactorEnabled,
            [LockoutEndDate] = @LockoutEndDate,
            [LockoutEnabled] = @LockoutEnabled,
            [AccessFailedCount] = @AccessFailedCount,
            [SecurityStamp] = @SecurityStamp,
            [FirstName] = @FirstName,
            [LastName] = @LastName,
            [Password] = @Password,
            [FullName] = @FullName,
            [IsActive] = @IsActive,
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
