-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to add classSection data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Add_ClassSection]
	(	
	@SectionId INT, 
	@ClassId INT
	)
AS
BEGIN
	IF NOT EXISTS(SELECT [Id] FROM [dbo].[ClassSection] WHERE [ClassId] = @ClassId AND [SectionId] = @SectionId AND [IsDelete] = 0)
	BEGIN
		INSERT INTO [dbo].[ClassSection]
		([ClassId],[SectionId],[IsDelete])
		VALUES
		(@ClassId,@SectionId,0)

		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN
		SELECT 0
	END
END
