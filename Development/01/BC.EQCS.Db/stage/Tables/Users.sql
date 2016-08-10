CREATE TABLE [stage].[Users] (
    [ObjectGUID] UNIQUEIDENTIFIER NOT NULL,
	[FirstName] VARCHAR (255) NOT NULL,
	[Surname] VARCHAR (255) NOT NULL,
	[Email] VARCHAR (255) NOT NULL,
	[Login] VARCHAR (255) NOT NULL, 
	[ExternalUserName]              [NVARCHAR](MAX) NULL,
	[ExternalUserPasswordHash]		[NVARCHAR](MAX) NULL,
	[ExternalUserSecurityStamp]		[NVARCHAR](MAX) NULL,
	[Discriminator]					[NVARCHAR](128) NULL,
    [Role] VARCHAR (25) NOT NULL,
	[AdminUnit] VARCHAR (255) NOT NULL
    CONSTRAINT PK_Users PRIMARY KEY ([ObjectGUID], [Role], [AdminUnit])
	CONSTRAINT IX1_Users UNIQUE ([Login], [Role], [AdminUnit])
);