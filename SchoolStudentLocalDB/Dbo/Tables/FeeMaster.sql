CREATE TABLE [dbo].[FeeMaster]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1),
	[FeeGroupId] INT NOT NULL, 
	[FeeTypeId] INT NOT NULL, 
	[Amount] NUMERIC(6,2) NOT NULL,
	[DueDate] DATETIME NULL,  	
	[Description] NVARCHAR(350) NULL,
	[IsActive] BIT NULL DEFAULT 0, 
	[IsDelete] BIT NULL DEFAULT 0, 
	[ModifyDate] DATETIME NULL DEFAULT GetDate(), 
	[ModifyBy] INT NULL, 
	[AddedDate] DATETIME NULL DEFAULT GetDate(), 
	[AddedBy] INT NULL,
)
