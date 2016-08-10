CREATE TABLE [dbo].[ApplicationAsset] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Code]   VARCHAR (45)  NOT NULL,
    [Name]        VARCHAR (255) NOT NULL,
    [Description] VARCHAR (255) NULL,
    CONSTRAINT [PK__ApplicationAsset] PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Code] ASC)
);

