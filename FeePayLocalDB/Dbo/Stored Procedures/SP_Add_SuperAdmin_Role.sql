CREATE PROCEDURE [dbo].[SP_Add_SuperAdmin_Role]
(
@Name NVARCHAR(256)
,@NormalizedName NVARCHAR(256)
,@IsActive BIT = 0
,@AddedBy INT NULL
)
AS
BEGIN
    IF NOT EXISTS(SELECT *FROM [dbo].[SuperAdmin_Role] WHERE [NormalizedName] = @NormalizedName AND [IsDelete] = 0)
    BEGIN

	    INSERT INTO [dbo].[SuperAdmin_Role]
               ([Name]
               ,[NormalizedName]
               ,[IsActive]
               ,[IsDelete]
               ,[ModifyDate]
               ,[ModifyBy]
               ,[AddedDate]
               ,[AddedBy])
         VALUES
               (@Name
               ,@NormalizedName
               ,@IsActive
               ,0
               ,GETDATE()
               ,@AddedBy
               ,GETDATE()
               ,@AddedBy)

        RETURN CAST(SCOPE_IDENTITY() AS INT)
    END
    ELSE
    BEGIN
        RETURN 0
    END
END
