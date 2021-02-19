-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to add Class data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Add_Class]
(
	@Name NVARCHAR(50), 
	@NormalizedName NVARCHAR(50),
	@Description NVARCHAR(350) = NULL,
	@IsActive BIT = NULL,  
	@AddedBy INT = NULL
)
AS
BEGIN
	IF NOT EXISTS(SELECT [Id] FROM [dbo].[Class] WHERE [Name] = @Name AND [NormalizedName] = @NormalizedName AND [IsDelete] = 0)
	BEGIN
		INSERT INTO [dbo].[Class]
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

