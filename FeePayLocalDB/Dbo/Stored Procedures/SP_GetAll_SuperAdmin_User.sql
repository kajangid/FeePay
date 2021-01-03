CREATE PROCEDURE [dbo].[SP_GetAll_SuperAdmin_User]
(
@Id INT = 0
,@UserName NVARCHAR(256) = NULL
,@NormalizedUserName NVARCHAR(256) = NULL
,@Email NVARCHAR(256) = NULL
,@NormalizedEmail NVARCHAR(256) = NULL
,@PhoneNumber NVARCHAR(50) = NULL
,@IsActive BIT = 1
,@ModifyBy INT = 0
,@AddedBy INT = 0
)
AS
BEGIN
	SELECT *FROM [dbo].[SuperAdmin_User] WHERE
			[IsDelete] = 0 AND
			([Id] != 0 AND [Id] = @Id) OR
			([UserName] IS NOT NULL AND [UserName] = @UserName) OR
			([NormalizedUserName] IS NOT NULL AND [NormalizedUserName] = @NormalizedUserName) OR
			([Email] IS NOT NULL AND [Email] = @Email) OR
			([NormalizedEmail] IS NOT NULL AND [NormalizedEmail] = @NormalizedEmail) OR
			([PhoneNumber] IS NOT NULL AND [PhoneNumber] = @PhoneNumber) OR			
			([IsActive] IS NOT NULL AND [IsActive] = @IsActive) OR
			([ModifyBy] != 0 AND [ModifyBy] = @ModifyBy) OR
			([AddedBy] !=0 AND [AddedBy] = @AddedBy)
	RETURN 
END
RETURN 0
