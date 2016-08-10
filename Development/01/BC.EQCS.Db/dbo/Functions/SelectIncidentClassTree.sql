CREATE FUNCTION [dbo].[SelectIncidentClassTree]
(
	@codes AS dbo.Codes READONLY
)
RETURNS TABLE AS RETURN
WITH 
	SelfAndParents AS
	( 
		SELECT Id, Code, Name, TypeId, ParentId, IsActive
		FROM IncidentClass 
		WHERE Code IN (SELECT Value FROM @codes)
		UNION ALL
		SELECT cls.Id, cls.Code, cls.Name, cls.TypeId, cls.ParentId, cls.IsActive
		FROM IncidentClass cls
		INNER JOIN SelfAndParents child ON cls.Id = child.ParentId
	), 
	SelfAndChildren AS
	( 
		SELECT Id, Code, Name, TypeId, ParentId, IsActive
		FROM IncidentClass 
		WHERE Code IN (SELECT Value FROM @codes)
		UNION ALL
		SELECT cls.Id, cls.Code, cls.Name, cls.TypeId, cls.ParentId, cls.IsActive
		FROM IncidentClass cls
		INNER JOIN SelfAndChildren parent ON cls.ParentId = parent.Id
		WHERE cls.ParentId IS NOT NULL
	), 
	Combined AS
	( 
		SELECT Id, Code, Name, TypeId, ParentId, IsActive
		FROM SelfAndParents 
		UNION 
		SELECT Id, Code, Name, TypeId, ParentId, IsActive
		FROM SelfAndChildren
	)
	SELECT 
		Combined.Id, 
		Combined.Code, 
		Combined.Name, 
		Combined.TypeId, 
		IncidentClassType.Code as TypeCode, 
		Combined.ParentId, 
		Combined.IsActive,
		RiskRating.Code AS RiskRatingDefault,
		ResidualRiskRating.Code AS ResidualRiskRatingDefault,
		UkviImmediateReportType.Code as UkviImmediateReportType
	FROM Combined
	JOIN IncidentClassType
		ON IncidentClassType.Id = Combined.TypeId
	LEFT JOIN IncidentClassDefault 
		ON Combined.Id = IncidentClassDefault.IncidentClassId
	LEFT JOIN RiskRating 
		ON IncidentClassDefault.RiskRatingId = RiskRating.Id
	LEFT JOIN ResidualRiskRating 
		ON IncidentClassDefault.ResidualRiskRatingId = ResidualRiskRating.Id
	LEFT JOIN IncidentClass ClassReportType
		ON ClassReportType.Id = Combined.Id
	LEFT JOIN UkviImmediateReportType
		ON UkviImmediateReportType.Id = IncidentClassDefault.UkviImmediateReportTypeId


/*
	usage example:

	declare @t Codes
	insert into @t values ('TRFTAM')
	insert into @t values ('VERIFI2')
	select * from [dbo].[SelectIncidentClassTree](@t)

*/