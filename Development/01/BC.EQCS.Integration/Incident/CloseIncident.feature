Feature: Close Incident

Background: 
	Given table of incidents to persist
	| Test Label  | TestCentre | TestLocation  | Category | SubCategory | IncidentDate | IncidentTime | Description                          | ImmediateActionTaken                            | RiskRating | Product   | RaisedBy     | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry | ReferringOrganisation | ReferringOrgExists | NoOfCandidates |
	| Typical     | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical)     | gen.paragraph(5,10,5,10;TypicalImmedAction)     | LOW        | UKVI      | Someone      | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
	| UpdateClose | GBS03      | UKVI-GBS03-84 | VERIFI1  | VERIFI2     | 2015-02-28   | 11:00        | gen.paragraph(5,10,5,10;Update)      | gen.paragraph(5,10,5,10;UpdateImmedAction)      | MED        | IELTSLSA1 | Someone Else | 2015-03-02 | False      | Whitford            | Karen                  | CEO                  | karen@another.org | EDU              | US                  | BC                    | True               | 2              |
	| CloseReopen | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;CloseReopen) | gen.paragraph(5,10,5,10;CloseReopenImmedAction) | LOW        | UKVI      | Someone      | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |

	Given table of incidents to view
	| Test Label  | IELTSRegion | TestCentre                           | TestLocation | Category      | SubCategory               | IncidentDate | IncidentTime | Description                 | ImmediateActionTaken                   | RiskRating | RaisedDate | Product    | RaisedBy     | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry      | ReferringOrgName | ResidualRiskRating | TestCentreName                 | TestCentreNumber | IsUkvi | NoOfCandidates | IncidentClassCode |
	| Typical     | UKIA        | GBS02 Ealing, Hammersmith And West L | London West  | Verifications | Aged TRF                  | 2015-03-01   | 10:00        | gen.paragraph(;Typical)     | gen.paragraph(;TypicalImmedAction)     | Low        | 2015-03-02 | IELTS UKVI | Someone      | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | Education        | United Kingdom           | British Council  | Low                | Ealing, Hammersmith And West L | GBS02            | True   | 1              | AGEDTR            |
	| UpdateClose | UKIA        | GBS03 Anglia Ruskin University       | Chelmsford   | Verifications | Verification Enquiry only | 2015-02-28   | 11:00        | gen.paragraph(;Update)      | gen.paragraph(;UpdateImmedAction)      | Medium     | 2015-03-01 | LS A1      | Someone Else | 2015-03-02 | False      | Whitford            | Karen                  | CEO                  | karen@another.org | Education        | United States of America | British Council  | Low                | Anglia Ruskin University       | GBS03            | True   | 2              | VERIFI2           |
	| CloseReopen | UKIA        | GBS02 Ealing, Hammersmith And West L | London West  | Verifications | Aged TRF                  | 2015-03-01   | 10:00        | gen.paragraph(;CloseReopen) | gen.paragraph(;CloseReopenImmedAction) | Low        | 2015-03-02 | IELTS UKVI | Someone      | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | Education        | United Kingdom           | British Council  | High               | Ealing, Hammersmith And West L | GBS02            | True   | 1              | AGEDTR            |


Scenario: Close Incident Successfully
	Given incident labeled Typical
		And new incident is raised and response is Ok
		And incident is accepted and response is Ok
	When incident is closed and response is Ok
		| Comments        | ResidualRiskRating |
		| gen.paragraph() | LOW                |
		And incident is retrieved and response is Ok
	Then incident status is Closed
		And incident details are correct
		And available commands for incident are reopen
		#And incident includes correct workflow activity

Scenario: Update while Closing Incident Successfully
	Given incident labeled Typical
		And new incident is raised and response is Ok
		And incident is accepted and response is Ok
	When incident is updated with row UpdateClose while closing and response is Ok
		| Comments        | ResidualRiskRating |
		| gen.paragraph() | LOW                |
		And incident is retrieved and response is Ok
	Then incident status is Closed
		And incident details are correct
		And available commands for incident are reopen
		#And incident includes correct workflow activity

Scenario: Close Reopened Incident Successfully
	Given incident labeled CloseReopen
		And new incident is raised and response is Ok
		And incident is accepted and response is Ok
		And incident is closed and response is Ok
			| Comments        | ResidualRiskRating |
			| gen.paragraph() | LOW                |
		And incident is reopened and response is Ok
			| Reason          |
			| gen.paragraph() |
	When incident is closed and response is Ok
		| Comments        | ResidualRiskRating |
		| gen.paragraph() | HIGH               |
		And incident is retrieved and response is Ok
	Then incident status is Closed
		And incident details are correct
		And available commands for incident are reopen
		#And incident includes correct workflow activity
