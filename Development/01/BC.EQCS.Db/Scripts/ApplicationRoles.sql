MERGE INTO [dbo].[ApplicationRole] AS Target
	USING (
	VALUES 
		( 'Global Operations Team', 'GLOBAL_OPERATIONS'),
		( 'Regional Management Team', 'RMT'),
		( 'Country Compliance Team',  'CCT'),
		( 'Verifications Team', 'VT'),
		( 'Test Centre Staff', 'TCS'),		
		( 'External Test Centre Staff', 'TCS_EXTERNAL'),	
		( 'Internal Auditor', 'AUDITOR_INTERNAL'),
		( 'External Auditor', 'AUDITOR_EXTERNAL'),
		( 'System Monitor', 'SYSTEM_MONITOR')
		) AS Source (Name, Code)

	ON (Target.Code = Source.Code)

	WHEN MATCHED THEN 
		UPDATE SET Name = Source.Name 

	-- insert new rows 
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT (Name, Code)
		VALUES (Name, Code)

	-- delete rows that are in the target but not the source 
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;