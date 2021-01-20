﻿-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to get Class data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Class]
	(
		@Id INT = NULL,
		@Name NVARCHAR(50) = NULL,
		@NormalizedName NVARCHAR(50) = NULL,
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
			[c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate],
			[c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[Class] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[Id] = @Id 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE IF(@Name IS NOT NULL)
	BEGIN
		SELECT 
			[c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate],
			[c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[Class] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[Name] = @Name
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE IF(@NormalizedName IS NOT NULL)
	BEGIN
		SELECT 
			[c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate],
			[c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[Class] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[NormalizedName] = @NormalizedName
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE 
	BEGIN
		SELECT 
			[c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate],
			[c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[Class] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
		
END