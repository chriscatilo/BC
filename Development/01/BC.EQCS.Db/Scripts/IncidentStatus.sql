SET IDENTITY_INSERT [dbo].[IncidentStatus] ON 

GO
INSERT [dbo].[IncidentStatus] ([Id], [StatusName], [Code]) VALUES (1, N'Draft', 'DRAFT')
GO
INSERT [dbo].[IncidentStatus] ([Id], [StatusName], [Code]) VALUES (2, N'Submitted', 'SUBMITTED')
GO
INSERT [dbo].[IncidentStatus] ([Id], [StatusName], [Code]) VALUES (3, N'In Progress', 'IN_PROGRESS')
GO
INSERT [dbo].[IncidentStatus] ([Id], [StatusName], [Code]) VALUES (4, N'Rejected', 'REJECTED')
GO
INSERT [dbo].[IncidentStatus] ([Id], [StatusName], [Code]) VALUES (5, N'Closed', 'CLOSED')
GO
SET IDENTITY_INSERT [dbo].[IncidentStatus] OFF
GO