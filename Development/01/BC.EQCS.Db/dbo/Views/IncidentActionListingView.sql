CREATE VIEW [dbo].[IncidentActionListingView]
	AS 
SELECT 
	  ia.[Id]
      ,ia.[IncidentId]
      ,CASE WHEN ia.AssignedToTestCentre = 1 THEN 'Test Centre' ELSE (SELECT au.DisplayName + ', '
		from [dbo].ApplicationUser au
		left outer join [dbo].ActionToAssigneeUser aau
		on au.Id = aau.ApplicationUserId 
		where aau.IncidentActionId = ia.Id and aau.IncidentActionId is not null FOR XML PATH(''))  END as [AssignedTo]
      ,auABy.DisplayName AS [AssignedBy]      
      ,ia.[AssignedOn]
      ,ia.[ActionDescription]
      ,ia.ActionResponse as [Comments]      
	  ,ia.Status AS [StatusId]
      ,ias.[StatusName] AS [Status]      
      ,ia.[AssignedById]
	  ,(SELECT CONVERT(varchar, Id) + ', ' From DocumentStorage ds WHERE ds.OwnerType = 'Action' AND ds.OwnerIdentifier = ia.Id FOR XML PATH(''))  AS [FileIds]      	  
	  ,(SELECT ContentName + ', ' From DocumentStorage ds WHERE ds.OwnerType = 'Action' AND ds.OwnerIdentifier = ia.Id FOR XML PATH(''))  AS [FileNames]      	  
  FROM [dbo].[IncidentActions] ia  
  Left outer join [dbo].ApplicationUser auABy
		ON ia.AssignedById = auABy.Id  
  Left outer join [dbo].IncidentActionStatus ias
		ON ia.Status = ias.Id
  Go