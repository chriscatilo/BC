Feature: Reopen Incident

Background: 
	Given table of incidents to persist
	| Test Label | TestCentre | TestLocation  | Category | SubCategory | IncidentDate | IncidentTime | Description                      | ImmediateActionTaken                        | RiskRating | Product | RaisedBy | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry | ReferringOrganisation | ReferringOrgExists | NoOfCandidates |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
																																																																																																								 
	Given table of incidents to view
	| Test Label | IELTSRegion | TestCentre                           | TestLocation | Category      | SubCategory | IncidentDate | IncidentTime | Description             | ImmediateActionTaken               | RiskRating | RaisedDate | Product    | RaisedBy | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry | ReferringOrgName | TestCentreName                 | TestCentreNumber | NoOfCandidates | ResidualRiskRating | IsUkvi | IncidentClassCode |
	| Typical    | UKIA        | GBS02 Ealing, Hammersmith And West L | London West  | Verifications | Aged TRF    | 2015-03-01   | 10:00        | gen.paragraph(;Typical) | gen.paragraph(;TypicalImmedAction) | Low        | 2015-03-02 | IELTS UKVI | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | Education        | United Kingdom      | British Council  | Ealing, Hammersmith And West L | GBS02            | 1              | Medium             | True   | AGEDTR            |
																																																																																																																															   
Scenario: Reopen Incident Successfully																																																																																																						
	Given incident labeled Typical
		And new incident is raised and response is Ok
		And incident is accepted and response is Ok
		And incident is closed and response is Ok
			| Comments        | ResidualRiskRating |
			| gen.paragraph() | MED                |
	When incident is reopened and response is Ok
		| Reason          |
		| gen.paragraph() |
		And incident is retrieved and response is Ok
		#And incident schema is retrieved and response is Ok
	Then incident details are correct
		And incident status is InProgress
		And available commands for incident are save, close, addCandidate
		#And incident includes correct workflow activity