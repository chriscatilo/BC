
CREATE VIEW [dbo].[IncidentView]
AS
SELECT 
	Incident.Id,
	Incident.FormalId,
	Incident.CreateDate,
	LoggedByUser.DisplayName AS LoggedByUser,
	ApplicationRole.Name AS LoggedByUserRole,
	Submission.DateTimeOfActivity AS RaisedDate,
	Incident.Status,
	Incident.IncidentDate,
	Incident.IncidentTime,
	Incident.Description,
	Incident.ImmediateActionTaken,
	Product.Name AS Product,
	IELTSCentreStructure.IELTS_REGION as IELTSRegion, 
	TestCentre.CentreNumber + ' ' + TestCentre.Name AS TestCentre,
	TestLocationAdminUnit.Name AS TestLocation,
	Incident.RaisedBy,
	Class.IncidentType,
	Class.Category,
	Class.SubCategory,
	RiskRating.Name AS RiskRating,
	ResidualRiskRating.Name AS ResidualRiskRating,
	Incident.TestDate,
	Incident.NumberOfCandidatesAffected,

	ISNULL(Incident.ReferringOrgName, ReferringOrganisation.Name) AS ReferringOrgName,
	Incident.ReferringOrgSurname,
	Incident.ReferringOrgFirstnames,
	Incident.ReferringOrgJobTitle,
	Incident.ReferringOrgEmail,
	ReferringOrgType.Name AS ReferringOrgType, 
	ReferringOrgCountry.Name AS ReferringOrgCountry,
	
	Product.IsUkvi,
	Incident.UkviFollowUpDate,
	Incident.ReportUkvi,
	UkviImmediateReportType.Name as UkviImmediateReportType,

	IncidentClass.Code As IncidentClassCode
	
FROM Incident
LEFT JOIN IncidentActivityLog Submission
	ON Submission.IncidentId = Incident.Id
	AND Submission.LogType = 3 -- submission type

	LEFT JOIN
		(ApplicationUser LoggedByUser 
			LEFT JOIN 	(UserToRoleToAdminUnit	
				LEFT JOIN ApplicationRole 
				ON UserToRoleToAdminUnit.ApplicationRoleId = ApplicationRole.Id)
			ON LoggedByUser.Id = UserToRoleToAdminUnit.ApplicationUserId)  
	ON Incident.LoggedById = LoggedByUser.Id


LEFT JOIN Product
	ON Product.Id = Incident.ProductId
LEFT JOIN TestLocation
	ON TestLocation.Id = Incident.TestLocationId
LEFT JOIN AdminUnit TestLocationAdminUnit
	ON TestLocationAdminUnit.Id = TestLocation.AdminUnitId
LEFT JOIN TestCentre
	ON TestCentre.Id = Incident.TestCentreId
LEFT JOIN IELTSAdminUnitPivotByNodeName IELTSCentreStructure
	ON IELTSCentreStructure.NodeId = TestCentre.AdminUnitId
LEFT JOIN IncidentClassPivotByNodeName Class
	ON Class.NodeId = Incident.IncidentClassId
LEFT JOIN RiskRating RiskRating
	ON RiskRating.Id = Incident.RiskRatingId
LEFT JOIN ResidualRiskRating ResidualRiskRating
	ON ResidualRiskRating.id = Incident.ResidualRiskRatingId
LEFT JOIN OrganisationType ReferringOrgType
	ON ReferringOrgType.Id = Incident.ReferringOrgTypeId
LEFT JOIN Country AS ReferringOrgCountry
	ON ReferringOrgCountry.Id = Incident.ReferringOrgCountryId
LEFT JOIN Organisation AS ReferringOrganisation
	ON ReferringOrganisation.Id = Incident.ReferringOrganisationId
LEFT JOIN IncidentClass
    ON Incident.IncidentClassId = IncidentClass.Id
LEFT JOIN UkviImmediateReportType
	ON UkviImmediateReportType.Id = Incident.UkviImmediateReportTypeId