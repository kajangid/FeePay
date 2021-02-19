-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get fee group data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_FeeGroup]
(
@Id INT = 0
,@Name NVARCHAR(256) = NULL
,@NormalizedName NVARCHAR(256) = NULL
,@IsActive BIT = NULL
)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
		
	IF(@Id != 0)
	BEGIN	
	SELECT 
		[fg].[Id], [fg].[Name], [fg].[NormalizedName], [fg].[Description], [fg].[IsActive], [fg].[IsDelete], [fg].[ModifyDate]
		, [fg].[ModifyBy], [fg].[AddedDate], [fg].[AddedBy]
		FROM [dbo].[FeeGroup] [fg]
		WHERE
		[fg].[IsDelete] = 0 
		AND [fg].[Id] = @Id 
		AND [fg].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [fg].[IsActive] END

		RETURN 
	END
	IF(@Name IS NOT NULL)
	BEGIN	
	SELECT 
		[fg].[Id], [fg].[Name], [fg].[NormalizedName], [fg].[Description], [fg].[IsActive], [fg].[IsDelete], [fg].[ModifyDate]
		, [fg].[ModifyBy], [fg].[AddedDate], [fg].[AddedBy]
		FROM [dbo].[FeeGroup] [fg]
		WHERE
		[fg].[IsDelete] = 0 
		AND [fg].[Name] = @Name 
		AND [fg].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [fg].[IsActive] END

		RETURN 
	END
	IF(@NormalizedName IS NOT NULL)
	BEGIN	
	SELECT 
		[fg].[Id], [fg].[Name], [fg].[NormalizedName], [fg].[Description], [fg].[IsActive], [fg].[IsDelete], [fg].[ModifyDate]
		, [fg].[ModifyBy], [fg].[AddedDate], [fg].[AddedBy]
		FROM [dbo].[FeeGroup] [fg]
		WHERE
		[fg].[IsDelete] = 0 
		AND [fg].[NormalizedName] = @NormalizedName
		AND [fg].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [fg].[IsActive] END

		RETURN 
	END
END