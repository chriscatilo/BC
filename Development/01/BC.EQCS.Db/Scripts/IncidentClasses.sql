-- Note: Any new incident class should be migrated using migration scripts in /stage/
IF NOT EXISTS (SELECT NULL FROM dbo.IncidentClass WHERE Code = 'ROOT') BEGIN

	INSERT INTO dbo.IncidentClass
	SELECT 'ROOT', 'Root', ID, 1, NULL, getdate()
	FROM dbo.AdminUnitType WHERE Code = 'ROOT'

END