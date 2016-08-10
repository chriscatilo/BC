MERGE INTO dbo.OrganisationType AS Target
	USING (
	VALUES 
		('EDU','Education',1 ),
		('EMP','Employer',1 ),
		('GOV','Government',1 ),
		('PRO','Professional Body',1 ),
		('NONRO','Non-RO',1 )
	) AS Source ( Code, Name, IsActive)

	ON (Target.Name = Source.Name)

	WHEN MATCHED THEN 
		UPDATE SET Name = Source.Name , IsActive = Source.IsActive

	-- insert new rows 
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT (Code,Name,IsActive) 
		VALUES (Code,Name,IsActive) 

	-- delete rows that are in the target but not the source 
	WHEN NOT MATCHED BY SOURCE THEN 
		UPDATE SET IsActive = 0;
