CREATE TABLE [dbo].[IncidentClassDefault] (
    [IncidentClassId]       INT           PRIMARY KEY NOT NULL,
    [IsActive]              BIT           NOT NULL,
    [RiskRatingId]          INT           NULL,
    [ResidualRiskRatingId]  INT           NULL, 
	[UkviImmediateReportTypeId]			  INT NULL,
	[LastUpdated]			DATETIME	  NOT NULL,
    CONSTRAINT [FK_IncidentClassDefault_IncidentClass] FOREIGN KEY ([IncidentClassId]) REFERENCES [dbo].[IncidentClass] ([Id]),
    CONSTRAINT [FK_IncidentClassDefault_RiskRating] FOREIGN KEY ([RiskRatingId]) REFERENCES [dbo].[RiskRating] ([Id]),
    CONSTRAINT [FK_IncidentClassDefault_ResidualRiskRating] FOREIGN KEY ([ResidualRiskRatingId]) REFERENCES [dbo].[ResidualRiskRating] ([Id]),
	CONSTRAINT [FK_IncidentClassDefault_UkviImmediateReportTypeId] FOREIGN KEY ([UkviImmediateReportTypeId]) REFERENCES [dbo].[UkviImmediateReportType] ([Id])
);





