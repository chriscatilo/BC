﻿CREATE TABLE [dbo].[IncidentClassType] (
	[Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY CLUSTERED,
	[Code] VARCHAR (15) NOT NULL UNIQUE,
	[Name] VARCHAR (255) NOT NULL
);

