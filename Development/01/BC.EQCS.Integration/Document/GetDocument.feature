Feature: GetDocument
		If file uploaded return the URI

@mytag
Scenario: Can I retrieve an existing Document
	Given A Valid Document
	When I press upload 'ValidDocument'
	Then I should get a valid url
	When I try to get the above saved document
	Then I should get back the above saved document

Scenario: Can I retrieve Non Existing Document
	Given A InValid URI
	When I try to get Non Existing Document
	Then I should get nothing