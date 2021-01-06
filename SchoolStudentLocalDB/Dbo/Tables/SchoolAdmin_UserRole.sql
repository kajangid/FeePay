CREATE TABLE [dbo].[SchoolAdmin_UserRole]
(
	[Id] INT NOT NULL PRIMARY KEY,
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
