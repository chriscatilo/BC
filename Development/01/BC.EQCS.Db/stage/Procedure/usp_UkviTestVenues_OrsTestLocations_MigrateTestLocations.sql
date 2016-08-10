CREATE PROCEDURE stage.usp_UkviTestVenues_OrsTestLocations_MigrateTestLocations AS
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
		PostCode nvarchar(20) NULL,
		SourceName VARCHAR(50),
		SourceGuid UNIQUEIDENTIFIER NULL
	)

	-- insert UKVI ORS test locations to holding table
	INSERT INTO @Source
		SELECT 
			UPPER('UKVI-'+ src.CentreNumber + '-' + CAST(src.VenueDbId AS VARCHAR)), 
			src.VenueUbiquitousCode,
			stage.CleanLocationName(VenueName, Town), 
			centre.AdminUnitId, 
			cty.Id as CountryId, 
			AddressLine1, 
			AddressLine2, 
			Town, 
			State, 
			PostCode,
			'SELT_ORS',
			[Guid]
		FROM stage.UkviTestVenues src
		LEFT JOIN dbo.Country cty
			ON cty.IsoCode = src.Country
		LEFT JOIN (
			SELECT au.id as AdminUnitId, au.Code
			FROM dbo.AdminUnit au
			JOIN dbo.AdminUnitType ty
				ON ty.id = au.TypeId
				AND ty.Code = 'TEST_CENTRE') centre
			ON centre.Code = src.CentreNumber
	
	-- insert SELTS ORS test locations to holding table
	INSERT INTO @Source
		SELECT 
			UPPER('IELTS-' + src.CentreNumber + '-' + CAST(VenueDbId AS VARCHAR)), 
			NULL,
			stage.CleanLocationName(VenueName, Town), 
			centre.AdminUnitId, 
			cty.Id as CountryId, 
			AddressLine1, 
			AddressLine2, 
			Town, 
			State, 
			PostCode,
			'IELTS_REG',
			[Guid]
		FROM stage.OrsTestLocations src
		LEFT JOIN dbo.Country cty
			ON cty.IsoCode = src.Country
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

	-- add, update or deactivate test locations as admin unit
	MERGE dbo.AdminUnit AS target
		USING @Source AS source
		ON (target.SourceName = source.SourceName AND target.SourceGuid = source.SourceGuid)
		WHEN MATCHED AND 
		(
			target.Code != source.UniqueCode OR 
			target.Name != source.Name OR 
			target.ParentId != source.CentreAdminUnitId OR 
			Target.UbiquitousCode != source.UbiquitousCode OR
			target.IsActive = 0
		)
		THEN
			UPDATE SET
				Code = source.UniqueCode, 
				Name = source.Name, 
				ParentId = source.CentreAdminUnitId,
				UbiquitousCode = source.UbiquitousCode,
				IsActive = 1,
				LastUpdated = @now
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT (Code, Name, Description, TypeId, IsActive, ParentId, LastUpdated, UbiquitousCode, SourceName, SourceGuid) 
			VALUES (UniqueCode, Name, null, @TypeIdTestLocation, 1, CentreAdminUnitId, @now, UbiquitousCode, SourceName, SourceGuid)
		
		WHEN NOT MATCHED BY SOURCE AND
			Target.TypeId = @TypeIdTestLocation
		THEN
			UPDATE SET IsActive = 0;

	-- add, update or deactivate test locations with country attribute
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
				PostCode = Source.PostCode, 
				IsActive = 1

		WHEN NOT MATCHED BY TARGET THEN 
			INSERT (Name, IsActive, CountryId, AdminUnitId, LastUpdated,AddressLine1,AddressLine2,Town,State,PostCode) 
			VALUES (Name, 1, CountryId, AdminUnitId, @now,AddressLine1,AddressLine2,Town,State,PostCode)
		
		WHEN NOT MATCHED BY SOURCE THEN
			UPDATE SET IsActive = 0
	;
	
END

