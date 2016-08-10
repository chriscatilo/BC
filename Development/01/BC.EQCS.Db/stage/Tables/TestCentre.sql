CREATE TABLE [stage].[TestCentre] (
	[CentreNumber] VARCHAR (10) NOT NULL PRIMARY KEY,
	[Organisation] VARCHAR (255) NOT NULL,
	[Name] VARCHAR (500) NOT NULL,
	[SubRegion] VARCHAR (255) NOT NULL,
	[Region] VARCHAR (255) NOT NULL, 
    [Address] VARCHAR(MAX) NOT NULL,
	[Administrator] NVARCHAR(100) NULL,
	[PrimaryEmail] VARCHAR(100) NULL,
	[SecondaryEmail] VARCHAR(100) NULL,
);
