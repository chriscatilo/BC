CREATE TABLE [dbo].[NotificationEvent] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [EventName] VARCHAR (255) NULL,
    [IsActive]  BIT           NOT NULL,
    CONSTRAINT [PK_NotificationEvent] PRIMARY KEY CLUSTERED ([Id] ASC)
);

