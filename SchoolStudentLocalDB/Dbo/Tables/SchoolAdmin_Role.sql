CREATE TABLE [dbo].[SchoolAdmin_Role]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1),
    [Name]             NVARCHAR (256)   NOT NULL,
    [NormalizedName]   NVARCHAR (256)   NOT NULL,
    [IsActive] BIT NULL DEFAULT 0, 
    [IsDelete] BIT NULL DEFAULT 0, 
    [ModifyDate] DATETIME NULL DEFAULT GetDate(), 
    [ModifyBy] INT NULL, 
    [AddedDate] DATETIME NULL DEFAULT GetDate(), 
    [AddedBy] INT NULL,
)
