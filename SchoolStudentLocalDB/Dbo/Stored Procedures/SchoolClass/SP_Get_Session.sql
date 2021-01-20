-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to get Session data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Session]
(
	@Id INT = NULL,
	@Year NVARCHAR(20) = NULL,
	@IsActive BIT = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	IF(@Id IS NOT NULL AND @Id != 0)
	BEGIN	
		SELECT 
			[s].[Id], [s].[Year], [s].[StartYear], [s].[EndYear], [s].[Description], [s].[IsActive], [s].[IsDelete],
			[s].[ModifyDate], [s].[ModifyBy], [s].[AddedDate], [s].[AddedBy]
			FROM [dbo].[Session] [s] 
			WHERE
			[s].[IsDelete] = 0 
			AND [s].[Id] = @Id 
			AND [s].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [s].[IsActive] END

			RETURN 
	END
	ELSE IF(@Year IS NOT NULL)
	BEGIN
		SELECT 
			[s].[Id], [s].[Year], [s].[StartYear], [s].[EndYear], [s].[Description], [s].[IsActive], [s].[IsDelete],
			[s].[ModifyDate], [s].[ModifyBy], [s].[AddedDate], [s].[AddedBy]
			FROM [dbo].[Session] [s] 
			WHERE
			[s].[IsDelete] = 0 
			AND [s].[Year] = @Year
			AND [s].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [s].[IsActive] END

			RETURN 
	END
	ELSE 
	BEGIN
		SELECT 
			[s].[Id], [s].[Year], [s].[StartYear], [s].[EndYear], [s].[Description], [s].[IsActive], [s].[IsDelete],
			[s].[ModifyDate], [s].[ModifyBy], [s].[AddedDate], [s].[AddedBy]
			FROM [dbo].[Session] [s]  
			WHERE
			[s].[IsDelete] = 0 
			AND [s].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [s].[IsActive] END

			RETURN 
	END
		
END
