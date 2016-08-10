CREATE TYPE stage.IncidentClasses
AS TABLE (
	[Code] VARCHAR (7) PRIMARY KEY NOT NULL,
	[Name] VARCHAR (255) NOT NULL,
	[ParentId] VARCHAR (10) NOT NULL,
	[RiskRating] VARCHAR(4) NULL,
	[ResidualRiskRating] VARCHAR(4) NULL,
	[UkviImmediateReportTypeId] INT NULL
)
GO