CREATE TABLE [dbo].[Session]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1),
	[Year] NVARCHAR(50) NOT NULL,
	[StartYear] NVARCHAR(20) NULL, 
	[EndYear] NVARCHAR(20) NULL, 
	[Description] NVARCHAR(350) NULL,
	[IsActive] BIT NULL DEFAULT 0, 
	[IsDelete] BIT NULL DEFAULT 0, 
	[ModifyDate] DATETIME NULL DEFAULT GetDate(), 
	[ModifyBy] INT NULL, 
	[AddedDate] DATETIME NULL DEFAULT GetDate(), 
	[AddedBy] INT NULL,
)

