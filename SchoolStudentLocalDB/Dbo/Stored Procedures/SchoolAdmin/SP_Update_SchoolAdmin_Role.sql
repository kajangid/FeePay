CREATE PROCEDURE [dbo].[SP_Update_SchoolAdmin_Role]
(
@Id INT 
,@Name NVARCHAR(256) NULL
,@NormalizedName NVARCHAR(256) NULL
,@IsActive BIT = 0
,@ModifyBy INT NULL
)
AS
BEGIN
    IF EXISTS(SELECT *FROM [dbo].[SchoolAdmin_Role] WHERE [Id] = @Id AND [IsDelete] = 0)
    BEGIN
        UPDATE [dbo].[SchoolAdmin_Role] SET 
        [Name] = @Name
        ,[NormalizedName] = @NormalizedName
        ,[IsActive] = @IsActive
        ,[ModifyDate] = GETDATE()
        ,[ModifyBy] = @ModifyBy 
        WHERE 
        [Id] = @Id AND 
        [IsDelete] = 0

	    RETURN @Id
    END
    ELSE
    BEGIN
        RETURN 0
    END
END
