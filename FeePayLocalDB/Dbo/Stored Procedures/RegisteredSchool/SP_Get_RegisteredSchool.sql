-- =============================================
-- Author:		Karan
-- Create date: 26-12-2020
-- Description:	Sp to get Registered Schools data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_RegisteredSchool]
(
@Id						INT				= NULL
,@NormalizedName		NVARCHAR(256)	= NULL
,@IsActive				BIT				= NULL
,@UniqueId				NVARCHAR(40)	= NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF(@Id IS NOT NULL AND @Id <> 0)
	
	BEGIN
		SELECT [Id], [Name], [NormalizedName], [UniqueId], [Address], [PrincipalName],
		[ContactNumber], [IsApproved], [SchoolImage], [IsActive], [IsDelete], [ModifyDate],
		[ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[RegisteredSchool] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND
		[Id] = @Id
		RETURN 
	END
	ELSE IF(@NormalizedName IS NOT NULL)
	BEGIN
		SELECT [Id], [Name], [NormalizedName], [UniqueId], [Address], [PrincipalName],
		[ContactNumber], [IsApproved], [SchoolImage], [IsActive], [IsDelete], [ModifyDate],
		[ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[RegisteredSchool] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND
		[NormalizedName] = @NormalizedName 
		RETURN 
	END
	ELSE IF(@UniqueId IS NOT NULL)
	BEGIN
		SELECT [Id], [Name], [NormalizedName], [UniqueId], [Address], [PrincipalName],
		[ContactNumber], [IsApproved], [SchoolImage], [IsActive], [IsDelete], [ModifyDate],
		[ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[RegisteredSchool] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END AND
		[UniqueId] = @UniqueId 
		RETURN 
	END 
	ELSE
	BEGIN
		SELECT [Id], [Name], [NormalizedName], [UniqueId], [Address], [PrincipalName],
		[ContactNumber], [IsApproved], [SchoolImage], [IsActive], [IsDelete], [ModifyDate],
		[ModifyBy], [AddedDate], [AddedBy]
		FROM [dbo].[RegisteredSchool] 
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END
		RETURN
	END
END