CREATE TABLE [dbo].[ApplicationRoleToAdminUnitType]
(
	[ApplicationRoleId] INT NOT NULL , 
    [AdminUnitTypeId] INT NOT NULL, 
    PRIMARY KEY ([AdminUnitTypeId], [ApplicationRoleId]),
	CONSTRAINT [FK__ApplicationRole__ApplicationRoleId] FOREIGN KEY ([ApplicationRoleId]) REFERENCES [dbo].[ApplicationRole] ([Id]),
	CONSTRAINT [FK__AdminUnitType__AdminUnitTypeId] FOREIGN KEY ([AdminUnitTypeId]) REFERENCES [dbo].[AdminUnitType] ([Id])
	
    
)
