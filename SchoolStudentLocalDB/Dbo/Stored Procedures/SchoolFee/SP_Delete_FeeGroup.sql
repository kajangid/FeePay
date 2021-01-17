CREATE PROCEDURE [dbo].[SP_Delete_FeeGroup]
	(
	@Id INT,
	@ModifyBy INT = NULL
	)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [FeeGroup] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [FeeGroup] SET
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
