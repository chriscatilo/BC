CREATE VIEW [dbo].[IncidentClassPivotByNodeName] AS
WITH 
	Nodes AS
	( 
		SELECT Id as NodeId, Id, Code, Name, TypeId, ParentId
		FROM IncidentClass -- starting node
		UNION ALL
		SELECT child.NodeId, cls.Id, cls.Code, cls.Name, cls.TypeId, cls.ParentId
		FROM IncidentClass cls -- ascendant node
		INNER JOIN Nodes child 
			ON cls.Id = child.ParentId
	),
	AscendantNodes AS
	(
		SELECT NodeId, Nodes.Name as AscendantNodeName, typ.Code as AscendantNodeType
		FROM Nodes 
		JOIN IncidentClassType Typ
			ON typ.Id = Nodes.TypeId
	),
	Result AS
	(
		SELECT * FROM AscendantNodes
		AS Source
		PIVOT
		(
			MAX(Source.AscendantNodeName) 
			FOR Source.AscendantNodeType IN (IncidentType,Category,SubCategory)
		) AS T1
	)
	SELECT 
		NodeId,
		IncidentType,
		Category,
		ISNULL(SubCategory, Category) AS SubCategory
	FROM Result;