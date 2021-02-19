CREATE PROCEDURE [dbo].[SP_Add_FeeMaster]
	(
	@FeeGroupId INT, 
	@FeeTypeId INT, 
	@Amount NUMERIC(6,2),
	@AcademicSessionId INT = NULL,
	@DueDate DATETIME = NULL,  
	@Description NVARCHAR(350) = NULL,
	@IsActive BIT = 0, 
	@AddedBy INT = NULL
	)
AS
BEGIN
	IF NOT EXISTS(SELECT [Id] FROM [FeeMaster] WHERE [FeeGroupId] = @FeeGroupId AND [FeeTypeId] = @FeeTypeId AND [IsDelete] = 0)
	BEGIN
		INSERT INTO [FeeMaster]([FeeGroupId],[FeeTypeId],[AcademicSessionId],[Amount],[DueDate],
		[Description],[IsActive],[IsDelete],[AddedBy],[AddedDate])

		VALUES(@FeeGroupId,@FeeTypeId,@AcademicSessionId,@Amount,@DueDate,
		@Description,@IsActive,0,@AddedBy,GETDATE())

		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN
		SELECT 0
	END
END


