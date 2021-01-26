-- =============================================
-- Author:		Karan
-- Create date: 21-01-2021
-- Description:	Sp to Get Student Admission Form data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_StudentAdmission]
	(	
		@Id					   INT = NULL,
		@StudentLoginId        INT = NULL,
		@FormNo				   NVARCHAR (50) = NULL,
		@EnrollNo              NVARCHAR (50) = NULL,
		@Sr_RegNo              NVARCHAR (80) = NULL,
		@MACHINEID             NVARCHAR (50) = NULL,
		@ClassId			   INT = NULL,
		@SectionId			   INT = NULL,
		@Gender 			   NVARCHAR (20) = NULL,
		@IsActive			   BIT = NULL
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	IF(@Id IS NOT NULL AND @Id <> 0)
	BEGIN	
		SELECT 
			[c].[Id], [c].[FormNo], [c].[ClassId], [c].[SectionId], [c].[StudentLoginId], [c].[AdmissionDate], [c].[FirstName],
			[c].[LastName], [c].[FatherName], [c].[MobileNo], [c].[Gender], [c].[GuardianMobileNo], [c].[Sr_RegNo], [c].[EnrollNo],
			[c].[MACHINEID], [c].[PreviousClass], [c].[PreviousInstituteName], [c].[YearOfPassing], [c].[PreviousRollNo],
			[c].[PreviousPercent], [c].[MotherName], [c].[DateOfBirth], [c].[Category], [c].[AlternateMobileNo], [c].[StudentEmail],
			[c].[GuardianEmail], [c].[StudentType], [c].[Medium], [c].[Address], [c].[CityId], [c].[Village], [c].[StateId], [c].[Religion],
			[c].[Image], [c].[Remarks], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate], [c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[StudentAdmission] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[Id] = @Id 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE IF(@StudentLoginId IS NOT NULL AND @StudentLoginId <> 0)
	BEGIN
		SELECT 
			[c].[Id], [c].[FormNo], [c].[ClassId], [c].[SectionId], [c].[StudentLoginId], [c].[AdmissionDate], [c].[FirstName], [c].[LastName], [c].[FatherName], [c].[MobileNo], [c].[Gender], [c].[GuardianMobileNo], [c].[Sr_RegNo], [c].[EnrollNo], [c].[MACHINEID], [c].[PreviousClass], [c].[PreviousInstituteName], [c].[YearOfPassing], [c].[PreviousRollNo], [c].[PreviousPercent], [c].[MotherName], [c].[DateOfBirth], [c].[Category], [c].[AlternateMobileNo], [c].[StudentEmail], [c].[GuardianEmail], [c].[StudentType], [c].[Medium], [c].[Address], [c].[CityId], [c].[Village], [c].[StateId], [c].[Religion], [c].[Image], [c].[Remarks], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate], [c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[StudentAdmission] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[StudentLoginId] = @StudentLoginId 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE IF(@FormNo IS NOT NULL)
	BEGIN
		SELECT 
			[c].[Id], [c].[FormNo], [c].[ClassId], [c].[SectionId], [c].[StudentLoginId], [c].[AdmissionDate], [c].[FirstName], [c].[LastName], [c].[FatherName], [c].[MobileNo], [c].[Gender], [c].[GuardianMobileNo], [c].[Sr_RegNo], [c].[EnrollNo], [c].[MACHINEID], [c].[PreviousClass], [c].[PreviousInstituteName], [c].[YearOfPassing], [c].[PreviousRollNo], [c].[PreviousPercent], [c].[MotherName], [c].[DateOfBirth], [c].[Category], [c].[AlternateMobileNo], [c].[StudentEmail], [c].[GuardianEmail], [c].[StudentType], [c].[Medium], [c].[Address], [c].[CityId], [c].[Village], [c].[StateId], [c].[Religion], [c].[Image], [c].[Remarks], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate], [c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[StudentAdmission] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[FormNo] = @FormNo 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE IF(@EnrollNo IS NOT NULL)
	BEGIN
		SELECT 
			[c].[Id], [c].[FormNo], [c].[ClassId], [c].[SectionId], [c].[StudentLoginId], [c].[AdmissionDate], [c].[FirstName], [c].[LastName], [c].[FatherName], [c].[MobileNo], [c].[Gender], [c].[GuardianMobileNo], [c].[Sr_RegNo], [c].[EnrollNo], [c].[MACHINEID], [c].[PreviousClass], [c].[PreviousInstituteName], [c].[YearOfPassing], [c].[PreviousRollNo], [c].[PreviousPercent], [c].[MotherName], [c].[DateOfBirth], [c].[Category], [c].[AlternateMobileNo], [c].[StudentEmail], [c].[GuardianEmail], [c].[StudentType], [c].[Medium], [c].[Address], [c].[CityId], [c].[Village], [c].[StateId], [c].[Religion], [c].[Image], [c].[Remarks], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate], [c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[StudentAdmission] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[EnrollNo] = @EnrollNo 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE IF(@Sr_RegNo IS NOT NULL)
	BEGIN
		SELECT 
			[c].[Id], [c].[FormNo], [c].[ClassId], [c].[SectionId], [c].[StudentLoginId], [c].[AdmissionDate], [c].[FirstName], [c].[LastName], [c].[FatherName], [c].[MobileNo], [c].[Gender], [c].[GuardianMobileNo], [c].[Sr_RegNo], [c].[EnrollNo], [c].[MACHINEID], [c].[PreviousClass], [c].[PreviousInstituteName], [c].[YearOfPassing], [c].[PreviousRollNo], [c].[PreviousPercent], [c].[MotherName], [c].[DateOfBirth], [c].[Category], [c].[AlternateMobileNo], [c].[StudentEmail], [c].[GuardianEmail], [c].[StudentType], [c].[Medium], [c].[Address], [c].[CityId], [c].[Village], [c].[StateId], [c].[Religion], [c].[Image], [c].[Remarks], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate], [c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[StudentAdmission] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[Sr_RegNo] = @Sr_RegNo 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE IF(@MACHINEID IS NOT NULL)
	BEGIN
		SELECT 
			[c].[Id], [c].[FormNo], [c].[ClassId], [c].[SectionId], [c].[StudentLoginId], [c].[AdmissionDate], [c].[FirstName], [c].[LastName], [c].[FatherName], [c].[MobileNo], [c].[Gender], [c].[GuardianMobileNo], [c].[Sr_RegNo], [c].[EnrollNo], [c].[MACHINEID], [c].[PreviousClass], [c].[PreviousInstituteName], [c].[YearOfPassing], [c].[PreviousRollNo], [c].[PreviousPercent], [c].[MotherName], [c].[DateOfBirth], [c].[Category], [c].[AlternateMobileNo], [c].[StudentEmail], [c].[GuardianEmail], [c].[StudentType], [c].[Medium], [c].[Address], [c].[CityId], [c].[Village], [c].[StateId], [c].[Religion], [c].[Image], [c].[Remarks], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate], [c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[StudentAdmission] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[MACHINEID] = @MACHINEID 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE IF(@ClassId IS NOT NULL AND @ClassId <> 0)
	BEGIN
		SELECT [sa].[Id], [sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
			[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
			[sa].[Sr_RegNo], [sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
			[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
			[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
			[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
			, [c].[Id], [c].[Name], [c].[NormalizedName], [c].[Description]
	
			, [s].[Id], [s].[Name], [s].[NormalizedName], [s].[Description]
			FROM
				[dbo].[StudentAdmission] [sa]
			INNER JOIN [dbo].[Class] [c] ON
			[c].[Id] = [sa].[ClassId] AND [c].[IsActive] = 1 AND [c].[IsDelete] = 0
			INNER JOIN [dbo].[Section] [s] ON
			[s].[Id] = [sa].[SectionId]
			WHERE
			[sa].[IsDelete] = 0 
			AND [sa].[ClassId] = @ClassId 
			AND [sa].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sa].[IsActive] END

			RETURN 
	END
	ELSE IF(@SectionId IS NOT NULL AND @SectionId <> 0)
	BEGIN
		SELECT 
			[c].[Id], [c].[FormNo], [c].[ClassId], [c].[SectionId], [c].[StudentLoginId], [c].[AdmissionDate], [c].[FirstName], [c].[LastName], [c].[FatherName], [c].[MobileNo], [c].[Gender], [c].[GuardianMobileNo], [c].[Sr_RegNo], [c].[EnrollNo], [c].[MACHINEID], [c].[PreviousClass], [c].[PreviousInstituteName], [c].[YearOfPassing], [c].[PreviousRollNo], [c].[PreviousPercent], [c].[MotherName], [c].[DateOfBirth], [c].[Category], [c].[AlternateMobileNo], [c].[StudentEmail], [c].[GuardianEmail], [c].[StudentType], [c].[Medium], [c].[Address], [c].[CityId], [c].[Village], [c].[StateId], [c].[Religion], [c].[Image], [c].[Remarks], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate], [c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[StudentAdmission] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[SectionId] = @SectionId 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE IF(@Gender IS NOT NULL)
	BEGIN
		SELECT 
			[c].[Id], [c].[FormNo], [c].[ClassId], [c].[SectionId], [c].[StudentLoginId], [c].[AdmissionDate], [c].[FirstName], [c].[LastName], [c].[FatherName], [c].[MobileNo], [c].[Gender], [c].[GuardianMobileNo], [c].[Sr_RegNo], [c].[EnrollNo], [c].[MACHINEID], [c].[PreviousClass], [c].[PreviousInstituteName], [c].[YearOfPassing], [c].[PreviousRollNo], [c].[PreviousPercent], [c].[MotherName], [c].[DateOfBirth], [c].[Category], [c].[AlternateMobileNo], [c].[StudentEmail], [c].[GuardianEmail], [c].[StudentType], [c].[Medium], [c].[Address], [c].[CityId], [c].[Village], [c].[StateId], [c].[Religion], [c].[Image], [c].[Remarks], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate], [c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[StudentAdmission] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[Gender] = @Gender 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
	ELSE 
	BEGIN
		SELECT 
			[c].[Id], [c].[FormNo], [c].[ClassId], [c].[SectionId], [c].[StudentLoginId], [c].[AdmissionDate], [c].[FirstName], [c].[LastName], [c].[FatherName], [c].[MobileNo], [c].[Gender], [c].[GuardianMobileNo], [c].[Sr_RegNo], [c].[EnrollNo], [c].[MACHINEID], [c].[PreviousClass], [c].[PreviousInstituteName], [c].[YearOfPassing], [c].[PreviousRollNo], [c].[PreviousPercent], [c].[MotherName], [c].[DateOfBirth], [c].[Category], [c].[AlternateMobileNo], [c].[StudentEmail], [c].[GuardianEmail], [c].[StudentType], [c].[Medium], [c].[Address], [c].[CityId], [c].[Village], [c].[StateId], [c].[Religion], [c].[Image], [c].[Remarks], [c].[IsActive], [c].[IsDelete], [c].[ModifyDate], [c].[ModifyBy], [c].[AddedDate], [c].[AddedBy]
			FROM [dbo].[StudentAdmission] [c] 
			WHERE
			[c].[IsDelete] = 0 
			AND [c].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [c].[IsActive] END

			RETURN 
	END
		
END