-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to add Section data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Add_Section]
	(	
	@Name NVARCHAR(10), 
	@NormalizedName NVARCHAR(10),
	@Description NVARCHAR(350) = NULL,
	@IsActive BIT = NULL,
	@AddedBy INT = NULL
	)
AS
BEGIN
	IF NOT EXISTS(SELECT [Id] FROM [dbo].[Section] WHERE [Name] = @Name AND [NormalizedName] = @NormalizedName AND [IsDelete] = 0)
	BEGIN
		INSERT INTO [dbo].[Section]
		([Name],[NormalizedName],[Description],[IsActive],[IsDelete],[AddedBy],[AddedDate])
		VALUES
		(@Name,UPPER(@NormalizedName),@Description,@IsActive,0,@AddedBy,GETDATE())

		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN
		SELECT 0
	END
END
