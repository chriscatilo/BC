Feature: RemoveDocument


Scenario: Can I remove an existing Document
	Given A Valid Document
	When I press upload 'ValidDocument'
	Then I should get a valid url
	When I try to get the above saved document
	Then I should get back the above saved document
	When I remove file
#
#Scenario: Can I get a non existing Document
#	Given A Valid Document
#	When I press upload 'ValidDocument'
#	Then I should get a valid url
#	When I try to get the above saved document
#	Then I should get back the above saved document
#	When I remove file
#	And I try to get the removed document
#	Then I should get response NotFound
