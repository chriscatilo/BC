Feature: IncidentRaceConditions
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
	Given table of incidents to persist
	| Test Label    | TestCentre | TestLocation  | Category | SubCategory | IncidentDate | IncidentTime | Description                            | ImmediateActionTaken                              | RiskRating | Product   | RaisedBy     | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry | ReferringOrganisation | ReferringOrgExists | NoOfCandidates |
	| Draft         | GBS03      | UKVI-GBS03-84 | VERIFI1  | VERIFI2     | 2015-03-01   | NULL         | gen.paragraph(5,10,5,10;Draft)         | gen.paragraph(5,10,5,10;DraftImmedAction)         | NULL       | NULL      | NULL         | NULL       | NULL       | NULL                | NULL                   | NULL                 | NULL              | NULL             | NULL                | NULL                  | NULL               | NULL           |
	| UpdateByUserA | GBS02      | UKVI-GBS02-87 | VERIFI1  | AGEDTR      | 2015-03-01   | 11:00        | gen.paragraph(5,10,5,10;UpdateByUser1) | gen.paragraph(5,10,5,10;UpdateByUser1ImmedAction) | MED        | IELTSLSA1 | Someone Else | 2015-03-02 | False      | Whitford            | Karen                  | CEO                  | karen@another.org | EDU              | US                  | BC                    | True               | 2              |
	| UpdateByUserB | GBS03      | UKVI-GBS03-84 | VERIFI1  | VERIFI2     | 2015-03-01   | 5:00         | gen.paragraph(5,10,5,10;UpdateByUser2) | gen.paragraph(5,10,5,10;UpdateByUser2ImmedAction) | NULL       | NULL      | NULL         | NULL       | NULL       | NULL                | NULL                   | NULL                 | NULL              | NULL             | NULL                | NULL                  | NULL               | 10             |

	Given table of incidents to view
	| Test Label    | IELTSRegion | TestCentre                           | TestLocation | Category      | SubCategory               | IncidentDate | IncidentTime | Description                   | ImmediateActionTaken                     | RiskRating | RaisedDate | Product | RaisedBy     | TestDate   | ReportUkvi | ReferringOrgSurname | ReferringOrgFirstnames | ReferringOrgJobTitle | ReferringOrgEmail | ReferringOrgType | ReferringOrgCountry      | ReferringOrgName | TestCentreName                 | TestCentreNumber | NoOfCandidates | IsUkvi | IncidentClassCode |
	| Draft         | UKIA        | GBS03 Anglia Ruskin University       | Chelmsford   | Verifications | Verification Enquiry only | 2015-03-01   | NULL         | gen.paragraph(;Draft)         | gen.paragraph(;DraftImmedAction)         | NULL       | NULL       | NULL    | NULL         | NULL       | NULL       | NULL                | NULL                   | NULL                 | NULL              | NULL             | NULL                     | NULL             | Anglia Ruskin University       | GBS03            | NULL           | NULL   | VERIFI2           |
	| UpdateByUserA | UKIA        | GBS02 Ealing, Hammersmith And West L | London West  | Verifications | Aged TRF                  | 2015-03-01   | 11:00        | gen.paragraph(;UpdateByUser1) | gen.paragraph(;UpdateByUser1ImmedAction) | Medium     | 2015-03-01 | LS A1   | Someone Else | 2015-03-02 | False      | Whitford            | Karen                  | CEO                  | karen@another.org | Education        | United States of America | British Council  | Ealing, Hammersmith And West L | GBS02            | 2              | True   | AGEDTR            |

Scenario: User A and B updates successfully
	Given incident labeled Draft
	When new incident is saved and response is Ok
		And the incident is retrieved by UserA where response is Ok
		And the incident details are correct
	When UserA modifies the retrieved incident UpdateByUserA
		And incident is saved and response is Ok
		And the incident is retrieved by UserA where response is Ok
		And the incident details are correct
		And the incident is retrieved by UserB where response is Ok
		And the incident details are correct
	When UserB modifies the retrieved incident UpdateByUserB
		And incident is saved
		Then response is ok

Scenario: User A and B updates unsuccessfully
	Given incident labeled Draft
	When new incident is saved and response is Ok
		And the incident is retrieved by UserA and UserB where response is Ok
		And the incident details are correct
	When UserA modifies the retrieved incident UpdateByUserA
		And incident is saved and response is Ok
		And the incident is retrieved by UserA where response is Ok
		And the incident details are correct
	When UserB modifies the retrieved incident UpdateByUserB
		And incident is saved
		Then response is Conflict
		And the response message is

Scenario: User A updates and UserB raise incident unsuccessfully
	Given incident labeled Draft
	When new incident is saved and response is Ok
		And the incident is retrieved by UserA and UserB where response is Ok
		And the incident details are correct
	When UserA modifies the retrieved incident UpdateByUserA
		And incident is saved and response is Ok
		And the incident is retrieved by UserA where response is Ok
		And the incident details are correct
	When UserB dones not modifiy the inciedent
		And incident is raised
		Then response is Conflict
		And the response message is

Scenario: User A updates and UserB updates + raise incident unsuccessfully
	Given incident labeled Draft
	When new incident is saved and response is Ok
		And the incident is retrieved by UserA and UserB where response is Ok
		And the incident details are correct
	When UserA modifies the retrieved incident UpdateByUserA
		And incident is saved and response is Ok
		And the incident is retrieved by UserA where response is Ok
		And the incident details are correct
	When UserB modifies the retrieved incident UpdateByUserB
		And incident is raised
		Then response is Conflict
		And the response message is

# TODO: Naushad Malik
#Scenario: User A updates and UserB updates + accept incident unsuccessfully
#	Given incident labeled Draft
#	When new incident is saved and response is Ok
#		And the incident is retrieved by UserA and UserB where response is Ok
#		And the incident details are correct
#	When UserA modifies the retrieved incident UpdateByUserA
#		And incident is raised
#		#And incident is saved and response is Ok
#		And the incident is retrieved by UserA where response is Ok
#		And the incident details are correct
#	When UserB modifies the retrieved incident UpdateByUserB
#		And incident is accepted
#		Then response is Conflict
#		And the response message is
