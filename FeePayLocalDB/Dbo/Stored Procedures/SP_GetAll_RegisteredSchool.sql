CREATE PROCEDURE [dbo].[SP_GetAll_RegisteredSchool]
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
	SELECT TOP(100) [Id], [Name], [NormalizedName], [IsActive], [IsDelete], [ModifyDate], [ModifyBy], [AddedDate], [AddedBy]
	FROM [dbo].[RegisteredSchool] WHERE
			[IsDelete] = 0 AND
			([Id] != 0 AND [Id] = @Id) OR
			([Name] IS NOT NULL AND [Name] = @Name) OR
			([NormalizedName] IS NOT NULL AND [NormalizedName] = @NormalizedName) OR
			([IsActive] IS NOT NULL AND [IsActive] = @IsActive) OR
			([ModifyBy] != 0 AND [ModifyBy] = @ModifyBy) OR
			([AddedBy] !=0 AND [AddedBy] = @AddedBy)

END
