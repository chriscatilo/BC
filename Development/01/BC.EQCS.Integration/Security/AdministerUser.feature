Feature: Administer User

Background:
	Given table of users roles to admin unit
	| Name          | Role              | AdminUnit |
	| Carey Nolette | GLOBAL_OPERATIONS | ROOT      |
	| Karey Nolete  | RMT               | GB        |
	| nulls         | NULL              | NULL      |

@ignore
Scenario: Save User Successfully
	Given user named Carey Nolette
	When new user is saved and response is Ok
		And user is retrieved and response is Ok
	Then user details are correct
		And user has expected provisions
		
@ignore
Scenario: Save User Unsuccessfully
	Given user named nulls
	When new user is saved
		And response is BadRequest	
		
@ignore
Scenario: Update User Successfully
	Given user named Carey Nolette
		And new user is saved and response is Ok
		And user is modified with Karey Nolete
	When user is saved and response is Ok
		And user is retrieved and response is Ok
	Then user details are correct
		And user has expected provisions
		
@ignore
Scenario: Update User Unsuccessfully
	Given user named Carey Nolette
		And new user is saved and response is Ok
		And user is modified with null
	When user is saved
	Then response is BadRequest