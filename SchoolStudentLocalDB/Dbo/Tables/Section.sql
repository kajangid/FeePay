CREATE TABLE [dbo].[Section]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1),
	[Name] NVARCHAR(10) NOT NULL, 
	[NormalizedName] NVARCHAR(10) NOT NULL,
	[Description] NVARCHAR(350) NULL,
	[IsActive] BIT NULL DEFAULT 0, 
	[IsDelete] BIT NULL DEFAULT 0, 
	[ModifyDate] DATETIME NULL DEFAULT GetDate(), 
	[ModifyBy] INT NULL, 
	[AddedDate] DATETIME NULL DEFAULT GetDate(), 
	[AddedBy] INT NULL,
)
