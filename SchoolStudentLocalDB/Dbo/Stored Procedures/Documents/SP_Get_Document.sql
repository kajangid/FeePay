-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to get Document data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Document]
(
	@UserId					INT , 
	@UserType				NVARCHAR(50) , 
	@Id						INT = NULL,
	@Name					NVARCHAR(50) = NULL,
	@NormalizedName			NVARCHAR(50) = NULL,
	@IsActive				BIT = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	IF(@Id IS NOT NULL AND @Id != 0)
	BEGIN	
		SELECT 
			[d].[Id], [d].[Name], [d].[NormalizedName], [d].[UserId], [d].[UserType], [d].[Type], [d].[FileName],
			[d].[FileExtension], [d].[FileType], [d].[DownloadUrl], [d].[HtmlAlt], [d].[HtmlTitle], [d].[Description],
			[d].[IsActive], [d].[IsDelete], [d].[ModifyDate], [d].[ModifyBy], [d].[AddedDate], [d].[AddedBy]
			FROM [dbo].[Documents] [d] 
			WHERE
			[d].[IsDelete] = 0 
			AND [d].[UserId] = @UserId 
			AND [d].[UserType] = @UserType 
			AND [d].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [d].[IsActive] END
			AND [d].[Id] = @Id 

			RETURN 
	END	
	IF(@Name IS NOT NULL OR @NormalizedName IS NOT NULL)
	BEGIN	
		SELECT 
			[d].[Id], [d].[Name], [d].[NormalizedName], [d].[UserId], [d].[UserType], [d].[Type], [d].[FileName],
			[d].[FileExtension], [d].[FileType], [d].[DownloadUrl], [d].[HtmlAlt], [d].[HtmlTitle], [d].[Description],
			[d].[IsActive], [d].[IsDelete], [d].[ModifyDate], [d].[ModifyBy], [d].[AddedDate], [d].[AddedBy]
			FROM [dbo].[Documents] [d] 
			WHERE
			[d].[IsDelete] = 0 
			AND [d].[UserId] = @UserId 
			AND [d].[UserType] = @UserType 
			AND [d].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [d].[IsActive] END
			AND [d].[NormalizedName] = CASE WHEN @Name IS NOT NULL THEN UPPER(@Name) ELSE @NormalizedName END 

			RETURN 
	END	

	SELECT 
		[d].[Id], [d].[Name], [d].[NormalizedName], [d].[UserId], [d].[UserType], [d].[Type], [d].[FileName],
		[d].[FileExtension], [d].[FileType], [d].[DownloadUrl], [d].[HtmlAlt], [d].[HtmlTitle], [d].[Description],
		[d].[IsActive], [d].[IsDelete], [d].[ModifyDate], [d].[ModifyBy], [d].[AddedDate], [d].[AddedBy]
		FROM [dbo].[Documents] [d] 
		WHERE
		[d].[IsDelete] = 0 
		AND [d].[UserId] = @UserId 
		AND [d].[UserType] = @UserType 
		AND [d].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [d].[IsActive] END

	RETURN 
END
