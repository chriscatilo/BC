Feature: Delete Incident
	
Background: 
	Given table of incidents to persist
	| Test Label | TestCentre | TestLocation  | Category | SubCategory | IncidentDate | IncidentTime | Description                      | ImmediateActionTaken | RiskRating | Product | RaisedBy | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry | ReferringOrganisation | ReferringOrgExists | NoOfCandidates |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph()      | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
																																																																																																		 
Scenario: Delete Incident Successfully
	Given incident labeled Typical
		And new incident is saved and response is Ok
	When incident is deleted and response is Ok
		And incident is retrieved
	Then response is NotFound

Scenario: Delete Incident Unsuccessfully
	Given incident labeled Typical
		And new incident is raised and response is Ok
	When incident is deleted and response is BadRequest
		And incident is retrieved
	Then response is Ok