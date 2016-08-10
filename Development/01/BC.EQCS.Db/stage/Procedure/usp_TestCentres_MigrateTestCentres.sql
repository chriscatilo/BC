CREATE PROCEDURE [stage].[usp_TestCentres_MigrateTestCentres] AS
BEGIN

	-- create source table of test centres 
	-- enforce unique centre number
	DECLARE @Source TABLE 
	(
		CentreNumber VARCHAR(255) PRIMARY KEY, 
		Name VARCHAR(255) NOT NULL, 
		SubRegionAdminUnitId INT NOT NULL,
		OrganisationId INT NULL,
		Address NVARCHAR(2048) NULL,
		Administrator NVARCHAR(100) NULL,
		PrimaryEmail VARCHAR(100) NULL,
		SecondaryEmail VARCHAR(100) NULL
	)
	INSERT INTO @Source
		SELECT src.CentreNumber, dbo.TitleCase(src.Name), subregion.AdminUnitId, Organisation.Id, Address, src.Administrator, src.PrimaryEmail, src.SecondaryEmail
		FROM stage.TestCentre src
		LEFT JOIN dbo.Organisation
			ON Organisation.Name = src.Organisation
		LEFT JOIN (
			SELECT au.id as AdminUnitId, au.Name
			FROM dbo.AdminUnit au
			JOIN dbo.AdminUnitType ty
				ON ty.id = au.TypeId
				AND ty.Code = 'IELTS_SUB_REGION') subregion
			ON subregion.Name = src.SubRegion

	DECLARE @TypeIdTestCentre INT
	SELECT @TypeIdTestCentre = Id FROM AdminUnitType WHERE Code = 'TEST_CENTRE'

	DECLARE @now DATETIME = getdate()

	-- add test centres as admin units
	MERGE dbo.AdminUnit AS target
		USING @Source AS source
		ON (target.Code = source.CentreNumber AND target.TypeId = @TypeIdTestCentre)
	
		-- update test centre admin units when it exists in dbo.AdminUnit
		WHEN MATCHED 
			AND (target.Name != source.Name OR target.ParentId != source.SubRegionAdminUnitId)
		THEN
			UPDATE SET 
				Name = source.Name, 
				ParentId = source.SubRegionAdminUnitId,
				UbiquitousCode = source.CentreNumber,
				LastUpdated = @now
	
		-- insert test centre admin units when it does not exists in dbo.AdminUnit
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT (Code, Name, Description, TypeId, IsActive, ParentId, LastUpdated, UbiquitousCode) 
			VALUES (CentreNumber, Name, null, @TypeIdTestCentre, 1, SubRegionAdminUnitId, @now, CentreNumber)
			
		-- deactivate test centre admin units when it does not exists in @Source
		WHEN NOT MATCHED BY SOURCE 
			AND target.TypeId = @TypeIdTestCentre
		THEN
			UPDATE 
				SET IsActive = 0

	;

	-- add test centres with organisation attribute
	MERGE dbo.TestCentre AS target
		USING (
			SELECT src.CentreNumber, src.Name, au.Id as AdminUnitId, OrganisationId, Address, Administrator, PrimaryEmail, SecondaryEmail
			FROM @Source src
			JOIN AdminUnit au
				ON au.Code = src.CentreNumber
				AND au.TypeId = @TypeIdTestCentre
		) AS source
		ON (target.CentreNumber = source.CentreNumber)
	
		-- update test centre when it exists in dbo.TestCentre
		WHEN MATCHED THEN
			UPDATE SET 
				Name = source.Name, 
				OrganisationId = source.OrganisationId,
				Address = source.Address,
				AdminUnitId = source.AdminUnitId,
				Administrator = source.Administrator,
				PrimaryEmail = source.PrimaryEmail,
				SecondaryEmail = source.SecondaryEmail,
				LastUpdated = getdate()

		-- insert test centre when it does not exists in dbo.TestCentre
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT (CentreNumber, Name, IsActive, OrganisationId, AdminUnitId, Address, Administrator, PrimaryEmail, SecondaryEmail, LastUpdated) 
			VALUES (CentreNumber, Name, 1, source.OrganisationId, AdminUnitId, Address, Administrator, PrimaryEmail, SecondaryEmail, getdate())
	
		-- deactivate test centre when it does not exists in @Source
		WHEN NOT MATCHED BY SOURCE 
		THEN
			UPDATE 
				SET IsActive = 0
	;

END