/*
	Test Locations from IELTS UKVI ORS.

	Server : 
		lnsukvi,4901
	
	Database : 
		SELT_ORS_LIVE
	
	Query : 
		SELECT 
			Venue.VeGuid As VenueGuid,
			Centre.CeCentreNumber As CentreNumber,
			Venue.VenueUniqueNumber as VenueUbiquitousCode,
			Venue.VeCounter As VenueDbId, 
			REPLACE(Venue.VeName, '''', '''''') As VenueName,
			Country.CoISOCode As Country,
			REPLACE(VenueAddress.AdLine1, '''', '''''') As AddressLine1,
			REPLACE(VenueAddress.AdLine2, '''', '''''') As AddressLine2,
			REPLACE(VenueAddress.AdTown, '''', '''''') As Town,
			REPLACE(VenueAddress.AdState, '''', '''''') As State,
			REPLACE(VenueAddress.AdPostCode, '''', '''''') As PostCode
		FROM tblCentre Centre
		JOIN tblVenue Venue
			ON Centre.CeCounter = Venue.VeCeCounter 
		JOIN tblOrganisationCountry OrgCountry
			ON OrgCountry.OcCounter = Centre.CeOcCounter
		JOIN tblCountry Country
			ON Country.CoCounter = OrgCountry.OcCoCounter
		JOIN tblAddress AS VenueAddress ON Venue.VeAdCounter = VenueAddress.AdCounter
		WHERE CeActive = 1
		ORDER BY CentreNumber

	 Procedure:
		1. Execute query in source data with details above
		2. Copy data as text from query to a regular expression capable tool such as Notepad++
		3. From the tool, replace the text using the following regular expression
			* Find: (.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)
			* Replace: \('$1','$2','$3','$4','$5','$6','$7','$8','$9','$10'\),
		4. You may have to find and remove tabs (\t) 
		5. Paste the results to the MERGE script below
*/

MERGE INTO [stage].[UkviTestVenues] AS Target
	USING (
	VALUES 
		('5E2F1F3F-F9BD-48C5-A1BF-85EDFFFD2B2F','AE001','AE001-01','25','Zayed University Convention Centre','AE','PO Box 19282','Academic City, Ruwaiya 2','Dubai','Dubai',''),
		('25B31C3F-C05E-468F-B179-2345FB94C138','AE110','AE110-01','95','Abu Dhabi Campus- Al Ain University of Science and Technology','AE','Near Pepsi cola opposite Police College Abu Dhabi','63rd Street Abu Dhabi, UAE','Abu Dhabi','',''),
		('58841BF3-E46C-4731-96BA-D12F3DC3BC52','AF009','AF009-01','104','Q Kabul Hotel and Business Complex','AF','Kabul','','Kabul','',''),
		('5BBB2A19-9C77-4A42-A18B-69C6ADE8B729','AL001','AL001-01','9','Universiteti Europian i Tiranes','AL','Bulevardi Gjergj Fishta','','Tirana','','1000'),
		('C9801890-DC8A-41C9-BB9E-3998DFB7732C','AL001','GR026-01','117','Venue Name','XK','Address','','Pristina','',''),
		('C12162A3-83CC-4211-8354-A8D6AD147E03','AM001','AM001-01','105','Yerevan','AM','9 Alex Manoogian','','Yerevan','','0070'),
		('BF7687DE-46D5-455E-9830-D57DAA4F3846','AR033','AR033-01','180','British Council Argentina','AR','Marcelo T. De Alvear 590, 4 floor.','','Buenos Aires','CABA','C1058AAF'),
		('E8A17473-1743-4AFE-BFC2-3C836A14BE44','AT040','AT040-01','106','Vienna','AT','Vienna','','Vienna','',''),
		('10233B9D-B916-4D51-92BC-A0890AC65606','AZ001','AZ001-01','86','Monolit Plaza','AZ','Badamdar','2a Mikail Mushvig Str','Baku','','AZ1001'),
		('9220D1B7-1389-4938-86E2-9A5FF691DB75','BA001','BA001-01','108','British Council Bosnia and Herzegovina','BA','Ljubljanska 9','','Sarajevo','',''),
		('83EF440F-D25D-4D5A-97AE-94AB881A847C','BD001','BD001-01','11','Banani - Future ED','BD','Banani','','Dhaka','',''),
		('3AC7F397-A908-4D2C-8329-DDD6C5440F2B','BD001','BD001-01','12','Hotel Rose View','BD','Sylhet','','Sylhet','',''),
		('ADFE370B-66C1-44C2-8D5E-456A9130BE90','BE003','BE003-01','107','Brussels','BE','Brussels','','Brussles','',''),
		('471D6DEB-0840-4417-87C3-650D26BEEFF2','BE003','BE003-01','146','Amsterdam','NL','Amserdam','','Amsterdam','',''),
		('F54225EE-5434-47F6-9B14-35D2BCB409E8','BG001','BG001-01','110','British Council Sofia','BG','7, Krakra Street','','Sofia','Bulgaria',''),
		('A50F5BD5-0723-45CD-8729-062AAFDC18AF','BH001','BH001-01','52','National Institute of Industrial Training','BH','Building 775 Road 1510 Block 115','PO Box 1663  Hidd Industrial Area','Manama','',''),
		('63E79479-F005-4C46-90A3-2511E4BBDC0A','BR051','BR051-01','1','Cultura Inglesa Brasilia','BR','SEPS, 709/909, Cj. B,','Asa Sul','Brasilia','DF',''),
		('E0D7F64E-4198-4927-9626-4811DF49CD84','BR051','BR051-02','2','Golden - São Paulo','BR','Rua Deputado Lacerda Franco','','Sao Paulo','SP',''),
		('2E3EFAC3-5EE4-43CA-B84F-3326B87BEA29','BR051','BR051-03','3','FGV Rio de Janeiro','BR','Rua Candelária, 6,','','Rio De Janeiro','',''),
		('A3EBB8D6-83D5-468C-BC44-53D8531F69C7','BU001','BU001-01','162','Myanmar ICT Park (MICT)','MM','Universities'' Hlaing Campus','Hlaing Township','Rangoon','',''),
		('4FDB9AFD-FD1D-4D5E-988D-BC7CB3BB0AD7','CH066','DE708-01','138','IELTS for UKVI Venue','EE','Estonia','','Tallinn','',''),
		('27E9B298-DEF9-4B90-9343-7C30C1DB8F28','CH066','DE708-01','139','IELTS Venue','LV','Latvia','','Riga','',''),
		('88DB5B45-6134-4F7F-B056-049EDED3749E','CH066','CH066-01','127','British Council','CH','IFAGE','Place des Augustins 19','Geneva','Geneva',''),
		('F0006911-09B2-4B5C-999F-CACBB05C1593','CLS01','CLS01-01','175','Chile','CL','Santiago','','Santiago','',''),
		('EF70D971-D0AA-438A-AE68-B73A3364D774','CM001','CM001-01','14','BC YAOUNDE','CM','BTC 4TH FLOOR','SGBC HOTEL DE VILLE','Yaounde','CENTRE',''),
		('AECC89B1-07F4-463D-8C21-C43DFE811045','CO001','CO001-01','5','BOGOTA - Hotel Windsor House','CO','Venue test: Hotel Windsor House - Calle 95 No. 9-97','','Bogota','',''),
		('D5241CCD-C530-44E0-BFDE-866D64761631','CO001','CO001-02','6','CALI- Universidad del Valle sede San Fernando','CO','Universidad del Valle. Sede San Fernando. Calle 4B No 36-00.','','Cali','',''),
		('22154932-D71F-4CDE-B2E7-3847BDA9881E','CO001','CO001-03','7','MEDELLIN- CONFAMA','CO','Carrera 48 # 20-34','','Medellin','',''),
		('6A6FA7F0-29DA-4C44-A14A-BC1F8E823AC0','CO001','CO001-01','102','QUITO','EC','QUITO','','Quito','',''),
		('7A1E86F3-E548-446A-BA64-9355B8B4DE12','CO001','CO001-02','103','GUAYAQUIL','EC','GUAYAQUIL','','Guayaquil','',''),
		('99C07F2A-E9F3-42A5-BBCB-E90FB7C535EA','CO001','CO001-01','171','Panama City','PA','Panama City','','Panama City','',''),
		('D6F52A90-C5F2-41AD-913C-EDFC1C31A272','CU003','CU003-01','161','International School of Havana','CU','Calle 18 esquina 5ta Avenida. Miramar','','Havana','',''),
		('99B8FDC6-ADAD-4818-B349-DEBBD34C8BA4','CY006','CY006-01','130','Journalist House','CY','12 RIK Avenue','Aglantzia','Nicosia South','',''),
		('384FDA8E-4F70-4D7C-BA10-9589F73E95FA','CY006','CY006-02','131','Near East College','CY','Yakin Dogu Koleji','Yeni Bina','Nicosia North','',''),
		('CD5BFEA3-D427-4D4D-ABAF-155605332585','CZ001','CZ001-01','112','British Council Prague','CZ','Politickych veznu 13','','Prague','the Czech Republic','110 00'),
		('68362AFB-4708-4E91-9A8E-7EA6C9FD79DF','DE708','DE708-01','22','A&O Düsseldorf','DE','.','','Dusseldorf','',''),
		('5FC04A44-2448-4E2A-86D3-FCEF853AA2E4','DR001','DR001-01','174','Santo Domingo','DO','Dominican Republic','','Santo Domingo','',''),
		('D33A4A9A-41FF-42D6-9380-1809CD4DF1D1','DZ015','DZ003-01','10','Language Solutions Algeria','DZ','Villa 5, route de Dely Brahim','Route distante en face de l''Hôtel El Amir','Algiers','Alger','16002'),
		('FF1886A6-B9FB-4D6B-8049-5534732AC87A','EA001','EA001-01','119','Podgorica','ME','Centar za strane jezike','','Podgorica','Montenegro','81000'),
		('B713AF5D-AF77-4DE0-9EF7-EDA798825FF9','EA001','EA001-01','122','Belgrade','RS','Terazije 8/II floor','','Belgrade','',''),
		('32450AFA-9939-4011-AEE6-FC6B2721C665','EG001','EG001-01','16','Pyramisa Hotel','EG','60 ElGiza Street','','Cairo','',''),
		('AFB46684-386A-4F77-8ED2-CABB74E3B321','EG002','EG002-01','15','Alexandria - Four Seasons Hotel','EG','399 El Geish Road','','Alexandria','','Egypt'),
		('7DBFF91F-8D1F-4827-B928-E9AAD905F73F','ES024','ES024-01','125','British Council','ES','Paseo General Martinez Campos 31','','Madrid','','28010'),
		('D51D2DAB-2DB6-4F71-98CD-5C4AA375FEC9','ET001','ET001-01','75','British Council','ET','Comoros street Addis Ababa Ethiopia','','Addis Ababa','',''),
		('DDAD3A2B-0842-4878-8D21-597112DFFA68','ET001','ET001-02','97','Tegen Guest Accommodation Hotel','ET','Comoros Street','','Addis Ababa','',''),
		('280C6909-88C2-4C5F-ADA4-0C90C8FCF8B8','ET001','ET001-03','98','Mekelle','ET','Nejashi Ethio-Turkish school','','Addis Ababa','',''),
		('1B69C186-E737-47C2-9D0A-1D8B2DA82337','FI001','FI001-01','132','Finnish-British Society r.y.','FI','Fredrikinkatu 20 A 9','','Helsinki','','00120'),
		('070F9F40-B7F7-48F6-94E8-DE058889299E','FR585','FR585-01','17','Eurosites Saint-Ouen','FR','27, rue Godillot','','Paris','','93400'),
		('D1BAF6E0-11B7-4B33-9053-2384D7D67E31','GBS01','GBS01-01','8','British Council Edinburgh','GB','Basil Paterson College, 65 Queen St','','Edinburgh','','EH2 4NA'),
		('CA220057-19FA-496D-B406-581C6D40D29F','GBS02','GBS02-01','87','British Council London West','GB','Ealing Hammersmith & West London College','HAmmersmith Campus, Gliddon Road, Barons Court','London','London','W14 9BL'),
		('A4FB9BF5-E291-4D33-AECB-A8C268775EEC','GBS03','GBS03-01','84','British Council Chelmsford','GB','Anglia Ruskin University','Bishop Hall Lane','Chelmsford','Chelmsford','CM1 1SQ'),
		('DD4C6716-27BE-4E37-8549-C78012BE5CBF','GBS04','GBS04-01','88','British Council London North','GB','London Metropolitan University','166-220 Holloway Road','London','London','N7 8DB'),
		('4768239A-A12F-4007-9870-CA2ED24E58AA','GBS05','GBS05-01','89','British Council London Central','GB','St James''s House','10 Rosebery Avenue, Holborn','London','London','EC1R 4TF'),
		('77A1CC0E-1CA1-497D-A8AE-4AC5ED7444D1','GBS06','GBS06-01','90','British Council Birmingham -  Same day Speaking','GB','Joseph Chamberlain 6th Form College','1 Belgrave Road, Highgate, Birmingham','Birmingham','','B12 9FF'),
		('D7714A2E-63C4-43F9-A62B-1E5E5A0E1D86','GBS06','GBS06-02','148','British Council Birmingham - Thursday speaking test','GB','Joseph Chamberlain sixth form college','1 Belgrave Road, Highgate, Birmingham','Birmingham','','B12 9FF'),
		('128BEA8A-1E7F-4BB1-B8C9-0384CB196CD8','GBS06','GBS06-03','149','British Council Birmingham -  Friday Speaking','GB','Joseph Chamberlain 6th Form College','1 Belgrave Road, Highgate, Birmingham','Birmingham','','B12 9FF'),
		('69153340-52CB-4BC2-903E-A2E996813F07','GBS07','GBS07-01','91','British Council Manchester - Same day speaking','GB','Connell 6th Form College','301 Alan Turing Way','Manchester','','M11 3BS'),
		('12E2DAC0-34F1-4504-A2BA-79BF66D3A68A','GBS07','GBS07-02','150','British Council - Manchester - Friday speaking','GB','Connell Sixth Form College','301 Alan Turning Way','Manchester','','M11 3BS'),
		('9D29E41F-5432-4C08-BADE-72826C65E146','GBS07','GBS07-03','151','bc','GB','bc','','Manchester','',''),
		('B54D88D2-BAA0-4F24-AECD-6C07C62EB564','GBS08','GBS08-01','92','British Council - Cardiff - Same day speaking','GB','Cardiff and Vale College - City Centre Campus','Dumballs Road - Cardiff Bay','Cardiff','Cardiff','CF10 5BF'),
		('AAB95C28-CD19-427C-9CC2-BE0B15350AC0','GBS08','GBS08-02','152','BC','GB','BC','','Cardiff','',''),
		('92C47D6C-236D-4FB6-829D-E833CF75D3F9','GBS08','GBS08-03','153','British Council - Cardiff - Friday speaking','GB','Cardiff and Vale college','Dumballs Road - Cardiff Bay','Cardiff','','CF10 5BF'),
		('5332C08E-13C6-4302-B735-866964F0B958','GBS09','GBS09-01','93','British Council Portsmouth - Same day Speaking','GB','Highbury College','Tudor Crescent','Portsmouth','','PO6 2SA'),
		('0183338E-642F-412C-B815-1D15C0C602D8','GBS09','GBS09-02','154','British Council Portsmouth - Thursday speaking test','GB','BC','','Portsmouth','',''),
		('E183AA75-A849-413E-8E75-69C0660AAA19','GBS09','GBS09-03','155','British Council - Portsmouth - Friday Speaking','GB','Highbury college','Tudor Crescent','Portsmouth','',''),
		('0D6F1675-614F-4E16-A22A-9C268F9BF88A','GBS10','GBS10-01','94','British Council Belfast - Friday Speaking','GB','Belfast Metropolitan College','7 Queens Road','Belfast','','BT3 9FQ'),
		('3C92D71F-A841-48A1-883C-87111C50E571','GBS10','GBS10-02','156','BC','GB','BC','','Belfast','',''),
		('30804D6F-4772-43BD-BF08-B9569883027A','GBS10','GBS10-03','157','British Council Belfast - Same day Speaking','GB','Belfast Metropolitan College','Speaking on Friday','Belfast','',''),
		('5DF22913-BB7D-4B56-9C90-1E54CFA8591B','GBS11','GBS11-02','145','IELTS Venue','IE','Dublin','','Dublin','',''),
		('DBDB54B5-C5B0-4667-A8FF-AC9FD7B226DE','GE001','GE001-01','113','Training Centre of Justice of Georgia','GE','3 Politkovskaia (former Jikia) Street','','Tbilisi','','0114'),
		('A620A9CE-023B-470F-885E-7D1687184D47','GH001','GH001-01','85','British Council','GH','Liberia Road','P.O Box GP 771','Accra','',''),
		('B4588054-7A77-4FE7-A458-D5C56CE7C11A','GH001','GH001-01','169','Senegal','SN','Rue AAB - 68','Amitié Zone A et B BP 6232','Dakar','',''),
		('851C34E4-7255-490E-8EC9-9CADAB2CB9F2','GH001','GH001-01','170','Sierra Leone','SL','20 A.J. Momoh Street,  Tower Hill,','','Freetown','',''),
		('1E70D2CF-70CA-4B9D-BA83-CF0B48F78093','GR005','DE708-01','136','EDU-IELTS','DK','Store Kirkestraede 1 - 2nd floor','Copenhagen C','Copenhagen','','DK-1067'),
		('B864EFD5-1829-4A01-8FA2-2241EBFD160E','GR005','GR005-01','101','Crowne Plaza Hotel','GR','Michalakopoulou 50','','Athens','','115 28'),
		('E1E64701-093F-4003-A9D6-DB7EDE296FAB','HK001','HK001-01','177','Ulaanbaatar','MN','ESP Foundation','Oyutnii St-2, Khoroo-8, Sukhbaatar district, Ulaanbaatar 210613','Ulaanbaatar','Mongolia',''),
		('B1B270F8-6E4B-4EC7-A183-5A49189E5652','HR002','HR002-01','111','Veleuciliste VERN - Vern Business School','HR','Importanne Galleria, Iblerov trg 10','','Zagreb','','10000'),
		('982973B7-6069-4680-8728-2E01C3BC523E','HU001','HU001-01','114','British Council Budapest','HU','13-14 Madách Imre út','Madách Trade Center','Budapest','','1075'),
		('08BCBFD2-AA3B-4199-81B7-9686C0DA9928','IL001','IL001-01','76','British Council Israel','IL','Sason Hogi Tower','12 Abba Hillel','Ramat Gan','',''),
		('68346060-BB0D-44E1-94CD-CB52C4EA84AC','IN001','IN001-01','67','Kochi','IN','Kochi','','Cochin','',''),
		('DF9BCDAC-8859-476C-9E98-D5A3CC9F0DA9','IN001','IN001-02','68','Chennai','IN','Chennai','','Chennai','',''),
		('DEB21414-9268-4E0E-AB70-C48700AF3C46','IN001','IN001-03','69','Bangalore','IN','Bangalore','','Bangalore','',''),
		('5EBD2F85-4BE1-4A88-9B9C-8637B5FA54B2','IN001','IN001-04','70','Hyderabad','IN','Hyderabad','','Hyderabad','',''),
		('63EB826D-E5BE-450D-9321-2A068B19FC46','IN002','IN002-01','53','Kolkata','IN','Kolkata','','Kolkata','','700020'),
		('421F8AED-0934-4491-B485-7FF508F25768','IN100','IN100-01','59','Ahmedabad','IN','Ahmedabad','Ahmedabad','Ahmedabad','',''),
		('03874FED-9E34-4EC6-B246-B3534FFD4F59','IN100','IN100-02','60','Pune','IN','Pune','Pune','Pune','',''),
		('6269C7E8-171B-4496-A901-E62392F30C38','IN100','IN100-03','61','Mumbai','IN','Mumbai','','Mumbai','',''),
		('9F6997C3-F0C4-47AE-9BED-31A7295CB860','IN100','IN100-04','73','Goa','IN','Goa','','Goa','',''),
		('6873A8FD-1827-4D07-84EB-21EBD3451445','IN120','IN120-01','54','Delhi','IN','Delhi','','New Delhi','','110001'),
		('41999BF6-02A8-44F0-96FC-962AFD60E4A9','IN120','IN120-02','55','Chandigarh','IN','Chandigarh','','Chandigarh','',''),
		('3B4A552F-61C3-45D9-90A9-92F9B2C1085B','IN120','IN120-03','56','Jalandhar','IN','Jalandhar','','Jalandhar','',''),
		('938B8104-E75D-4328-92C9-D338E6719C87','IQ016','IQ016-01','23','Baghdad','IQ','British Embassy, International Zone','','Baghdad','',''),
		('3E1A5E2C-5790-4ABE-A383-E4D301BBCC63','IQ016','IQ016-01','24','Erbil','IQ','Dedeman Hotel','','Erbil','',''),
		('3FF6A799-DC76-4BDC-953C-B10A2D37B76E','IT264','IT264-01','115','La Valletta','IT','Malta Chamber of Commerce','Malta','Rome','',''),
		('4B0E898F-E907-4292-943F-FC4D92184F8C','IT264','IT264-02','116','Roma','IT','Venue address will be sent by email 7 days before the test. The speaking test will be scheduled on a different day from the main test, within 7 days before and 7 days after','','Rome','',''),
		('B46F1B90-1B22-4D06-AA9F-1C38DC919E8D','IT264','IT264-01','142','La Valletta','MT','Malta Chamber of Commerce','Malta','Valletta','',''),
		('1B769779-4917-4037-BC8B-019CEF7566A3','JMS01','JMS01-01','173','University of the West Indies','JM','Jamaica','','Kingston','',''),
		('41F51A5C-3CFC-4056-81AC-F77670A7C9E5','JO001','JO001-01','26','British Council Jordan','JO','39B Rainbow Street','First Circle - Jabal Amman','Amman','',''),
		('81B1EB58-DA63-48C2-BF77-C3ACDAE52729','KE001','KE001-01','29','BC Nairobi','KE','Upper Hill Road','Upper Hill','Nairobi','',''),
		('C3652A3F-9458-4AAB-836D-F9891ABAA818','KE001','KE001-01','134','BC Rwanda','RW','KN 87 Street, RMI (Muhima) Campus','','Kigali','Rwanda',''),
		('50F00814-8C82-4E28-9387-D5B003A120E7','KW001','KW001-01','34','American University of Kuwait','KW','Salmiya','','Kuwait City','',''),
		('678F14E2-4F50-4976-BC79-59A10844C366','KZ001','KZ001-01','27','Almaty','KZ','Samal Towers block A-2, 11th floor','97 Zholdasbekov Street','Almaty','Kazakhstan','050051'),
		('61E705B5-9194-415D-A97C-2CD45BB70631','KZ001','KZ001-02','28','Astana','KZ','business centre Renco, 6th floor','Chubary district, 62 Kosmonavtov Street','Astana','Kazakhstan','010000'),
		('2C7E7DDC-5C81-4381-BBF5-31156529B1EF','KZ001','KZ001-01','140','IELTS Venue','TJ','Tajikistan','University of Central Asia','Dushanbe','',''),
		('17DDBF09-21D3-4FE4-8EDC-14EE4BCB6A50','KZ001','KZ001-01','141','IELTS for UKVI Venue','TM','Turkmenistan','Ak Altyn Hotel','Ashgabat','',''),
		('31E3CEAA-34CE-4FAC-BF6C-B7591B823F38','LB001','LB001-01','13','Université Saint Joseph','LB','USJ Faculty of Economics and Sports','Mathaf','Beirut','','2064 1509'),
		('9761B1FB-972C-4FF7-AA40-5A987FA3772D','LK001','LK001-01','49','BCIS (Bandaranayke Centre of International Studies) at BMICH - Colombo','LK','Bauddhaloka Mawatha, Colombo 07','','Colombo','',''),
		('02617C0D-E3CA-4CF8-8353-D0307EA2D6D3','LK001','LK001-02','78','BMICH (Bandaranaike Memorial International Conference Hall) - Colombo','LK','Bauddhaloka Mawatha, Colombo 07','','Colombo','',''),
		('E9C07AA0-6795-41E1-AFF0-EAD572885D74','LK001','LK001-03','79','College of Islamic Studies - Maldives','LK','Violet Magu','Henveiru','Colombo','',''),
		('E76277D0-A727-4E64-9CDE-0E2398510A95','LK001','LK001-01','147','State Electric Company Ltd.','MV','Ameenee Magu','Male 20349','Male','Rep of Maldives',''),
		('60C4F2CE-B193-44D5-AB4F-423246182B54','LT001','DE708-01','137','IELTS for UKVI Venue','BY','Minsk','','VILNIUS','',''),
		('08F03424-1473-4342-97E4-95A4D219C79C','LT001','L2001-02','179','Minsk In-service Teacher Training Institute','BY','Bronevoy st. 15A','','Minsk','',''),
		('5A66A071-27B9-4A6E-AE03-4FCBAB5CD993','MA002','MA002-01','77','École de gouvernance et d''économie de Rabat','MA','EGE Avenue Mohamed Ben Abdellah Regragui, Rabat 10112, Morocco','','Rabat','',''),
		('6F1D3BC0-4987-4748-ADA6-94F334D4D885','MK001','MK001-01','166','British Council','MK','Bul.Goce Delcev 6','','Skopje','','1000'),
		('70CD8420-F2EA-4909-BE17-55A17A224389','MU861','MU861-01','158','Lycee Francais de Tananarive','MG','101 Ambatobe','','Antananarivo','',''),
		('ACC9960F-9E7C-4EB5-8319-A3D690ED1B5F','MX030','MX030-01','21','ITESM Campus Cd. de México','MX','Calle del Puente 222','Col. Ejidos de Huipulco','Mexico City','',''),
		('2690A4AE-1B4C-4B7B-87D1-F600C0F2EA0D','MY001','MY001-01','176','CfBT Examinations Centre - Brunei','BN','Unit 8, 1st Floor, Block C, Kiarong Complex, Lebuhraya Sultan Haji Hassanal Bolkiah','Gadong','Bandar Seri Begawan','Brunei','BE1318'),
		('D4B0C272-CB8C-4248-B608-45B8CBAC794C','NG050','NG050-01','18','British Nigeria Academy','NG','Prince & Princess Estate','Duboyi District','Abuja','Abuja',''),
		('1508DFE4-7DC0-40DA-BA25-33FACC3E5932','NG050','NG050-02','19','National Theatre','NG','Iganmu','','Lagos','Lagos',''),
		('125D6FB3-3412-4579-BCD5-A6A8FA884E9F','NG050','NG050-03','80','British Council Lagos','NG','20 Thompson Avenue','Iyoki','Lagos','Lagos',''),
		('C0FB6AF1-3BE2-4C74-942E-02BA30918F83','NG050','NG050-04','81','British Council Abuja and Denis Hotel','NG','Plot 910 Ndjamena Street','Wuse 2','Abuja','',''),
		('BD00F85F-F857-44B4-9701-9B12061B2F43','NG150','NG150-01','82','British Council Lagos','NG','20 Thompson Avenue','Iyoki','Lagos','Lagos',''),
		('F97B4874-6625-41D6-8598-87F75DEED1CF','NG150','NG150-02','83','National Theatre','NG','Iganmu','Lagos','Lagos','',''),
		('6A137A47-17B1-4F4B-A048-3101DE57BDB4','NO002','NO002-01','133','Folkeuniversitetet East','NO','Torggata 7','','Oslo','','0105'),
		('1EA8249F-F025-49AC-BA97-3D41BE5FA36E','NP004','NP004-01','39','Kathmandu','NP','Lainchaur','','Kathmandu','',''),
		('5C20723C-8E9F-489F-9A00-E04101F11C5B','OM001','OM001-01','40','Madina Plaza','OM','Road 1, Madinat Sultan Qaboos','P O Box 73','Muscat','Muscat','115'),
		('0894DB28-4A85-47BA-B971-992A66335094','PES01','PES01-01','178','Lima','PE','Lima','','Lima','',''),
		('3E8AFED5-743C-4FDC-B37A-604797148A45','PK010','PK010-01','42','Marriot Hotel','PK','9 Abdullah Haroon Road','','Karachi','Sind',''),
		('9C98EA99-3FC3-4372-85CC-A63D1345AE8F','PK011','PK011-01','43','DeSOM','PK','71 Tufail Road','+92 42 36680387','Lahore','',''),
		('DEABA28A-935E-4978-8815-5B227276E96D','PK015','PK015-01','41','Ramada Hotel Islamabad','PK','Islamabad Club road','','Islamabad','',''),
		('0535A6C1-07FD-4D70-9554-AAA0776E1768','PL002','PL002-01','160','Golden Floor Millennium Plaza','PL','Al. Jerozolimskie 123 A','','Warsaw','',''),
		('9B88DE9B-53A2-45DA-BE96-1B63DA9D909C','PS003','PS003-01','163','Gaza','PS','Gaza','','East Jerusalem','',''),
		('A5B788C2-D4DF-44E8-92CE-0EE8E6EA873C','PS003','PS003-02','164','West Bank','PS','West Bank','','East Jerusalem','',''),
		('68D7F4C5-C710-4B28-9C93-188226DCB357','PT008','PT008-01','120','British Council Lisbon','PT','Rua Luis Frenandes 1-3','1249-062','Lisbon','',''),
		('EA6C2649-50E9-4149-A4AC-17D66FD263FF','QA001','QA001-01','51','British Council','QA','99, Al Sadd Street','','Doha','Qatar',''),
		('765E04EF-61F4-48E7-AD42-4779D0910A14','RO001','RO001-01','121','Caro Hotel','RO','Barbu Vacarescu 164A','','Bucharest','',''),
		('B402B370-0AB6-40D3-9085-90B60D80F33E','SA100','SA100-01','47','Movenpick males','SA','Opposite Ministry of Interiors,','Madinah Road','Jeddah (Male)','',''),
		('1436A94E-CD1B-43C1-8ADF-9413438E2AA2','SA100','SA100-02','100','Movenpick Females','SA','opposite to Ministry of Interiors','Madinah Road','Jeddah (Female)','',''),
		('20D5E6CC-E3D9-408B-AA67-28192028EBE6','SA102','SA102-01','46','British Council Riyadh - Male','SA','Office No: C14,','Al Fazary Square, Diplomatic Quarter','Riyadh(Male)','',''),
		('C60281C6-7C70-4B3C-9058-A005C8DE2A9C','SA102','SA102-02','96','Ibn Khaldun International School - Female','SA','Al Amir Meshal Ibn Abdulaziz Road','Irqah','Riyadh (Female)','',''),
		('0131C4F6-26B3-4993-8FE6-F11151146067','SA102','SA102-03','99','British Council Riyadh - Female','SA','Office No: C14, Al Fazary Square,','Diplomatic Quarter','Riyadh (Female)','',''),
		('038EC3AE-D3A7-4B43-AE86-987CFB96B942','SA105','SA105-01','44','NOVOTEL hotel,','SA','Dammam','','Al Khobar (Male)','',''),
		('676D6F7B-226C-44B8-98B3-15A13A145E5F','SA105','SA105-02','45','Al-Hussan training Centre','SA','Dammam','','Al Khobar (Female)','',''),
		('862C1F97-9E78-4831-A1F5-3C28F96E3E3A','SD001','SD001-01','50','Mamoun Biherei Conference Hall','SD','Mohamed Najib Street','South East of Building of Sudanese Working Abroad','Khartoum','Khartoum','11111'),
		('A9EC7C2E-B400-4006-8DA0-788ECE8C6EA4','SE001','SE011-01','126','Folkuniversitetet','SE','Kungstensgatan 45','','Stockholm','','113 59'),
		('FFC9C7B0-3BD4-476E-AE32-8DD041315E4D','SI003','SI003-01','123','Tivoli Center','SI','Bleiweisova ulica 30','','Ljubjana','','1000'),
		('C2F9CAD0-9EA9-4FF2-AAA2-DFD911E391A0','TN001','TN001-01','165','British Council Tunis','TN','87 Avenue Mohamed','V. BP 96 Le Belvédère','Tunis','Tunisia','1002'),
		('BF79DBF4-BF69-400D-A047-299AF7A642C4','TR002','TR002-01','32','Adana- Riva Resatbey Hotel','TR','Reatbey Mah','Adalet Cad No:20 Seyhan','Adana','',''),
		('E31F3177-7305-42B3-BF2B-34851451CBF8','TR002','TR002-02','33','Ankara- Limak Ambassadore Boutique Hotel','TR','Bogaz sokak No:19','Kavaklidere','Ankara','',''),
		('BFF5EE85-3C43-4A45-9166-011F59DA8235','TR002','TR002-03','35','Antalya- Crowne Plaza Hotel','TR','Gürsu Mah. Akdeniz Bulvari 306 Sk','Konyaalti','Antalya','',''),
		('E5EEFDDE-2BD7-4726-AF9F-A6C8B12081E8','TR002','TR002-04','36','Bursa- Hampton by Hilton','TR','Soganli Mah, Yeni Yalova Yolu Caddesi','No:349 Osmangazi','Bursa','',''),
		('714FE869-5B9A-448B-89C0-C80B51148BEB','TR002','TR002-05','37','Istanbul- Elite World Hotel Talimhane','TR','Sehit Muhtar Caddesi No:42','Taksim','Istanbul','',''),
		('307B3BD9-0E94-4D17-8658-49CB4EEED6B4','TR002','TR002-06','38','Izmir- The Address Education Center','TR','Gazi Bulvar No:59','Zeytinoglu Is Merkezi K. 6-7 Cankaya','Izmir','',''),
		('6A326C5D-0B0A-46F7-8B8C-A13DA4142113','TTS01','CO001-01','135','TT','TT','TT','','(choose)','',''),
		('F078B5AA-6AA3-4FAE-AA77-63B8E2FF79DA','TTS01','TTS01-02','172','Trinidad and Tobago','TT','University of the West Indies','','Port of Spain','',''),
		('0E6E0B79-6D54-4D21-8223-FA89D88A5042','TZ003','TZ003-01','128','BRITISH COUNCIL','TZ','SAMORA AVENUE/OHIO STREET','BOX 9100','Dar Es Salaam','',''),
		('F72FE497-9357-408E-ACCA-4436C481ADB4','UA001','UA001-01','30','Kyiv UKVI, LS','UA','4/12 Skovorody str.','','Kiev','',''),
		('5495C468-1D06-4442-ABCF-C8025A1B7325','UG001','UG001-01','31','Kabira Country Club','UG','Plot 4 Windsor Loop, off Kira Road','PO Box 7070 Kampala.','Kampala','',''),
		('3E346934-2662-4818-A250-10F7B71DAC36','UG001','UG001-01','168','Juba Regency Hotel','SS','Off Airport Road','Kololo Area','Juba','',''),
		('C8212D7A-B672-4F14-9D71-297F099EF72D','UZ025','UZ025-01','129','Tashkent','UZ','107B, A.Temur Street','','Tashkent','Uzbekistan','100084'),
		('1E3FE146-91EF-4326-9E7E-CD0E7E91377E','VE001','VE001-01','4','Colegio Simón Bolívar II','VE','Av. Este, Urb. Manzanares.','Caracas, 1080.','Caracas','Distrito Capital','1080'),
		('800CDBC9-5008-4946-A834-A3BA0FDB1283','VE001','VE001-02','181','British Council Caracas','VE','Av. Principal del Bosque','Torre Credicard, piso 3','Caracas','Distrito Capital','1050'),
		('7CD3133F-6FB1-4C2B-81D1-E5A04D1E833D','ZA001','ZA001-01','48','Tshwane Events Centre (Pretoria Showgrounds)','ZA','203 Soutter Street','Entrance G2','Pretoria','Pretoria',''),
		('CA76FB7B-9F32-4DC7-AC1A-32A746539D04','ZA001','ZA001-02','57','Cape Town','ZA','British Council Exam Venue','Upper East side, Woodstock','Cape Town','',''),
		('DDAF3E87-2420-4566-B1BE-937B25D5796D','ZA001','ZA001-03','58','Durban','ZA','267 ANTON LEMBEDE STREET','','Durban','',''),
		('FE536B89-D888-4828-8BDF-2A44CF5F57BE','ZA001','ZA001-04','71','Johannesburg','ZA','275 Jan Smuts Avenue','','Johannesburg','',''),
		('ED25C9EA-4689-485F-BE5D-2885A18D182F','ZA001','ZA001-05','74','Port Elizabeth','ZA','Nelson Mandela University','','Port Elizabeth','',''),
		('CBFC6E6A-E1B5-4C46-ADEA-55BDA9EFADC2','ZA001','ZA001-01','109','Botswana Productivity Centre (BNPC)','BW','Private Bag 00392','','Gaborone','',''),
		('3A0EA74B-5704-47A0-B6A4-9C8DC4965959','ZA001','ZA001-01','118','British Council-Malawi','MW','plot 13/12D','','Lilongwe','',''),
		('A01E3C87-1E1E-492E-8D55-9BB90A38A091','ZA001','ZA001-01','143','IELTS Venue','NA','Windhoek','','Windhoek','',''),
		('554449AB-0894-441E-9EFD-44F4B4865CA5','ZA001','ZA001-01','159','Maputo','MZ','Mozambique','','Maputo','',''),
		('3D4EE963-031D-4ED4-BEA5-311C90464D17','ZM601','ZM601-01','167','Zambia Centre for Accountancy Studies (ZCAS)','ZM','Didan Kimati Rd','','Lusaka','',''),
		('A624F84B-3473-44E4-B30D-1B552B639FA6','ZW001','ZW001-01','20','Jameson Hotel','ZW','Cnr Park Street & S Machel av','','Harare','','')
	) AS Source 
	(
		[Guid],
		[CentreNumber],
		[VenueUbiquitousCode],
		[VenueDbId],
		[VenueName],
		[Country],
		[AddressLine1],
		[AddressLine2],
		[Town],
		[State],
		[PostCode]
	)
	ON (Target.[Guid] = Source.[Guid])
	
	WHEN MATCHED THEN 
		UPDATE SET 
			[CentreNumber] = Source.[CentreNumber],
			[VenueUbiquitousCode] = Source.[VenueUbiquitousCode],
			[VenueDbId] = Source.[VenueDbId],
			[VenueName] = Source.[VenueName],
			[Country] = Source.[Country],
			[AddressLine1] = ISNULL(CASE Source.[AddressLine1] WHEN '' THEN NULL ELSE Source.[AddressLine1] END, 'UNKNOWN ADDRESS LINE 1'),
			[AddressLine2] = CASE Source.[AddressLine2] WHEN '' THEN NULL ELSE Source.[AddressLine2] END,
			[Town] = ISNULL(Source.[Town], 'UKNOWN TOWN'),
			[State] = CASE Source.[State] WHEN '' THEN NULL ELSE Source.[State] END,
			[PostCode] = CASE Source.[PostCode] WHEN '' THEN NULL ELSE Source.[PostCode] END

	-- insert new rows 
	WHEN NOT MATCHED BY TARGET THEN 
		INSERT 
		(
			[Guid],
			[CentreNumber],
			[VenueUbiquitousCode],
			[VenueDbId],
			[VenueName],
			[Country],
			[AddressLine1],
			[AddressLine2],
			[Town],
			[State],
			[PostCode]
		)
		VALUES 
		(
			[Guid],
			[CentreNumber],
			[VenueUbiquitousCode],
			[VenueDbId],
			[VenueName],
			[Country],
			ISNULL(CASE Source.[AddressLine1] WHEN '' THEN NULL ELSE Source.[AddressLine1] END, 'UNKNOWN ADDRESS LINE 1'),
			CASE Source.[AddressLine2] WHEN '' THEN NULL ELSE Source.[AddressLine2] END,
			ISNULL(Source.[Town], 'UKNOWN TOWN'),
			CASE Source.[State] WHEN '' THEN NULL ELSE Source.[State] END,
			CASE Source.[PostCode] WHEN '' THEN NULL ELSE Source.[PostCode] END
		)

	-- delete rows that are in the target but not the source 
	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE
	;