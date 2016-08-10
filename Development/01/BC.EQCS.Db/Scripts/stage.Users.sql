-- WARNING: technicians only
MERGE [stage].[Users] AS Target
	USING (
	VALUES 
	
		-- email: eqcstestuser@gmail.com, password: Passw0rdz1

		-- When creating external users, use this password hash
		-- External User Password: Passw0rdz1
		-- External User Password Hash: ABLzq4leJPEWBbhrFs2UQ8b2eQK1+YfmhNO9SdE3op/7h8/8CRrpfnabUo5eK/B6qA==

		-- System & Test Users 
		('5878C85F-AE65-423D-8AF7-44C185BD3633', 'System Monitor', 'System-Monitor', 'Ravi.Puri@britishcouncil.org', 'ANONYMOUS', 'SYSTEM_MONITOR', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('e6cf4819-1afc-4c57-9126-ab84a8a01425', 'Ravi', 'Puri', 'Ravi.Puri@britishcouncil.org', 'thesysadmin', 'GLOBAL_OPERATIONS', 'ROOT',  NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('8EF69F90-A0D8-4A7C-8EF3-A274D0E374FA', 'Build', 'User', 'Build.User@britishcouncil.org', '_DinoAntonello', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),		
		('46D342D1-E8B0-49D2-9F0D-73105CB153B3', 'External', 'User1', 'puri.ravi@gmail.com', 'ExternalUser1', 'GLOBAL_OPERATIONS', 'ROOT', 'externaluser1', 'AHiF88Q0M2eTFPynPzjdDz/I9wsjwBZk3OAzUM2wY11ylRhImwm07YE8R6e128wq+A==', '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('0516786E-DCC8-4ED4-BEDD-7079D0C70F33', 'EQCS', 'TestUser', 'eqcstestuser@gmail.com', 'eqcstestuser', 'GLOBAL_OPERATIONS', 'ROOT', 'eqcstestuser', 'ABLzq4leJPEWBbhrFs2UQ8b2eQK1+YfmhNO9SdE3op/7h8/8CRrpfnabUo5eK/B6qA==', '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('5701ADF4-5D20-47DD-B008-94A58EC519EE', 'Admin', 'User', 'eqcstestuser@gmail.com', 'adminuser', 'GLOBAL_OPERATIONS', 'ROOT', 'adminuser', 'ABLzq4leJPEWBbhrFs2UQ8b2eQK1+YfmhNO9SdE3op/7h8/8CRrpfnabUo5eK/B6qA==', '40D54FD8-9940-4090-B393-59EF885BDB83'),

		-- EQCS Team
		('44EFEB5A-8EBC-4736-A433-4E4B18B55B8F', 'Andrew', 'Dawson', 'Andrew.Dawson@britishcouncil.org', 'AndrewDawson', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('44EFEB5A-8EBC-4736-A433-4E4B18B55B8F', 'Andrew', 'Dawson', 'Andrew.Dawson@britishcouncil.org', 'AndrewDawson', 'TCS', 'MX030', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('D926E947-78C1-4797-B76E-3806E68F332D', 'Ankur', 'Srivastava', 'Ankur.Srivastava@britishcouncil.org', 'AnkurSrivastava', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('EB777078-0FED-4D4D-A602-C6AC7AA9B403', 'Bryan', 'Brookes Smith', 'Bryan.BrookesSmith@britishcouncil.org', 'BryanBrookesSmith', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('EB777078-0FED-4D4D-A602-C6AC7AA9B403', 'Bryan', 'Brookes Smith', 'Bryan.BrookesSmith@britishcouncil.org', 'BryanBrookesSmith', 'TCS', 'FR585', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('A683B25F-6A04-4A5D-BEBC-BCAF27044A5D', 'Christopher', 'Catilo', 'Christopher.Catilo@britishcouncil.org', 'ChristopherCatilo', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('A683B25F-6A04-4A5D-BEBC-BCAF27044A5D', 'Christopher', 'Catilo', 'Christopher.Catilo@britishcouncil.org', 'ChristopherCatilo', 'TCS', 'HK001', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('94363030-C175-4935-B9CC-B0C431E56D90', 'Dawn', 'Smith', 'Dawn.Smith@britishcouncil.org', 'DawnSmith', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('3DD7C502-B15D-45D6-8576-5B1C98592967', 'Dino', 'Antonello', 'Dino.Antonello@britishcouncil.org', 'DinoAntonello', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('3DD7C502-B15D-45D6-8576-5B1C98592967', 'Dino', 'Antonello', 'Dino.Antonello@britishcouncil.org', 'DinoAntonello', 'RMT', 'GBS02', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),	
		('53B2C0A8-0554-4E63-A976-7239BA6024BD', 'Giles', 'Morris', 'Giles.Morris@britishcouncil.org', 'GilesMorris', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),		
		('C1F24FFE-03BA-41A7-8230-7EBB180B9084', 'Louise', 'Parker', 'Louise.Parker@britishcouncil.org', 'LouiseParker', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('A8EEBD53-C88C-4883-97CC-1FA22851824D', 'Naushad', 'Malik', 'Naushad.Malik@britishcouncil.org', 'NaushadMalik', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('03F30E9A-447C-4917-BD6F-7FAC8B081479', 'Nosheen', 'Akram', 'Nosheen.Akram@britishcouncil.org', 'NosheenAkram', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('BA9BBFC1-715D-40DE-83ED-9E20A704988A', 'Philip', 'Street', 'Philip.Street@britishcouncil.org', 'PhilipStreet', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('BA9BBFC1-715D-40DE-83ED-9E20A704988A', 'Philip', 'Street', 'Philip.Street@britishcouncil.org', 'PhilipStreet', 'TCS', 'AMESA1', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('BA9BBFC1-715D-40DE-83ED-9E20A704988A', 'Philip', 'Street', 'Philip.Street@britishcouncil.org', 'PhilipStreet', 'TCS', 'AE001', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('1054F5B9-F225-4941-A94A-38A9FECD0AD8', 'Ravi Other', 'Puri Other', 'Ravi.Puri@britishcouncil.org', 'RaviPuri', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
		('27696666-C206-4256-8A1F-1DE2F4066C94', 'Vaibhav', 'Srivastava', 'Vaibhav.Srivastava@britishcouncil.org', 'VaibhavSrivastava', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83')

		) AS Source ([ObjectGUID],[FirstName],[Surname],[Email],[Login],[Role],[AdminUnit], [ExternalUserName], [ExternalUserPasswordHash], [ExternalUserSecurityStamp])
	ON (
		Target.[ObjectGUID] = Source.[ObjectGUID] AND 
		Target.[Role] = Source.[Role] AND
		Target.[AdminUnit] = Source.[AdminUnit]
	   )

	WHEN MATCHED THEN 
		UPDATE SET 
			[Login] = Source.[Login],
			[FirstName] = Source.[FirstName],
			[Surname] = Source.[Surname],
			[Email] = Source.[Email],
			[Role] = Source.[Role],
			[AdminUnit] = Source.[AdminUnit],
			[ExternalUserName] = Source.[ExternalUserName]
			
			-- WARNING: Never-ever-ever update the external password hash & stamp
			--[ExternalUserPasswordHash] = Source.[ExternalUserPasswordHash],
			--[ExternalUserSecurityStamp] = Source.[ExternalUserSecurityStamp]
			
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([ObjectGUID], [FirstName],[Surname],[Email],[Login],[Role],[AdminUnit],[ExternalUserName],[ExternalUserPasswordHash], [ExternalUserSecurityStamp])
		VALUES ([ObjectGUID], [FirstName],[Surname],[Email],[Login],[Role],[AdminUnit],[ExternalUserName],[ExternalUserPasswordHash], [ExternalUserSecurityStamp])

	-- delete rows that are in the target but not the source 
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;

DECLARE @DB NVARCHAR(128)
SELECT @DB = DB_NAME();

-- ALL OTHER USERS
IF (@DB = 'EQCS_UAT') BEGIN

	MERGE [stage].[Users] AS Target
		USING (
		VALUES 
			('86736F4A-7F03-4101-8B4E-F4FD9C797995', 'Ade', 'Dehinbo', 'ade.dehinbo@britishcouncil.org', 'AdeDehinbo', 'TCS', 'CY006', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('EDDE8B3D-1748-414E-AD27-A6C26DAD2431', 'Aidan', 'Walters', 'Aidan.Walters@britishcouncil.org', 'AidanWalters', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),	
			('2E4999BC-0275-4F62-AA09-912FE37967B7', 'Alexander', 'Ramjing', 'Alexander.Ramjing@britishcouncil.org', 'AlexanderRamjing', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('A638F57A-578E-4F2F-968A-E6D6D8E2C70B', 'Andy', 'Curtis-Brignell', 'Andy.CurtisBrignell@britishcouncil.org', 'AndyCurtisBrignell', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('C8415115-5EF4-40C1-829E-BF5243FC9D6B', 'Anusha', 'Kumar', 'anusha.kumar@britishcouncil.org', 'AnushaKumar', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('8AE8F7C3-485B-4EA7-BF2B-FA01260FDB99', 'Ben', 'Gilligan', 'Ben.Gilligan@britishcouncil.org', 'BenGilligan', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('198C3E19-CBA9-4D7E-85EE-FA9537503154', 'Ben', 'Preston', 'Ben.Preston@britishcouncil.org', 'BenPreston', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),  
			('736D004A-612C-43BA-8F7A-DE31395D4A09', 'Borislav', 'Lazarov', 'Borislav.Lazarov@britishcouncil.org', 'BorislavLazarov', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('E85E7687-B60E-42FC-9BC4-068360E8825A', 'Elijah', 'Sopade', 'elijah.sopade@britishcouncil.org', 'ElijahSopade', 'TCS', 'IN100', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),	
			('5C8DE414-61D5-4BB1-B926-21FAB93E3DC2', 'Gavin', 'Mather', ' Gavin.Mather@britishcouncil.org', 'GavinMather', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('52612BEC-EE0F-47F4-AA20-13CDAC7C6C1D', 'Harpal', 'Thiara', 'Harpal.Thiara@britishcouncil.org', 'HarpalThiara', 'RMT', 'UKIA', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('932FDD15-5108-4158-B553-33ED05DA5484', 'Jake', 'Hassall', 'Jake.Hassall@britishcouncil.org', 'JakeHassall', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('666E053C-82C2-4014-90A3-4EDE66D02BB1', 'James', 'Allison', 'James.Allison@britishcouncil.org', 'JamesAllison', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('B41FB4A9-7594-41B6-8B20-ED576203B773', 'Jonathan', 'Bolton', 'Jonathan.Bolton@britishcouncil.org', 'JonathanBolton', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('5BAE91DD-BC5C-4257-AA65-264E676DD05E', 'Karen', 'Flowers Matini', 'karen.flowersmatini@britishcouncil.org', 'KarenFlowers Matini', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('BF597044-E713-4289-A599-6DAF14331804', 'Kritika', 'Kenth', 'kritika.kenth@britishcouncil.org', 'KritikaKenth', 'RMT', 'UKIA', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('0F938E33-23E7-4F97-B0F5-344C3B4EA200', 'Laurence', 'Taylor', 'Laurence.Taylor@britishcouncil.org', 'LaurenceTaylor', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),		
			('3249E880-D969-4CF5-BC29-74B7F54BE247', 'Maggie', 'Lister', 'Maggie.Lister@britishcouncil.org', 'MaggieLister', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),	
			('FB632BF9-0DFA-4CD2-92B5-2C4A09D31271', 'Megan', 'Agnew', 'megan.agnew@britishcouncil.org', 'MeganAgnew', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),			
			('5EC3CD94-E533-4274-B26C-81270F7A6968', 'Neil', 'Roberts', 'Neil.Roberts@britishcouncil.org', 'NeilRoberts', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),	
			('D1F2A85A-EA31-4370-9290-19002683CD45', 'Paula', 'McCafferty', 'Paula.McCafferty@britishcouncil.org', 'PaulaMcCafferty', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('A652E0AA-799D-476D-B581-9DCDA37FC32F', 'Samuel', 'Naylor', 'Samuel.Naylor@britishcouncil.org', 'SamuelNaylor', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),		
			('FEC3A63B-1C84-4292-9EC1-99D24CFDD80A', 'Steve', 'Copeland', 'steve.copeland@britishcouncil.org', 'SteveCopeland', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('B16CAA38-6D04-4A24-8C32-EC83DB78E29F', 'Sunny', 'Chohan', 'Sunny.Chohan@britishcouncil.org', 'SunnyChohan', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),	
			('CA9DDFD3-D543-4756-AD9E-866F864D2C15', 'Vicki', 'Beal', 'Vicki.Beal@britishcouncil.org', 'VickiBeal', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),								
			('604B16EB-DC88-47EC-A1CE-C9DFB9E63222', 'Anuja', 'Kumar', 'Anuja.kumar@britishcouncil.org', 'AnujaKumar', 'TCS', 'IN002', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),	
			('1428F105-2E02-41A6-A1DB-166075FF5466', 'Eda', 'Akbulut', 'eda.akbulut@britishcouncil.org.tr', 'EdaAkbulut', 'TCS', 'TR002', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),		
			('2E04FED3-7B9B-4743-BC80-4115B41B67A1', 'Hu', 'Hebe', 'Hu.hebe@britishcouncil.org.cn', 'HebeHu', 'TCS', 'CN001', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('ECAA9C1B-537F-4315-A766-74D360BE784E', 'Jiang', 'Xin', 'Jiang.Xin@britishcouncil.org.cn', 'JiangXin', 'CCT', 'CN001', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('C30AE44E-6C55-40BF-B6C3-39CE4397AB89', 'Jyotika', 'sharma', 'Jyotika.sharma@britishcouncil.org', 'JyotiKasharma', 'TCS', 'IN100', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),						
			('2E8231C0-F0D2-4D6F-9E8C-1138867C9A16', 'Laura', 'Huang', 'laura.huang@britishcouncil.org.cn ', 'LauraHuang', 'TCS', 'CN002', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('58915FD7-153B-46CE-BDF9-1DF91BFB00F0', 'Lorraine', 'Niblett', 'Lorraine.niblett@britishcouncil.org', 'LorraineNiblett', 'TCS', 'IN001', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('70CC3F41-AC5F-4FA5-B3E2-B4983DD5A7A9', 'Pauline', 'Sun', 'pauline.sun@britishcouncil.org.cn', 'PaulineSun', 'TCS', 'CN004', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('50EF83AB-025C-4832-BD18-721DFE93C85F', 'Penny', 'Karadima', 'penny.karadima@britishcouncil.gr', 'PennyKaradima', 'TCS', 'GR005', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('E274F8EA-61EC-44AB-9365-4E09F0E4E51D', 'Saurabh', 'Sabharwal', 'Saurabh.sabharwal@britishcouncil.org', 'SaurabhSabharwal', 'TCS', 'IN120', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),		
			('622D466F-980E-40E3-96DC-0C2C99044657', 'Sharon', 'meyne', 'Sharon.meyne@britishcouncil.org', 'Sharonmeyne', 'CCT', 'IN001', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),																												
			('336F15DF-10D2-49F4-8FE7-A55FE110626C', 'Alouis', 'Zengenene', 'Alouis.Zengenene@britishcouncil.org.za', 'AlouisZengenene', 'TCS', 'ZA001', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),			
			('D3394523-D3CF-4E3D-AE6B-508340C34FC3', 'Catherine', 'Chan', 'Catherine.Chan@britishcouncil.org', 'CatherineChan', 'RMT', 'SEA', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),			
			('165FDF78-8B03-49D8-8D3F-5E43F43A6F4E', 'Edward', 'Law', 'edward.law@britishcouncil.org', 'EdwardLaw', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('B688AF01-9DF8-4C25-BA1B-3D0C10848130', 'Fiona', 'Mason', 'Fiona.Mason@britishcouncil.org', 'FionaMason', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('F167DC08-4BBD-452D-9B97-D9A2964F211C', 'Florence', 'Koh', 'Florence.Koh@britishcouncil.org.my', 'FlorenceKoh', 'RMT', 'SEA', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('F167DC08-4BBD-452D-9B97-D9A2964F211C', 'Florence', 'Koh', 'Florence.Koh@britishcouncil.org.my', 'FlorenceKoh', 'TCS', 'AE001', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('7458FB17-F672-4C41-93BA-4D59FEBDC7F5', 'Gaurav', 'Arora', 'gaurav.arora@britishcouncil.org', 'gauravarora', 'RMT', 'AMESA2', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('67E142E6-BB1D-4AEE-B844-26E42AF9DA65', 'Gerlinde', 'Heindl', 'Gerlinde.Heindl@britishcouncil.at', 'GerlindeHeindl', 'RMT', 'EUROPE', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('AA90E2C5-81A5-4E69-9610-928EA6223B49', 'Hareena', 'Kenth', 'hareena.kenth@britishcouncil.org', 'HareenaKenth', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),			
			('D4282EF6-19FC-40EE-B2F9-609D8C4E8EE4', 'Harun', 'Rashid', 'Harun.Rashid@bd.britishcouncil.org', 'HarunRashid', 'RMT', 'AMESA1', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),			
			('77245625-B53D-484C-BD7F-318EB933ADE0', 'Isabel', 'Gowda', 'Isabel.Gowda@britishcouncil.de', 'IsabelGowda', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('1B8F6661-65D6-4DCB-8373-DC7DCFC4A280', 'Jacob', 'Bickford', 'jacob.bickford@britishcouncil.org', 'JacobBickford', 'TCS', 'AE110', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),						
			('8D9C8AC6-E34F-4D76-8333-76FDEB9632A9', 'Krystyna-Maria', 'Chrzanowska', 'KrystynaMaria.Chrzanowska@britishcouncil.org', 'krystynamariac', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),						
			('95D9751B-CA68-4B0E-8FFF-DA0112CF55D9', 'Maria', 'Ellwood', 'Maria.Ellwood@britishcouncil.org', 'MariaEllwood', 'RMT', 'UKIA', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('E43342BD-6C65-4371-8B7B-DDB3E041D5A4', 'MayWin', 'Than', 'MayWin.Than@britishcouncil.org', 'MayWinThan', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),			
			('1FF96E07-2F1D-494E-AC69-14DC8DAFA3A9', 'Nadine', 'Asiedu-Dankwa', 'nadine.asiedudankwa@britishcouncil.org', 'NadineAsiedudankwa', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),			
			('0FF82293-2178-4673-B0B4-F096BC8F4070', 'Nizamul', 'Hoque', 'Nizamul.Hoque@britishcouncil.org', 'NizamulHoque', 'RMT', 'UKIA', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),						
			('DABD57E3-7BC6-4AFF-90D1-EFBC81D0C1FD', 'Phyllis', 'Luo', 'Phyllis.Luo@britishcouncil.org.cn ', 'PhyllisLuo', 'TCS', 'CN172', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('28BBCBB1-942F-4719-BBD4-248A8DD665B5', 'Powel', 'Ren', 'Powel.Ren@britishcouncil.org.cn', 'PowelRen', 'RMT', 'SEA', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('4BB89B4D-4A53-4748-8C26-39F6F336B082', 'Richard', 'Halstead', 'Richard.Halstead@britishcouncil.org', 'RichardHalstead', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('0F96593B-2CD5-4CF9-8407-F5E08A707405', 'Saima', 'Ehtesham', 'Saima.Ehtesham@ae.britishcouncil.org', 'SaimaEhtesham', 'RMT', 'AMESA2', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('2B91D080-A897-4171-87FF-4A72F601B61B', 'Saima', 'Satti', 'saima.satti@britishcouncil.org', 'SaimaSatti', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('E705262B-D8F1-48E7-9523-9E7FD48E9BAC', 'Sally', 'Dimitri', 'Sally.Dimitri@britishcouncil.org', 'SallyDimitri', 'RMT', 'AMESA1', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),			
			('E539D548-FB73-42BD-8644-F77840903DCC', 'Sheraz', 'Alvi', 'sheraz.alvi@britishcouncil.org', 'SherazAlvi', 'GLOBAL_OPERATIONS', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),			
			('8CDEFEA9-C995-41DF-973C-787F24DE0ABC', 'Tariq', 'Rao', 'Tariq.Rao@britishcouncil.org', 'TariqRao', 'RMT', 'AMESA1', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('E0D39EF5-4DC5-409B-96DE-5D60A14BAE96', 'Uyen', 'Tran', 'Uyen.Tran@britishcouncil.org', 'UyenTran', 'RMT', 'EUROPE', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('513C4B84-F339-4DAA-BE71-33796E0A10FD', 'Vincenzo', 'DeCostanzo', 'Vincenzo.DeCostanzo@britishcouncil.it', 'VincenzoDeCostanzo', 'RMT', 'EUROPE', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('513C4B84-F339-4DAA-BE71-33796E0A10FD', 'Vincenzo', 'DeCostanzo', 'Vincenzo.DeCostanzo@britishcouncil.it', 'VincenzoDeCostanzo', 'TCS', 'IE', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('B9D444C7-190C-4621-B692-F6CCE96D1919', 'Xin', 'Gu', 'Xin.Gu@britishcouncil.org', 'XinGu', 'RMT', 'UKIA', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),												
			('B7C93958-5D5B-40AE-8129-B840C8A9EE94', 'David', 'Cheney', 'David.Cheney@britishcouncil.org', 'DavidCheney', 'VT', 'ROOT', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('c5c824b7-f837-4874-bbc9-c724e4cdbde9', 'Liu', 'Ying', 'LIU.Ying@britishcouncil.org.cn', 'LiuYing', 'CCT', 'CN001', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('ea22e73d-5416-4f58-a21e-b16de2572337', 'Murat', 'Altintas', 'murat.altintas@britishcouncil.org.tr', 'MuratAltintas', 'TCS', 'TR002', NULL, NULL, '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('526816D8-122B-467C-A3FA-5167287D3ECE', 'Michelle', 'Ford', 'michelle.ford@englishcanada.org', 'MichelleFord', 'TCS', 'CA030', 'MichelleFord', 'ABLzq4leJPEWBbhrFs2UQ8b2eQK1+YfmhNO9SdE3op/7h8/8CRrpfnabUo5eK/B6qA==', '40D54FD8-9940-4090-B393-59EF885BDB83'),
			('E697BC1C-55DA-45AB-B008-A1A33CA83D53', 'Carina', 'Peigne', 'Carina.Peigne@britishcouncil.org', 'CarinaPeigne', 'GLOBAL_OPERATIONS', 'ROOT', 'CarinaPeigne', 'ABLzq4leJPEWBbhrFs2UQ8b2eQK1+YfmhNO9SdE3op/7h8/8CRrpfnabUo5eK/B6qA==', '40D54FD8-9940-4090-B393-59EF885BDB83'),		
			('C116B95F-2F74-4374-A0E1-A03D365C7BF8', 'David', 'Vahey', 'David.Vahey@britishcouncil.org', 'DavidVahey', 'GLOBAL_OPERATIONS', 'ROOT', 'DavidVahey', 'ABLzq4leJPEWBbhrFs2UQ8b2eQK1+YfmhNO9SdE3op/7h8/8CRrpfnabUo5eK/B6qA==', '40D54FD8-9940-4090-B393-59EF885BDB83'),		
			
			('7D39CDFE-68D1-4177-8200-E9F43C669734', 'Geetika', 'Joshi', 'Geetika.Joshi@britishcouncil.it', 'GeetikaJoshi', 'IN120', 'IE', NULL, NULL, NULL),
			('54CF4F20-4A83-4885-B009-3983DEDC3169', 'Li', 'Zufu', 'Li.Zufu@britishcouncil.it', 'LiZufu', 'TCS', 'CN001', NULL, NULL, NULL),
			
			('4C894714-44CC-4296-8D25-C34F794B7E2A', 'Sam', 'Yates', 'Sam.Yates@britishcouncil.org', 'SamYates', 'GLOBAL_OPERATIONS', 'ROOT', 'SamYates', 'ABLzq4leJPEWBbhrFs2UQ8b2eQK1+YfmhNO9SdE3op/7h8/8CRrpfnabUo5eK/B6qA==', '40D54FD8-9940-4090-B393-59EF885BDB83'),		
			
			-- Pent Test Users
			('EDD85139-CB65-49EF-BC92-CA63D4F5A7FB', 'TestUser', 'TCS', 'eqcstestuser@gmail.com', 'TestUser_TCS', 'TCS', 'ROOT', 'TestUser_TCS', 'ABLzq4leJPEWBbhrFs2UQ8b2eQK1+YfmhNO9SdE3op/7h8/8CRrpfnabUo5eK/B6qA==', '40D54FD8-9940-4090-B393-59EF885BDB83'),		
			('E4CB886A-A1C1-41C6-B156-F5DD9AEE6E48', 'TestUser', 'CCT', 'eqcstestuser@gmail.com', 'TestUser_CCT', 'CCT', 'ROOT', 'TestUser_CCT', 'ABLzq4leJPEWBbhrFs2UQ8b2eQK1+YfmhNO9SdE3op/7h8/8CRrpfnabUo5eK/B6qA==', '40D54FD8-9940-4090-B393-59EF885BDB83'),		
			('76BD5DC0-3781-4348-AB3E-802BE19A7070', 'TestUser', 'RMT', 'eqcstestuser@gmail.com', 'TestUser_RMT', 'RMT', 'ROOT', 'TestUser_RMT', 'ABLzq4leJPEWBbhrFs2UQ8b2eQK1+YfmhNO9SdE3op/7h8/8CRrpfnabUo5eK/B6qA==', '40D54FD8-9940-4090-B393-59EF885BDB83'),		
			('549FF3B4-3C3D-4699-A35F-24B1CF18F74B', 'TestUser', 'GLOBAL_OPERATIONS', 'eqcstestuser@gmail.com', 'TestUser_GLOBAL_OPERATIONS', 'GLOBAL_OPERATIONS', 'ROOT', 'TestUser_GLOBAL_OPERATIONS', 'ABLzq4leJPEWBbhrFs2UQ8b2eQK1+YfmhNO9SdE3op/7h8/8CRrpfnabUo5eK/B6qA==', '40D54FD8-9940-4090-B393-59EF885BDB83')
		
			) AS Source ([ObjectGUID],[FirstName],[Surname],[Email],[Login],[Role],[AdminUnit], [ExternalUserName], [ExternalUserPasswordHash], [ExternalUserSecurityStamp])
		ON (
			Target.[login] = Source.[login] AND 
			Target.[Role] = Source.[Role] AND
			Target.[AdminUnit] = Source.[AdminUnit]
		   )

		WHEN MATCHED THEN 
			UPDATE SET 
				ObjectGuid = Source.[ObjectGuid],
				[FirstName] = Source.[FirstName],
				[Surname] = Source.[Surname],
				[Email] = Source.[Email],
				[Role] = Source.[Role],
				[AdminUnit] = Source.[AdminUnit]
			
				-- WARNING: Never-ever-ever update the external password hash & stamp
				--[ExternalUserPasswordHash] = Source.[ExternalUserPasswordHash],
				--[ExternalUserSecurityStamp] = Source.[ExternalUserSecurityStamp]
			
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT ([ObjectGUID], [FirstName],[Surname],[Email],[Login],[Role],[AdminUnit], [ExternalUserName], [ExternalUserPasswordHash], [ExternalUserSecurityStamp])
			VALUES ([ObjectGUID], [FirstName],[Surname],[Email],[Login],[Role],[AdminUnit], [ExternalUserName], [ExternalUserPasswordHash], [ExternalUserSecurityStamp]);

		---- delete rows that are in the target but not the source 
		--WHEN NOT MATCHED BY SOURCE THEN 
		--	DELETE;

END