CREATE TABLE [dbo].[TestLocation] (
	[Id] INT IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[Name] VARCHAR (100) NOT NULL,
	[CountryId] INT NOT NULL,
	[AdminUnitId] INT NOT NULL UNIQUE,
	[IsActive] BIT DEFAULT 0 NOT NULL, 
	[AddressLine1] [nvarchar](250) NOT NULL,
	[AddressLine2] [nvarchar](150) NULL,
	[Town] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NULL,
	[PostCode] [nvarchar](20) NULL,
	[GeoLat] [float] NULL,
	[GeoLng] [float] NULL,
	[LastUpdated] DATETIME NOT NULL
	CONSTRAINT [FK_TestLocation_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]),
	CONSTRAINT [FK_TestLocation_AdminUnitId] FOREIGN KEY ([AdminUnitId]) REFERENCES [dbo].[AdminUnit] ([Id])
);

GO

