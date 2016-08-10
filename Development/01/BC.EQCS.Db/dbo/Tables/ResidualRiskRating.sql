CREATE TABLE [dbo].[ResidualRiskRating] (
    [Id] INT       IDENTITY (1, 1) NOT NULL,
	[Code]         VARCHAR (4) NOT NULL UNIQUE,
    [Name]         VARCHAR (255) NOT NULL,
    [IsActive]     BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

