CREATE PROCEDURE [dbo].[SP_Delete_SchoolAdmin_Role]
(
@Id INT 
,@ModifyBy INT NULL
)
AS
BEGIN
    IF EXISTS(SELECT *FROM [dbo].[SchoolAdmin_Role] WHERE [Id] = @Id)
    BEGIN
        UPDATE [dbo].[SchoolAdmin_Role] SET 
        [IsDelete] = 1
        ,[ModifyDate] = GETDATE()
        ,[ModifyBy] = @ModifyBy
        WHERE 
        [Id] = @Id

	    RETURN @Id
    END
    ELSE
    BEGIN
        RETURN 0
    END
END

