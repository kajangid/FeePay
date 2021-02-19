-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to add Session data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Add_Session]
(
	@Year NVARCHAR(20),
	@StartYear NVARCHAR(20) = NULL,
	@EndYear NVARCHAR(20) = NULL,
	@Description NVARCHAR(350) = NULL,
	@IsActive BIT = NULL,
	@AddedBy INT = NULL
)
AS
BEGIN
	IF NOT EXISTS(SELECT [Id] FROM [dbo].[Session] WHERE [Year] = @Year AND [IsDelete] = 0)
	BEGIN
		INSERT INTO [dbo].[Session]
		([Year],[StartYear],[EndYear],[Description],[IsActive],[IsDelete],[AddedBy],[AddedDate])
		VALUES
		(@Year,@StartYear,@EndYear,@Description,@IsActive,0,@AddedBy,GETDATE())

		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN
		SELECT 0
	END
END

