CREATE TABLE [stage].[UkviTestVenues] (
    [Guid] UNIQUEIDENTIFIER NOT NULL,
	[CentreNumber] CHAR (5) NOT NULL,
	[VenueDbId] INT NOT NULL,
	[VenueUbiquitousCode] VARCHAR (50) NOT NULL,
	[VenueName] VARCHAR (255) NOT NULL, 
	[Country] CHAR (2) NOT NULL,
	[AddressLine1] [nvarchar](250) NOT NULL,
	[AddressLine2] [nvarchar](150) NULL,
	[Town] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NULL,
	[PostCode] [nvarchar](20) NULL
	PRIMARY KEY ([Guid])
)

