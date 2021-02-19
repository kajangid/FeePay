-- =============================================
-- Author:		Karan
-- Create date: 17-02-2021
-- Description:	Sp to add Document data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Add_Document]
(
@Name					NVARCHAR(50),
@NormalizedName			NVARCHAR(50), 
@UserId					INT, 
@UserType				NVARCHAR(50), 
@Type					NVARCHAR(50) = NULL, 
@FileName				NVARCHAR(50) = NULL, 
@FileExtension			NVARCHAR(15) = NULL, 
@FileType				NVARCHAR(25) = NULL, 
@DownloadUrl			NVARCHAR(255) = NULL, 
@HtmlAlt				NVARCHAR(255) = NULL, 
@HtmlTitle				NVARCHAR(255) = NULL, 
@Description			NVARCHAR(350) = NULL, 
@IsActive				BIT = 0, 
@AddedBy				INT = NULL
)
AS
BEGIN
	IF NOT EXISTS(SELECT [Id] FROM [dbo].[Documents]
					WHERE
					[UserId] = @UserId AND
					[UserType] = @UserType AND
					[FileName] = @FileName AND
					[FileExtension] = @FileExtension AND
					[IsDelete] = 0)
	BEGIN
		INSERT INTO [dbo].[Documents]
			   ([Name],[NormalizedName],[UserId],[UserType],[Type],[FileName],[FileExtension],[FileType]
			   ,[DownloadUrl],[HtmlAlt],[HtmlTitle],[Description],[IsActive],[IsDelete],[ModifyDate],[AddedDate],[AddedBy])
		 VALUES
			   (@Name,@NormalizedName,@UserId,@UserType,@Type,@FileName,@FileExtension,@FileType , 
			   @DownloadUrl,@HtmlAlt,@HtmlTitle,@Description,@IsActive,0 ,GETDATE(),GETDATE(),@AddedBy)

		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN
		SELECT 0
	END
END
