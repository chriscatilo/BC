CREATE TABLE [dbo].[IncidentActivityLog]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IncidentId] INT NOT NULL, 
    [DateTimeOfActivity] DATETIME2 NOT NULL, 
    [ApplicationUserId] INT NOT NULL, 
    [LogType] INT NOT NULL, 
    [Payload] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_IncidentActivityLog_To_Incident] FOREIGN KEY ([IncidentId]) REFERENCES [dbo].[Incident]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_IncidentActivityLog_To_ApplicationUser] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[ApplicationUser]([Id])
)
