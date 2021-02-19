-- =============================================
-- Author:		Karan
-- Create date: 18-01-2021
-- Description:	Sp to remove classSection data
-- =============================================
CREATE PROCEDURE [dbo].[SP_Remove_ClassSection]
    (	
    @Id INT = NULL,
    @SectionId INT = NULL, 
    @ClassId INT = NULL
    )
AS
BEGIN
    IF(@SectionId IS NOT NULL AND @SectionId != 0 AND @ClassId IS NOT NULL AND @ClassId != 0 )
    BEGIN
        IF EXISTS(SELECT [Id] FROM [dbo].[ClassSection] WHERE [ClassId] = @ClassId AND [SectionId] = @SectionId AND [IsDelete] = 0)
        BEGIN
            DECLARE @LastID INT = NULL

            UPDATE [dbo].[ClassSection] SET 
            [IsDelete] = 1,
            @LastID = [Id]
            WHERE
            [ClassId] = @ClassId AND
            [SectionId] = @SectionId AND 
            [IsDelete] = 0

            RETURN @LastID
        END
    END
    ELSE IF(@Id IS NOT NULL AND @Id != 0 )
    BEGIN
        IF EXISTS(SELECT [Id] FROM [dbo].[ClassSection] WHERE [Id] = @Id AND [IsDelete] = 0)
        BEGIN
            UPDATE [dbo].[ClassSection] SET 
            [IsDelete] = 1
            WHERE
            [Id] = @Id AND 
            [IsDelete] = 0

            RETURN @Id
        END
    END
    ELSE
    BEGIN
        RETURN 0
    END
END