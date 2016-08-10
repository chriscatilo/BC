
MERGE INTO [dbo].[UserRoleToIncidentClassPurpose] AS Target
	USING (
	VALUES 
		( N'IncR', N'Incident Raise'),
		( N'IncV', N'Incident View'),
		( N'IncReadOnly', N'Incident Read Only')
		) AS Source ([Code], [Description])

	ON (Target.[Code] = Source.[Code])

	WHEN MATCHED THEN 
		UPDATE SET [Description] = Source.[Description]

	-- insert new rows 
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([Description], [Code])
		VALUES ([Description], [Code])

	-- delete rows that are in the target but not the source 
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;