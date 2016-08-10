CREATE PROC [stage].[usp_TypeCategorySubCategory_BuildUserRoleToIncidentClass]
AS BEGIN

	DECLARE @IncidentClassCodes Codes

	-- GLOBAL_OPERATIONS
	BEGIN
		-- Raise permissions 
		DELETE @IncidentClassCodes
		INSERT INTO @IncidentClassCodes 
		SELECT ISNULL(SubCategoryCode, CategoryCode) FROM stage.TypeCategorySubCategory WHERE Raise_GO = 1
		EXEC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass] @IncidentClassCodes, 'GLOBAL_OPERATIONS', 'IncR'

		-- View permissions 
		DELETE @IncidentClassCodes
		INSERT INTO @IncidentClassCodes 
		SELECT ISNULL(SubCategoryCode, CategoryCode) FROM stage.TypeCategorySubCategory WHERE View_GO = 1
		EXEC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass] @IncidentClassCodes, 'GLOBAL_OPERATIONS', 'IncV'	
	END 

	-- RMT
	BEGIN
		-- Raise permissions 
		DELETE @IncidentClassCodes
		INSERT INTO @IncidentClassCodes 
		SELECT ISNULL(SubCategoryCode, CategoryCode) FROM stage.TypeCategorySubCategory WHERE Raise_RMT = 1
		EXEC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass] @IncidentClassCodes, 'RMT', 'IncR'

		-- View permissions 
		DELETE @IncidentClassCodes
		INSERT INTO @IncidentClassCodes 
		SELECT ISNULL(SubCategoryCode, CategoryCode) FROM stage.TypeCategorySubCategory WHERE View_RMT = 1
		EXEC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass] @IncidentClassCodes, 'RMT', 'IncV'	
	END  

	-- CCT
	BEGIN
		-- Raise permissions 
		DELETE @IncidentClassCodes
		INSERT INTO @IncidentClassCodes 
		SELECT ISNULL(SubCategoryCode, CategoryCode) FROM stage.TypeCategorySubCategory WHERE Raise_CCT = 1
		EXEC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass] @IncidentClassCodes, 'CCT', 'IncR'

		-- View permissions 
		DELETE @IncidentClassCodes
		INSERT INTO @IncidentClassCodes 
		SELECT ISNULL(SubCategoryCode, CategoryCode) FROM stage.TypeCategorySubCategory WHERE View_CCT = 1
		EXEC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass] @IncidentClassCodes, 'CCT', 'IncV'	
		
		-- Read Only permissions
		DELETE @IncidentClassCodes
		INSERT INTO @IncidentClassCodes VALUES ('PRRC')	
		EXEC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass] @IncidentClassCodes, 'CCT', 'IncReadOnly'	

	END 

	-- VT
	BEGIN
		-- Raise permissions 
		DELETE @IncidentClassCodes
		INSERT INTO @IncidentClassCodes 
		SELECT ISNULL(SubCategoryCode, CategoryCode) FROM stage.TypeCategorySubCategory WHERE Raise_VT = 1
		EXEC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass] @IncidentClassCodes, 'VT', 'IncR'

		-- View permissions 
		DELETE @IncidentClassCodes
		INSERT INTO @IncidentClassCodes 
		SELECT ISNULL(SubCategoryCode, CategoryCode) FROM stage.TypeCategorySubCategory WHERE View_VT = 1
		EXEC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass] @IncidentClassCodes, 'VT', 'IncV'	
	END 

	-- TCS
	BEGIN
		-- Raise permissions 
		DELETE @IncidentClassCodes
		INSERT INTO @IncidentClassCodes 
		SELECT ISNULL(SubCategoryCode, CategoryCode) FROM stage.TypeCategorySubCategory WHERE Raise_TC = 1
		EXEC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass] @IncidentClassCodes, 'TCS', 'IncR'

		-- View permissions 
		DELETE @IncidentClassCodes
		INSERT INTO @IncidentClassCodes 
		SELECT ISNULL(SubCategoryCode, CategoryCode) FROM stage.TypeCategorySubCategory WHERE View_TC = 1
		EXEC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass] @IncidentClassCodes, 'TCS', 'IncV'	
	END 
END


/*
SELECT ApplicationRole.Code, IncidentClass.Code, UserRoleToIncidentClassPurpose.Code
FROM UserRoleToIncidentClass
JOIN ApplicationRole
	ON ApplicationRole.Id = UserRoleToIncidentClass.ApplicationRoleId
	AND ApplicationRole.Code = 'GLOBAL_OPERATIONS'
JOIN IncidentClass
	ON IncidentClass.Id = UserRoleToIncidentClass.IncidentClassId
	AND IncidentClass.Code NOT IN ('PRRC', 'VERIFI1')
JOIN UserRoleToIncidentClassPurpose
	ON UserRoleToIncidentClassPurpose.Id = UserRoleToIncidentClass.RoleToClassPurposeId
	AND UserRoleToIncidentClassPurpose.Code = 'IncR'
*/