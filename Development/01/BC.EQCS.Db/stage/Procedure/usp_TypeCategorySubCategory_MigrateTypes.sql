CREATE PROCEDURE [stage].[usp_TypeCategorySubCategory_MigrateTypes] AS
BEGIN

	DECLARE @RootId INT
	SELECT @RootId = au.Id 
		FROM IncidentClass au 
		JOIN IncidentClassType ty
			ON ty.Id = au.TypeId
			AND ty.Code = 'ROOT'
		WHERE au.Code = 'ROOT'
		
	DECLARE @TypeId INT
	SELECT @TypeId = Id FROM IncidentClassType WHERE Code = 'IncidentType'

	DECLARE @now DATETIME = getdate()

	MERGE dbo.IncidentClass AS target
		USING (
			SELECT DISTINCT TypeCode, TypeName FROM [stage].[TypeCategorySubCategory]
		) AS source
		ON (Code = source.TypeCode AND TypeId = @TypeId)
	
		WHEN MATCHED
			AND Name != source.TypeName
			OR IsActive = 0
		THEN
			UPDATE SET 
				Name = source.TypeName,
				LastUpdated = @now, 
				IsActive = 1
	
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT (Code, Name, TypeId, IsActive, LastUpdated, ParentId) 
			VALUES (dbo.Trim(TypeCode), TypeName, @TypeId, 1, @now, @RootId)

		WHEN NOT MATCHED BY SOURCE 
			AND TypeId = @TypeId
		THEN 
			UPDATE SET 
				IsActive = 0,
				LastUpdated = @now
	;


END