-- =============================================
-- Author:		Karan
-- Create date: 12-02-2021
-- Description:	Sp to Add Student Academic Sessions
-- =============================================
CREATE PROCEDURE [dbo].[SP_Add_Student_Academic_Sessions]
(
	@StudentAdmissionId		INT,
	@SessionId				INT,
	@ClassId				INT,
	@SectionId				INT = NULL,
	@IsActive				BIT = NULL,
	@AddedBy				INT = NULL
)
AS
BEGIN
	IF NOT EXISTS (SELECT [Id] FROM [dbo].[Student_Academic_Sessions] 
	WHERE [StudentAdmissionId] = @StudentAdmissionId AND [SessionId] = @SessionId AND [IsDelete] = 0)
	BEGIN
		INSERT INTO [dbo].[Student_Academic_Sessions]
		([StudentAdmissionId],[SessionId],[ClassId],[SectionId],[IsActive],[IsDelete],
		[AddedBy],[AddedDate],[ModifyDate])

		VALUES
		(@StudentAdmissionId,@SessionId,@ClassId,@SectionId,@IsActive,0,
		@AddedBy,GETDATE(),GETDATE())

		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN 
		SELECT 0
	END
END
