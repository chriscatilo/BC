CREATE TABLE [dbo].[UserRoleToIncidentClass] (
    [ApplicationRoleId] INT NOT NULL,
    [IncidentClassId] INT NOT NULL,
    [RoleToClassPurposeId] INT NOT NULL, 
    CONSTRAINT [FK_RoleToIncidentClass_IncidentClass] FOREIGN KEY ([IncidentClassId]) REFERENCES [dbo].[IncidentClass] ([Id]),
    CONSTRAINT [FK_RoleToIncidentClass_ApplicationRole] FOREIGN KEY ([ApplicationRoleId]) REFERENCES [dbo].[ApplicationRole] ([Id]),
    CONSTRAINT [FK_RoleToIncidentClass_UserRoleToIncidentClassPurpose] FOREIGN KEY ([RoleToClassPurposeId]) REFERENCES [dbo].[UserRoleToIncidentClassPurpose] ([Id]),
    PRIMARY KEY ([ApplicationRoleId], [IncidentClassId], [RoleToClassPurposeId])
);