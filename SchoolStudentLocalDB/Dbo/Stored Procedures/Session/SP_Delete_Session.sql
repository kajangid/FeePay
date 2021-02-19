-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to delete (isdelete column true) Session data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Delete_Session]
	(
	@Id INT,
	@ModifyBy INT = NULL
	)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [Session] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [Session] SET
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
