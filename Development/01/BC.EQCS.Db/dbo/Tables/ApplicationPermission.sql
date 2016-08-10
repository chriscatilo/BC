CREATE TABLE [dbo].[ApplicationPermission] (
    [ApplicationRoleId]  INT NOT NULL,
    [ApplicationAssetId] INT NOT NULL,
    CONSTRAINT [PK__ApplicationPermission] PRIMARY KEY NONCLUSTERED ([ApplicationRoleId] ASC, [ApplicationAssetId] ASC),
    CONSTRAINT [FK__ApplicationPermission__ApplicationAssetId] FOREIGN KEY ([ApplicationAssetId]) REFERENCES [dbo].[ApplicationAsset] ([Id]),
    CONSTRAINT [FK__ApplicationPermission__ApplicationRoleId] FOREIGN KEY ([ApplicationRoleId]) REFERENCES [dbo].[ApplicationRole] ([Id])
);

