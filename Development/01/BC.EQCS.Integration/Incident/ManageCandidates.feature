Feature: Manage Candidates

Background: 
	Given table of incidents to persist
	| Test Label | TestCentre | TestLocation  | Category | SubCategory | IncidentDate | IncidentTime | Description                      | RiskRating | Product | RaisedBy | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry | ReferringOrganisation | ReferringOrgExists | NoOfCandidates |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
																																																																																														
	Given table of candidates to persist
	| Test Label | Number | Surname | Firstnames  | Address                           | DateOfBirth | Gender | IdNumber | TrfNumber | DateTrfCancelled | UKVIRefNumber | Nationality   | IdDocumentNumber |
	| Candidate1 | 123456 | Perry   | Karen Tomas | gen.paragraph(1,5,1,5;Candidate1) | 2000-03-01  | Female | Doc1234  | Trf1234   | 2015-03-02       | UKVI1234      | GB            | Doc1             |
	| Candidate2 | 7890   | Tomas   | Perry Kevin | gen.paragraph(1,5,1,5;Candidate2) | 2000-12-12  | Male   | Doc6789  | Trf5678   | 2015-03-01       | UKVI5678      | US            | Doc2             |

	Given table of candidates to view
	| Test Label | Number | Surname | Firstnames  | Address                    | DateOfBirth | Gender | IdNumber | TrfNumber | DateTrfCancelled | UKVIRefNumber | Nationality              | IdDocumentNumber |
	| Candidate1 | 123456 | Perry   | Karen Tomas | gen.paragraph(;Candidate1) | 2000-03-01  | Female | Doc1234  | Trf1234   | 2015-03-02       | UKVI1234      | United Kingdom           | Doc1             |
	| Candidate2 | 7890   | Tomas   | Perry Kevin | gen.paragraph(;Candidate2) | 2000-12-12  | Male   | Doc6789  | Trf5678   | 2015-03-01       | UKVI5678      | United States of America | Doc2             |

Scenario: Add Candidate Successfully
	Given incident labeled Typical
		And new incident is saved and response is Ok
		And candidate labeled Candidate1
	When candidate is created and response is Ok
		And candidate is retrieved and response is OK
	Then candidate details are correct

Scenario: Add Candidate unsuccessfully on incident rejection
	Given incident labeled Typical
		And new incident is raised and response is Ok
		And incident is rejected and response is Ok
			| Reason          |
			| gen.paragraph() |
		And candidate labeled Candidate1
	When candidate is created 
		Then response is BadRequest

Scenario: Add Candidate unsuccessfully on incident closure
	Given incident labeled Typical
		And new incident is raised and response is Ok
		And incident is accepted and response is Ok
		And incident is closed and response is Ok
			| Comments        | ResidualRiskRating |
			| gen.paragraph() | Low                |
		And candidate labeled Candidate1
	When candidate is created 
		Then response is BadRequest

Scenario: Delete Candidate Successfully
	Given incident labeled Typical
		And new incident is saved and response is Ok
		And candidate labeled Candidate1
		And candidate is created and response is Ok
	When candidate is deleted and response is Ok
		And candidate is retrieved 
	Then response is NotFound

Scenario: Delete Candidate unsuccessfully on incident rejection
	Given incident labeled Typical
		And new incident is raised and response is Ok
		And candidate labeled Candidate1
		And candidate is created and response is Ok
		And incident is rejected and response is Ok
			| Reason          |
			| gen.paragraph() |
	When candidate is deleted 
		Then response is BadRequest

Scenario: Delete Candidate unsuccessfully on incident closure
	Given incident labeled Typical
		And new incident is raised and response is Ok
		And incident is accepted and response is Ok
		And candidate labeled Candidate1
		And candidate is created and response is Ok
		And incident is closed and response is Ok
			| Comments        | ResidualRiskRating |
			| gen.paragraph() | Low                |
	When candidate is deleted 
		Then response is BadRequest

Scenario: Get all candidates for incident
	Given incident labeled Typical
		And new incident is saved and response is Ok 
		And candidate labeled Candidate1
		And candidate is created and response is Ok
		And candidate labeled Candidate2
		And candidate is created and response is Ok
	When all candidate for incident are retrieved
		Then response is OK
		And candidates retrieved are
		| Number |
		| 123456 |
		| 7890   |

Scenario: Update Candidate Successfully
	Given incident labeled Typical
		And new incident is saved and response is Ok
		And candidate labeled Candidate1
		And candidate is created and response is Ok
	When candidate is updated with Candidate2 label and response is Ok
		And candidate is retrieved and response is Ok
	Then candidate details are correct
	
Scenario: Update Candidate unsuccessfully on incident rejection
	Given incident labeled Typical
		And new incident is raised and response is Ok
		And candidate labeled Candidate1
		And candidate is created and response is Ok
		And incident is rejected and response is Ok
			| Reason          |
			| gen.paragraph() |
	When candidate is updated with Candidate2 label
		Then response is BadRequest
		
Scenario: Update Candidate unsuccessfully on incident closure
	Given incident labeled Typical
		And new incident is raised and response is Ok
		And incident is accepted and response is Ok
		And candidate labeled Candidate1
		And candidate is created and response is Ok
		And incident is closed and response is Ok
			| Comments        | ResidualRiskRating |
			| gen.paragraph() | Low                |
	When candidate is updated with Candidate2 label
		Then response is BadRequest