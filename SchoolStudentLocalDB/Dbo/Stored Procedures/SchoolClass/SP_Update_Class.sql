﻿-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to Update Class data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Class]
(
	@Id INT,
	@Name NVARCHAR(50) = NULL,
	@NormalizedName NVARCHAR(50) = NULL,
	@Description NVARCHAR(350) = NULL,
	@IsActive BIT = NULL,  
	@ModifyBy INT = NULL
)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[Class] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [dbo].[Class] SET
			[Name] = CASE WHEN @Name IS NOT NULL THEN @Name ELSE [Name] END,
			[NormalizedName] = CASE WHEN @NormalizedName IS NOT NULL THEN UPPER(@NormalizedName) ELSE [NormalizedName] END,
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


