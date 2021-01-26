CREATE TABLE [dbo].[SchoolAdmin_Role] (
    [Id]             INT            IDENTITY (100, 1) NOT NULL,
    [Name]           NVARCHAR (256) NOT NULL,
    [NormalizedName] NVARCHAR (256) NOT NULL,
    [IsActive]       BIT            DEFAULT ((0)) NULL,
    [IsDelete]       BIT            DEFAULT ((0)) NULL,
    [ModifyDate]     DATETIME       DEFAULT (getdate()) NULL,
    [ModifyBy]       INT            NULL,
    [AddedDate]      DATETIME       DEFAULT (getdate()) NULL,
    [AddedBy]        INT            NULL,
    [Access]         NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


