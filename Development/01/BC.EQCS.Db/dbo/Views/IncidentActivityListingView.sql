CREATE VIEW [dbo].[IncidentActivityListingView]
AS
SELECT        dbo.IncidentActivityLog.Id, dbo.IncidentActivityLog.IncidentId, dbo.IncidentActivityLog.DateTimeOfActivity, dbo.IncidentActivityLog.ApplicationUserId, 
                         dbo.IncidentActivityLog.LogType, dbo.IncidentActivityLog.Payload, dbo.ApplicationUser.DisplayName AS UserDisplayName, 
                         dbo.ApplicationUser.ObjectGUID AS ApplicationUserGuid
FROM            dbo.IncidentActivityLog LEFT OUTER JOIN
                         dbo.ApplicationUser ON dbo.IncidentActivityLog.ApplicationUserId = dbo.ApplicationUser.Id

GO