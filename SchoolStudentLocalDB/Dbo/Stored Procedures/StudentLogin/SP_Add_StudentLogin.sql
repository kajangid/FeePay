CREATE PROCEDURE [dbo].[SP_Add_StudentLogin]
(
@AccessFailedCount INT = NULL
,@AddedBy INT = NULL
,@Email NVARCHAR(256) = NULL
,@EmailConfirmed BIT						--Required 
,@IsActive  BIT = NULL
,@IsLogin BIT = NULL
,@LastLoginDate DATETIMEOFFSET(7) = NULL
,@LastLoginDevice NVARCHAR(500) = NULL
,@LastLoginIP NVARCHAR(50) = NULL
,@LastLoginLocation NVARCHAR(110) = NULL
,@LockoutEnabled BIT = NULL
,@LockoutEndDate DATETIMEOFFSET(7) = NULL
,@NormalizedEmail NVARCHAR(256) = NULL
,@NormalizedUserName NVARCHAR(256)			--Required
,@Password NVARCHAR(50)						--Required
,@PasswordHash NVARCHAR(1000)				--Required
,@PhoneNumber NVARCHAR(50) = NULL
,@PhoneNumberConfirmed BIT					--Required
,@SecurityStamp NVARCHAR(1000) = NULL
,@TwoFactorEnabled BIT						--Required
,@UserName NVARCHAR(256)					--Required

)
AS
BEGIN
    IF NOT EXISTS(SELECT *FROM [dbo].[StudentLogin] WHERE [UserName] = @UserName AND [IsDelete] = 0)
    BEGIN 
        INSERT INTO [dbo].[StudentLogin]
               ([UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed]
               ,[TwoFactorEnabled],[LockoutEndDate],[LockoutEnabled],[AccessFailedCount],[SecurityStamp],[Password],[IsLogin],[LastLoginIP]
               ,[LastLoginDate],[LastLoginDevice],[LastLoginLocation],[IsActive],[IsDelete],[ModifyDate],[ModifyBy],[AddedDate]
               ,[AddedBy])
         VALUES
               (@UserName,@NormalizedUserName,@Email,@NormalizedEmail,@EmailConfirmed,@PasswordHash,@PhoneNumber,@PhoneNumberConfirmed
               ,@TwoFactorEnabled,@LockoutEndDate,@LockoutEnabled,@AccessFailedCount,@SecurityStamp,@Password,@IsLogin,@LastLoginIP
               ,@LastLoginDate,@LastLoginDevice,@LastLoginLocation,@IsActive,0,GETDATE(),@AddedBy,GETDATE()
               ,@AddedBy)

        SELECT CAST(SCOPE_IDENTITY() AS INT)
    END
    ELSE
    BEGIN
        SELECT 0
    END
END
