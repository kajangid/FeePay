-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to delete super user data (isdelete = 1)
-- =============================================
CREATE PROCEDURE [dbo].[SP_Delete_SuperAdmin_User]
	@Id				INT ,
	@ModifyBy		INT = NULL
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[SuperAdmin_User] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN
		UPDATE [dbo].[SuperAdmin_User] SET
		[IsDelete]			= 1			,
		[ModifyDate]		= GETDATE()	,
		[ModifyBy]			= @ModifyBy
		WHERE 
		[Id]		= @Id AND 
		[IsDelete]	= 0

		SELECT  @Id
	END
	ELSE	
		RETURN 0
END
