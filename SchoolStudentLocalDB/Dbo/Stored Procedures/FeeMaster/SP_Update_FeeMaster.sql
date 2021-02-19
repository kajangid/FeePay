CREATE PROCEDURE [dbo].[SP_Update_FeeMaster]
	(
	@Id INT,
	@FeeGroupId INT = NULL, 
	@FeeTypeId INT = NULL, 
	@Amount NUMERIC(6,2) = NULL,
	@AcademicSessionId INT = NULL,
	@DueDate DATETIME = NULL,  
	@Description NVARCHAR(350) = NULL,
	@IsActive BIT = NULL, 
	@ModifyBy INT = NULL
	)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [FeeMaster] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [FeeMaster] SET
			[FeeGroupId] = CASE WHEN @FeeGroupId IS NOT NULL THEN @FeeGroupId ELSE [FeeGroupId] END,
			[FeeTypeId] = CASE WHEN @FeeTypeId IS NOT NULL THEN @FeeTypeId ELSE [FeeTypeId] END,
			[Amount] = CASE WHEN @Amount IS NOT NULL THEN @Amount ELSE [Amount] END,
			[DueDate] = CASE WHEN @DueDate IS NOT NULL THEN @DueDate ELSE [DueDate] END,
			[Description] = CASE WHEN @Description IS NOT NULL THEN @Description ELSE [Description] END,
			[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END,
			[AcademicSessionId] = CASE WHEN @AcademicSessionId IS NOT NULL AND @AcademicSessionId <> 0 THEN @AcademicSessionId ELSE [AcademicSessionId] END ,
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

