CREATE PROCEDURE [dbo].[SP_Add_FeeType]
	(
	@Name NVARCHAR(50),
	@NormalizedName NVARCHAR(50),
	@Code NVARCHAR(50),
	@Description NVARCHAR(350) = NULL,
	@IsActive BIT = 0, 
	@AddedBy INT = NULL
	)
AS
BEGIN
	IF NOT EXISTS(SELECT [Id] FROM [FeeType] WHERE [Name] = @Name AND [NormalizedName] = @NormalizedName AND [Code] = @Code AND [IsDelete] = 0)
	BEGIN
		INSERT INTO [FeeType]([Name],[NormalizedName],[Code],[Description],[IsActive],[IsDelete],[AddedBy],[AddedDate])
		VALUES(@Name,@NormalizedName,@Code,@Description,@IsActive,0,@AddedBy,GETDATE())

		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN
		SELECT 0
	END
END
