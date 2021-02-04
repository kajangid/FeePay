-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to add super user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Add_SuperAdmin_User]
(
@UserName                                    NVARCHAR(256)      --Required
,@EmailConfirmed                             BIT                --Required
,@NormalizedUserName                         NVARCHAR(256)      --Required
,@Password                                   NVARCHAR(50)       --Required
,@PasswordHash                               NVARCHAR(1000)     --Required
,@PhoneNumberConfirmed                       BIT                --Required
,@TwoFactorEnabled                           BIT                --Required
,@AddedBy                                    INT                = NULL
,@City                                       NVARCHAR(50)       = NULL
,@Email                                      NVARCHAR(256)      = NULL
,@FirstName                                  NVARCHAR(50)       = NULL
,@FullName                                   NVARCHAR(110)      = NULL
,@IsActive                                   BIT                = NULL
,@LastLoginDate                              DATETIME           = NULL
,@LastLoginIP                                NVARCHAR(50)       = NULL
,@LastName                                   NVARCHAR(50)       = NULL
,@LockoutEnabled                             BIT                = NULL
,@LockoutEndDate                             DATETIME           = NULL
,@NormalizedEmail                            NVARCHAR(256)      = NULL
,@PhoneNumber                                NVARCHAR(50)       = NULL
,@Photo                                      NVARCHAR(500)      = NULL
,@SecurityStamp                              NVARCHAR(1000)     = NULL
,@AccessFailedCount                          INT                = NULL

)
AS
BEGIN
    IF NOT EXISTS(SELECT [Id] FROM [dbo].[SuperAdmin_User] WHERE [UserName] = @UserName AND [IsDelete] = 0)
    BEGIN 
        INSERT INTO [dbo].[SuperAdmin_User]
               ([UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed]
               ,[TwoFactorEnabled],[LockoutEndDate],[LockoutEnabled],[AccessFailedCount],[SecurityStamp],[FirstName],[LastName],[Password]
               ,[FullName],[Photo],[City],[LastLoginIP],[LastLoginDate],[IsActive],[IsDelete],[ModifyDate],[AddedDate],[AddedBy])
         VALUES
               (@UserName,@NormalizedUserName,@Email,@NormalizedEmail,@EmailConfirmed,@PasswordHash,@PhoneNumber,@PhoneNumberConfirmed
               ,@TwoFactorEnabled,@LockoutEndDate,@LockoutEnabled,@AccessFailedCount,@SecurityStamp,@FirstName,@LastName,@Password
               ,@FullName,@Photo,@City,@LastLoginIP,@LastLoginDate,@IsActive,0,GETDATE(),GETDATE(),@AddedBy)

        SELECT CAST(SCOPE_IDENTITY() AS INT)
    END
    ELSE
        RETURN 0
END