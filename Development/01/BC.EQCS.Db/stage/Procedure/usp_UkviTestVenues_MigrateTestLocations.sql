CREATE PROCEDURE stage.usp_UkviTestVenues_MigrateTestLocations AS
BEGIN	

	DECLARE @Source TABLE 
	(
		UniqueCode VARCHAR(255) PRIMARY KEY, 
		UbiquitousCode VARCHAR(255), 
		Name VARCHAR(255) NOT NULL, 
		CentreAdminUnitId INT  NULL, 
		CountryId INT NOT NULL, 
		AddressLine1 nvarchar(250) NOT NULL,
		AddressLine2 nvarchar(150) NULL,
		Town nvarchar(50) NOT NULL,
		State nvarchar(50) NULL,
		PostCode nvarchar(20) NULL
	)
	INSERT INTO @Source
		SELECT 
			UPPER(src.VenueUbiquitousCode + '-' + REPLACE(REPLACE(REPLACE(src.Town, ')', ''), '(', ''),' ', '-')), 
			src.VenueUbiquitousCode,
			src.VenueName, 
			centre.AdminUnitId, 
			cty.Id as CountryId, 
			AddressLine1, 
			AddressLine2, 
			Town, 
			State, 
			PostCode
		FROM stage.UkviTestVenues src
		LEFT JOIN dbo.Country cty
			ON cty.IsoCode = src.CountryCode
		LEFT JOIN (
			SELECT au.id as AdminUnitId, au.Code
			FROM dbo.AdminUnit au
			JOIN dbo.AdminUnitType ty
				ON ty.id = au.TypeId
				AND ty.Code = 'TEST_CENTRE') centre
			ON centre.Code = src.CentreNumber
	
	DECLARE @TypeIdTestLocation INT
	SELECT @TypeIdTestLocation = Id FROM AdminUnitType WHERE Code = 'TEST_LOCATION'
	
	DECLARE @now DATETIME = getdate()

	-- add test locations as admin unit
	MERGE dbo.AdminUnit AS target
		USING @Source AS source
		ON (target.Code = source.UniqueCode)
		WHEN MATCHED AND 
		(
			target.Name != source.Name OR 
			target.ParentId != source.CentreAdminUnitId OR 
			Target.UbiquitousCode != source.UbiquitousCode
		)
		THEN
			UPDATE SET 
				Name = source.Name, 
				ParentId = source.CentreAdminUnitId,
				UbiquitousCode = source.UbiquitousCode,
				LastUpdated = @now
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT (Code, Name, Description, TypeId, IsActive, ParentId, LastUpdated, UbiquitousCode) 
			VALUES (UniqueCode, Name, null, @TypeIdTestLocation, 1, CentreAdminUnitId, @now, UbiquitousCode)
		
		WHEN NOT MATCHED BY SOURCE AND
			Target.TypeId = @TypeIdTestLocation
		THEN
			UPDATE SET IsActive = 0;

	-- add test locations with country attribute
	MERGE dbo.TestLocation AS target
		USING (
			SELECT src.Name, au.Id as AdminUnitId, src.CountryId, AddressLine1, AddressLine2, Town, State, PostCode
			FROM @Source src
			JOIN AdminUnit au
				ON au.Code = src.UniqueCode
				AND au.TypeId =  @TypeIdTestLocation
		) AS source
		ON (target.AdminUnitId = source.AdminUnitId)

		WHEN MATCHED AND 
		(
			target.Name != source.Name OR 
			target.CountryId != source.CountryId OR
			target.AddressLine1 != source.AddressLine1 OR
			target.AddressLine2 != source.AddressLine2 OR
			target.Town != source.Town OR
			target.State != source.State OR
			target.PostCode != source.PostCode
		)
		THEN
			UPDATE SET 
				Name = source.Name, 
				CountryId = source.CountryId,
				LastUpdated = @now,
				AddressLine1 = Source.AddressLine1,
				AddressLine2 = Source.AddressLine2,
				Town = Source.Town,
				State = Source.State,
				PostCode = Source.PostCode

		WHEN NOT MATCHED BY TARGET THEN 
			INSERT (Name, IsActive, CountryId, AdminUnitId, LastUpdated,AddressLine1,AddressLine2,Town,State,PostCode) 
			VALUES (Name, 1, CountryId, AdminUnitId, @now,AddressLine1,AddressLine2,Town,State,PostCode)
		
		WHEN NOT MATCHED BY SOURCE THEN
			UPDATE SET IsActive = 0
	;
	
END

