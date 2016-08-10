MERGE INTO [dbo].[ResidualRiskRating] AS Target
	USING (
	VALUES  
		('LOW','Low'),
		('MED','Medium'),
		('HIGH','High')
	) AS Source (Code, Name)

	ON (Target.Code = Source.Code)

	WHEN MATCHED THEN 
		UPDATE SET 
			Name = Source.Name,
			IsActive = 1

	-- insert new rows 
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT (Code, Name, IsActive) VALUES (Code, Name, 1)

	-- delete rows that are in the target but not the source 
	WHEN NOT MATCHED BY SOURCE THEN 
		UPDATE SET
			IsActive = 0;