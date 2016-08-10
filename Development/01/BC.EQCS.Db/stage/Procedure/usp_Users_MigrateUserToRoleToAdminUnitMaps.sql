CREATE PROCEDURE [stage].[usp_Users_MigrateUserToRoleToAdminUnitMaps] AS 
BEGIN
	-- create user-role-adminunit mapping source
	DECLARE @source TABLE (
		[ApplicationUserId] INT NOT NULL,
		[ApplicationRoleId] INT NOT NULL,
		[AdminUnitId] INT NOT NULL,
		PRIMARY KEY ([ApplicationUserId], [ApplicationRoleId], [AdminUnitId])
	)
	INSERT INTO @source
		SELECT auser.Id, arole.Id, aunit.Id
		FROM [stage].[Users] u
		LEFT JOIN dbo.ApplicationUser auser
			ON auser.ObjectGUID = u.ObjectGUID 
		LEFT JOIN dbo.ApplicationRole arole
			ON arole.Code = u.Role
		LEFT JOIN dbo.AdminUnit aunit
			ON aunit.Code = u.AdminUnit

	-- add user-role-adminunit mapping
	MERGE [dbo].[UserToRoleToAdminUnit] AS Target
		USING @source AS source
		ON (
			target.[ApplicationUserId] = source.[ApplicationUserId] AND
			target.[ApplicationRoleId] = source.[ApplicationRoleId] AND
			target.[AdminUnitId] = source.[AdminUnitId])

		WHEN NOT MATCHED BY TARGET THEN
			INSERT ([ApplicationUserId], [ApplicationRoleId], [AdminUnitId])
			VALUES ([ApplicationUserId], [ApplicationRoleId], [AdminUnitId])

		WHEN NOT MATCHED BY SOURCE THEN
			DELETE
	;

END