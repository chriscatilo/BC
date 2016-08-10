CREATE TABLE [dbo].[IncidentClass] (
	[Id] INT IDENTITY (1, 1) PRIMARY KEY CLUSTERED ([Id] ASC),
	[Code] VARCHAR (7) NOT NULL UNIQUE,
	[Name] VARCHAR (255) NOT NULL,
	[TypeId] INT NOT NULL,
	[IsActive] BIT NOT NULL,
	[ParentId] INT NULL,
	[LastUpdated] DATETIME NOT NULL,
	CONSTRAINT FK_IncidentClass_ParentId FOREIGN KEY ([ParentId]) REFERENCES [dbo].[IncidentClass](Id),
	CONSTRAINT FK_IncidentClass_Type FOREIGN KEY ([TypeId]) REFERENCES [dbo].[IncidentClassType] ([Id])
);
