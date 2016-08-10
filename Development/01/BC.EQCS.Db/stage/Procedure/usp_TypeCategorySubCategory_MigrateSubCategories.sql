CREATE PROCEDURE [stage].[usp_TypeCategorySubCategory_MigrateSubCategories] AS
BEGIN

	-- create & populate incident classes table of sub categories
	DECLARE @SubCategories stage.IncidentClasses
	INSERT INTO @SubCategories
		SELECT SubCategoryCode, SubCategoryName, cat.ParentId, RiskRating, ResidualRiskRating, UkviImmediateReportType.Id
		FROM [stage].[TypeCategorySubCategory]
		LEFT JOIN (
			SELECT ic.id as ParentId, ic.Code
			FROM dbo.IncidentClass ic
			JOIN dbo.IncidentClassType ty
			ON ty.id = ic.TypeId
			AND ty.Code = 'Category') cat
			ON cat.Code = CategoryCode
		LEFT JOIN UkviImmediateReportType
			ON UkviImmediateReportType.Code = TypeCategorySubCategory.UkviImmediateReportType
		WHERE SubCategoryCode IS NOT NULL
	
	DECLARE @now DATETIME = getdate()	
	
	EXEC [stage].[usp_TypeCategorySubCategory_AddUpdateDeactivateIncidentClasses] @SubCategories, 'SubCategory', @now

END