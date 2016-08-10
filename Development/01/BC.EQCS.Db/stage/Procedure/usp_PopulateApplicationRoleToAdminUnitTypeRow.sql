CREATE PROCEDURE [stage].[usp_PopulateApplicationRoleToAdminUnitTypeRow]
	@RoleCode VARCHAR(255),
	@AdminUnitTypeCode  VARCHAR(255)
AS
BEGIN
	
			INSERT INTO [dbo].[ApplicationRoleToAdminUnitType] ([ApplicationRoleId], [AdminUnitTypeId])
			VALUES 
			(
				(SELECT [Id] FROM [ApplicationRole] WHERE [Code] = @RoleCode ),
				(SELECT [Id] FROM [AdminUnitType] WHERE [Code] = @AdminUnitTypeCode )
			)

END


