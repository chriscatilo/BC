CREATE TABLE [dbo].[UserRoleToIncidentClassPurpose]
(
	[Id] INT  IDENTITY (1, 1) NOT NULL,
    [Code] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(255) NOT NULL,
	CONSTRAINT [PK_UserRoleToIncidentClassPurpose] PRIMARY KEY CLUSTERED ([Id] ASC)
)
