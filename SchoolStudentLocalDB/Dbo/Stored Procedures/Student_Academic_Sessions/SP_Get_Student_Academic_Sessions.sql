-- =============================================
-- Author:		Karan
-- Create date: 12-02-2021
-- Description:	Sp to Get Student Academic Sessions
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Student_Academic_Sessions]
(
	@Id						INT = NULL,
	@StudentAdmissionId		INT = NULL,
	@SessionId				INT = NULL,
	@ClassId				INT = NULL,
	@SectionId				INT = NULL,
	@IsActive				BIT = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	IF(@StudentAdmissionId IS NOT NULL AND @StudentAdmissionId != 0 AND @SessionId IS NOT NULL AND @SessionId != 0)
		BEGIN
			SELECT 
			[Id],[StudentAdmissionId],[SessionId],[ClassId],[SectionId],[IsActive],[IsDelete],
			[AddedBy],[ModifyBy],[AddedDate],[ModifyDate]
			FROM [dbo].[Student_Academic_Sessions]
			WHERE 
			[IsDelete]	= 0 AND 
			[IsActive]	= CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND 
			[StudentAdmissionId] = @StudentAdmissionId AND 
			[SessionId] = @SessionId

			RETURN
		END
	IF(@Id IS NOT NULL AND @Id != 0)
		BEGIN
			SELECT 
			[Id],[StudentAdmissionId],[SessionId],[ClassId],[SectionId],[IsActive],[IsDelete],
			[AddedBy],[ModifyBy],[AddedDate],[ModifyDate]
			FROM [dbo].[Student_Academic_Sessions]
			WHERE 
			[IsDelete]	= 0 AND 
			[IsActive]	= CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND 
			[Id]		= @Id

			RETURN
		END
	IF(@StudentAdmissionId IS NOT NULL AND @StudentAdmissionId != 0)
		BEGIN
			SELECT 
			[Id],[StudentAdmissionId],[SessionId],[ClassId],[SectionId],[IsActive],[IsDelete],
			[AddedBy],[ModifyBy],[AddedDate],[ModifyDate]
			FROM [dbo].[Student_Academic_Sessions]
			WHERE 
			[IsDelete]	= 0 AND 
			[IsActive]	= CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND 
			[StudentAdmissionId] = @StudentAdmissionId

			RETURN
		END
	IF(@SessionId IS NOT NULL AND @SessionId != 0)
		BEGIN
			SELECT 
			[Id],[StudentAdmissionId],[SessionId],[ClassId],[SectionId],[IsActive],[IsDelete],
			[AddedBy],[ModifyBy],[AddedDate],[ModifyDate]
			FROM [dbo].[Student_Academic_Sessions]
			WHERE 
			[IsDelete]	= 0 AND 
			[IsActive]	= CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND 
			[SessionId] = @SessionId

			RETURN
		END
	IF(@ClassId IS NOT NULL AND @ClassId != 0)
		BEGIN
			SELECT 
			[Id],[StudentAdmissionId],[SessionId],[ClassId],[SectionId],[IsActive],[IsDelete],
			[AddedBy],[ModifyBy],[AddedDate],[ModifyDate]
			FROM [dbo].[Student_Academic_Sessions]
			WHERE 
			[IsDelete]	= 0 AND 
			[IsActive]	= CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND 
			[ClassId]	= @ClassId

			RETURN
		END
	IF(@SectionId IS NOT NULL AND @SectionId != 0)
		BEGIN
			SELECT 
			[Id],[StudentAdmissionId],[SessionId],[ClassId],[SectionId],[IsActive],[IsDelete],
			[AddedBy],[ModifyBy],[AddedDate],[ModifyDate]
			FROM [dbo].[Student_Academic_Sessions]
			WHERE 
			[IsDelete]	= 0 AND 
			[IsActive]	= CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND 
			[SectionId] = @SectionId

			RETURN
		END 

	SELECT 
	[Id],[StudentAdmissionId],[SessionId],[ClassId],[SectionId],[IsActive],[IsDelete],
	[AddedBy],[ModifyBy],[AddedDate],[ModifyDate]
	FROM [dbo].[Student_Academic_Sessions]
	WHERE 
	[IsDelete]	= 0 AND 
	[IsActive]	= CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END

	RETURN

END