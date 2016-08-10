CREATE VIEW [dbo].[ReadOnly_UserRoleToIncidentClass]
	AS 	
	SELECT        ApplicationRoleId, IncidentClassId
	FROM            dbo.UserRoleToIncidentClass
	WHERE        RoleToClassPurposeId = (SELECT [Id]  FROM [dbo].[UserRoleToIncidentClassPurpose] WHERE Code = 'IncReadOnly')
