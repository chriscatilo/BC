CREATE PROCEDURE [stage].[usp_TypeCategorySubCategory_MigrateCategories] AS
BEGIN

	DECLARE @Categories stage.IncidentClasses
	INSERT INTO @Categories
		SELECT DISTINCT CategoryCode, CategoryName, CATEGORY_TYPE.ParentId, RiskRating, ResidualRiskRating, UkviImmediateReportType.Id
		FROM [stage].[TypeCategorySubCategory]
		LEFT JOIN (
			SELECT ic.id as ParentId, ic.Code
			FROM dbo.IncidentClass ic
			JOIN dbo.IncidentClassType ty
			ON ty.id = ic.TypeId
			AND ty.Code = 'IncidentType') CATEGORY_TYPE
			ON CATEGORY_TYPE.Code = TypeCode
		LEFT JOIN UkviImmediateReportType
			ON UkviImmediateReportType.Code = TypeCategorySubCategory.UkviImmediateReportType
		WHERE SubCategoryCode IS NULL

	DECLARE @now DATETIME = getdate()

	EXEC [stage].[usp_TypeCategorySubCategory_AddUpdateDeactivateIncidentClasses] @Categories, 'Category', @now

END