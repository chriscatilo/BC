Feature: Raise Incident
	
Background: 
	Given table of incidents to persist
	| Test Label | TestCentre | TestLocation  | Category | SubCategory | IncidentDate | IncidentTime | Description                      | ImmediateActionTaken                        | RiskRating | Product   | RaisedBy     | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry | ReferringOrganisation | ReferringOrgExists | NoOfCandidates |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI      | Someone      | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
	| Update     | GBS03      | UKVI-GBS03-84 | VERIFI1  | VERIFI2     | 2015-02-28   | 11:00        | gen.paragraph(5,10,5,10;Update)  | gen.paragraph(5,10,5,10;UpdateImmedAction)  | MED        | IELTSLSA1 | Someone Else | 2015-03-02 | False      | Whitford            | Karen                  | CEO                  | karen@another.org | EDU              | US                  | BC                    | True               | 2              |
	| Nulls      | GBS03      | UKVI-GBS03-84 | NULL     | NULL        | NULL         | NULL         | NULL                             | NULL                                        | NULL       | NULL      | NULL         | NULL       | NULL       | NULL                | NULL                   | NULL                 | NULL              | NULL             | NULL                | NULL                  | NULL               | NULL           |
	| Draft      | GBS03      | UKVI-GBS03-84 | VERIFI1  | VERIFI2     | 2015-03-01   | NULL         | gen.paragraph(5,10,5,10;Draft)   | gen.paragraph(5,10,5,10;DraftImmedAction)   | NULL       | NULL      | NULL         | NULL       | NULL       | NULL                | NULL                   | NULL                 | NULL              | NULL             | NULL                | NULL                  | NULL               | NULL           |

	Given table of incidents to view
	| Test Label | IELTSRegion | TestCentre                           | TestLocation | Category      | SubCategory               | IncidentDate | IncidentTime | Description             | ImmediateActionTaken               | RiskRating | RaisedDate | Product    | RaisedBy     | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry      | ReferringOrgName | TestCentreName                 | TestCentreNumber | IsUkvi | NoOfCandidates | IncidentClassCode |
	| Typical    | UKIA        | GBS02 Ealing, Hammersmith And West L | London West  | Verifications | Aged TRF                  | 2015-03-01   | 10:00        | gen.paragraph(;Typical) | gen.paragraph(;TypicalImmedAction) | Low        | 2015-03-02 | IELTS UKVI | Someone      | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | Education        | United Kingdom           | British Council  | Ealing, Hammersmith And West L | GBS02            | True   | 1              | AGEDTR            |
	| Update     | UKIA        | GBS03 Anglia Ruskin University       | Chelmsford   | Verifications | Verification Enquiry only | 2015-02-28   | 11:00        | gen.paragraph(;Update)  | gen.paragraph(;UpdateImmedAction)  | Medium     | 2015-03-01 | LS A1      | Someone Else | 2015-03-02 | False      | Whitford            | Karen                  | CEO                  | karen@another.org | Education        | United States of America | British Council  | Anglia Ruskin University       | GBS03            | True   | 2              | VERIFI2           |
	| Draft      | NULL        | GBS02 Ealing, Hammersmith And West L | Chelmsford   | NULL          | NULL                      | 2015-03-01   | NULL         | gen.paragraph(;Draft)   | gen.paragraph(;DraftImmedAction)   | NULL       | NULL       | NULL       | NULL         | NULL       | NULL       | NULL                | NULL                   | NULL                 | NULL              | NULL             | NULL                     | NULL             | Ealing, Hammersmith And West L | GBS03            | True   | NULL           | VERIFI2           |
Scenario: Draft Incident Successfully
	Given incident labeled Draft
	When new incident is saved and response is Ok 
		And incident is retrieved and response is Ok 			
		Then incident status is Draft

Scenario: Draft Incident Unsuccessfully
	Given incident labeled Nulls
	When new incident is saved 
	Then response is BadRequest

Scenario: Save Incident Successfully
	Given incident labeled Typical
	When new incident is saved and response is Ok
		And incident is retrieved and response is Ok
		#And incident schema is retrieved and response is Ok
	Then incident details are correct
		And incident status is Draft
		And available commands for incident are save, raise, delete, addCandidate

Scenario: Save Incident Unsuccessfully
	Given incident labeled Nulls
	When new incident is saved
		And response is BadRequest	

Scenario: Raise Incident Successfully
	Given incident labeled Typical
	When new incident is raised and response is Ok
		And incident is retrieved and response is Ok
		#And incident schema is retrieved and response is Ok
	Then incident details are correct
		And incident status is Submitted
		And available commands for incident are save, accept, reject, addCandidate
		#And incident includes correct workflow activity
	
Scenario: Raise Incident Unsuccessfully
	Given incident labeled Nulls
	When new incident is raised
		And response is BadRequest	
		
Scenario: Save And Raise Incident Successfully
	Given incident labeled Typical
		And new incident is saved and response is Ok
		And incident is retrieved and response is Ok
		And incident is modified with Update 
	When incident is raised and response is Ok
		And incident is retrieved and response is Ok
		#And incident schema is retrieved and response is Ok
	Then incident details are correct
		And incident status is Submitted
		And available commands for incident are save, accept, reject, addCandidate
		#And incident includes correct workflow activity

Scenario: Update Incident Successfully
	Given incident labeled Typical
		And new incident is saved and response is Ok
		And incident is retrieved and response is Ok
		#And incident schema is retrieved and response is Ok
		And incident is modified with Update 
	When incident is saved and response is Ok
		And incident is retrieved and response is Ok
	Then incident details are correct
		And incident status is Draft
		And available commands for incident are save, raise, delete, addCandidate

Scenario: Update Raised Incident Unsuccessfully
	Given incident labeled Typical
		And new incident is raised and response is Ok
		And incident is retrieved and response is Ok
		#And incident schema is retrieved and response is Ok
		And incident is modified with Update 
	When incident is saved
	Then response is Ok



