CREATE PROCEDURE [dbo].[SP_Update_SchoolAdmin_Role]
(
@Id INT 
,@Name NVARCHAR(256) = NULL
,@NormalizedName NVARCHAR(256) = NULL
,@Access NVARCHAR(MAX) = NULL
,@IsActive BIT = NULL
,@ModifyBy INT = NULL
)
AS
BEGIN
    IF EXISTS(SELECT *FROM [dbo].[SchoolAdmin_Role] WHERE [Id] = @Id AND [IsDelete] = 0)
    BEGIN
        UPDATE [dbo].[SchoolAdmin_Role] SET 
        [Name] = CASE WHEN @Name IS NOT NULL THEN @Name ELSE [Name] END 
        ,[NormalizedName] = CASE WHEN @NormalizedName IS NOT NULL THEN @NormalizedName ELSE [NormalizedName] END 
		,[Access] = CASE WHEN @Access IS NOT NULL THEN @Access ELSE [Access] END 
        ,[IsActive] =  CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END 
        ,[ModifyDate] = GETDATE()
        ,[ModifyBy] = @ModifyBy 
        WHERE 
        [Id] = @Id AND 
        [IsDelete] = 0

	    SELECT @Id
    END
    ELSE
    BEGIN
        RETURN 0
    END
END
