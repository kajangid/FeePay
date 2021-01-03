CREATE PROCEDURE [dbo].[SP_Add_SuperAdmin_User]
(
@AccessFailedCount INT = NULL
,@AddedBy INT = NULL
,@City NVARCHAR(50) = NULL
,@Email NVARCHAR(256) = NULL
,@EmailConfirmed BIT						--Required
,@FirstName NVARCHAR(50) = NULL
,@FullName NVARCHAR(110) = NULL
,@IsActive  BIT = NULL
,@LastLoginDate DATETIMEOFFSET(7) = NULL
,@LastLoginIP NVARCHAR(50) = NULL
,@LastName NVARCHAR(50) = NULL
,@LockoutEnabled BIT = NULL
,@LockoutEndDate DATETIMEOFFSET(7) = NULL
,@NormalizedEmail NVARCHAR(256) = NULL
,@NormalizedUserName NVARCHAR(256)			--Required
,@Password NVARCHAR(50)						--Required
,@PasswordHash NVARCHAR(1000)				--Required
,@PhoneNumber NVARCHAR(50) = NULL
,@PhoneNumberConfirmed BIT					--Required
,@Photo NVARCHAR(500) = NULL
,@SecurityStamp NVARCHAR(1000) = NULL
,@TwoFactorEnabled BIT						--Required
,@UserName NVARCHAR(256)					--Required

)
AS
BEGIN
    IF NOT EXISTS(SELECT *FROM [dbo].[SuperAdmin_User] WHERE [UserName] = @UserName)
    BEGIN 
        INSERT INTO [dbo].[SuperAdmin_User]
               ([UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed]
               ,[TwoFactorEnabled],[LockoutEndDate],[LockoutEnabled],[AccessFailedCount],[SecurityStamp],[FirstName],[LastName],[Password]
               ,[FullName],[Photo],[City],[LastLoginIP],[LastLoginDate],[IsActive],[IsDelete],[ModifyDate],[ModifyBy],[AddedDate]
               ,[AddedBy])
         VALUES
               (@UserName,@NormalizedUserName,@Email,@NormalizedEmail,@EmailConfirmed,@PasswordHash,@PhoneNumber,@PhoneNumberConfirmed
               ,@TwoFactorEnabled,@LockoutEndDate,@LockoutEnabled,@AccessFailedCount,@SecurityStamp,@FirstName,@LastName,@Password
               ,@FullName,@Photo,@City,@LastLoginIP,@LastLoginDate,@IsActive,0,GETDATE(),@AddedBy,GETDATE()
               ,@AddedBy)

        RETURN CAST(SCOPE_IDENTITY() AS INT)
    END
    ELSE
    BEGIN
        RETURN 0
    END
END