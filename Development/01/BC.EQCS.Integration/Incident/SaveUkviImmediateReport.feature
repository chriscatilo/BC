Feature: Save Incident's UKVI Immediate Report
	
Background: 
	Given table of incidents to persist
	| Test Label               | ReportUkvi | Category | SubCategory | UkviImmediateReportType | TestCentre | TestLocation  | IncidentDate | IncidentTime | Description     | ImmediateActionTaken | RiskRating | Product | RaisedBy | TestDate   | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry | ReferringOrganisation | ReferringOrgExists | NoOfCandidates |
	| VENUE1 Incident          | True       | VENUE1   | NULL        | Rubbish                 | GBS02      | UKVI-GBS02-87 | 2015-03-01   | 10:00        | gen.paragraph() | gen.paragraph()      | LOW        | UKVI    | Someone  | 2015-03-01 | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
	| CLERIC2 Incident         | True       | CLERIC2  | NULL        | Rubbish                 | GBS02      | UKVI-GBS02-87 | 2015-03-01   | 10:00        | gen.paragraph() | gen.paragraph()      | LOW        | UKVI    | Someone  | 2015-03-01 | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
	| VIDEO1 Incident          | True       | VIDEO1   | NULL        | Rubbish                 | GBS02      | UKVI-GBS02-87 | 2015-03-01   | 10:00        | gen.paragraph() | gen.paragraph()      | LOW        | UKVI    | Someone  | 2015-03-01 | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
	| COUNTE Incident          | True       | VERIFI1  | COUNTE      | Rubbish                 | GBS02      | UKVI-GBS02-87 | 2015-03-01   | 10:00        | gen.paragraph() | gen.paragraph()      | LOW        | UKVI    | Someone  | 2015-03-01 | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
	| No Report Incident       | False      | VERIFI1  | COUNTE      | Rubbish                 | GBS02      | UKVI-GBS02-87 | 2015-03-01   | 10:00        | gen.paragraph() | gen.paragraph()      | LOW        | UKVI    | Someone  | 2015-03-01 | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
	| OTHER SLF Incident       | True       | OTHER    | NULL        | SLF                     | GBS02      | UKVI-GBS02-87 | 2015-03-01   | 10:00        | gen.paragraph() | gen.paragraph()      | LOW        | UKVI    | Someone  | 2015-03-01 | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
	| OTHER NCT Incident       | True       | OTHER    | NULL        | NCT                     | GBS02      | UKVI-GBS02-87 | 2015-03-01   | 10:00        | gen.paragraph() | gen.paragraph()      | LOW        | UKVI    | Someone  | 2015-03-01 | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
	| OTHER No Report Incident | False      | OTHER    | NULL        | NULL                    | GBS02      | UKVI-GBS02-87 | 2015-03-01   | 10:00        | gen.paragraph() | gen.paragraph()      | LOW        | UKVI    | Someone  | 2015-03-01 | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
	
Scenario: Successfully save UkviImmediateReportType while updating incident during workflow

	# draft incident
	Given incident labeled VENUE1 Incident
	When new incident is saved and response is Ok
		And incident is retrieved and response is Ok
		And incident attribute UkviImmediateReportType is Service Level Failure

	# update draft incident
		And incident is modified with No Report Incident 
		And incident is saved and response is Ok
		And incident is retrieved and response is Ok
		And incident attribute UkviImmediateReportType is NULL

	# raise incident
		And incident is modified with CLERIC2 Incident 
		And incident is raised and response is Ok
		And incident is retrieved and response is Ok
		And incident attribute UkviImmediateReportType is Notification of Compromised Test
		
	# update submitted incident
		And incident is modified with No Report Incident 
		And incident is saved and response is Ok
		And incident is retrieved and response is Ok
		And incident attribute UkviImmediateReportType is NULL

	# accept incident (with update)
		And incident is updated with row VIDEO1 Incident while accepting and response is Ok
		And incident is retrieved and response is Ok
		And incident attribute UkviImmediateReportType is Service Level Failure

	# update accepted incident
		And incident is modified with No Report Incident 
		And incident is saved and response is Ok
		And incident is retrieved and response is Ok
		And incident attribute UkviImmediateReportType is NULL

	# close incident (with update)
		And incident is updated with row COUNTE Incident while closing and response is Ok
		| Comments        | ResidualRiskRating |
		| gen.paragraph() | LOW                |
		And incident is retrieved and response is Ok
	Then incident attribute UkviImmediateReportType is Notification of Compromised Test

Scenario: Successfully save UkviImmediateReportType while updating OTHER-class incident during workflow

	# draft incident
	Given incident labeled OTHER SLF Incident
	When new incident is saved and response is Ok
		And incident is retrieved and response is Ok
		And incident attribute UkviImmediateReportType is Service Level Failure

	# update draft incident
		And incident is modified with OTHER NCT Incident
		And incident is saved and response is Ok
		And incident is retrieved and response is Ok
		And incident attribute UkviImmediateReportType is Notification of Compromised Test

	# raise incident
		And incident is modified with OTHER SLF Incident 
		And incident is raised and response is Ok
		And incident is retrieved and response is Ok
		And incident attribute UkviImmediateReportType is Service Level Failure
		
	# update submitted incident
		And incident is modified with OTHER No Report Incident 
		And incident is saved and response is Ok
		And incident is retrieved and response is Ok
		And incident attribute UkviImmediateReportType is NULL

	# accept incident (with update)
		And incident is updated with row OTHER NCT Incident while accepting and response is Ok
		And incident is retrieved and response is Ok
		And incident attribute UkviImmediateReportType is Notification of Compromised Test

	# close incident (with update)
		And incident is updated with row OTHER SLF Incident while closing and response is Ok
		| Comments        | ResidualRiskRating |
		| gen.paragraph() | LOW                |
		And incident is retrieved and response is Ok
	Then incident attribute UkviImmediateReportType is Service Level Failure
