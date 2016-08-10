Feature: RaceConditions
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
	Given table of incidents to persist
	| Test Label | TestCentre | TestLocation  | Category | SubCategory | IncidentDate | IncidentTime | Description                      | RiskRating | Product | RaisedBy | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry | ReferringOrganisation | ReferringOrgExists | NoOfCandidates |
	| Typical    | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 10:00        | gen.paragraph(5,10,5,10;Typical) | LOW        | UKVI    | Someone  | 2015-03-01 | False      | Bradshaw            | Benjamin               | Director             | ben@some.org      | EDU              | GB                  | BC                    | True               | 1              |
																																																																																														
	Given table of action to persist
	| Test Label | ActionDescription              | ActionResponse | Status     | AssignedOn | AssignedById | AssignedToTestCenter |
	| Action1    | gen.paragraph(1,5,1,5;Action1) | Null           | InProgress | Null       | 1         | Null                 |
	| Action2    | gen.paragraph(1,5,1,5;Action2) | Null           | Closed     | Null       | 1         | Null                 |

	Given table of action to view
	| Test Label | ActionDescription       | ActionResponse | Status     | AssignedOn | AssignedById | AssignedToTestCenter |
	| Action1    | gen.paragraph(;Action1) | Null           | InProgress | Null       | 1         | Null                 |
	| Action2    | gen.paragraph(;Action2) | Null           | Closed     | Null       | 1         | Null                 |


Scenario: User A and B updates action successfully
	Given incident labeled Typical
		And new incident is saved and response is Ok
		And action labeled Action1
	When action is created and response is Ok
		And action is retrieved and response is OK
		And action details are correct
		And UserA view the action
	When action is updated by UserA with Action1 label and response is Ok
		And action is retrieved and response is Ok
		And action details are correct
		And UserB view the action
	Then action is updated by UserB with Action2 label and response is Ok
		And action is retrieved and response is Ok
		And action details are correct

Scenario: User A and B updates action unsuccessfully
	Given incident labeled Typical
		And new incident is saved and response is Ok
		And action labeled Action1
	When action is created and response is Ok
		And action is retrieved and response is OK
		And action details are correct
		And UserA and UserB view the action
	When action is updated by UserA with Action1 label and response is Ok
		And action is retrieved and response is Ok
		And action details are correct
	When action is updated by UserB with Action2 label and response is Conflict
	Then the response message is