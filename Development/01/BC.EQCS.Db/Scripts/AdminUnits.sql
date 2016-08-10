-- Note: Any new admin unit should use migration scripts in /stage/ (e.g. [stage].[usp_UkviTestVenues_MigrateRegions])
IF NOT EXISTS (SELECT NULL FROM dbo.AdminUnit WHERE Code = 'ROOT') BEGIN

	INSERT INTO dbo.AdminUnit 
	SELECT 'ROOT', 'Root', 'Root Admin Unit', NULL, ID, 1, NULL, NULL, NULL, getdate()
	FROM dbo.AdminUnitType WHERE Code = 'ROOT';
	
END

