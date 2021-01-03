CREATE PROCEDURE [dbo].[SP_AddLoginInfo_SuperAdmin]
(
@Id INT
,@LastLoginIP NVARCHAR(50) NULL
)
AS
BEGIN
    IF EXISTS(SELECT *FROM [dbo].[SuperAdmin_User] WHERE [Id] = @Id AND [IsActive] = 1 AND [IsDelete] = 0)
    BEGIN 
        UPDATE [dbo].[SuperAdmin_User]
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
