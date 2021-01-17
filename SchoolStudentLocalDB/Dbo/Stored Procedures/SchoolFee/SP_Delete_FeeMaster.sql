CREATE PROCEDURE [dbo].[SP_Delete_FeeMaster]
	(
	@Id INT,
	@ModifyBy INT = NULL
	)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [FeeMaster] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [FeeMaster] SET
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
