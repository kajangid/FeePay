-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to delete (isdelete column true) StudentFees data 
-- =============================================
CREATE PROCEDURE [dbo].[SP_Remove_StudentFees]
	(		
	 @Id INT
	,@ModifyBy INT = NULL
	)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [StudentFees] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [StudentFees] SET
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
