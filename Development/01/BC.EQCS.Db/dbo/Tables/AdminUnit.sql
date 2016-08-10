CREATE TABLE [dbo].[AdminUnit] (
	[Id] INT IDENTITY (1, 1) PRIMARY KEY CLUSTERED ([Id] ASC),
	[Code] VARCHAR (50) NOT NULL UNIQUE,
	[Name] VARCHAR (255) NOT NULL,
	[Description] VARCHAR (255) NULL,
	[UbiquitousCode] VARCHAR (50) NULL,
	[TypeId] INT NOT NULL,
	[IsActive] BIT NOT NULL,
	[ParentId] INT NULL,
	[SourceName] VARCHAR(50) NULL,
	[SourceGuid] UNIQUEIDENTIFIER NULL,
	[LastUpdated] DATETIME NOT NULL,
	CONSTRAINT FK_AdminUnit_ParentId FOREIGN KEY ([ParentId]) REFERENCES [dbo].[AdminUnit](Id),
	CONSTRAINT FK_AdminUnit_Type FOREIGN KEY ([TypeId]) REFERENCES [dbo].[AdminUnitType] ([Id])
);


