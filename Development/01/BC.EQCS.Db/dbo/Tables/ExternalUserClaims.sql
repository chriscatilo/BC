
CREATE TABLE [dbo].[ExternalUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.ApplicationtUsers_Id] FOREIGN KEY([UserId]) REFERENCES [dbo].[ApplicationUser] ([Id]),
 CONSTRAINT [PK_dbo.ExternalUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


