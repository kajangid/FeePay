-- =============================================
-- Author:		Karan
-- Create date: 12-02-2021
-- Description:	Sp to Delete Student Academic Sessions
-- =============================================
CREATE PROCEDURE [dbo].[SP_Remove_Student_Academic_Sessions]
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
		[IsActive]				= 0,
		[IsDelete]				= 1,
		[ModifyBy]				=	@ModifyBy,
		[ModifyDate]			=	GETDATE()
		WHERE 
		[Id]					= @Id AND
		[IsDelete]				= 0

		RETURN @Id
	END
	ELSE
	BEGIN 
		RETURN 0
	END
END