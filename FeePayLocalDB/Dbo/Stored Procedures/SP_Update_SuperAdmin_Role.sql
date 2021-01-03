CREATE PROCEDURE [dbo].[SP_Update_SuperAdmin_Role]
(
@Id int 
,@Name nvarchar(256) NULL
,@NormalizedName NVARCHAR(256) NULL
,@IsActive BIT = 0
,@ModifyBy INT NULL
)
AS
BEGIN
    IF NOT EXISTS(SELECT *FROM [dbo].[SuperAdmin_Role] WHERE [Id] = @Id AND [IsDelete] = 0)
    BEGIN
        UPDATE [dbo].[SuperAdmin_Role] SET 
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
