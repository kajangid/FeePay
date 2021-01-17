CREATE PROCEDURE [dbo].[SP_Add_FeeGroup]
	(
	@Name NVARCHAR(50),
	@NormalizedName NVARCHAR(50),
	@Description NVARCHAR(350) = NULL,
	@IsActive BIT = 0, 
	@AddedBy INT = NULL
	)
AS
BEGIN
	IF NOT EXISTS(SELECT [Id] FROM [FeeGroup] WHERE [Name] = @Name AND [IsDelete] = 0)
	BEGIN
		INSERT INTO [FeeGroup]([Name],[NormalizedName],[Description],[IsActive],[IsDelete],[AddedBy],[AddedDate])
		VALUES(@Name,@NormalizedName,@Description,@IsActive,0,@AddedBy,GETDATE())

		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN
		SELECT 0
	END
END

