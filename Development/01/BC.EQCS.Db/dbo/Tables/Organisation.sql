CREATE TABLE [dbo].[Organisation] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (150) NOT NULL,
    [IsActive]           BIT            NOT NULL,
    [OrganisationTypeId] INT            NULL,
    [Code]               VARCHAR (150)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Organisation_OrganisationTypeId] FOREIGN KEY ([OrganisationTypeId]) REFERENCES [dbo].[OrganisationType] ([Id]),
    UNIQUE NONCLUSTERED ([Name] ASC)
);




GO

