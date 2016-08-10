CREATE PROCEDURE [stage].[usp_PopulateApplicationRoleToAdminUnitType]
	
AS
BEGIN
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'GLOBAL_OPERATIONS', 'ROOT' 
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'RMT', 'IELTS_REGION' 
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'CCT', 'IELTS_SUB_REGION' 
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'VT', 'ROOT' 
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'TCS', 'TEST_CENTRE' 
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'TCS_EXTERNAL', 'TEST_CENTRE' 
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'AUDITOR_INTERNAL', 'TEST_CENTRE' 
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'AUDITOR_EXTERNAL', 'TEST_CENTRE' 
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'TCS', 'TEST_LOCATION' 
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'TCS_EXTERNAL', 'TEST_LOCATION' 
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'AUDITOR_INTERNAL', 'TEST_LOCATION' 
	 EXEC [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow] 'AUDITOR_EXTERNAL', 'TEST_LOCATION' 
	 
END





