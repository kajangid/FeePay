﻿CREATE TABLE [dbo].[States]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Name] NVARCHAR(50) NOT NULL,
	[IsActive] BIT NULL DEFAULT 0 ,
	[IsDelete] BIT NULL DEFAULT 0 
)
