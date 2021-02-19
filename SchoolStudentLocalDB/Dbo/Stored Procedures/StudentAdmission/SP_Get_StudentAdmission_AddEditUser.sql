-- =============================================
-- Author:		Karan
-- Create date: 21-01-2021
-- Description:	Sp to Get Student Admission Form data with add update user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_StudentAdmission_AddEditUser]
	(	
		@Id							INT = NULL,
		@StudentLoginId				INT = NULL,
		@AcademicSessionId			INT = NULL,
		@FormNo						NVARCHAR (50) = NULL,
		@EnrollNo					NVARCHAR (50) = NULL,
		@Sr_RegNo					NVARCHAR (80) = NULL,
		@MACHINEID					NVARCHAR (50) = NULL,
		@IsActive					BIT = NULL
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	IF(@Id IS NOT NULL AND @Id != 0)
	BEGIN
		SELECT 
			[sa].[Id], [sa].[AcademicSessionId], [sa].[Sr_RegNo],
			[sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
			[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
			[sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
			[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
			[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
			[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
			, [c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description]
	
			, [s].[Id], [s].[Name], [s].[NormalizedName], [s].[Description]
			
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
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
			[sa].[IsDelete] = 0 
			AND [sa].[Id] = @Id 
			AND [sa].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sa].[IsActive] END

			RETURN 
	END
	ELSE IF(@StudentLoginId IS NOT NULL AND @StudentLoginId != 0)
	BEGIN
		SELECT  
			[sa].[Id], [sa].[AcademicSessionId], [sa].[Sr_RegNo],
			[sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
			[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
			[sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
			[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
			[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
			[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
			, [c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description]
	
			, [s].[Id], [s].[Name], [s].[NormalizedName], [s].[Description]
			
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
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
			[sa].[IsDelete] = 0 
			AND [sa].[StudentLoginId] = @StudentLoginId 
			AND [sa].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sa].[IsActive] END

			RETURN 
	END
	ELSE IF(@FormNo IS NOT NULL)
	BEGIN
		SELECT  
			[sa].[Id], [sa].[AcademicSessionId], [sa].[Sr_RegNo],
			[sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
			[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
			[sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
			[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
			[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
			[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
			, [c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description]
	
			, [s].[Id], [s].[Name], [s].[NormalizedName], [s].[Description]
			
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
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
			[sa].[IsDelete] = 0 
			AND [sa].[FormNo] = @FormNo 
			AND [sa].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sa].[IsActive] END

			RETURN 
	END
	ELSE IF(@EnrollNo IS NOT NULL)
	BEGIN
		SELECT  
			[sa].[Id], [sa].[AcademicSessionId], [sa].[Sr_RegNo],
			[sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
			[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
			[sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
			[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
			[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
			[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
			, [c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description]
	
			, [s].[Id], [s].[Name], [s].[NormalizedName], [s].[Description]
			
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
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
			[sa].[IsDelete] = 0 
			AND [sa].[EnrollNo] = @EnrollNo 
			AND [sa].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sa].[IsActive] END

			RETURN 
	END
	ELSE IF(@Sr_RegNo IS NOT NULL)
	BEGIN
		SELECT  
			[sa].[Id], [sa].[AcademicSessionId], [sa].[Sr_RegNo],
			[sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
			[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
			[sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
			[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
			[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
			[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
			, [c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description]
	
			, [s].[Id], [s].[Name], [s].[NormalizedName], [s].[Description]
			
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
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
			[sa].[IsDelete] = 0 
			AND [sa].[Sr_RegNo] = @Sr_RegNo 
			AND [sa].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sa].[IsActive] END

			RETURN 
	END
	ELSE IF(@MACHINEID IS NOT NULL)
	BEGIN
		SELECT  
			[sa].[Id], [sa].[AcademicSessionId], [sa].[Sr_RegNo],
			[sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
			[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
			[sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
			[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
			[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
			[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
			, [c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description]
	
			, [s].[Id], [s].[Name], [s].[NormalizedName], [s].[Description]
			
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
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
			[sa].[IsDelete] = 0 
			AND [sa].[MACHINEID] = @MACHINEID 
			AND [sa].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sa].[IsActive] END

			RETURN 
	END
	ELSE 
	BEGIN
		SELECT  
			[sa].[Id], [sa].[AcademicSessionId], [sa].[Sr_RegNo],
			[sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
			[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
			[sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
			[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
			[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
			[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
			, [c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description]
	
			, [s].[Id], [s].[Name], [s].[NormalizedName], [s].[Description]
			
			, [t4].[Id],[t4].[FullName],[t4].[Email] --  <-------- Added by user 
			, [t5].[Id],[t5].[FullName],[t5].[Email] --  <-------- modify by user
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
			[sa].[IsDelete] = 0 
			AND [sa].[AcademicSessionId] = @AcademicSessionId
			AND [sa].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sa].[IsActive] END

			RETURN 
	END
		
END