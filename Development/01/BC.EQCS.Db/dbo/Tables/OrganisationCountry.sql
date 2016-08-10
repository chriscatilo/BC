CREATE TABLE [dbo].[OrganisationCountry] (
	[Id] INT NOT NULL PRIMARY KEY CLUSTERED,
	[OrganisationId] INT NOT NULL,
	[CountryId] INT NOT NULL,
	[IsActive] BIT DEFAULT 0 NOT NULL,
	CONSTRAINT [FK_OrganisationCountry_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]),
	CONSTRAINT [FK_OrganisationCountry_Organisation] FOREIGN KEY ([OrganisationId]) REFERENCES [dbo].[Organisation] ([Id])
);
GO
