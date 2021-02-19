CREATE TABLE [dbo].[Student_Academic_Sessions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1), 
	[StudentAdmissionId] INT NOT NULL, 
	[SessionId] INT NOT NULL, 
    [ClassId] INT NOT NULL, 
    [SectionId] INT NULL,
	[IsActive] BIT NULL DEFAULT 0,
	[IsDelete] BIT NULL DEFAULT 0,
	[ModifyDate] DATETIME NULL DEFAULT GetDate(), 
	[ModifyBy] INT NULL, 
	[AddedDate] DATETIME NULL DEFAULT GetDate(), 
	[AddedBy] INT NULL,
)
