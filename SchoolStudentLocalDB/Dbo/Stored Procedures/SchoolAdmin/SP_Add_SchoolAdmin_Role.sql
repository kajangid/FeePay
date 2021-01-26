CREATE PROCEDURE [dbo].[SP_Add_SchoolAdmin_Role]
(
@Name NVARCHAR(256)
,@NormalizedName NVARCHAR(256)
,@IsActive BIT = 0
,@Access NVARCHAR(MAX) = NULL
,@AddedBy INT NULL
)
AS
BEGIN
    IF NOT EXISTS(SELECT *FROM [dbo].[SchoolAdmin_Role] WHERE [NormalizedName] = @NormalizedName AND [IsDelete] = 0)
    BEGIN

	    INSERT INTO [dbo].[SchoolAdmin_Role]
               ([Name]
               ,[NormalizedName]
			   ,[Access]
               ,[IsActive]
               ,[IsDelete]
               ,[ModifyDate]
               ,[ModifyBy]
               ,[AddedDate]
               ,[AddedBy])
         VALUES
               (@Name
               ,@NormalizedName
			   ,@Access
               ,@IsActive
               ,0
               ,GETDATE()
               ,@AddedBy
               ,GETDATE()
               ,@AddedBy)

			   SELECT CAST(SCOPE_IDENTITY() AS INT)
    END
    ELSE
    BEGIN
        RETURN 0
    END
END