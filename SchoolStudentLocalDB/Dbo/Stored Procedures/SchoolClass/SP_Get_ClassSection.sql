-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to get classSection data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_ClassSection]
	(	
	@Id INT = NULL,
	@SectionId INT = NULL, 
	@ClassId INT = NULL
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	IF(@SectionId IS NOT NULL AND @SectionId != 0 AND @ClassId IS NOT NULL AND @ClassId != 0 )
	BEGIN

		SELECT 
		[c].[Id],[c].[Name],[c].[NormalizedName], [s].[Description],
		[s].[Id], [s].[Name],[s].[NormalizedName], [s].[Description]

		FROM [dbo].[ClassSection] [cs]
		INNER JOIN [dbo].[Class] [c] 
		ON [c].[Id] = [cs].[ClassId] AND [c].[IsDelete] = 0
		INNER JOIN [dbo].[Section] [s] 
		ON [s].[Id] = [cs].[SectionId] AND [s].[IsDelete] = 0
		WHERE 
		[cs].[ClassId] = @ClassId AND
		[cs].[SectionId] = @SectionId


		RETURN
	END
	ELSE IF(@SectionId IS NOT NULL AND @SectionId != 0 )
	BEGIN

		SELECT 
		[c].[Id],[c].[Name],[c].[NormalizedName], [s].[Description],
		[s].[Id], [s].[Name],[s].[NormalizedName], [s].[Description]

		FROM [dbo].[ClassSection] [cs]
		INNER JOIN [dbo].[Class] [c] 
		ON [c].[Id] = [cs].[ClassId] AND [c].[IsDelete] = 0
		INNER JOIN [dbo].[Section] [s] 
		ON [s].[Id] = [cs].[SectionId] AND [s].[IsDelete] = 0
		WHERE 
		[cs].[SectionId] = @SectionId


		RETURN
	END
	ELSE IF(@ClassId IS NOT NULL AND @ClassId != 0 )
	BEGIN

		SELECT 
		[c].[Id],[c].[Name],[c].[NormalizedName], [s].[Description],
		[s].[Id], [s].[Name],[s].[NormalizedName], [s].[Description]

		FROM [dbo].[ClassSection] [cs]
		INNER JOIN [dbo].[Class] [c] 
		ON [c].[Id] = [cs].[ClassId] AND [c].[IsDelete] = 0
		INNER JOIN [dbo].[Section] [s] 
		ON [s].[Id] = [cs].[SectionId] AND [s].[IsDelete] = 0
		WHERE 
		[cs].[ClassId] = @ClassId 


		RETURN
	END
	ELSE IF(@Id IS NOT NULL AND @Id != 0 )
	BEGIN

		SELECT 
		[c].[Id],[c].[Name],[c].[NormalizedName], [s].[Description],
		[s].[Id], [s].[Name],[s].[NormalizedName], [s].[Description]

		FROM [dbo].[ClassSection] [cs]
		INNER JOIN [dbo].[Class] [c] 
		ON [c].[Id] = [cs].[ClassId] AND [c].[IsDelete] = 0
		INNER JOIN [dbo].[Section] [s] 
		ON [s].[Id] = [cs].[SectionId] AND [s].[IsDelete] = 0
		WHERE 
		[cs].[Id] = @Id 


		RETURN
	END
	ELSE
	BEGIN

		SELECT 
		[c].[Id],[c].[Name],[c].[NormalizedName], [s].[Description],
		[s].[Id], [s].[Name],[s].[NormalizedName], [s].[Description]

		FROM [dbo].[ClassSection] [cs]
		INNER JOIN [dbo].[Class] [c] 
		ON [c].[Id] = [cs].[ClassId] AND [c].[IsDelete] = 0
		INNER JOIN [dbo].[Section] [s] 
		ON [s].[Id] = [cs].[SectionId] AND [s].[IsDelete] = 0


		RETURN
	END
END