-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to get Payment Gateway Document data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_PaymentGatewayDocument]
(
@Id						INT				= NULL
,@RegisteredSchoolId	INT				= NULL
,@IsApproved			BIT				= NULL
,@IsActive				BIT				= NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF(@Id IS NOT NULL AND @Id <> 0)	
	BEGIN
		SELECT  [Id], [BusinessDisplayName], [BusinessPANCardNumber], [BusinessPANCardCopy], [RegisteredAddress], [AddressPinCode],
		[AddressCityId], [AddressStateId], [AccountNumber], [IFSC], [BankPassbookCopy], [ContactName], [ContactEmail],
		[ContactPhoneNumber], [IdentityProof], [RegisteredSchoolId], [IsApproved], [IsActive], [IsDelete], [ModifyDate], 
		[ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[PaymentGatewayDocument] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND
		[Id] = @Id
		RETURN 
	END
	ELSE IF(@RegisteredSchoolId IS NOT NULL AND @RegisteredSchoolId <> 0)
	BEGIN
		SELECT  [Id], [BusinessDisplayName], [BusinessPANCardNumber], [BusinessPANCardCopy], [RegisteredAddress], [AddressPinCode],
		[AddressCityId], [AddressStateId], [AccountNumber], [IFSC], [BankPassbookCopy], [ContactName], [ContactEmail],
		[ContactPhoneNumber], [IdentityProof], [RegisteredSchoolId], [IsApproved], [IsActive], [IsDelete], [ModifyDate],
		[ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[PaymentGatewayDocument] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND
		[RegisteredSchoolId] = @RegisteredSchoolId 
		RETURN 
	END
	ELSE IF(@IsApproved IS NOT NULL)
	BEGIN
		SELECT  [Id], [BusinessDisplayName], [BusinessPANCardNumber], [BusinessPANCardCopy], [RegisteredAddress], [AddressPinCode], 
		[AddressCityId], [AddressStateId], [AccountNumber], [IFSC], [BankPassbookCopy], [ContactName], [ContactEmail], 
		[ContactPhoneNumber], [IdentityProof], [RegisteredSchoolId], [IsApproved], [IsActive], [IsDelete], [ModifyDate],
		[ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[PaymentGatewayDocument] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND
		[IsApproved] = @IsApproved 
		RETURN 
	END 
	ELSE
	BEGIN
		SELECT [Id], [BusinessDisplayName], [BusinessPANCardNumber], [BusinessPANCardCopy], [RegisteredAddress], [AddressPinCode],
		[AddressCityId], [AddressStateId], [AccountNumber], [IFSC], [BankPassbookCopy], [ContactName], [ContactEmail],
		[ContactPhoneNumber], [IdentityProof], [RegisteredSchoolId], [IsApproved], [IsActive], [IsDelete], [ModifyDate],
		[ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[PaymentGatewayDocument] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END
		RETURN
	END
END