CREATE TABLE [dbo].[ApplicationRole] (
	 [Id] INT IDENTITY (1, 1) NOT NULL,
	 [Code] VARCHAR (25) NULL,
	 [Name] NVARCHAR (100) NOT NULL,
	 [Description] NVARCHAR (255) NULL,
	 [DataAuthorisation] BIT DEFAULT ((0)) NOT NULL,
	 PRIMARY KEY CLUSTERED ([Id] ASC),
	 UNIQUE NONCLUSTERED ([Code] ASC)
);


