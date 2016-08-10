CREATE TABLE [dbo].[Product] (
    [Id] INT           IDENTITY (1, 1) NOT NULL,
    [Code]      VARCHAR (10)   NOT NULL,
    [Name]      VARCHAR (255) NOT NULL,
	[IsUkvi] BIT NOT NULL,
    [IsActive]  BIT           NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);



