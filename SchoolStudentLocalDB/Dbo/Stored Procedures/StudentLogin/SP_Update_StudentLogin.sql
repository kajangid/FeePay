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
,@FirstName NVARCHAR(50) = NULL
,@LastName NVARCHAR(50) = NULL
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
