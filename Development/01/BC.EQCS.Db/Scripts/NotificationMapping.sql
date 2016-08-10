IF NOT EXISTS (SELECT * FROM dbo.NotificationMapping)
BEGIN
	insert into dbo.NotificationMapping (RoleId,MessageTemplateId,RaisedByRoleId)
----Raised Incident
		--notification for raised incident raised by test centre staff going to rmt
		select Role.Id, MessageTemplate.Id, RaisedBy.Id as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - incident raised - <incidentno>'
		full outer join dbo.ApplicationRole as RaisedBy on RaisedBy.Code = 'TCS'
		where Role.Code = 'RMT' union all
		--notification for raised incident raised by test centre staff going to country compliance team
		select Role.Id, MessageTemplate.Id,RaisedBy.Id as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - incident raised - <incidentno>'
		full outer join dbo.ApplicationRole as RaisedBy on RaisedBy.Code = 'TCS'
		where Role.Code = 'CCT' union all
		--notification for raised incident raised by Gops going to Test centre staff
		select Role.Id, MessageTemplate.Id,RaisedBy.Id as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - incident raised - <incidentno>'
		full outer join dbo.ApplicationRole as RaisedBy on RaisedBy.Code = 'GLOBAL_OPERATIONS'
		where Role.Code = 'TCS' union all
		--notification for raised incident raised by RMT going to Test centre staff
		select Role.Id, MessageTemplate.Id,RaisedBy.Id as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - incident raised - <incidentno>'
		full outer join dbo.ApplicationRole as RaisedBy on RaisedBy.Code = 'RMT'
		where Role.Code = 'TCS' union all
		--notification for raised incident raised by Country Compliance Team going to Test centre staff
		select Role.Id, MessageTemplate.Id,RaisedBy.Id as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - incident raised - <incidentno>'
		full outer join dbo.ApplicationRole as RaisedBy on RaisedBy.Code = 'CCT'
		where Role.Code = 'TCS' union all
		--notification for raised incident raised by Verifications Team going to Test centre staff
		select Role.Id, MessageTemplate.Id,RaisedBy.Id as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - incident raised - <incidentno>'
		full outer join dbo.ApplicationRole as RaisedBy on RaisedBy.Code = 'VT'
		where Role.Code = 'TCS' union all
--Rejected Incident
		--notification for rejected incident going to Test centre staff
		select Role.Id, MessageTemplate.Id, null as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - incident rejected - <incidentno>'		
		where Role.Code = 'TCS' union all
--Add Work Note
		--notification for add work note going to Gops
		select Role.Id, MessageTemplate.Id, null as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - worknote added - <incidentno>'		
		where Role.Code = 'GLOBAL_OPERATIONS' union all
		--notification for add work note going to RMT
		select Role.Id, MessageTemplate.Id, null as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - worknote added - <incidentno>'		
		where Role.Code = 'RMT' union all
		--notification for add work note going to country compliance team
		select Role.Id, MessageTemplate.Id, null as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - worknote added - <incidentno>'		
		where Role.Code = 'CCT' union all
		--notification for add work note going to verifications team
		select Role.Id, MessageTemplate.Id, null as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - worknote added - <incidentno>'		
		where Role.Code = 'VT' union all
----Send FYI
--		--notification for FYI going to single user
		select Role.Id, MessageTemplate.Id, null as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<TCNumber> - FYI Sent - <INCIDENTNO>'			
		where Role.Code = 'GLOBAL_OPERATIONS' union all
		--notification for add work note going to RMT
		select Role.Id, MessageTemplate.Id, null as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<TCNumber> - FYI Sent - <INCIDENTNO>'			
		where Role.Code = 'RMT' union all
		--notification for add work note going to country compliance team
		select Role.Id, MessageTemplate.Id, null as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<TCNumber> - FYI Sent - <INCIDENTNO>'		
		where Role.Code = 'CCT' union all
		--notification for add work note going to verifications team
		select Role.Id, MessageTemplate.Id, null as RaisedByRoleId
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<TCNumber> - FYI Sent - <INCIDENTNO>'			
		where Role.Code = 'VT' union all
----Accepted Incident
		--notification for accepted incident going to Test centre staff
		select Role.Id, MessageTemplate.Id, null
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - incident accepted - <incidentno>'		
		where Role.Code = 'TCS' union all
----Closed Incident
		--notification for closed incident going to Test centre staff
		select Role.Id, MessageTemplate.Id, null
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - incident closed - <incidentno>'		
		where Role.Code = 'TCS' union all
----Closed Incident Action
		--notification for closed incident GOPs
		select Role.Id, MessageTemplate.Id, null
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - action closed - <incidentno>'		
		where Role.Code = 'GLOBAL_OPERATIONS' union all
		--notification for closed incident RMT
		select Role.Id, MessageTemplate.Id, null
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - action closed - <incidentno>'		
		where Role.Code = 'RMT' union all
		--notification for closed incident CCT
		select Role.Id, MessageTemplate.Id, null
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - action closed - <incidentno>'		
		where Role.Code = 'CCT' union all
		--notification for closed incident Verifications
		select Role.Id, MessageTemplate.Id, null
		from ApplicationRole as Role 
		full outer join dbo.NotificationMessageTemplate MessageTemplate on LOWER(MessageTemplate.SubjectLine) = '<tcnumber> - action closed - <incidentno>'		
		where Role.Code = 'VT'
END