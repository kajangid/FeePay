-- =============================================
-- Author:		Karan
-- Create date: 17-02-2021
-- Description:	Sp to Delete[Archive] Document data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Delete_Document]
(
	@Id INT,
	@ModifyBy INT = NULL
)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[Documents] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [dbo].[Documents] SET
			[IsActive] = 0,
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
