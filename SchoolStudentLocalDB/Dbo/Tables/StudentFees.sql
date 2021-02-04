CREATE TABLE [dbo].[StudentFees]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1),
	[StudentAdmissionId] INT NOT NULL, 
	[FeeMasterId] INT NOT NULL, 
	[FeeGroupId] INT NULL, 
	[Status] NVARCHAR(20) NULL,
	[PaymentId] NVARCHAR(50) NULL,
	[Mode] NVARCHAR(50) NULL,
	[PaymentDate] DATETIME NULL,
	[IsPaid] BIT NULL DEFAULT 0,
	[IsActive] BIT NULL DEFAULT 0, 
	[IsDelete] BIT NULL DEFAULT 0, 
	[ModifyDate] DATETIME NULL DEFAULT GetDate(), 
	[ModifyBy] INT NULL, 
	[AddedDate] DATETIME NULL DEFAULT GetDate(), 
	[AddedBy] INT NULL,
)
