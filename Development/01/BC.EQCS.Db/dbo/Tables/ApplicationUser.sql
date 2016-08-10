CREATE TABLE [dbo].[ApplicationUser] (
    [Id]                             INT              IDENTITY (1, 1) NOT NULL,
    [ObjectGUID]                     UNIQUEIDENTIFIER NOT NULL,
    [DisplayName]                    VARCHAR(100)      NOT NULL,
    [Department]                     VARCHAR (128)    NULL,
    [FirstName]                      VARCHAR (64)     NOT NULL,
    [Surname]                        VARCHAR (64)     NOT NULL,
    [Email]                          VARCHAR (128)    NOT NULL,
    [Login]                          VARCHAR (128)    NOT NULL UNIQUE,

	[ExternalUserName]              [NVARCHAR](MAX) NULL,
	[ExternalUserPasswordHash]		[NVARCHAR](MAX) NULL,
	[ExternalUserSecurityStamp]		[NVARCHAR](MAX) NULL,
	[Discriminator]					[NVARCHAR](128) NULL,

    [JobTitle]                       VARCHAR (128)    NULL,
    [Telephone]                      VARCHAR (128)    NULL,
    [Country]                        VARCHAR (128)    NULL,
    [DefaultCountryId]               INT              NULL,
    [Enabled]                        BIT              DEFAULT ((0)) NOT NULL,
    [ApplicationCountryDepartmentId] INT              NULL,
    [SelectedCountryId]              INT              NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationUser_To_Country] FOREIGN KEY ([DefaultCountryId]) REFERENCES [dbo].[Country] ([Id])
);



GO

CREATE UNIQUE INDEX [IX_ApplicationUser_ObjectGUID_Unique] ON [dbo].[ApplicationUser] ([ObjectGUID]) 
