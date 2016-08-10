MERGE INTO [dbo].[Product] AS Target
	USING (
	VALUES  
		('IELTS','IELTS',1, 0),	
		('UKVI','IELTS UKVI',1, 1),	
		('IELTSLSA1','LS A1',1, 1),	
		('IELTSLSB2','LS B1',1, 1)
	) AS Source (Code, Name, IsActive, IsUkvi)

	ON (Target.Code = Source.Code)

	WHEN MATCHED THEN 
		UPDATE SET Name = Source.Name 

	-- insert new rows 
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT (Code, Name,IsActive, IsUkvi) VALUES (Code, Name, IsActive, IsUkvi)

	-- delete rows that are in the target but not the source 
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;