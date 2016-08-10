CREATE VIEW [dbo].[UserRoleToClassToSchemaKey] AS
WITH 
	Nodes AS
	( 
		SELECT Id as NodeId, Id, Code, Name, TypeId, ParentId
		FROM IncidentClass 
		UNION ALL
		SELECT parent.NodeId, cls.Id, cls.Code, cls.Name, cls.TypeId, cls.ParentId
		FROM IncidentClass cls
		INNER JOIN Nodes parent ON cls.ParentId = parent.Id
		WHERE cls.ParentId IS NOT NULL
	)
	SELECT ApplicationRoleId, Id as IncidentClassId, SchemaKey FROM SchemaKeyMap sk
	JOIN Nodes cac
		ON cac.NodeId = sk.IncidentClassId
	WHERE sk.MapIncludesChildren = 1
	UNION
	SELECT ApplicationRoleId, sk.IncidentClassId, SchemaKey FROM SchemaKeyMap sk
	WHERE sk.MapIncludesChildren = 0
