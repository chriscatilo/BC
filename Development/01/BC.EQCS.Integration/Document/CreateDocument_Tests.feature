Feature: CreateDocument_Tests
	File should not be loaded if file format is not pdf, doc, docx or jpg
	File should not be loaded if it is empty
	File should not be loaded if its size is greater than 10mb
	If file uploaded return the URI

Scenario: Uploading a Valid Document
	Given A Valid Document
	When I press upload 'ValidDocument'
	Then I should get a valid url

Scenario: Should Throw Exception Uploading a Valid Document That Exceeds 10mb Size
	Given A Valid Document That Exceeds Ten MB Size
	When I press upload 'ValidDocumentExceeds10MB'
	Then I should get an exception 'ValidDocumentExceeds10MB'

Scenario: Should Throw Exception Uploading a InValid Document That Exceeds 10mb Size
	Given A InValid Document That Exceeds Ten MB Size
	When I press upload 'InValidDocumentExceeds10MB'
	Then I should get an exception 'InValidDocumentExceeds10MB'

Scenario: Should Not Throw Exception If Document type is DOC
	Given A Valid Document of Type 'DOC'
	When I press upload 'DOC'
	Then I should get a valid url

Scenario: Should Not Throw Exception If Document type is DOCX
	Given A Valid Document of Type 'DOCX'
	When I press upload 'DOCX'
	Then I should get a valid url

Scenario: Should Not Throw Exception If Document type is PDF
	Given A Valid Document of Type 'PDF'
	When I press upload 'PDF'
	Then I should get a valid url

Scenario: Should Not Throw Exception If Document type is JPG
	Given A Valid Document of Type 'JPG'
	When I press upload 'JPG'
	Then I should get a valid url

Scenario: Should Throw Exception If Document type is DOC and Empty
	Given A Valid Empty Document of Type 'DOC'
	When I press upload 'Empty-DOC'
	Then I should get an exception 'Empty-DOC'

Scenario: Should Throw Exception If Document type is DOCX and Empty
	Given A Valid Empty Document of Type 'DOCX'
	When I press upload 'Empty-DOCX'
	Then I should get an exception 'Empty-DOCX'

Scenario: Should Throw Exception If Document type is PDF and Empty
	Given A Valid Empty Document of Type 'PDF'
	When I press upload 'Empty-PDF'
	Then I should get an exception 'Empty-PDF'

Scenario: Should Throw Exception If Document type is JPG and Empty
	Given A Valid Empty Document of Type 'JPG'
	When I press upload 'Empty-JPG'
	Then I should get an exception 'Empty-JPG'
	
Scenario: Should Throw Exception If Document Name Too Long
	Given A Valid Document of Type 'PDF' And FileName Too Long
	When I press upload 'PDF'
	Then I should get an exception 'FileName-Too-Long'
