MERGE INTO [dbo].[NotificationEvent] AS Target
	USING (
	VALUES  
		('Raised Incident',1 ),
		('Accepted Incident',1 ),
		('Rejected Incident',1 ),
		('Add Worknote',1 ),
		('Send FYI',1 ),
		('New Action',1 ),
		('Edit Action',1 ),
		('Respond to Action',1 ),
		('Closed Incident',1 )		
	) AS Source (EventName, IsActive)

	ON (Target.EventName = Source.EventName)

	WHEN MATCHED THEN 
		UPDATE SET EventName = Source.EventName 

	-- insert new rows 
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT (EventName,IsActive) VALUES (EventName, IsActive)

	-- delete rows that are in the target but not the source 
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;