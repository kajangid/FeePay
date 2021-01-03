CREATE PROCEDURE [dbo].[SP_GetAll_SuperAdmin_Role]
(
@Id INT = 0
,@Name NVARCHAR(256) = NULL
,@NormalizedName NVARCHAR(256) = NULL
,@IsActive BIT = 0
,@ModifyBy BIT = 0
,@AddedBy INT NULL
)
AS
BEGIN
	SELECT *FROM [dbo].[SuperAdmin_Role] WHERE
			[IsDelete] = 0 AND
			([Id] != 0 AND [Id] = @Id) OR
			([Name] IS NOT NULL AND [Name] = @Name) OR
			([NormalizedName] IS NOT NULL AND [NormalizedName] = @NormalizedName) OR
			([IsActive] IS NOT NULL AND [IsActive] = @IsActive) OR
			([ModifyBy] != 0 AND [ModifyBy] = @ModifyBy) OR
			([AddedBy] !=0 AND [AddedBy] = @AddedBy)
	RETURN 
END
RETURN 0
