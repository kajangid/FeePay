CREATE PROCEDURE [dbo].[SP_Add_RegisteredSchool]
(
@Name			 NVARCHAR (356)   --required
,@NormalizedName NVARCHAR (356)   --required
,@UniqueId		 NVARCHAR (40)    --required
,@ContactNumber  NVARCHAR (50)    NULL
,@PrincipalName  NVARCHAR (50)    NULL
,@Address		 NVARCHAR (256)   NULL
,@SchoolImage    NVARCHAR (500)   NULL
,@IsApproved	 BIT = 0		  	
,@IsActive		 BIT = 0
,@AddedBy        INT              NULL
)
AS 
BEGIN
	IF NOT EXISTS(SELECT [Id] FROM [dbo].[RegisteredSchool] WHERE [NormalizedName] = @NormalizedName OR [UniqueId] = @UniqueId)
	BEGIN
		INSERT INTO [dbo].[RegisteredSchool]
			([Name],[NormalizedName],[UniqueId],[ContactNumber],[PrincipalName],[Address],[SchoolImage],
			[IsApproved],[IsActive],[IsDelete],[ModifyBy],[ModifyDate],[AddedBy],[AddedDate])

			VALUES(@Name,@NormalizedName,@UniqueId,@ContactNumber,@PrincipalName,@Address,@SchoolImage
			,@IsApproved,@IsActive,0,@AddedBy,GETDATE(),@AddedBy,GETDATE())

		RETURN CAST(SCOPE_IDENTITY() AS INT)
    END
    ELSE
    BEGIN
        RETURN 0
	END
END