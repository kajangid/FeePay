CREATE TABLE [dbo].[FeesTranscation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1),
	[UserId] INT NOT NULL, 
	[TransactionId] NVARCHAR(50) NOT NULL, 
	[TransactionMode] NVARCHAR(50) NOT NULL, 
	[Amount] NUMERIC(10,2) NOT NULL, 
	[IsComplete] BIT NOT NULL,
	[State] NVARCHAR(50) NOT NULL,
	[Date] DATETIME NULL
)
