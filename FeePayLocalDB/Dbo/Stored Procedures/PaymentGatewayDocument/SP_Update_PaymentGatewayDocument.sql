-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to update Payment Gateway Document data.
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_PaymentGatewayDocument]
(
@Id								INT,					--required
@BusinessDisplayName			NVARCHAR(50) = NULL,
@BusinessPANCardNumber			NVARCHAR(30) = NULL,
@BusinessPANCardCopy			NVARCHAR(255) = NULL,
@RegisteredAddress				NVARCHAR(255) = NULL,
@AddressPinCode					NVARCHAR(10) = NULL,
@AddressCityId					INT = NULL,
@AddressStateId					INT = NULL,
@AccountNumber					NVARCHAR(20) = NULL,
@IFSC							NVARCHAR(50) = NULL,
@BankPassbookCopy				NVARCHAR(255) = NULL,
@ContactName					NVARCHAR(50) = NULL,
@ContactEmail					NVARCHAR(50) = NULL,
@ContactPhoneNumber				NVARCHAR(50) = NULL,
@IdentityProof					NVARCHAR(255) = NULL,
@RegisteredSchoolId				INT = NULL,
@IsApproved						BIT  = NULL, 
@IsActive						BIT  = NULL, 
@ModifyBy						INT  = NULL
)
AS 
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[PaymentGatewayDocument] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN		
		UPDATE [dbo].[PaymentGatewayDocument] SET
		[BusinessDisplayName]		= CASE WHEN @BusinessDisplayName IS NOT NULL THEN @BusinessDisplayName ELSE [BusinessDisplayName] END,
		[BusinessPANCardNumber]		= CASE WHEN @BusinessPANCardNumber IS NOT NULL THEN @BusinessPANCardNumber ELSE [BusinessPANCardNumber] END,
		[BusinessPANCardCopy]		= CASE WHEN @BusinessPANCardCopy IS NOT NULL THEN @BusinessPANCardCopy ELSE [BusinessPANCardCopy] END,
		[RegisteredAddress]			= CASE WHEN @RegisteredAddress IS NOT NULL THEN @RegisteredAddress ELSE [RegisteredAddress] END,
		[AddressPinCode]			= CASE WHEN @AddressPinCode IS NOT NULL THEN @AddressPinCode ELSE [AddressPinCode] END,
		[AddressCityId]				= CASE WHEN @AddressCityId IS NOT NULL AND @AddressCityId <> 0 THEN @AddressCityId ELSE [AddressCityId] END,
		[AddressStateId]			= CASE WHEN @AddressStateId IS NOT NULL AND @AddressStateId <> 0 THEN @AddressStateId ELSE [AddressStateId] END,
		[AccountNumber]				= CASE WHEN @AccountNumber IS NOT NULL THEN @AccountNumber ELSE [AccountNumber] END,
		[IFSC]						= CASE WHEN @IFSC IS NOT NULL THEN @IFSC ELSE [IFSC] END,
		[BankPassbookCopy]			= CASE WHEN @BankPassbookCopy IS NOT NULL THEN @BankPassbookCopy ELSE [BankPassbookCopy] END,
		[ContactName]				= CASE WHEN @ContactName IS NOT NULL THEN @ContactName ELSE [ContactName] END,
		[ContactEmail]				= CASE WHEN @ContactEmail IS NOT NULL THEN @ContactEmail ELSE [ContactEmail] END,
		[ContactPhoneNumber]		= CASE WHEN @ContactPhoneNumber IS NOT NULL THEN @ContactPhoneNumber ELSE [ContactPhoneNumber] END,
		[IdentityProof]				= CASE WHEN @IdentityProof IS NOT NULL THEN @IdentityProof ELSE [IdentityProof] END,
		[RegisteredSchoolId]		= CASE WHEN @RegisteredSchoolId IS NOT NULL AND @RegisteredSchoolId <> 0  THEN @RegisteredSchoolId ELSE [RegisteredSchoolId] END,
		[IsApproved]				= CASE WHEN @IsApproved IS NOT NULL THEN @IsApproved ELSE [IsApproved] END,
		[IsActive]					= CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END,
		[ModifyBy]					= @ModifyBy,
		[ModifyDate]				= GETDATE()
		WHERE 
		[Id]	= @Id AND 
		[IsDelete]	= 0

		SELECT @Id
	END
	ELSE
	BEGIN
		RETURN 0
	END
END