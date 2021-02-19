-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to update Session data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Session]
(
	@Id INT,
	@Year NVARCHAR(20) = NULL,
	@StartYear NVARCHAR(20) = NULL,
	@EndYear NVARCHAR(20) = NULL,
	@Description NVARCHAR(350) = NULL,
	@IsActive BIT = NULL,
	@ModifyBy INT = NULL
)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[Session] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [dbo].[Session] SET
			[Year] = CASE WHEN @Year IS NOT NULL THEN @Year ELSE [Year] END,
			[StartYear] = CASE WHEN @StartYear IS NOT NULL THEN @StartYear ELSE [StartYear] END,
			[EndYear] = CASE WHEN @EndYear IS NOT NULL THEN @EndYear ELSE [EndYear] END,
			[Description] = CASE WHEN @Description IS NOT NULL THEN @Description ELSE [Description] END,
			[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END,
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

