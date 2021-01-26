-- =============================================
-- Author:		Karan
-- Create date: 21-01-2021
-- Description:	Sp to add student fee
-- =============================================
CREATE PROCEDURE [dbo].[SP_Add_StudentFees]
	(		
	@StudentAdmissionId		 INT
	,@FeeMasterId			 INT
	,@FeeGroupId			 INT = NULL
	,@IsPaid				 BIT = NULL
	,@IsActive				 BIT = NULL
	,@AddedBy				 INT = NULL
	)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM [dbo].[StudentFees] WHERE [StudentAdmissionId] = @StudentAdmissionId AND [FeeMasterId] = @FeeMasterId AND [IsDelete] = 0)
	BEGIN

		INSERT 
			INTO [dbo].[StudentFees]
				   ([StudentAdmissionId],[FeeMasterId],[FeeGroupId],[IsPaid],[IsActive],[IsDelete],[AddedDate],[ModifyDate],[AddedBy])
			 VALUES
				   (@StudentAdmissionId,@FeeMasterId,@FeeGroupId,@IsPaid,@IsActive,0,GETDATE(),GETDATE(),@AddedBy)

		SELECT CAST(SCOPE_IDENTITY() AS INT)
	END
	ELSE
	BEGIN 
		SELECT 0
	END
END