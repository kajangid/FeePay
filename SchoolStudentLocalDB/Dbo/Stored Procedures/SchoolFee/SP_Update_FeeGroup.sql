﻿CREATE PROCEDURE [dbo].[SP_Update_FeeGroup]
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
	IF EXISTS(SELECT [Id] FROM [FeeGroup] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [FeeGroup] SET
			[Name] = CASE WHEN @Name IS NOT NULL THEN @Name ELSE [Name] END,
			[NormalizedName] = CASE WHEN @NormalizedName IS NOT NULL THEN @NormalizedName ELSE [NormalizedName] END,
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

