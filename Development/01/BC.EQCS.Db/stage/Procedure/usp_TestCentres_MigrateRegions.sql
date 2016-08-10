CREATE PROCEDURE [stage].[usp_TestCentres_MigrateRegions] AS
BEGIN

	DECLARE @RegionTypeId INT
	SELECT @RegionTypeId = Id FROM AdminUnitType WHERE Code = 'IELTS_REGION'

	DECLARE @RootAdminUnitId INT
	SELECT @RootAdminUnitId = au.Id 
		FROM AdminUnit au 
		JOIN AdminUnitType at ON at.Id = au.TypeId AND at.Code = 'ROOT'
		WHERE au.Code = 'ROOT'

	DECLARE @now DATETIME = getdate()

	MERGE dbo.AdminUnit AS target
		USING (
			SELECT DISTINCT Region FROM [stage].[TestCentre]
		) AS source
		ON (target.Code = source.Region AND target.TypeId = @RegionTypeId)
	
		-- insert new region admin units
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT (Code, Name, Description, TypeId, IsActive, LastUpdated, ParentId) 
			VALUES (UPPER(Region), Region, null, @RegionTypeId, 1, @now, @RootAdminUnitId)

		-- deactivate region when it does not exist in stage.TestCentre
		WHEN NOT MATCHED BY SOURCE 
			AND target.TypeId = @RegionTypeId
		THEN
			UPDATE
				SET IsActive = 0
	;


END 

