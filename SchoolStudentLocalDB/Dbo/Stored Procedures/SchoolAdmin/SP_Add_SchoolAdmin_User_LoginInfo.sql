CREATE PROCEDURE [dbo].[SP_Add_SchoolAdmin_User_LoginInfo]
(
@Id INT
,@LastLoginIP NVARCHAR(50) NULL
)
AS
BEGIN
    IF EXISTS(SELECT *FROM [dbo].[SchoolAdmin_User] WHERE [Id] = @Id AND [IsActive] = 1 AND [IsDelete] = 0)
    BEGIN 
        UPDATE [dbo].[SchoolAdmin_User]
        SET 
            [LastLoginDate] = GETDATE()
            ,[LastLoginIP] = @LastLoginIP 
        WHERE 
        [Id] = @Id AND
        [IsActive] = 1 AND
        [IsDelete] = 0

        RETURN @Id
    END
    ELSE
    BEGIN
        RETURN 0
    END
END
