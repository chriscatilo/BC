Feature: IncidentCandidateRaceConditions
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

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

Scenario: User A and B updates successfully
	Given incident labeled Typical
		And new incident is saved and response is Ok
		And candidate labeled Candidate1
	When candidate is created and response is Ok
		And candidate is retrieved and response is OK
		And candidate details are correct
		And UserA view the candidate
	When candidate is updated by UserA with Candidate1 label and response is Ok
		And candidate is retrieved and response is Ok
		And candidate details are correct
		And UserB view the candidate
	Then candidate is updated by UserB with Candidate2 label and response is Ok
		And candidate is retrieved and response is Ok
		And candidate details are correct

Scenario: User A and B updates unsuccessfully
	Given incident labeled Typical
		And new incident is saved and response is Ok
		And candidate labeled Candidate1
	When candidate is created and response is Ok
		And candidate is retrieved and response is OK
		And candidate details are correct
		And UserA and UserB view the candidate
	When candidate is updated by UserA with Candidate1 label and response is Ok
		And candidate is retrieved and response is Ok
		And candidate details are correct
	When candidate is updated by UserB with Candidate2 label and response is Conflict
	Then the response message is