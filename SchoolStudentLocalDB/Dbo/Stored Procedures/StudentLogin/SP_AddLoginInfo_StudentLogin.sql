CREATE PROCEDURE [dbo].[SP_AddLoginInfo_StudentLogin]
(
@Id INT
,@IsLogin BIT = NULL
,@LastLoginIP NVARCHAR(50) NULL
,@LastLoginDevice NVARCHAR(500) = NULL
,@LastLoginLocation NVARCHAR(110) = NULL
)
AS
BEGIN
    IF EXISTS(SELECT *FROM [dbo].[StudentLogin] WHERE [Id] = @Id AND [IsActive] = 1 AND [IsDelete] = 0)
    BEGIN 
        UPDATE [dbo].[StudentLogin]
        SET 
            [LastLoginDate] = GETDATE()
            ,[LastLoginIP] = @LastLoginIP 
            ,[LastLoginDevice] = @LastLoginDevice 
            ,[LastLoginLocation] = @LastLoginLocation
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
