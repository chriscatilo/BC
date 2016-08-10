Feature: IncidentsListView
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


Background: 
	Given table of incidents to persist
	| Test Label | TestCentre | TestLocation  | Category | SubCategory | IncidentDate | IncidentTime | Description                      | ImmediateActionTaken                        | RiskRating | Product | RaisedBy | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry | ReferringOrganisation | ReferringOrgExists | NoOfCandidates |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 2              |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 3              |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 4              |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 5              |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 6              |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 7              |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 8              |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 9              |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 10             |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | gen.paragraph(5,10,5,10;TypicalImmedAction) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 10             |


#TODO : NM 
Scenario: Rejected incidents is no longer displayed on any of the 3 list views
	Given List of incidents create Typical
		And I have permission to raise them and response is Ok
		And I can view all the above incidents except REJECTED
	When the above raised incients are marked as rejected and response is Ok
	Then I can view all the above incidents except REJECTED

