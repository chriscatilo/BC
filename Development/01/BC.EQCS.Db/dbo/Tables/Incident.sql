CREATE TABLE [dbo].[Incident] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [FormalId]                AS             ('INC'+right(replicate('0',(6))+CONVERT([varchar],[Id]),(6))) PERSISTED,
    [Status]                  INT            NOT NULL,
    [CreateDate]              DATETIME       NOT NULL,
    [RaisedDate]              DATETIME       NULL,
    [IncidentDate]            DATETIME       NULL,    
    [Description]             NVARCHAR (MAX) NULL,
    [ImmediateActionTaken]    NVARCHAR (MAX) NULL,
    [ProductId]               INT            NULL,
    [RaisedBy]                NVARCHAR (50)  NULL,
    [IncidentClassId]         INT            NULL,
    [RiskRatingId]            INT            NULL,
    [ResidualRiskRatingId]    INT            NULL,
    [LoggedById]              INT            NULL,
    [TestLocationId]          INT            NULL,
    [TestCentreId]            INT            NULL,
    [TestDate]                DATE           NULL,
    [ReferringOrgName]        VARCHAR (255)  NULL,
    [ReferringOrgSurname]     VARCHAR (255)  NULL,
    [ReferringOrgFirstnames]  VARCHAR (255)  NULL,
    [ReferringOrgJobTitle]    VARCHAR (255)  NULL,
    [ReferringOrgEmail]       VARCHAR (255)  NULL,
    [ReferringOrgCountryId]   INT            NULL,
    [ReferringOrgTypeId]      INT            NULL,
    [ReferringOrganisationId] INT            NULL,
    [IncidentTime]            VARCHAR (50)   NULL,
    [ReportUkvi]              BIT            NULL,
	[NumberOfCandidatesAffected] INT		 NULL,
	[UkviFollowUpDate]        DATE           NULL,
	[UkviImmediateReportTypeId]  INT         NULL,
	[RowVersion] TIMESTAMP NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Incident_IncidentClassId] FOREIGN KEY ([IncidentClassId]) REFERENCES [dbo].[IncidentClass] ([Id]),
    CONSTRAINT [FK_Incident_LoggedById] FOREIGN KEY ([LoggedById]) REFERENCES [dbo].[ApplicationUser] ([Id]),
    CONSTRAINT [FK_Incident_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]),
    CONSTRAINT [FK_Incident_ResidualRiskRatingId] FOREIGN KEY ([ResidualRiskRatingId]) REFERENCES [dbo].[ResidualRiskRating] ([Id]),
    CONSTRAINT [FK_Incident_RiskRatingId] FOREIGN KEY ([RiskRatingId]) REFERENCES [dbo].[RiskRating] ([Id]),
    CONSTRAINT [FK_Incident_TestCentreVenueId] FOREIGN KEY ([TestLocationId]) REFERENCES [dbo].[TestLocation] ([Id]),
    CONSTRAINT [FK_Incident_UkviImmediateReportTypeId] FOREIGN KEY ([UkviImmediateReportTypeId]) REFERENCES [dbo].[UkviImmediateReportType] ([Id])
);



























