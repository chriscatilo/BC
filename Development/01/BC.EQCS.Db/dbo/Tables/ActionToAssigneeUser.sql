CREATE TABLE [dbo].[ActionToAssigneeUser]
(
	[IncidentActionId] INT NOT NULL , 
    [ApplicationUserId] INT NOT NULL, 
    PRIMARY KEY ([IncidentActionId], [ApplicationUserId]), 
    CONSTRAINT [FK_ActionToAssigneeUser_ToAction] FOREIGN KEY ([IncidentActionId]) REFERENCES [IncidentActions]([Id]) On Delete Cascade, 
    CONSTRAINT [FK_ActionToAssigneeUser_ToApplicationUser] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser]([Id])
)
