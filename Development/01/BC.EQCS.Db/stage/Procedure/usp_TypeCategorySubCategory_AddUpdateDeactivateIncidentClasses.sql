CREATE PROCEDURE [stage].[usp_TypeCategorySubCategory_AddUpdateDeactivateIncidentClasses] 
(
	@classes AS stage.IncidentClasses READONLY,
	@typeCode AS VARCHAR(15), 
	@updateDate AS DATETIME
)
AS
BEGIN

	DECLARE @targetTypeId INT
	SELECT @targetTypeId = Id FROM IncidentClassType WHERE Code = @typeCode
	
	MERGE dbo.IncidentClass AS target
		USING @classes AS source ON 
		(	
			target.Code = source.Code AND target.TypeId = @targetTypeId
		)

		-- update dbo.IncidentClass if different from @classes
		WHEN MATCHED AND 
		(
			target.Name != source.Name OR 
			target.ParentId != source.ParentId OR 
			target.IsActive = 0
		) 
		THEN
			UPDATE SET 
				Name = source.Name,
				ParentId = source.ParentId,
				IsActive = 1,
				LastUpdated = @updateDate

		-- add to dbo.IncidentClass from @classes if not in dbo.IncidentClass
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT (Code, Name, TypeId, IsActive, ParentId, LastUpdated) 
			VALUES (dbo.Trim(Code), dbo.Trim(Name), @targetTypeId, 1, ParentId, @updateDate)

		-- deactivate dbo.IncidentClass if not in @classes
		WHEN NOT MATCHED BY SOURCE AND 
		( 
			target.TypeId = @targetTypeId AND target.IsActive = 1
		)
		THEN
			UPDATE SET 
				IsActive = 0,
				LastUpdated = @updateDate
		;


	MERGE dbo.IncidentClassDefault AS target
		USING (
			SELECT cls.Id as IncidentClassId, rr.Id as RiskRatingId, rrr.Id as ResidualRiskRatingId, src.UkviImmediateReportTypeId
			FROM @classes src
			JOIN IncidentClass cls
				ON cls.Code = src.Code
				AND cls.TypeId = @targetTypeId
			LEFT JOIN dbo.RiskRating rr
				ON rr.Code = src.RiskRating
			LEFT JOIN dbo.ResidualRiskRating rrr
				ON rrr.Code = src.ResidualRiskRating
		) AS source
		ON (target.IncidentClassId = source.IncidentClassId)
	
		-- update dbo.IncidentClassDefault if different in source
		WHEN MATCHED AND 
		(
			target.RiskRatingId != source.RiskRatingId OR 
			target.ResidualRiskRatingId != source.ResidualRiskRatingId OR 
			target.UkviImmediateReportTypeId != source.UkviImmediateReportTypeId OR
			target.IsActive = 0
		)
		THEN
			UPDATE SET 
				RiskRatingId = source.RiskRatingId,
				ResidualRiskRatingId = source.ResidualRiskRatingId,
				UkviImmediateReportTypeId = source.UkviImmediateReportTypeId,
				LastUpdated = @updateDate,
				IsActive = 1

		-- add to dbo.IncidentClassDefault from source if not in dbo.IncidentClassDefault
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT ([IncidentClassId], [IsActive], [RiskRatingId], [ResidualRiskRatingId], [LastUpdated], [UkviImmediateReportTypeId]) 
			VALUES ([IncidentClassId], 1, [RiskRatingId], [ResidualRiskRatingId], @updateDate, [UkviImmediateReportTypeId]) 

		-- deactivate dbo.IncidentClassDefault if not in source
		WHEN NOT MATCHED BY SOURCE AND 
			target.IsActive = 1	
		THEN 
			UPDATE SET IsActive = 0
		;

END