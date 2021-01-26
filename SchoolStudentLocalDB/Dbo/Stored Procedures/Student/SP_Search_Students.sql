-- =============================================
-- Author:		Karan
-- Create date: 21-01-2021
-- Description:	Sp to search students data in school
-- =============================================
CREATE PROCEDURE [dbo].[SP_Search_Students]
(
	@SearchParam		NVARCHAR(90) = NULL, -- will match in mobileNo,name,class,father'sName,srNo,formNo
	@ClassId			INT = NULL,
	@SectionId			INT = NULL,
	@Gender				NVARCHAR(20) = NULL
)
AS
BEGIN
	IF(@ClassId IS NOT NULL AND @SectionId IS NOT NULL AND @Gender IS NOT NULL)
	BEGIN
		SELECT [sa].[Id], [sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
		[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
		[sa].[Sr_RegNo], [sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
		[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
		[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
		[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
		, [c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate], [c].[ModifyBy],
		[c].[AddedDate], [c].[AddedBy]
	
		, [s].[Id], [s].[Name], [s].[NormalizedName], [s].[Description], [s].[IsActive], [s].[IsDelete], [s].[ModifyDate], [s].[ModifyBy],
		[s].[AddedDate], [s].[AddedBy]
	
		, [t4].[Id], [t4].[FullName], [t4].[Email]
		, [t5].[Id], [t5].[FullName], [t5].[Email]
		FROM
			[dbo].[StudentAdmission] [sa]
		INNER JOIN [dbo].[Class] [c] ON
		[c].[Id] = [sa].[ClassId] AND [c].[IsActive] = 1 AND [c].[IsDelete] = 0
		INNER JOIN [dbo].[Section] [s] ON
		[s].[Id] = [sa].[SectionId]
		OUTER APPLY (
					SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
					FROM [dbo].[SchoolAdmin_User] [ab]
					WHERE [ab].[Id] = [sa].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]

		OUTER APPLY (
					SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
					FROM [dbo].[SchoolAdmin_User] [mb]
					WHERE [mb].[Id] = [sa].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]

		WHERE 
			[sa].[IsDelete] = 0 AND 
			[sa].[IsActive] = 1 AND
			[sa].[ClassId] = @ClassId AND 
			[sa].[SectionId] = @SectionId AND
			[sa].[Gender] = @Gender
	END
	ELSE
	BEGIN
		SELECT [sa].[Id], [sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
		[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
		[sa].[Sr_RegNo], [sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
		[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
		[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
		[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
		, [c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate], [c].[ModifyBy],
		[c].[AddedDate], [c].[AddedBy]
	
		, [s].[Id], [s].[Name], [s].[NormalizedName], [s].[Description], [s].[IsActive], [s].[IsDelete], [s].[ModifyDate], [s].[ModifyBy],
		[s].[AddedDate], [s].[AddedBy]
	
		, [t4].[Id], [t4].[FullName], [t4].[Email]
		, [t5].[Id], [t5].[FullName], [t5].[Email]
		FROM
			[dbo].[StudentAdmission] [sa]
		INNER JOIN [dbo].[Class] [c] ON
		[c].[Id] = [sa].[ClassId] AND [c].[IsActive] = 1 AND [c].[IsDelete] = 0
		INNER JOIN [dbo].[Section] [s] ON
		[s].[Id] = [sa].[SectionId]
		OUTER APPLY (
					SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
					FROM [dbo].[SchoolAdmin_User] [ab]
					WHERE [ab].[Id] = [sa].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]

		OUTER APPLY (
					SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
					FROM [dbo].[SchoolAdmin_User] [mb]
					WHERE [mb].[Id] = [sa].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]

		WHERE 
			[sa].[IsDelete] = 0 AND 
			[sa].[IsActive] = 1 AND
			(
			[sa].[FirstName] LIKE '%'+@SearchParam+'%' OR
			[sa].[LastName] LIKE '%'+@SearchParam+'%' OR
			[sa].[FatherName] LIKE '%'+@SearchParam+'%' OR
			[sa].[MobileNo] LIKE '%'+@SearchParam+'%' OR
			[sa].[Sr_RegNo] LIKE '%'+@SearchParam+'%' OR
			[c].[Name] LIKE '%'+@SearchParam+'%'
			)
	END
END
