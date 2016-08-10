CREATE TABLE [dbo].[NotificationMessageTemplate] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [BodyText]             VARCHAR (MAX) NULL,
    [SubjectLine]          VARCHAR (500) NULL,
    [EventId]              INT           NULL,
    [AssignedToTestCentre] BIT           NULL,
    CONSTRAINT [PK_NoticationMessageTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_NotificationMessageTemplate_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[NotificationEvent] ([Id])
);





