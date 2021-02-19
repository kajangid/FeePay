-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get fee master list data
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAll_FeeMaster]
(
@Id INT = 0
,@FeeGroupId INT = NULL
,@FeeTypeId INT = NULL
,@AcademicSessionId INT = NULL 
,@IsActive BIT = NULL
,@ModifyBy BIT = NULL
,@AddedBy INT = NULL
)
AS
BEGIN	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT  
			[fm].[Id], [fm].[FeeGroupId], [fm].[FeeTypeId], [fm].[AcademicSessionId], [fm].[Amount], [fm].[DueDate], [fm].[Description],
			[fm].[IsActive], [fm].[IsDelete], [fm].[ModifyDate], [fm].[AddedDate],

			[ft].[Id], [ft].[Name], [ft].[NormalizedName], [ft].[Code], [ft].[Description], [ft].[IsActive], [ft].[IsDelete],
			[ft].[ModifyDate], [ft].[ModifyBy], [ft].[AddedDate], [ft].[AddedBy],

			[fg].[Id], [fg].[Name], [fg].[NormalizedName], [fg].[Description], [fg].[IsActive], [fg].[IsDelete], [fg].[ModifyDate],
			[fg].[ModifyBy], [fg].[AddedDate], [fg].[AddedBy] 

			FROM [dbo].[FeeMaster] [fm] 
			INNER JOIN [dbo].[FeeType] [ft]
			ON [ft].[Id] = [fm].[FeeTypeId] AND [ft].[IsDelete] = 0
			INNER JOIN [dbo].[FeeGroup] [fg]
			ON [fg].[Id] = [fm].[FeeGroupId] AND [fg].[IsDelete] = 0
	WHERE
		[fg].[IsDelete] = 0 AND
		[fm].[AcademicSessionId] = @AcademicSessionId AND 
		[fg].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN  @IsActive ELSE [fg].[IsActive] END 
END
