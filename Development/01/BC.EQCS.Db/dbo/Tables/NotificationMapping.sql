CREATE TABLE [dbo].[NotificationMapping] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [RoleId]            INT NOT NULL,
    [MessageTemplateId] INT NOT NULL,
    [RaisedByRoleId]    INT NULL,
    CONSTRAINT [PK_NotificationMapping] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_Notification_MessageTemplateId] FOREIGN KEY ([MessageTemplateId]) REFERENCES [dbo].[NotificationMessageTemplate] ([Id]),
    CONSTRAINT [fk_Notification_RaisedByRoleId] FOREIGN KEY ([RaisedByRoleId]) REFERENCES [dbo].[ApplicationRole] ([Id]),
    CONSTRAINT [fk_Notification_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[ApplicationRole] ([Id])
);









