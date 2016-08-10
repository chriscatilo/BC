CREATE TABLE [dbo].[DocumentStorage]
(
	[Id] INT IDENTITY (1, 1) PRIMARY KEY CLUSTERED ([Id] ASC),
	[ContentName] VARCHAR(255) NOT NULL,
    [ContentType] NVARCHAR(5) NOT NULL,
	[Content] VARBINARY (MAX) NOT NULL,
	[UploadedDate] DATETIME NOT NULL, 
    [UploadedBy] INT NULL, 
    [OwnerIdentifier] INT NULL, 
    [OwnerType] VARCHAR(50) NULL, 
)