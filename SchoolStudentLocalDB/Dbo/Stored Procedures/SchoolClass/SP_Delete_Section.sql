CREATE PROCEDURE [dbo].[SP_Delete_Section]
	(
	@Id INT,
	@ModifyBy INT = NULL
	)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [Section] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [Section] SET
			[IsDelete] = 1,
			[ModifyBy] =  @ModifyBy,
			[ModifyDate] = GETDATE()
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
