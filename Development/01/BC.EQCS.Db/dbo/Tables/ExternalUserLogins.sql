CREATE TABLE [dbo].[ExternalUserLogins](
	[UserId] int NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	CONSTRAINT [FK_dbo.ExternalUserLogins_dbo.ApplicationUser_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[ApplicationUser] ([Id]),
 CONSTRAINT [PK_dbo.ExternalUserLogins] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

