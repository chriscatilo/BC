CREATE TABLE [dbo].[OrganisationType] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Code]     VARCHAR (5)    NOT NULL,
    [Name]     NVARCHAR (255) NOT NULL,
    [IsActive] BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

