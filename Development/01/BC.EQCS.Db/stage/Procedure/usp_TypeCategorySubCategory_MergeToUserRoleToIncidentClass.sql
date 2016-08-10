CREATE PROC [stage].[usp_TypeCategorySubCategory_MergeToUserRoleToIncidentClass]
(
	@codes AS dbo.Codes READONLY,
	@role AS VARCHAR(25),
	@purpose AS VARCHAR(50)
)
AS BEGIN

	DECLARE @appRoleId INT, @purposeId INT
	
	SELECT @appRoleId = Id FROM ApplicationRole WHERE Code = @role
	SELECT @purposeId = Id FROM UserRoleToIncidentClassPurpose WHERE Code = @purpose

	MERGE [dbo].[UserRoleToIncidentClass] AS TARGET
		USING (
			SELECT @appRoleId, Class.Id, @purposeId
			FROM @codes Codes
			JOIN IncidentClass Class
				ON Class.Code = Codes.Value
		) AS Source (ApplicationRoleId, IncidentClassId, RoleToClassPurposeId)
		ON (Target.ApplicationRoleId = Source.ApplicationRoleId AND Target.IncidentClassId = Source.IncidentClassId AND Target.RoleToClassPurposeId = Source.RoleToClassPurposeId)
		
		-- insert new rows 
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT (ApplicationRoleId, IncidentClassId, RoleToClassPurposeId)
			VALUES (ApplicationRoleId, IncidentClassId, RoleToClassPurposeId)

		-- delete rows that are in the target but not the source 
		WHEN NOT MATCHED BY SOURCE 
			AND Target.ApplicationRoleId = @appRoleId AND Target.RoleToClassPurposeId = @purposeId
		THEN 
			DELETE;
END