﻿CREATE TABLE [dbo].[ClassSection]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1), 
	[SectionId] INT NOT NULL, 
	[ClassId] INT NOT NULL, 
	[IsDelete] BIT NULL DEFAULT 0
)
