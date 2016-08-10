CREATE TABLE [dbo].[UkviImmediateReportType] (
	[Id] INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,
	[Code] VARCHAR(10) UNIQUE NOT NULL,
	[Name] VARCHAR(150) NOT NULL,
	[TemplateName] VARCHAR(150) NOT NULL,
	[IsActive] BIT DEFAULT 0 NOT NULL
);
GO

CREATE NONCLUSTERED INDEX [IX_UkviImmediateReportType_Code] ON [dbo].[UkviImmediateReportType]([Code] ASC) WITH (FILLFACTOR = 90);
GO
