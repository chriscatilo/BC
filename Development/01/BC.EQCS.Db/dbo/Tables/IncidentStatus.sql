CREATE TABLE [dbo].[IncidentStatus]
(
	[Id] TINYINT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [StatusName] VARCHAR(50) NOT NULL, 
    [Code] VARCHAR(20) NULL
)
