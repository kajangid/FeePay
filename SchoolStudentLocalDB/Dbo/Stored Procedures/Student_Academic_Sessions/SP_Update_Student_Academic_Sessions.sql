-- =============================================
-- Author:		Karan
-- Create date: 12-02-2021
-- Description:	Sp to Add Student Academic Sessions
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Student_Academic_Sessions]
(
	@Id						INT,
	@StudentAdmissionId		INT = NULL,
	@SessionId				INT = NULL,
	@ClassId				INT = NULL,
	@SectionId				INT = NULL,
	@IsActive				BIT = NULL,
	@ModifyBy				INT = NULL
)
AS
BEGIN
	IF EXISTS (SELECT [Id] FROM [dbo].[Student_Academic_Sessions] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN
		UPDATE [dbo].[Student_Academic_Sessions] SET
		[StudentAdmissionId]	=	CASE WHEN @StudentAdmissionId IS NOT NULL AND @StudentAdmissionId != 0 THEN @StudentAdmissionId ELSE [StudentAdmissionId] END,
		[SessionId]				=	CASE WHEN @SessionId IS NOT NULL AND @SessionId != 0 THEN @SessionId ELSE [SessionId] END,
		[ClassId]				=	CASE WHEN @ClassId IS NOT NULL AND @ClassId != 0 THEN @ClassId ELSE [ClassId] END,
		[SectionId]				=	CASE WHEN @SectionId IS NOT NULL AND @SectionId != 0 THEN @SectionId ELSE [SectionId] END,
		[IsActive]				=	CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END,
		[ModifyBy]				=	@ModifyBy,
		[ModifyDate]			=	GETDATE()
		WHERE 
		[Id]					= @Id AND
		[IsDelete]				= 0

		SELECT @Id
	END
	ELSE
	BEGIN 
		SELECT 0
	END
END