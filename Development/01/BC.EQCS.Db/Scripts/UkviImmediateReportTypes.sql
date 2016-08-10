MERGE INTO dbo.UkviImmediateReportType AS Target
	USING (
	VALUES 
		('SLF', 'Service Level Failure', 'ServiceLevelFailure', 1),
		('NCT', 'Notification of Compromised Test','CompromisedTestNotification', 1)
	) AS Source (Code, Name, TemplateName, IsActive)

	ON (Target.Code = Source.Code)

	WHEN MATCHED THEN 
		UPDATE SET 
			Name = Source.Name, 	
			TemplateName = Source.TemplateName,
			IsActive = Source.IsActive

	-- insert new rows 
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT (Code, Name, TemplateName, IsActive) 
		VALUES (Code, Name, TemplateName, IsActive)

	-- delete rows that are in the target but not the source 
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;