SET IDENTITY_INSERT [dbo].[IncidentActionStatus] ON 
INSERT [dbo].[IncidentActionStatus] ([Id], [StatusName]) VALUES (1, N'InProgress')
GO
INSERT [dbo].[IncidentActionStatus] ([Id], [StatusName]) VALUES (2, N'Closed')
GO
SET IDENTITY_INSERT [dbo].[IncidentActionStatus] OFF
GO