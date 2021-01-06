CREATE PROCEDURE [dbo].[SP_Delete_StudentLogin]
	@Id INT ,@ModifyBy INT NULL
AS
BEGIN
	IF EXISTS(SELECT *FROM [dbo].[StudentLogin] WHERE [Id] = @Id)
	BEGIN
		UPDATE [dbo].[StudentLogin] SET
		[IsDelete] = 1
        ,[ModifyDate] = GETDATE()
        ,[ModifyBy] = @ModifyBy
		WHERE 
		[Id] = @Id

		RETURN  @Id
	END
	RETURN 0
END
