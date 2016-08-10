 
exec [stage].[usp_TestCentres_MigrateRegions]
GO
exec [stage].[usp_TestCentres_MigrateSubRegions]
GO
exec [stage].[usp_TestCentres_MigrateTestCentres]
GO
exec [stage].[usp_UkviTestVenues_OrsTestLocations_MigrateTestLocations]
GO
exec [stage].[usp_PopulateApplicationRoleToAdminUnitType]
GO
exec [stage].[usp_Users_MigrateUsers]
GO
exec [stage].[usp_Users_MigrateUserToRoleToAdminUnitMaps]
GO
exec [stage].[usp_TypeCategorySubCategory_Migrate]
GO

