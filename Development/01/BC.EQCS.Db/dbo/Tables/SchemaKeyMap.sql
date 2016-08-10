CREATE TABLE [dbo].[SchemaKeyMap] (
	[ApplicationRoleId] INT NOT NULL,
	[IncidentClassId] INT NOT NULL,
	[SchemaKey] VARCHAR (50) NOT NULL,
	[MapIncludesChildren] BIT NOT NULL,
	CONSTRAINT PK_SchemaKey PRIMARY KEY ([ApplicationRoleId], [IncidentClassId]),
	CONSTRAINT FK_SchemaKey_ApplicationRoleId FOREIGN KEY ([ApplicationRoleId]) REFERENCES [dbo].[ApplicationRole](Id),
	CONSTRAINT FK_SchemaKey_IncidentClassId FOREIGN KEY ([IncidentClassId]) REFERENCES [dbo].[IncidentClass] ([Id])
);
