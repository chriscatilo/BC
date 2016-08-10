CREATE TABLE [dbo].[IncidentActions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ActionDescription] NVARCHAR(MAX) NOT NULL, 
    [ActionResponse] NVARCHAR(MAX) NULL, 
    [IncidentId] INT NOT NULL,     
    [Status] TINYINT NOT NULL DEFAULT 1, 
    [AssignedOn] DATETIME NOT NULL,     
    [AssignedById] INT NOT NULL, 
	[AssignedToTestCentre] Bit NOT NULL,    
	[RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [FK_IncidentActions_Incidents] FOREIGN KEY ([IncidentId]) REFERENCES [dbo].[Incident]([Id]) On Delete Cascade,    
    CONSTRAINT [FK_IncidentActions_ToApplicationUser_AssignedBy] FOREIGN KEY ([AssignedByID]) REFERENCES [ApplicationUser]([Id])    
)
