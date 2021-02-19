-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to delete [Archive] Payment Gateway Document data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Delete_PaymentGatewayDocument]
(
@Id				INT 
,@ModifyBy		INT	= NULL
)
AS
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[PaymentGatewayDocument] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN
		UPDATE [dbo].[PaymentGatewayDocument] SET 
		[IsActive]		= 0
		,[IsDelete]		= 1
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