CREATE PROCEDURE [dbo].[SP_Update_RegisteredSchool]
(
@Id				 INT              --required
,@Name			 NVARCHAR (356)   NULL
,@NormalizedName NVARCHAR (356)   NULL
,@UniqueId		 NVARCHAR (40)    NULL
,@ContactNumber  NVARCHAR (50)    NULL
,@PrincipalName  NVARCHAR (50)    NULL
,@Address		 NVARCHAR (256)   NULL
,@SchoolImage    NVARCHAR (500)   NULL
,@IsApproved	 BIT = 0		  	
,@IsActive		 BIT = 0
,@ModifyBy       INT              NULL
)
AS 
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[RegisteredSchool] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN		
		UPDATE [dbo].[RegisteredSchool] SET
		[Name] = @Name
		,[NormalizedName] = @NormalizedName
		,[UniqueId] = @UniqueId
		,[ContactNumber] = @ContactNumber
		,[PrincipalName] = @PrincipalName
		,[Address] = @Address
		,[SchoolImage] = @SchoolImage
		,[IsApproved] = @IsApproved
		,[IsActive] = @IsActive
		,[ModifyBy] = @ModifyBy
		,[ModifyDate] = GETDATE()
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