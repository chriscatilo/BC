

/*


Replaced by usp_Populate_Org_Regions_Countries_Centres_Locations_AdminUnit.sql
*/


MERGE INTO [dbo].[AdminUnitType] AS Target
	USING (
	VALUES 
		('ROOT', 'Root'),
		('IELTS_REGION', 'IELTS Region'),
		('IELTS_SUB_REGION', 'IELTS Sub Region'),
		('TEST_CENTRE', 'Test Centre'),
		('TEST_LOCATION', 'Test Location')
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

