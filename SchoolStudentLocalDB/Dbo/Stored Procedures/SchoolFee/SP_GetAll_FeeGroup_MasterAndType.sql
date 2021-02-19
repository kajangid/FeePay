-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to get all fee group list with fee master and fee type join data
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAll_FeeGroup_MasterAndType]
@AcademicSessionId INT 
AS
BEGIN	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
		



	SELECT 
	[fg].[Id],[fg].[Name],
	[fm].[Id], [fm].[Amount],[fm].[DueDate],
	[ft].[Id], [ft].[Name], [ft].[Code]

	FROM [dbo].[FeeGroup] AS [fg]
	INNER JOIN [dbo].[FeeMaster] AS [fm] 
	ON [fg].[Id] = [fm].FeeGroupId AND [fm].[IsDelete] = 0 AND [fm].[IsActive] = 1
	INNER JOIN [dbo].[FeeType] AS [ft] 
	ON [ft].[Id] = [fm].FeeTypeId AND [ft].[IsDelete] = 0  AND [ft].[IsActive] = 1
	WHERE 
	[fg].[IsDelete] = 0 AND 
	[fg].[IsActive] = 1 AND
	[fm].[AcademicSessionId] = @AcademicSessionId


END

