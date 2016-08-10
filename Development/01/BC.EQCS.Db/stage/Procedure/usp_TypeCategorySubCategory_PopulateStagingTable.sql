CREATE PROCEDURE [stage].[usp_TypeCategorySubCategory_PopulateStagingTable]
AS BEGIN

	/*
	Populate stage.TypeCategorySubCategory table

	Source : 
		http://intranet.britishcouncil.org/uk/sites/EQCS/Docs/10%20Requirements/Reference%20Data/Incident%20Type%20Category%20Sub%20Category%20Mapping.xlsx

	MERGE Update Procedure:
		1. Copy to clipboard the data from the spreadsheet source above to a regular expression capable tool such as Notepad++
		2. From the tool, replace the text using the following regular expression
			* Find: (.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)
			* Replace: \('$1','$2','$3','$4','$5','$6','$7','$8','$9','$10','$11','$12','$13','$14','$15','$16','$17','$18','$19'\),
		3. Replace '' with NULL
		4. Paste the results to the MERGE script below
	*/
	MERGE INTO [stage].[TypeCategorySubCategory] AS Target
		USING (
		VALUES 
			('CCOM','Candidate compliance','CANDID1','Early Exit',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('CCOM','Candidate compliance','CANDID2','Inappropriate/Incorrect ID',NULL,NULL,'LOW','LOW',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('CCOM','Candidate compliance','CANDID3','No ID',NULL,NULL,'LOW','LOW',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('CCOM','Candidate compliance','CANDID4','Non-Transfer of answers to OMR/WAS',NULL,NULL,'LOW','LOW',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','CLERIC1','Clerical Marker - Breach of COP/CU regulations',NULL,NULL,'MED','HIGH','NCT','1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','CLERIC2','Clerical Marker - Collusion with candidates',NULL,NULL,'HIGH','HIGH','NCT','1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','CLERIC3','Clerical Marker - Inappropriate behaviour',NULL,NULL,'MED','MED',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','EXAMIN1','Examiner - Breach of COP/CU regulations',NULL,NULL,'MED','HIGH','NCT','1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','EXAMIN2','Examiner - Collusion with candidates',NULL,NULL,'HIGH','HIGH','NCT','1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','EXAMIN3','Examiner - Conflict of Interest',NULL,NULL,'HIGH','HIGH',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','EXAMIN4','Examiner - Inappropriate Behaviour/Unfair Treatment',NULL,NULL,'MED','MED',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','EXAMIN5','Examiner - Removal of test materials',NULL,NULL,'HIGH','HIGH','NCT','1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','INVIGI1','Invigilation Staff - Breach of COP/CU regulations',NULL,NULL,'MED','MED','NCT','1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','INVIGI2','Invigilation Staff - Collusion with candidates',NULL,NULL,'HIGH','HIGH','NCT','1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','INVIGI3','Invigilation Staff - Conflict of Interest',NULL,NULL,'HIGH','HIGH',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('CSTA','Centre staff','INVIGI4','Invigilation Staff - Inappropriate Behaviour/Unfair Treatment',NULL,NULL,'MED','MED',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC',NULL,NULL,NULL,NULL,NULL,'1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','INTCAN','Internal Fraud - Candidate assisted by staff','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','MEMORI2','Memorised Script','LOW','LOW',NULL,'1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','NONCED','Non-standard invigilation - Collusion - Electronic Device','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','PNONCGA','Non-standard invigilation - Collusion - Giving Assistance','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','PNONCRA','Non-standard invigilation - Collusion - Receiving Assistance','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','NONIC','Non-standard invigilation - Copying','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','NONPED','Non-standard invigilation - Electronic device (incl mobile phone)','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','NONICI','Non-standard invigilation - Impersonation - Counterfeit/Tampered ID','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','NONIMP','Non-standard invigilation - Impersonation - Lookalike','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','NOIRS','Non-standard invigilation - In-room Identity Swap','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','NOPPN','Non-standard invigilation - Pre-prepared notes (non-test version specific)','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','NOIPPN','Non-standard invigilation - Pre-prepared notes (test-version specific)','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','STATIS','Statistically Improbable Result','HIGH','HIGH','NCT','1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','PRRC','PRRC','TECEADE','Test Centre Administrative Error','LOW','LOW',NULL,'1','1','0','0','0','1','1','1','0','1'),
			('INET','Investigation','VERIFI1','Verifications',NULL,NULL,NULL,NULL,NULL,'1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','AGEDTR','Aged TRF','LOW','LOW',NULL,'1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','CONIDE','Confirming - Impersonation - Counterfeit/Tampered ID','LOW','LOW',NULL,'1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','CONIMP','Confirming - Impersonation - Lookalike','LOW','LOW',NULL,'1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','CONFIR','Confirming previously cancelled scores','LOW','LOW',NULL,'1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','COUNTE','Counterfeit TRF','HIGH','HIGH','NCT','1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','GENUIN','Genuine result confirmed','LOW','LOW',NULL,'1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','IDPIU','IDP/IUSA centre','LOW','LOW',NULL,'1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','INTCAB','Internal Fraud - Staff assisting Candidate','HIGH','HIGH','NCT','1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','NONROR','Non RO request','LOW','LOW',NULL,'1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','NONCEE','Non-standard invigilation - Collusion - Electronic Device','HIGH','HIGH','NCT','1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','VNONCGA','Non-standard invigilation - Collusion - Giving Assistance','HIGH','HIGH','NCT','1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','VNONCRA','Non-standard invigilation - Collusion - Receiving Assistance','HIGH','HIGH','NCT','1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','NONICP','Non-standard invigilation - Copying','HIGH','HIGH','NCT','1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','NOICID','Non-standard invigilation - Impersonation - Counterfeit/Tampered ID','HIGH','HIGH','NCT','1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','NONIM','Non-standard invigilation - Impersonation - Lookalike','HIGH','HIGH','NCT','1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','NOIRSW','Non-standard invigilation - In-room Identity Swap','HIGH','HIGH','NCT','1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','STAKEH','Stakeholder Ceased Interaction','LOW','LOW',NULL,'1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','TESTCEN','Test Centre Administrative Error','MED','MED','SLF','1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','TRFTAM','TRF Tampering','HIGH','HIGH','NCT','1','1','1','1','0','1','1','1','1','1'),
			('INET','Investigation','VERIFI1','Verifications','VERIFI2','Verification Enquiry only','LOW','LOW',NULL,'1','1','1','1','0','1','1','1','1','1'),
			('OTHR','Other','OTHER','Other (see description)',NULL,NULL,NULL,NULL,NULL,'1','1','1','0','1','1','1','1','0','1'),
			('RPRO','Result processing','RESULT1','Results not released on time - Centre Fault',NULL,NULL,'HIGH','HIGH',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('RPRO','Result processing','RESULT2','Results not released on time - Not Centre Fault',NULL,NULL,'LOW','LOW',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('RPRO','Result Processing','RESULT3','UKVI IELTS/Life Skills Results not released within 28 days - Centre Fault',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('RPRO','Result Processing','RESULT4','UKVI IELTS/Life Skills Results not released within 28 days - Not Centre Fault',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('RPRO','Result Processing','RESULT5','Test Centre Administrative Error',NULL,NULL,'LOW','LOW','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','DVOICE1','Digital Voice Recording Failure - IELTS Speaking Test - Full',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','DVOICE2','Digital Voice Recording Failure - IELTS Speaking Test - Partial',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','DVOICE3','Digital Voice Recording Failure - Life Skills Test - Full',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','DVOICE4','Digital Voice Recording Failure - Life Skills Test - Partial',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','DVOICE5','Digital Voice Recording Failure - Life Skills Voice Sample',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','IAMCA1','IAM Camera, System or Process Failure (Contingency Process applied)',NULL,NULL,'LOW','LOW','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','IAMCA2','IAM Camera, System or Process Failure (Contingency Process not applied)',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','IAMFI','IAM Fingerscanner Failure',NULL,NULL,'MED','MED',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','IAMPUE','IAM Photo Upload Error',NULL,NULL,'LOW','LOW','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','ITNET','IT Network / Archived data',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','IWAS','IWAS',NULL,NULL,'MED','MED','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','ORS2','ORS',NULL,NULL,'MED','MED','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','VIDEO1','Video Recording Failure - Registration - Partial',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','VIDEO2','Video Recording Failure - Registration - Full',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','VIDEO4','Video Recording Failure - Speaking Test Room - Partial',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','VIDEO5','Video Recording Failure - Speaking Test Room - Full',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','VIDEO6','Video Recording Failure - Test Room - Partial',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('SYSE','Systems Equipment','VIDEO7','Video Recording Failure - Test Room - Full',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','COLLUS1','Collusion - Communicating with another candidate',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','COLLUS2','Collusion - Electronic Device',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','COLLUS3','Collusion - Giving Assistance',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','COLLUS4','Collusion - Receiving Assistance',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','COPYIN','Copying',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','IGNORI','Ignoring Invigilator Instructions - Talking, early QB opening, continuing to write, disruptive behaviour',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','IMPERS1','Impersonation - Counterfeit/Tampered ID',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','IMPERS2','Impersonation - Lookalike',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','INPOS1','In possession of pre-prepared notes (non test-version specific)',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','INPOS2','In possession of pre-prepared notes (test-version specific)',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','INPOS3','In possessing electronic device (including mobile phone)',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','INPOS4','In possession of prohibited item(s) (including watches)',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','INROO','In-room Identity Swap',NULL,NULL,'LOW','LOW','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','MEMORI1','Memorised script',NULL,NULL,'LOW','LOW',NULL,'1','1','1','0','1','1','1','1','0','1'),
			('TDCA','Test Day Candidate malpractice','REMOVI','Removing Test Materials/Content from Test Room',NULL,NULL,'MED','MED','NCT','1','1','1','0','1','1','1','1','0','1'),
			('TMAT','Test materials','INSUFF','Insufficient test materials',NULL,NULL,'MED','MED','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TMAT','Test materials','LIVET1','Live Test Materials lost/taken/stolen',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TMAT','Test materials','LIVET2','Live Test Materials opened before the test - but no leak',NULL,NULL,'MED','MED','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TMAT','Test materials','MATERI','Material Content leak',NULL,NULL,'HIGH','HIGH','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TMAT','Test materials','WRONG','Wrong test materials used',NULL,NULL,'MED','MED','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TLOC','Testing Locations','LISTEN1','Listening Test - Sound System/CD Fault',NULL,NULL,'MED','MED','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TLOC','Testing Locations','CLOSINO','Opening/Moving/Closing Test Location - outside policy',NULL,NULL,'MED','MED','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TLOC','Testing Locations','CLOSINW','Opening/Moving/Closing Test Location - within policy',NULL,NULL,'LOW','LOW','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TLOC','Testing Locations','DISRUP1','Test Disruption - Fire Alarm, Bomb Threat etc.',NULL,NULL,'LOW','LOW','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TLOC','Testing Locations','TESTSE1','Test Session Cancellation',NULL,NULL,'LOW','LOW','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TLOC','Testing Locations','TESTSE2','Test Session Cancellation UKVI - outside policy',NULL,NULL,'MED','MED','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TLOC','Testing Locations','TESTSE3','Test Session Cancellation UKVI - within policy',NULL,NULL,'LOW','LOW','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TLOC','Testing Locations','VENUE1','Test Venue not available on Test Day',NULL,NULL,'MED','MED','SLF','1','1','1','0','1','1','1','1','0','1'),
			('TLOC','Testing Locations','VENUE2','Test Venue not to standard',NULL,NULL,'LOW','LOW','SLF','1','1','1','0','1','1','1','1','0','1')


		) AS Source (
			[TypeCode],
			[TypeName],
			[CategoryCode],
			[CategoryName],
			[SubCategoryCode],
			[SubCategoryName],
			[RiskRating], 
			[ResidualRiskRating], 
			[UkviImmediateReportType],

			[Raise_GO],
			[Raise_RMT],
			[Raise_CCT],
			[Raise_VT],
			[Raise_TC],

			[View_GO],
			[View_RMT],
			[View_CCT],
			[View_VT],
			[View_TC])

		ON (Target.[TypeCode] = Source.[TypeCode] AND Target.[CategoryCode] = Source.[CategoryCode] AND ISNULL(Target.[SubCategoryCode], 'XX') = ISNULL(Source.[SubCategoryCode], 'XX'))

		WHEN MATCHED THEN 
			UPDATE SET 
				[TypeName] = Source.[TypeName],
				[CategoryName] = Source.[CategoryName],
				[SubCategoryName] = Source.[SubCategoryName],
				[RiskRating] = Source.[RiskRating],
				[ResidualRiskRating] = Source.[ResidualRiskRating],
				[UkviImmediateReportType] = Source.[UkviImmediateReportType],

				[Raise_GO] = Source.[Raise_GO],
				[Raise_RMT] = Source.[Raise_RMT],
				[Raise_CCT] = Source.[Raise_CCT],
				[Raise_VT] = Source.[Raise_VT],
				[Raise_TC] = Source.[Raise_TC],

				[View_GO] = Source.[View_GO],
				[View_RMT] = Source.[View_RMT],
				[View_CCT] = Source.[View_CCT],
				[View_VT] = Source.[View_VT],
				[View_TC] = Source.[View_TC]
			
		-- insert new rows 
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT ([TypeCode],[TypeName],[CategoryCode],[CategoryName],[SubCategoryCode],[SubCategoryName],[RiskRating],[ResidualRiskRating],[UkviImmediateReportType],[Raise_GO],[Raise_RMT],[Raise_CCT],[Raise_VT],[Raise_TC],[View_GO],[View_RMT],[View_CCT],[View_VT],[View_TC])
			VALUES ([TypeCode],[TypeName],[CategoryCode],[CategoryName],[SubCategoryCode],[SubCategoryName],[RiskRating],[ResidualRiskRating],[UkviImmediateReportType],[Raise_GO],[Raise_RMT],[Raise_CCT],[Raise_VT],[Raise_TC],[View_GO],[View_RMT],[View_CCT],[View_VT],[View_TC])

		-- delete rows that are in the target but not the source 
		WHEN NOT MATCHED BY SOURCE THEN 
			DELETE;

END 
