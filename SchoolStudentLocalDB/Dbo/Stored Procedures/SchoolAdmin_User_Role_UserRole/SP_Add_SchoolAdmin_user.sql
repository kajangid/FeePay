CREATE PROCEDURE [dbo].[SP_Add_SchoolAdmin_User]
(
@AccessFailedCount INT = NULL
,@AddedBy INT = NULL
,@Email NVARCHAR(256) = NULL
,@EmailConfirmed BIT						--Required
,@FirstName NVARCHAR(50) = NULL
,@FullName NVARCHAR(110) = NULL
,@IsActive  BIT = NULL
,@LastName NVARCHAR(50) = NULL
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
    IF NOT EXISTS(SELECT *FROM [dbo].[SchoolAdmin_User] WHERE [UserName] = @UserName)
    BEGIN 
        INSERT INTO [dbo].[SchoolAdmin_User]
               ([UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed]
               ,[TwoFactorEnabled],[LockoutEndDate],[LockoutEnabled],[AccessFailedCount],[SecurityStamp],[FirstName],[LastName],[Password]
               ,[FullName],[IsActive],[IsDelete],[ModifyDate],[ModifyBy],[AddedDate]
               ,[AddedBy])
         VALUES
               (@UserName,@NormalizedUserName,@Email,@NormalizedEmail,@EmailConfirmed,@PasswordHash,@PhoneNumber,@PhoneNumberConfirmed
               ,@TwoFactorEnabled,@LockoutEndDate,@LockoutEnabled,@AccessFailedCount,@SecurityStamp,@FirstName,@LastName,@Password
               ,@FullName,@IsActive,0,GETDATE(),@AddedBy,GETDATE()
               ,@AddedBy)

        RETURN CAST(SCOPE_IDENTITY() AS INT)
    END
    ELSE
    BEGIN
        RETURN 0
    END
END