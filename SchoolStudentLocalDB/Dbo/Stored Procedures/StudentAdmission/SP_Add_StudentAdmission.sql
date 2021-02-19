-- =============================================
-- Author:		Karan
-- Create date: 21-01-2021
-- Description:	Sp to add student admission form data  [StudentAcademicSessionId] INT
-- =============================================
CREATE PROCEDURE [dbo].[SP_Add_StudentAdmission]
	(
	@Sr_RegNo					NVARCHAR (80),
	@AcademicSessionId	INT,
	@ClassId					INT,
	@SectionId					INT,
	@StudentLoginId				INT,
	@AdmissionDate				DATETIME,
	@FirstName					NVARCHAR (50),
	@LastName					NVARCHAR (50),
	@FatherName					NVARCHAR (50),
	@MobileNo					NVARCHAR (50),
	@Gender						NVARCHAR (50),
	@FormNo						NVARCHAR (50) = NULL,
	@GuardianMobileNo			NVARCHAR (50) = NULL,
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
	@CityId						INT = NULL,
	@Village					NVARCHAR (60) = NULL,
	@StateId					INT = NULL,
	@Religion					NVARCHAR (50) = NULL,
	@Image						NVARCHAR (350)= NULL,
	@Remarks					NVARCHAR (350)= NULL,
	@IsActive					BIT = 0,
	@AddedBy					INT = NULL
	)
AS
BEGIN

	IF NOT EXISTS (SELECT * FROM [dbo].[StudentAdmission] WHERE [FormNo] = @FormNo AND [IsDelete] = 0)
	BEGIN

		INSERT 
			INTO [dbo].[StudentAdmission]
				   ([AcademicSessionId],[Sr_RegNo],
				   
				   [FormNo],[ClassId],[SectionId],[StudentLoginId],[AdmissionDate],[FirstName],[LastName],[FatherName],[MobileNo],[Gender]
				   ,[GuardianMobileNo],[EnrollNo],[MACHINEID],[PreviousClass],[PreviousInstituteName],[YearOfPassing],[PreviousRollNo]
				   ,[PreviousPercent],[MotherName],[DateOfBirth],[Category],[AlternateMobileNo],[StudentEmail],[GuardianEmail],[StudentType]
				   ,[Medium],[Address],[CityId],[Village],[StateId],[Religion],[Image],[Remarks],[IsActive],[IsDelete],[ModifyDate]
				   ,[AddedDate],[AddedBy])
			 VALUES
				   (@AcademicSessionId,@Sr_RegNo,
				   
				   @FormNo,@ClassId,@SectionId,@StudentLoginId,@AdmissionDate,@FirstName,@LastName,@FatherName,@MobileNo,@Gender
				   ,@GuardianMobileNo,@EnrollNo,@MACHINEID,@PreviousClass,@PreviousInstituteName,@YearOfPassing,@PreviousRollNo
				   ,@PreviousPercent,@MotherName,@DateOfBirth,@Category,@AlternateMobileNo,@StudentEmail,@GuardianEmail,@StudentType
				   ,@Medium,@Address,@CityId,@Village,@StateId,@Religion,@Image,@Remarks,@IsActive,0,GETDATE()
				   ,GETDATE(),@AddedBy)

		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN 
		SELECT 0
	END
END
