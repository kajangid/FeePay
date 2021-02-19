﻿-- =============================================
-- Author:		Karan
-- Create date: 12-01-2021
-- Description:	Sp to get fee master data with add edit user data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_FeeMaster_AddEditUser]
(
@Id INT = 0
,@FeeGroupId INT = NULL
,@FeeTypeId INT = NULL
,@IsActive BIT = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
		
	IF (@Id != 0)
	BEGIN	
		SELECT  
			[fm].[Id], [fm].[FeeGroupId], [fm].[FeeTypeId], [fm].[AcademicSessionId], [fm].[Amount], [fm].[DueDate], [fm].[Description],
			[fm].[IsActive], [fm].[IsDelete], [fm].[ModifyDate], [fm].[AddedDate]
			
			, [ft].[AddedBy] AS [Id]
			, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [ft].[AddedBy] AND [IsDelete] = 0) AS [FullName]
			, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [ft].[AddedBy] AND [IsDelete] = 0) AS [Email]
			, [ft].[ModifyBy] AS [Id]
			, (SELECT [FullName] FROM [SchoolAdmin_User] WHERE Id = [ft].[ModifyBy] AND [IsDelete] = 0) AS [FullName]
			, (SELECT [Email] FROM [SchoolAdmin_User] WHERE Id = [ft].[ModifyBy] AND [IsDelete] = 0) AS [Email]		,


			[ft].[Id], [ft].[Name], [ft].[NormalizedName], [ft].[Code], [ft].[Description], [ft].[IsActive], [ft].[IsDelete],
			[ft].[ModifyDate], [ft].[ModifyBy], [ft].[AddedDate], [ft].[AddedBy],

			[fg].[Id], [fg].[Name], [fg].[NormalizedName], [fg].[Description], [fg].[IsActive], [fg].[IsDelete], [fg].[ModifyDate],
			[fg].[ModifyBy], [fg].[AddedDate], [fg].[AddedBy] 

			FROM [dbo].[FeeMaster] [fm] 
			INNER JOIN [dbo].[FeeType] [ft]
			ON [ft].[Id] = [fm].[FeeTypeId] AND [ft].[IsDelete] = 0
			INNER JOIN [dbo].[FeeGroup] [fg]
			ON [fg].[Id] = [fm].[FeeGroupId] AND [fg].[IsDelete] = 0
			WHERE
			[fm].[IsDelete] = 0 
			AND [fm].[Id] = @Id 
			AND [fm].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [fm].[IsActive] END

			RETURN 
	END
END