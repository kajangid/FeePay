-- =============================================
-- Author:		Karan
-- Create date: 21-01-2021
-- Description:	Sp to delete(IsDelete column true) Student Admission Form data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Delete_StudentAdmission]
	(
	@Id INT,
	@ModifyBy INT = NULL
	)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[StudentAdmission] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [dbo].[StudentAdmission] SET
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
