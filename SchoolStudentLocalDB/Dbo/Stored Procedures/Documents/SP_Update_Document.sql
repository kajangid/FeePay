-- =============================================
-- Author:		Karan
-- Create date: 17-02-2021
-- Description:	Sp to add Document data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Document]
(
@Id						INT,
@Name					NVARCHAR(50) = NULL,
@NormalizedName			NVARCHAR(50) = NULL, 
@UserId					INT = NULL, 
@UserType				NVARCHAR(50) = NULL, 
@Type					NVARCHAR(50) = NULL, 
@FileName				NVARCHAR(50) = NULL, 
@FileExtension			NVARCHAR(15) = NULL, 
@FileType				NVARCHAR(25) = NULL, 
@DownloadUrl			NVARCHAR(255) = NULL, 
@HtmlAlt				NVARCHAR(255) = NULL, 
@HtmlTitle				NVARCHAR(255) = NULL, 
@Description			NVARCHAR(350) = NULL, 
@IsActive				BIT = NULL, 
@ModifyBy				INT = NULL
)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[Documents] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [dbo].[Documents] SET 
			[Name]			 = CASE WHEN @Name IS NOT NULL THEN @Name ELSE [Name] END,
			[NormalizedName] = CASE WHEN @NormalizedName IS NOT NULL THEN @NormalizedName ELSE [NormalizedName] END, 
			[UserId]		 = CASE WHEN @UserId IS NOT NULL AND @UserId <> 0 THEN @UserId ELSE [UserId] END, 
			[UserType]		 = CASE WHEN @UserType IS NOT NULL THEN @UserType ELSE [UserType] END, 
			[Type]			 = CASE WHEN @Type IS NOT NULL THEN @Type ELSE [Type] END, 
			[FileName]		 = CASE WHEN @FileName IS NOT NULL THEN @FileName ELSE [FileName] END, 
			[FileExtension]	 = CASE WHEN @FileExtension IS NOT NULL THEN @FileExtension ELSE [FileExtension] END, 
			[FileType]		 = CASE WHEN @FileType IS NOT NULL THEN @FileType ELSE [FileType] END, 
			[DownloadUrl]	 = CASE WHEN @DownloadUrl IS NOT NULL THEN @DownloadUrl ELSE [DownloadUrl] END, 
			[HtmlAlt]		 = CASE WHEN @HtmlAlt IS NOT NULL THEN @HtmlAlt ELSE [HtmlAlt] END, 
			[HtmlTitle]		 = CASE WHEN @HtmlTitle IS NOT NULL THEN @HtmlTitle ELSE [HtmlTitle] END, 
			[Description]	 = CASE WHEN @Description IS NOT NULL THEN @Description ELSE [Description] END, 
			[IsActive]		 = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END, 
			[ModifyBy]		 = @ModifyBy,
			[ModifyDate]	 = GETDATE()
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
