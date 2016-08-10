CREATE VIEW [dbo].[IELTSAdminUnitPivotByNodeName] AS
WITH 
	Nodes AS
	( 
		SELECT Id as NodeId, Id, Code, Name, TypeId, ParentId
		FROM AdminUnit -- starting node
		UNION ALL
		SELECT child.NodeId, cls.Id, cls.Code, cls.Name, cls.TypeId, cls.ParentId
		FROM AdminUnit cls -- ascendant node
		INNER JOIN Nodes child 
			ON cls.Id = child.ParentId
	),
	AscendantNodes AS
	(
		SELECT NodeId, Nodes.Name as AscendantNodeName, typ.Code as AscendantNodeType
		FROM Nodes 
		JOIN AdminUnitType Typ
			ON typ.Id = Nodes.TypeId
	)
	SELECT * FROM AscendantNodes
	AS Source
	PIVOT
	(
		MAX(Source.AscendantNodeName) 
		FOR Source.AscendantNodeType IN (IELTS_REGION, IELTS_SUB_REGION, TEST_CENTRE, TEST_LOCATION)
	) AS T1