IF NOT EXISTS (SELECT * FROM dbo.NotificationMessageTemplate)
BEGIN
	insert into dbo.NotificationMessageTemplate (BodyText, SubjectLine, EventId, AssignedToTestCentre)	
	select 'A new worknote has been added to Test Centre <TCNumber> - <Name> incident <INCIDENTNO>','<TCNumber> - Worknote Added - <INCIDENTNO>' , Event.Id , null
	from dbo.NotificationEvent Event where LOWER(Event.EventName) = 'add worknote' UNION ALL
	select 'An FYI has been sent to you for  incident <INCIDENTNO> at Test Centre <TCNumber> - <TCName>. <OptionalText>','<TCNumber> - FYI Sent - <INCIDENTNO>', Event.Id , null
	from dbo.NotificationEvent Event where LOWER(Event.EventName) = 'send fyi' UNION ALL
	select 'Incident <INCIDENTNO> at Test Centre <TCNumber> - <Name> has been accepted.','<TCNumber> - Incident Accepted - <INCIDENTNO>', Event.Id, null
	from dbo.NotificationEvent Event where LOWER(Event.EventName) = 'accepted incident' UNION ALL
	select 'Test Centre <TCNumber> - <Name> incident <INCIDENTNO> was closed on <date> by <user> <role>.','<TCNumber> - Incident Closed - <INCIDENTNO>', Event.Id , null
	from dbo.NotificationEvent Event where LOWER(Event.EventName) = 'closed incident' UNION ALL
	select 'The incident <INCIDENTNO> at Test Centre <TCNumber> - <Name> has been rejected for the following reason. <RejectionText>.','<TCNumber> - Incident Rejected - <INCIDENTNO>' , Event.Id , null
	from dbo.NotificationEvent Event where LOWER(Event.EventName) = 'rejected incident' UNION ALL
	select 'A new incident <INCIDENTNO> has been raised for venue <TLName> attached  to Test Centre <TCNumber> - <Name>. Please access EQCS to view this incident.','<TCNumber> - Incident Raised - <INCIDENTNO>', Event.Id , null
	from dbo.NotificationEvent Event where LOWER(Event.EventName) = 'raised incident' UNION ALL
	select 'The action <action> has been closed for Test Centre <TCNumber> - <Name>  incident <INCIDENTNO>. <Response>','<TCNumber> - Action Closed - <INCIDENTNO>' , Event.Id , null
	from dbo.NotificationEvent Event where LOWER(Event.EventName) = 'respond to action' UNION ALL
	select 'The action <ACTDESC> has been updated for Test Centre <TCNumber> - <Name>  incident <INCIDENTNO>.','<TCNumber> - Action Updated - <INCIDENTNO>' , Event.Id , null
	from dbo.NotificationEvent Event where LOWER(Event.EventName) = 'edit action' UNION ALL	
	select 'The action <ACTDESC> has been assigned to Test Centre <TCNumber> - <Name>  incident <INCIDENTNO>.','<TCNumber> - Action Assigned - <INCIDENTNO>' , Event.Id , 1
	from dbo.NotificationEvent Event where LOWER(Event.EventName) = 'new action' UNION ALL
	select 'You have been assigned the action <ACTDESC> for Test Centre  <TCNumber> - <Name>  incident <INCIDENTNO>.','<TCNumber> - Action Assigned - <INCIDENTNO>' , Event.Id , 0
	from dbo.NotificationEvent Event where LOWER(Event.EventName) = 'new action' --UNION ALL
	



	
END