CREATE PROCEDURE [stage].[usp_TypeCategorySubCategory_Migrate]
AS BEGIN

	DECLARE @starttime DATETIME;
	SET @starttime = GETDATE()

	-- store the previous role-incidentclass mapping
	DECLARE @UserRoleToIncidentClass AS TABLE
	(
	    [ApplicationRole] VARCHAR(255) NOT NULL,
		[IncidentClass] VARCHAR(255) NOT NULL,
		[RoleToClassPurpose] VARCHAR(255) NOT NULL,
		[Result] VARCHAR(255) NULL
	)
	INSERT INTO @UserRoleToIncidentClass
		SELECT rol.Code, cls.Code, pur.Code, NULL
		FROM dbo.[UserRoleToIncidentClass] urcls
		JOIN ApplicationRole rol
			on rol.Id = urcls.ApplicationRoleId
		JOIN IncidentClass cls
			on cls.Id = urcls.IncidentClassId
		JOIN UserRoleToIncidentClassPurpose pur
			on pur.Id = urcls.RoleToClassPurposeId

	EXEC [stage].[usp_TypeCategorySubCategory_PopulateStagingTable]

	-- migrate Types, Categories and Sub Categories from stage.TypeCategorySubCategory
	EXEC [stage].[usp_TypeCategorySubCategory_MigrateTypes]		
	EXEC [stage].[usp_TypeCategorySubCategory_MigrateCategories]
	EXEC [stage].[usp_TypeCategorySubCategory_MigrateSubCategories]
	
	-- update role-incidentclass mapping
	EXEC [stage].[usp_TypeCategorySubCategory_BuildUserRoleToIncidentClass]
	

	PRINT N'Check actual new & updated incident classes matches expected changes';
	SELECT * 
		FROM IncidentClass
		WHERE LastUpdated >= @starttime
		ORDER BY code
	

	PRINT N'Check actual new & updated incident classes defaults matches expected changes';
	SELECT cls.Code, cls.Name, def.*
		FROM IncidentClassDefault def
		JOIN IncidentClass cls 
			ON cls.Id = def.IncidentClassId
		WHERE def.LastUpdated >= @starttime	
		ORDER BY cls.Code

	-- list role-incidentclass changes
	MERGE INTO @UserRoleToIncidentClass AS Target
		USING 
		(
			SELECT rol.Code, cls.Code, pur.Code
			FROM dbo.[UserRoleToIncidentClass] urcls
			JOIN ApplicationRole rol
				on rol.Id = urcls.ApplicationRoleId
			JOIN IncidentClass cls
				on cls.Id = urcls.IncidentClassId
			JOIN UserRoleToIncidentClassPurpose pur
				on pur.Id = urcls.RoleToClassPurposeId
		) 
		AS SOURCE
		(
			ApplicationRole, IncidentClass, RoleToClassPurpose
		)
		ON 
		(
			target.ApplicationRole = source.ApplicationRole AND
			target.IncidentClass = source.IncidentClass AND
			target.RoleToClassPurpose = source.RoleToClassPurpose
		)
		WHEN MATCHED THEN
			DELETE
		WHEN NOT MATCHED BY TARGET THEN
			INSERT (ApplicationRole, IncidentClass, RoleToClassPurpose, Result)
			VALUES (ApplicationRole, IncidentClass, RoleToClassPurpose, 'Added')
		WHEN NOT MATCHED BY SOURCE THEN
			UPDATE 
				SET Result = 'Updated'
		;
		
	PRINT N'Check actual new & updated role-incidentclass mapping matches expected changes';
	SELECT * FROM @UserRoleToIncidentClass
END 
