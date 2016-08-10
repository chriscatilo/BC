/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------

*/

:r ApplicationRoles.sql
:r ApplicationAssets.sql
:r ApplicationPermissions.sql
:r RiskRatings.sql
:r ResidualRiskRating.sql
:r IncidentProducts.sql
:r IncidentStatus.sql
:r IncidentActionStatus.sql

/* remove these for new Excel data*/
:r Countries.sql
:r OrganisationTypes.sql
:r Organisations.sql
:r AdminUnitTypes.sql
:r AdminUnits.sql

:r IncidentClassTypes.sql
:r IncidentClasses.sql

:r UkviImmediateReportTypes.sql

:r UserRoleToIncidentClassPurpose.sql

:r stage.TestCentres.sql
:r stage.UkviTestVenues.sql
:r stage.OrsTestLocations.sql
:r stage.Users.sql
:r stage.Migrate.sql

:r SchemaKeyMaps.sql

:r NotificationEvent.sql
:r NotificationMessageTemplate.sql
:r NotificationMapping.sql




