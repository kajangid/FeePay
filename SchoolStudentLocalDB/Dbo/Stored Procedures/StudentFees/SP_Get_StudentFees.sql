CREATE PROCEDURE [dbo].[SP_Get_StudentFees]
(
@FeeGroupId						INT = NULL,
@StudentAdmissionId				INT = NULL,
@AcademicSessionId				INT = NULL
)
AS
BEGIN
	IF(@FeeGroupId IS NOT NULL AND @FeeGroupId <> 0)
	BEGIN 
		SELECT 
		[sd].[Id], [sd].[FormNo], [sd].[ClassId], [sd].[SectionId], [sd].[StudentLoginId], [sd].[AdmissionDate],[sd].[FirstName],
		[sd].[LastName], [sd].[FatherName], [sd].[MobileNo], [sd].[Gender],[sd].[Sr_RegNo],[sd].[MotherName],[sd].[Address],
		[sd].[IsActive], [sd].[IsDelete], [sd].[ModifyDate], [sd].[ModifyBy], [sd].[AddedDate], [sd].[AddedBy],		
		[fg].[Id], [fg].[Name], [fg].[NormalizedName], [fg].[Description], [fg].[IsActive], [fg].[IsDelete], [fg].[ModifyDate],
		[fg].[ModifyBy], [fg].[AddedDate], [fg].[AddedBy] 
		FROM [dbo].[StudentFees] [sf] 
		INNER JOIN [dbo].[StudentAdmission] [sd] ON
		[sd].[Id] = [sf].[StudentAdmissionId] AND [sd].[IsDelete] = 0 AND [sd].[IsActive] = 1
		INNER JOIN [dbo].[FeeGroup] [fg] ON
		[fg].[Id] = [sf].[FeeGroupId] AND [fg].[IsDelete] = 0 AND [fg].[IsActive] = 1
		WHERE 
		[sf].[AcademicSessionId] = @AcademicSessionId AND
		[sf].[FeeGroupId] = @FeeGroupId		
	END
	IF(@StudentAdmissionId IS NOT NULL AND @StudentAdmissionId <> 0)
	BEGIN
		SELECT 
		[fg].[Id],[fg].[Name],
		[fm].[Id], [fm].[Amount],[fm].[DueDate],
		[ft].[Id], [ft].[Name], [ft].[Code]
		FROM [dbo].[StudentFees] [sf] 
		INNER JOIN [dbo].[FeeGroup] AS [fg]  
		ON [fg].[Id] = [sf].[FeeGroupId] AND [fg].[IsDelete] = 0 AND [fg].[IsActive] = 1
		INNER JOIN [dbo].[FeeMaster] AS [fm] 
		ON [fg].[Id] = [fm].[FeeGroupId] AND [fm].[IsDelete] = 0 AND [fm].[IsActive] = 1
		INNER JOIN [dbo].[FeeType] AS [ft] 
		ON [ft].[Id] = [fm].[FeeTypeId] AND [ft].[IsDelete] = 0  AND [ft].[IsActive] = 1
		WHERE 
		[sf].[StudentAdmissionId] = @StudentAdmissionId
	END
	ELSE
	BEGIN 
		SELECT 
		[sd].[Id], [sd].[FormNo], [sd].[ClassId], [sd].[SectionId], [sd].[StudentLoginId], [sd].[AdmissionDate],[sd].[FirstName],
		[sd].[LastName], [sd].[FatherName], [sd].[MobileNo], [sd].[Gender],[sd].[Sr_RegNo],[sd].[MotherName],[sd].[Address],
		[sd].[IsActive], [sd].[IsDelete], [sd].[ModifyDate], [sd].[ModifyBy], [sd].[AddedDate], [sd].[AddedBy],		
		[fg].[Id], [fg].[Name], [fg].[NormalizedName], [fg].[Description], [fg].[IsActive], [fg].[IsDelete], [fg].[ModifyDate],
		[fg].[ModifyBy], [fg].[AddedDate], [fg].[AddedBy] 
		FROM [dbo].[StudentFees] [sf] 
		INNER JOIN [dbo].[StudentAdmission] [sd] ON
		[sd].[Id] = [sf].[StudentAdmissionId] AND [sd].[IsDelete] = 0 AND [sd].[IsActive] = 1
		INNER JOIN [dbo].[FeeGroup] [fg] ON
		[fg].[Id] = [sf].[FeeGroupId] AND [fg].[IsDelete] = 0 AND [fg].[IsActive] = 1
		WHERE 
		[sf].[AcademicSessionId] = @AcademicSessionId 

	END
END
