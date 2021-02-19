-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to delete Registered Schools data (isdelete = 1)
-- =============================================
CREATE PROCEDURE [dbo].[SP_Delete_RegisteredSchool]
(
@Id				INT 
,@ModifyBy		INT	= NULL
)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[RegisteredSchool] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN
		UPDATE [dbo].[RegisteredSchool] SET 
		[IsDelete]		= 1
		,[ModifyDate]	= GETDATE()
		,[ModifyBy]		= @ModifyBy
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