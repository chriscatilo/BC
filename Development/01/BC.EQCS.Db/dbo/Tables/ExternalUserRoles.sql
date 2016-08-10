CREATE TABLE [dbo].[ExternalUserRoles](
	[UserId] int NOT NULL,
	[RoleId] int NOT NULL,

CONSTRAINT [FK_dbo.ExternalUserRoles_dbo.ExternalRoles_RoleId] FOREIGN KEY([RoleId]) REFERENCES [dbo].[ExternalRoles] ([Id]),
CONSTRAINT [FK_dbo.ExternalUserRoles_dbo.ApplicationUser_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[ApplicationUser] ([Id]),
CONSTRAINT [PK_dbo.ExternalUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]