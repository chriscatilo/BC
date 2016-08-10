CREATE FUNCTION [dbo].[SelectAdminUnitTree]
(
	@codes AS dbo.Codes READONLY
)
RETURNS TABLE AS RETURN
WITH 
	SelfAndParents AS
	( 
		SELECT Id, Code, Name, Description, TypeId, ParentId, IsActive
		FROM AdminUnit 
		WHERE Code IN (SELECT Value FROM @codes)
		UNION ALL
		SELECT unit.Id, unit.Code, unit.Name, unit.Description, unit.TypeId, unit.ParentId, unit.IsActive
		FROM AdminUnit unit
		INNER JOIN SelfAndParents child ON unit.Id = child.ParentId
	), 
	SelfAndChildren AS
	( 
		SELECT Id, Code, Name, Description, TypeId, ParentId, IsActive
		FROM AdminUnit 
		WHERE Code IN (SELECT Value FROM @codes)
		UNION ALL
		SELECT unit.Id, unit.Code, unit.Name, unit.Description, unit.TypeId, unit.ParentId, unit.IsActive
		FROM AdminUnit unit
		INNER JOIN SelfAndChildren parent ON unit.ParentId = parent.Id
		WHERE unit.ParentId IS NOT NULL
	),
	Combined AS 
	(
		SELECT Id, Code, Name, Description, TypeId, ParentId, IsActive 
		FROM SelfAndParents 
		UNION 
		SELECT Id, Code, Name, Description, TypeId, ParentId, IsActive 
		FROM SelfAndChildren
	)
	SELECT
		Combined.Id,
		Combined.Code,
		Combined.Name,
		Combined.TypeId,
		AdminUnitType.Code as TypeCode,
		Combined.ParentId,
		Combined.IsActive
	FROM Combined
	JOIN AdminUnitType
		ON AdminUnitType.Id = Combined.TypeId

/*

usage example:

declare @t Codes
insert into @t values ('NP004-01')
select * from [dbo].[SelectAdminUnitTree](@t)

*/