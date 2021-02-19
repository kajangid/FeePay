CREATE PROCEDURE [dbo].[SP_Update_RegisteredSchool]
(
@Id				 INT              --required
,@Name			 NVARCHAR (356)   = NULL
,@NormalizedName NVARCHAR (356)   = NULL
,@UniqueId		 NVARCHAR (40)    = NULL
,@ContactNumber  NVARCHAR (50)    = NULL
,@PrincipalName  NVARCHAR (50)    = NULL
,@Address		 NVARCHAR (256)   = NULL
,@SchoolImage    NVARCHAR (500)   = NULL
,@IsApproved	 BIT			  = NULL
,@IsActive		 BIT			  = NULL
,@ModifyBy       INT              = NULL
)
AS 
BEGIN
	IF EXISTS(SELECT [Id] FROM [dbo].[RegisteredSchool] WHERE [Id] = @Id AND [IsDelete] = 0)
	BEGIN		
		UPDATE [dbo].[RegisteredSchool] SET
		[Name]				= CASE WHEN @Name IS NOT NULL THEN @Name ELSE [Name] END
		,[NormalizedName]	= CASE WHEN @NormalizedName IS NOT NULL THEN @NormalizedName ELSE [NormalizedName] END
		,[UniqueId]			= CASE WHEN @UniqueId IS NOT NULL THEN @UniqueId ELSE [UniqueId] END
		,[ContactNumber]	= CASE WHEN @ContactNumber IS NOT NULL THEN @ContactNumber ELSE [ContactNumber] END
		,[PrincipalName]	= CASE WHEN @PrincipalName IS NOT NULL THEN @PrincipalName ELSE [PrincipalName] END
		,[Address]			= CASE WHEN @Address IS NOT NULL THEN @Address ELSE [Address] END
		,[SchoolImage]		= CASE WHEN @SchoolImage IS NOT NULL THEN @SchoolImage ELSE [SchoolImage] END
		,[IsApproved]		= CASE WHEN @IsApproved IS NOT NULL THEN @IsApproved ELSE [IsApproved] END
		,[IsActive]			= CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END
		,[ModifyBy]			= @ModifyBy
		,[ModifyDate]		= GETDATE()
		WHERE 
		[Id]				= @Id AND 
		[IsDelete]			= 0

		SELECT @Id
	END
	ELSE
	BEGIN
		RETURN 0
	END
END