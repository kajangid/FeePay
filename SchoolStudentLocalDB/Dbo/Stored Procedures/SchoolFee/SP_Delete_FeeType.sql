CREATE PROCEDURE [dbo].[SP_Delete_FeeType]
	(
	@Id INT,
	@ModifyBy INT = NULL
	)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [FeeType] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [FeeType] SET
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
		SELECT 0
	END
END
