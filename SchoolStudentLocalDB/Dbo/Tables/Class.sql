CREATE TABLE [dbo].[Class]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1),
	[Name] NVARCHAR(50) NOT NULL, 
	[NormalizedName] NVARCHAR(50) NOT NULL,
	[SessionId] INT NOT NULL, 
	[Section] CHAR(10) NULL, 
	[Description] NVARCHAR(350) NULL,
	[IsActive] BIT NULL DEFAULT 0, 
	[IsDelete] BIT NULL DEFAULT 0, 
	[ModifyDate] DATETIME NULL DEFAULT GetDate(), 
	[ModifyBy] INT NULL, 
	[AddedDate] DATETIME NULL DEFAULT GetDate(), 
	[AddedBy] INT NULL,
)
