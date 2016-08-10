CREATE TABLE [dbo].[UserToRoleToAdminUnit] (
    [ApplicationUserId] INT NOT NULL,
    [ApplicationRoleId] INT NOT NULL,
    [AdminUnitId] INT NOT NULL
    CONSTRAINT [FK_UserToRoleToAdminUnit_AdminUnit] FOREIGN KEY ([AdminUnitId]) REFERENCES [dbo].[AdminUnit] ([Id]),
    CONSTRAINT [FK_UserToRoleToAdminUnit_ApplicationRole] FOREIGN KEY ([ApplicationRoleId]) REFERENCES [dbo].[ApplicationRole] ([Id]),
    CONSTRAINT [FK_UserToRoleToAdminUnit_ApplicationUser] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[ApplicationUser] ([Id]),
	PRIMARY KEY ([ApplicationUserId], [ApplicationRoleId], [AdminUnitId])
);

