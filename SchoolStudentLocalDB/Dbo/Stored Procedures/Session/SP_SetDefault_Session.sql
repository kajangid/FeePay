-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to set default Session data
-- =============================================
CREATE PROCEDURE [dbo].[SP_SetDefault_Session]
	@Id INT
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[Session] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN
	  -- First Set All Inactive --
		UPDATE [dbo].[Session] SET
			[IsActive] = 0
		WHERE 
			[IsDelete] = 0

	  -- First Set Active For Given Id --
		UPDATE [dbo].[Session] SET
			[IsActive] = 1
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