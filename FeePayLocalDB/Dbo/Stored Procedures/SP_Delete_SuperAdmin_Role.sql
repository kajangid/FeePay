CREATE PROCEDURE [dbo].[SP_Delete_SuperAdmin_Role]
(
@Id INT 
)
AS
BEGIN
    IF NOT EXISTS(SELECT *FROM [dbo].[SuperAdmin_Role] WHERE [Id] = @Id)
    BEGIN
        UPDATE [dbo].[SuperAdmin_Role] SET 
        [IsDelete] = 1 
        WHERE 
        [Id] = @Id

	    RETURN @Id
    END
    ELSE
    BEGIN
        RETURN 0
    END
END
