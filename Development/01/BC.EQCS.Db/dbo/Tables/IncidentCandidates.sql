CREATE TABLE [dbo].[IncidentCandidates] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [IncidentId]       INT           NOT NULL,
    [Number]           VARCHAR (50)  NULL,
    [Surname]          VARCHAR (255) NULL,
    [Firstnames]       VARCHAR (255) NULL,
    [Address]          VARCHAR (MAX) NULL,
    [DateOfBirth]      DATE          NULL,
    [Gender]           VARCHAR (10)  NULL,
    [IdDocumentNumber] VARCHAR (50)  NULL,
    [TrfNumber]        VARCHAR (50)  NULL,
    [DateTrfCancelled] DATE          NULL,
    [NationalityId]  INT           NULL,
    [UKVIRefNumber]    VARCHAR (50)  NULL,
	[RowVersion] TIMESTAMP NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IncidentCandidate_IncidentId] FOREIGN KEY ([IncidentId]) REFERENCES [dbo].[Incident] ([Id]),
    CONSTRAINT [FK_IncidentCandidate_OriginCountryId] FOREIGN KEY ([NationalityId]) REFERENCES [dbo].[Country] ([Id])
);

