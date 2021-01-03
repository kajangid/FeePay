CREATE PROCEDURE [dbo].[SP_Delete_SuperAdmin_User]
	@Id INT ,@ModifyBy INT NULL
AS
BEGIN
	IF EXISTS(SELECT *FROM [dbo].[SuperAdmin_User] WHERE [Id] = @Id)
	BEGIN
		UPDATE [dbo].[SuperAdmin_User] SET
		[IsDelete] = 1
        ,[ModifyDate] = GETDATE()
        ,[ModifyBy] = @ModifyBy
		WHERE 
		[Id] = @Id

		RETURN  @Id
	END
	RETURN 0
END
