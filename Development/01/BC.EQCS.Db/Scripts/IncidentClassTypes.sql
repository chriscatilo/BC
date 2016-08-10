MERGE INTO [dbo].[IncidentClassType] AS Target
	USING (
	VALUES 
		('Root', 'Root'),
		('IncidentType', 'Incident Category Type'),
		('Category', 'Incident Category'),
		('SubCategory', 'Incident Sub Category')
	) AS Source ([Code],[Name])

	ON (Target.[Code] = Source.[Code])

	WHEN MATCHED THEN 
		UPDATE SET Name = Source.Name 

	-- insert new rows 
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([Code],[Name]) VALUES ([Code],[Name])

	-- delete rows that are in the target but not the source 
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;