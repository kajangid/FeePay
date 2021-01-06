CREATE TABLE [dbo].[RegisteredSchool]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1),
    [Name]             NVARCHAR (356)   NOT NULL,
    [NormalizedName]   NVARCHAR (356)   NOT NULL,
    [UniqueId]             NVARCHAR (40)   NOT NULL,
    [Address]   NVARCHAR (256)   NULL,
    [PrincipalName]             NVARCHAR (50)   NULL,
    [ContactNumber]   NVARCHAR (50)   NULL,
    [IsApproved]             BIT   NOT NULL DEFAULT 0,
    [SchoolImage]   NVARCHAR (500)   NULL,
    [IsActive] BIT NULL DEFAULT 0, 
    [IsDelete] BIT NULL DEFAULT 0, 
    [ModifyDate] DATETIME NULL DEFAULT GetDate(), 
    [ModifyBy] INT NULL, 
    [AddedDate] DATETIME NULL DEFAULT GetDate(), 
    [AddedBy] INT NULL,
)
