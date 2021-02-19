-- =============================================
-- Author:		Karan
-- Create date: 21-01-2021
-- Description:	Sp to Update Student Admission form data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_StudentAdmission]
(
	@Id							INT,
	@AcademicSessionId			INT = NULL,
	@FormNo						NVARCHAR (50) = NULL,
	@ClassId					INT = NULL,
	@SectionId					INT = NULL,
	@StudentLoginId				INT = NULL,
	@AdmissionDate				DATETIME = NULL,
	@FirstName					NVARCHAR (50) = NULL,
	@LastName					NVARCHAR (50) = NULL,
	@FatherName					NVARCHAR (50) = NULL,
	@MobileNo					NVARCHAR (50) = NULL,
	@Gender						NVARCHAR (50) = NULL,
	@GuardianMobileNo			NVARCHAR (50) = NULL,
	@Sr_RegNo					NVARCHAR (80) = NULL,
	@EnrollNo					NVARCHAR (50) = NULL,
	@MACHINEID					NVARCHAR (50) = NULL,
	@PreviousClass				NVARCHAR (50) = NULL,
	@PreviousInstituteName		NVARCHAR (250)= NULL,
	@YearOfPassing				NVARCHAR (50) = NULL,
	@PreviousRollNo				NVARCHAR (50) = NULL,
	@PreviousPercent			NVARCHAR (50) = NULL,
	@MotherName					NVARCHAR (50) = NULL,
	@DateOfBirth				DATETIME      = NULL,
	@Category					NVARCHAR (50) = NULL,
	@AlternateMobileNo			NVARCHAR (50) = NULL,
	@StudentEmail				NVARCHAR (60) = NULL,
	@GuardianEmail				NVARCHAR (60) = NULL,
	@StudentType				NVARCHAR (50) = NULL,
	@Medium						NVARCHAR (50) = NULL,
	@Address					NVARCHAR (500)= NULL,
	@CityId						NVARCHAR (50) = NULL,
	@Village					NVARCHAR (60) = NULL,
	@StateId					NVARCHAR (50) = NULL,
	@Religion					NVARCHAR (50) = NULL,
	@Image						NVARCHAR (350)= NULL,
	@Remarks					NVARCHAR (350)= NULL,
	@IsActive					BIT = 0,
	@ModifyBy					INT = NULL
)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[StudentAdmission] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN

		UPDATE [dbo].[StudentAdmission] SET
			[Sr_RegNo]						=	CASE WHEN @Sr_RegNo IS NOT NULL THEN @Sr_RegNo ELSE [Sr_RegNo] END
			,[AcademicSessionId]			=	CASE WHEN @AcademicSessionId IS NOT NULL AND @AcademicSessionId != 0 THEN @AcademicSessionId ELSE [AcademicSessionId] END
			,[ClassId]						=	CASE WHEN @ClassId IS NOT NULL AND @ClassId != 0 THEN @ClassId ELSE [ClassId] END
			,[SectionId]					=	CASE WHEN @SectionId IS NOT NULL AND @SectionId != 0 THEN @SectionId ELSE [SectionId] END
			,[StudentLoginId]				=	CASE WHEN @StudentLoginId IS NOT NULL AND @StudentLoginId != 0 THEN @StudentLoginId ELSE [StudentLoginId] END
			,[AdmissionDate]				=	CASE WHEN @AdmissionDate IS NOT NULL THEN @AdmissionDate ELSE [AdmissionDate] END
			,[FirstName]					=	CASE WHEN @FirstName IS NOT NULL THEN @FirstName ELSE [FirstName] END
			,[LastName]						=	CASE WHEN @LastName IS NOT NULL THEN @LastName ELSE [LastName] END
			,[FatherName]					=	CASE WHEN @FatherName IS NOT NULL THEN @FatherName ELSE [FatherName] END
			,[MobileNo]						=	CASE WHEN @MobileNo IS NOT NULL THEN @MobileNo ELSE [MobileNo] END
			,[Gender]						=	CASE WHEN @Gender IS NOT NULL THEN @Gender ELSE [Gender] END
			,[GuardianMobileNo]				=	CASE WHEN @GuardianMobileNo IS NOT NULL THEN @GuardianMobileNo ELSE [GuardianMobileNo] END
			,[EnrollNo]						=	CASE WHEN @EnrollNo IS NOT NULL THEN @EnrollNo ELSE [EnrollNo] END
			,[MACHINEID]					=	CASE WHEN @MACHINEID IS NOT NULL THEN @MACHINEID ELSE [MACHINEID] END
			,[FormNo]						=	CASE WHEN @FormNo IS NOT NULL THEN @FormNo ELSE [FormNo] END
			,[PreviousClass]				=	CASE WHEN @PreviousClass IS NOT NULL THEN @PreviousClass ELSE [PreviousClass] END
			,[PreviousInstituteName]		=	CASE WHEN @PreviousInstituteName IS NOT NULL THEN @PreviousInstituteName ELSE [PreviousInstituteName] END
			,[YearOfPassing]				=	CASE WHEN @YearOfPassing IS NOT NULL THEN @YearOfPassing ELSE [YearOfPassing] END
			,[PreviousRollNo]				=	CASE WHEN @PreviousRollNo IS NOT NULL THEN @PreviousRollNo ELSE [PreviousRollNo] END
			,[PreviousPercent]				=	CASE WHEN @PreviousPercent IS NOT NULL THEN @PreviousPercent ELSE [PreviousPercent] END
			,[MotherName]					=	CASE WHEN @MotherName IS NOT NULL THEN @MotherName ELSE [MotherName] END
			,[DateOfBirth]					=	CASE WHEN @DateOfBirth IS NOT NULL THEN @DateOfBirth ELSE [DateOfBirth] END
			,[Category]						=	CASE WHEN @Category IS NOT NULL THEN @Category ELSE [Category] END
			,[AlternateMobileNo]			=	CASE WHEN @AlternateMobileNo IS NOT NULL THEN @AlternateMobileNo ELSE [AlternateMobileNo] END
			,[StudentEmail]					=	CASE WHEN @StudentEmail IS NOT NULL THEN @StudentEmail ELSE [StudentEmail] END
			,[GuardianEmail]				=	CASE WHEN @GuardianEmail IS NOT NULL THEN @GuardianEmail ELSE [GuardianEmail] END
			,[StudentType]					=	CASE WHEN @StudentType IS NOT NULL THEN @StudentType ELSE [StudentType] END
			,[Medium]						=	CASE WHEN @Medium IS NOT NULL THEN @Medium ELSE [Medium] END
			,[Address]						=	CASE WHEN @Address IS NOT NULL THEN @Address ELSE [Address] END
			,[CityId]						=	CASE WHEN @CityId IS NOT NULL THEN @CityId ELSE [CityId] END
			,[Village]						=	CASE WHEN @Village IS NOT NULL THEN @Village ELSE [Village] END
			,[StateId]						=	CASE WHEN @StateId IS NOT NULL THEN @StateId ELSE [StateId] END
			,[Religion]						=	CASE WHEN @Religion IS NOT NULL THEN @Religion ELSE [Religion] END
			,[Image]						=	CASE WHEN @Image IS NOT NULL THEN @Image ELSE [Image] END	
			,[IsActive]						=	CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END
			,[Remarks]						=	CASE WHEN @Remarks IS NOT NULL THEN @Remarks ELSE [Remarks] END
			,[ModifyBy]						=	@ModifyBy
			,[ModifyDate]					=	GETDATE()
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


