﻿CREATE TABLE [dbo].[SuperAdmin_UserRole]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1),
    [UserId] INT NOT NULL, 
    [RoleId] INT NOT NULL, 
    [IsActive] BIT NULL DEFAULT 0, 
    [IsDelete] BIT NULL DEFAULT 0, 
    [EndDate] DATETIME NULL , 
    [BeginDate] DATETIME NULL , 
    [ModifyDate] DATETIME NULL DEFAULT GetDate(), 
    [ModifyBy] INT NULL, 
    [AddedDate] DATETIME NULL DEFAULT GetDate(), 
    [AddedBy] INT NULL,

)
