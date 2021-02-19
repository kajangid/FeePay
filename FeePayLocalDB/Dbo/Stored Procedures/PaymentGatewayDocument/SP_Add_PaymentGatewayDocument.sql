-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to add Payment Gateway Document data.
-- =============================================
CREATE PROCEDURE [dbo].[SP_Add_PaymentGatewayDocument]
(
@BusinessDisplayName			NVARCHAR(50),				--required
@BusinessPANCardNumber			NVARCHAR(30),				--required
@BusinessPANCardCopy			NVARCHAR(255),				--required
@RegisteredAddress				NVARCHAR(255),				--required
@AddressPinCode					NVARCHAR(10),				--required
@AddressCityId					INT,						--required
@AddressStateId					INT,						--required
@AccountNumber					NVARCHAR(20),				--required
@IFSC							NVARCHAR(50),				--required
@BankPassbookCopy				NVARCHAR(255),				--required
@ContactName					NVARCHAR(50),				--required
@ContactEmail					NVARCHAR(50),				--required
@ContactPhoneNumber				NVARCHAR(50),				--required
@IdentityProof					NVARCHAR(255),				--required
@RegisteredSchoolId				INT,						--required
@IsApproved						BIT = 0, 
@IsActive						BIT = 0, 
@AddedBy						INT = NULL
)
AS 
BEGIN
	IF NOT EXISTS(SELECT [Id] FROM [dbo].[PaymentGatewayDocument] WHERE 
		[BusinessDisplayName] = @BusinessDisplayName AND
		[BusinessPANCardNumber] = @BusinessPANCardNumber AND 
		[IsDelete] = 0)
	BEGIN
		INSERT INTO [dbo].[PaymentGatewayDocument]
			([BusinessDisplayName],[BusinessPANCardNumber],[BusinessPANCardCopy],[RegisteredAddress],[AddressPinCode],[RegisteredSchoolId],
			[AddressCityId],[AddressStateId],[AccountNumber],[IFSC],[BankPassbookCopy],[ContactName],[ContactEmail],
			[ContactPhoneNumber],[IdentityProof],[IsApproved],[IsActive],[IsDelete],[ModifyDate],[AddedDate],[AddedBy])

			VALUES(
			@BusinessDisplayName,@BusinessPANCardNumber,@BusinessPANCardCopy,@RegisteredAddress,@AddressPinCode,@RegisteredSchoolId,
			@AddressCityId,@AddressStateId,@AccountNumber,@IFSC,@BankPassbookCopy,@ContactName,@ContactEmail,
			@ContactPhoneNumber,@IdentityProof,@IsApproved,@IsActive,0,GETDATE(),GETDATE(),@AddedBy)

		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN
		RETURN 0
	END
END