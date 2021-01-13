CREATE TABLE [dbo].[StudentFees]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1),
    [StudentProfileId] INT NOT NULL, 
    [FeeMasterId] INT NOT NULL, 
    [IsPaid] BIT NULL DEFAULT 0, 
    [PaymentId] INT NULL, 
    [IsActive] BIT NULL DEFAULT 0, 
    [IsDelete] BIT NULL DEFAULT 0, 
    [ModifyDate] DATETIME NULL DEFAULT GetDate(), 
    [ModifyBy] INT NULL, 
    [AddedDate] DATETIME NULL DEFAULT GetDate(), 
    [AddedBy] INT NULL,
)
