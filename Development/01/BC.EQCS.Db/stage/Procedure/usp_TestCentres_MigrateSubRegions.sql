CREATE PROCEDURE [stage].[usp_TestCentres_MigrateSubRegions] AS
BEGIN

	-- create source table of countries
	-- enforce unique country code
	-- enforce region exists
	DECLARE @Countries TABLE (
		[CountryCode] CHAR (2) PRIMARY KEY NOT NULL,
		[CountryName] VARCHAR (255) NOT NULL,
		[RegionAdminUnitId] VARCHAR (10) NOT NULL
	)
	INSERT INTO @Countries
		SELECT DISTINCT Country.IsoCode, Country.Name , region.RegionAdminUnitId 
		FROM [stage].[TestCentre] TC
		JOIN dbo.Country 
			ON TC.SubRegion = Country.Name
		LEFT JOIN (
			SELECT au.id as RegionAdminUnitId, au.Code
			FROM dbo.AdminUnit au
			JOIN dbo.AdminUnitType ty
			ON ty.id = au.TypeId
			AND ty.Code = 'IELTS_REGION') region
			ON region.Code = TC.Region

	DECLARE @TypeIdSubRegion INT
	SELECT @TypeIdSubRegion = Id FROM AdminUnitType WHERE Code = 'IELTS_SUB_REGION'
	
	DECLARE @now DATETIME = getdate()

	---- add countries as sub region admin units
	MERGE dbo.AdminUnit AS target
		USING @Countries AS source
		ON (target.Code = source.CountryCode AND target.TypeId = @TypeIdSubRegion)

		-- update sub region admin units when it exists in dbo.AdminUnit
		WHEN MATCHED 
			AND (Name != source.CountryName OR ParentId != source.RegionAdminUnitId) 
		THEN
			UPDATE SET 
				Name = source.CountryName,
				ParentId = source.RegionAdminUnitId,
				LastUpdated = @now, 
				IsActive = 1
				
		-- insert new sub region admin unit in dbo.AdminUnit
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT (Code, Name, Description, TypeId, IsActive, ParentId, LastUpdated) 
			VALUES (CountryCode, CountryName, null, @TypeIdSubRegion, 1, RegionAdminUnitId, @now)
			
		-- deactivate sub region admin unit when it does not exists in stage.AdminUnit
		WHEN NOT MATCHED BY SOURCE 
			AND target.TypeId = @TypeIdSubRegion
		THEN
			UPDATE 
				SET IsActive = 0
	;


END

