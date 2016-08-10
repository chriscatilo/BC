/// <reference path="../../BC.EQCS.Web/Scripts/angular.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/angular-route.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/angular-mocks.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/angular-animate.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/jquery-2.1.3.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/toaster.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/kendo/kendo.all.min.js" />
/// <reference path="../../BC.EQCS.Web/Scripts/underscore.min.js" />
/// <reference path="/Scripts/jasmine/jasmine.js" />

/// <reference path="../../BC.EQCS.Web/app/core/eqcs.core.module.js" />
/// <reference path="../../BC.EQCS.Web/app/incident/eqcs.incident.module.js" />
/// <reference path="../../BC.EQCS.Web/app/incident/eqcs.incident.constant.appconfig.js" />

// directive under test
/// <reference path="../../BC.EQCS.Web/app/core/eqcs.core.directive.availablecommands.js" />

describe('eqcs.incident.service.adminUnitHelper =>', function () {


    //Tests to implement
    //Any correct admin unit will return true
    //Any invalid admin unit will return false
    


    describe("given an adminUnit hierarchy", function() {

        //    //Set up the admin unit hierarchy
        var adminUnitHierarchyJSON = "{\"code\":\"ROOT\",\"name\":\"Root\",\"type\":\"ROOT\",\"parent\":null," +
            "\"children\":[{\"code\":\"All\",\"name\":\"All Regions\",\"type\":\"REGION\",\"parent\":\"ROOT\",\"children\":[{\"code\":\"AE\",\"name\":\"United Arab Emirates\"," +
            "\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"AE001\",\"name\":\"British Council Dubai\",\"type\":\"TEST_CENTRE\",\"parent\":\"AE\"," +
            "\"children\":[{\"code\":\"AE001-01\",\"name\":\"Zayed University Convention Centre\",\"type\":\"TEST_LOCATION\",\"parent\":\"AE001\",\"children\":[]}]},{\"code\":\"AE110\"," +
            "\"name\":\"British Council, Abu Dhabi\",\"type\":\"TEST_CENTRE\",\"parent\":\"AE\",\"children\":[{\"code\":\"AE110-01\",\"name\":\"Abu Dhabi Campus- Al Ain University of Science and Technology\"," +
            "\"type\":\"TEST_LOCATION\",\"parent\":\"AE110\",\"children\":[]}]}]},{\"code\":\"AL\",\"name\":\"Albania\",\"type\":\"SUB_REGION\",\"parent\":\"All\"," +
            "\"children\":[{\"code\":\"AL001\",\"name\":\"British Council Albania\",\"type\":\"TEST_CENTRE\",\"parent\":\"AL\",\"children\":[{\"code\":\"AL001-01\"," +
            "\"name\":\"Universiteti Europian i Tiranes\",\"type\":\"TEST_LOCATION\",\"parent\":\"AL001\",\"children\":[]}]}]},{\"code\":\"AZ\",\"name\":\"Azerbaijan\",\"type\":\"SUB_REGION\"," +
            "\"parent\":\"All\",\"children\":[{\"code\":\"AZ001\",\"name\":\"British Council Azerbaijan\",\"type\":\"TEST_CENTRE\",\"parent\":\"AZ\",\"children\":[{\"code\":\"AZ001-01\"," +
            "\"name\":\"Monolit Plaza\",\"type\":\"TEST_LOCATION\",\"parent\":\"AZ001\",\"children\":[]}]}]},{\"code\":\"BD\",\"name\":\"Bangladesh\",\"type\":\"SUB_REGION\"," +
            "\"parent\":\"All\",\"children\":[{\"code\":\"BD001\",\"name\":\"British Council Bangladesh\",\"type\":\"TEST_CENTRE\",\"parent\":\"BD\",\"children\":[{\"code\":\"BD001-01\"," +
            "\"name\":\"Banani - Future ED\",\"type\":\"TEST_LOCATION\",\"parent\":\"BD001\",\"children\":[]},{\"code\":\"BD001-02\",\"name\":\"Hotel Rose View\",\"type\":\"TEST_LOCATION\"," +
            "\"parent\":\"BD001\",\"children\":[]}]}]},{\"code\":\"BH\",\"name\":\"Bahrain\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"BH001\",\"name\":\"Manama\"," +
            "\"type\":\"TEST_CENTRE\",\"parent\":\"BH\",\"children\":[{\"code\":\"BH001-01\",\"name\":\"National Institute of Industrial Training\",\"type\":\"TEST_LOCATION\",\"parent\":\"BH001\"," +
            "\"children\":[]}]}]},{\"code\":\"BR\",\"name\":\"Brazil\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"BR051\",\"name\":\"British Council Brazil\"," +
            "\"type\":\"TEST_CENTRE\",\"parent\":\"BR\",\"children\":[{\"code\":\"BR051-01\",\"name\":\"Cultura Inglesa Brasilia\",\"type\":\"TEST_LOCATION\",\"parent\":\"BR051\"," +
            "\"children\":[]},{\"code\":\"BR051-02\",\"name\":\"Golden - São Paulo\",\"type\":\"TEST_LOCATION\",\"parent\":\"BR051\",\"children\":[]},{\"code\":\"BR051-03\",\"name\":\"FGV Rio de Janeiro\"," +
            "\"type\":\"TEST_LOCATION\",\"parent\":\"BR051\",\"children\":[]}]}]},{\"code\":\"CM\",\"name\":\"Cameroon\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"CM001\"," +
            "\"name\":\"BRITISH COUNCIL\",\"type\":\"TEST_CENTRE\",\"parent\":\"CM\",\"children\":[{\"code\":\"CM001-01\",\"name\":\"BC YAOUNDE\",\"type\":\"TEST_LOCATION\",\"parent\":\"CM001\"," +
            "\"children\":[]}]}]},{\"code\":\"CO\",\"name\":\"Colombia\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"CO001\",\"name\":\"British Council Colombia\"," +
            "\"type\":\"TEST_CENTRE\",\"parent\":\"CO\",\"children\":[{\"code\":\"CO001-01\",\"name\":\"BOGOTA - Colegio Anglo Colombiano\",\"type\":\"TEST_LOCATION\",\"parent\":\"CO001\"," +
            "\"children\":[]},{\"code\":\"CO001-02\",\"name\":\"CALI- Universidad del Valle sede San Fernando\",\"type\":\"TEST_LOCATION\",\"parent\":\"CO001\",\"children\":[]},{\"code\":\"CO001-03\"," +
            "\"name\":\"MEDELLIN- CONFAMA\",\"type\":\"TEST_LOCATION\",\"parent\":\"CO001\",\"children\":[]}]}]},{\"code\":\"DE\",\"name\":\"Germany\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"DE708\"," +
            "\"name\":\"British Council Germany\",\"type\":\"TEST_CENTRE\",\"parent\":\"DE\",\"children\":[{\"code\":\"DE708-01\",\"name\":\"A&O Düsseldorf\",\"type\":\"TEST_LOCATION\"," +
            "\"parent\":\"DE708\",\"children\":[]}]}]},{\"code\":\"DZ\",\"name\":\"Algeria\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"DZ015\"," +
            "\"name\":\"British Council Algeria\",\"type\":\"TEST_CENTRE\",\"parent\":\"DZ\",\"children\":[{\"code\":\"DZ003-01\",\"name\":\"Language Solutions Algeria\"," +
            "\"type\":\"TEST_LOCATION\",\"parent\":\"DZ015\",\"children\":[]}]}]},{\"code\":\"EG\",\"name\":\"Egypt\",\"type\":\"SUB_REGION\",\"parent\":\"All\"," +
            "\"children\":[{\"code\":\"EG001\",\"name\":\"British Council, Cairo\",\"type\":\"TEST_CENTRE\",\"parent\":\"EG\",\"children\":[{\"code\":\"EG001-01\",\"name\":\"Pyramisa Hotel\"," +
            "\"type\":\"TEST_LOCATION\",\"parent\":\"EG001\",\"children\":[]}]},{\"code\":\"EG002\",\"name\":\"British Council, Alexandria\",\"type\":\"TEST_CENTRE\",\"parent\":\"EG\"," +
            "\"children\":[{\"code\":\"EG002-01\",\"name\":\"Alexandria - Four Seasons Hotel\",\"type\":\"TEST_LOCATION\",\"parent\":\"EG002\",\"children\":[]}]}]},{\"code\":\"ET\"," +
            "\"name\":\"Ethiopia\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"ET001\",\"name\":\"British Council Ethiopia\",\"type\":\"TEST_CENTRE\"," +
            "\"parent\":\"ET\",\"children\":[{\"code\":\"ET001-01\",\"name\":\"British Council\",\"type\":\"TEST_LOCATION\",\"parent\":\"ET001\",\"children\":[]},{\"code\":\"ET001-02\"," +
            "\"name\":\"Tegen Guest Accommodation Hotel\",\"type\":\"TEST_LOCATION\",\"parent\":\"ET001\",\"children\":[]},{\"code\":\"ET001-03\",\"name\":\"Mekelle\"," +
            "\"type\":\"TEST_LOCATION\",\"parent\":\"ET001\",\"children\":[]}]}]},{\"code\":\"FR\",\"name\":\"France\",\"type\":\"SUB_REGION\",\"parent\":\"All\"," +
            "\"children\":[{\"code\":\"FR585\",\"name\":\"British Council\",\"type\":\"TEST_CENTRE\",\"parent\":\"FR\",\"children\":[{\"code\":\"FR585-01\",\"name\":\"Eurosites Saint-Ouen\"," +
            "\"type\":\"TEST_LOCATION\",\"parent\":\"FR585\",\"children\":[]}]}]},{\"code\":\"GB\",\"name\":\"United Kingdom\",\"type\":\"SUB_REGION\",\"parent\":\"All\"," +
            "\"children\":[{\"code\":\"GBS01\",\"name\":\"British Council Edinburgh\",\"type\":\"TEST_CENTRE\",\"parent\":\"GB\",\"children\":[{\"code\":\"GBS01-01\"," +
            "\"name\":\"British Council Edinburgh\",\"type\":\"TEST_LOCATION\",\"parent\":\"GBS01\",\"children\":[]}]},{\"code\":\"GBS02\",\"name\":\"British Council London West\"," +
            "\"type\":\"TEST_CENTRE\",\"parent\":\"GB\",\"children\":[{\"code\":\"GBS02-01\",\"name\":\"British Council London West\",\"type\":\"TEST_LOCATION\",\"parent\":\"GBS02\"," +
            "\"children\":[]}]},{\"code\":\"GBS03\",\"name\":\"British Council Chelmsford\",\"type\":\"TEST_CENTRE\",\"parent\":\"GB\",\"children\":[{\"code\":\"GBS03-01\"," +
            "\"name\":\"British Council Chelmsford\",\"type\":\"TEST_LOCATION\",\"parent\":\"GBS03\",\"children\":[]}]},{\"code\":\"GBS04\",\"name\":\"British Council London North\"," +
            "\"type\":\"TEST_CENTRE\",\"parent\":\"GB\",\"children\":[{\"code\":\"GBS04-01\",\"name\":\"British Council London North\",\"type\":\"TEST_LOCATION\",\"parent\":\"GBS04\"," +
            "\"children\":[]}]},{\"code\":\"GBS05\",\"name\":\"British Council London Central\",\"type\":\"TEST_CENTRE\",\"parent\":\"GB\",\"children\":[{\"code\":\"GBS05-01\"," +
            "\"name\":\"British Council London Central\",\"type\":\"TEST_LOCATION\",\"parent\":\"GBS05\",\"children\":[]}]},{\"code\":\"GBS06\",\"name\":\"British Council Birmingham\"," +
            "\"type\":\"TEST_CENTRE\",\"parent\":\"GB\",\"children\":[{\"code\":\"GBS06-01\",\"name\":\"British Council Birmingham\",\"type\":\"TEST_LOCATION\",\"parent\":\"GBS06\"," +
            "\"children\":[]}]},{\"code\":\"GBS07\",\"name\":\"British Council Manchester\",\"type\":\"TEST_CENTRE\",\"parent\":\"GB\",\"children\":[{\"code\":\"GBS07-01\"," +
            "\"name\":\"British Council Manchester\",\"type\":\"TEST_LOCATION\",\"parent\":\"GBS07\",\"children\":[]}]},{\"code\":\"GBS08\",\"name\":\"British Council Cardiff\"," +
            "\"type\":\"TEST_CENTRE\",\"parent\":\"GB\",\"children\":[{\"code\":\"GBS08-01\",\"name\":\"British Council Cardiff\",\"type\":\"TEST_LOCATION\",\"parent\":\"GBS08\"," +
            "\"children\":[]}]},{\"code\":\"GBS09\",\"name\":\"British Council Portsmouth\",\"type\":\"TEST_CENTRE\",\"parent\":\"GB\",\"children\":[{\"code\":\"GBS09-01\"," +
            "\"name\":\"Higbury College\",\"type\":\"TEST_LOCATION\",\"parent\":\"GBS09\",\"children\":[]}]},{\"code\":\"GBS10\",\"name\":\"British Council Belfast\",\"type\":\"TEST_CENTRE\"," +
            "\"parent\":\"GB\",\"children\":[{\"code\":\"GBS10-01\",\"name\":\"British Council Belfast\",\"type\":\"TEST_LOCATION\",\"parent\":\"GBS10\",\"children\":[]}]}]},{\"code\":\"GH\"," +
            "\"name\":\"Ghana\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"GH001\",\"name\":\"British Council\",\"type\":\"TEST_CENTRE\",\"parent\":\"GH\"," +
            "\"children\":[{\"code\":\"GH001-01\",\"name\":\"British Council\",\"type\":\"TEST_LOCATION\",\"parent\":\"GH001\",\"children\":[]}]}]},{\"code\":\"IL\",\"name\":\"Israel\"," +
            "\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"IL001\",\"name\":\"British Council Israel\",\"type\":\"TEST_CENTRE\",\"parent\":\"IL\"," +
            "\"children\":[{\"code\":\"IL001-01\",\"name\":\"British Council Israel\",\"type\":\"TEST_LOCATION\",\"parent\":\"IL001\",\"children\":[]}]}]},{\"code\":\"IN\",\"name\":\"India\"," +
            "\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"IN001\",\"name\":\"South India\",\"type\":\"TEST_CENTRE\",\"parent\":\"IN\"," +
            "\"children\":[{\"code\":\"IN001-01\",\"name\":\"Kochi\",\"type\":\"TEST_LOCATION\",\"parent\":\"IN001\",\"children\":[]},{\"code\":\"IN001-02\",\"name\":\"Chennai\"," +
            "\"type\":\"TEST_LOCATION\",\"parent\":\"IN001\",\"children\":[]},{\"code\":\"IN001-03\",\"name\":\"Bangalore\",\"type\":\"TEST_LOCATION\",\"parent\":\"IN001\"," +
            "\"children\":[]},{\"code\":\"IN001-04\",\"name\":\"Hyderabad\",\"type\":\"TEST_LOCATION\",\"parent\":\"IN001\",\"children\":[]}]},{\"code\":\"IN002\",\"name\":\"East India\"," +
            "\"type\":\"TEST_CENTRE\",\"parent\":\"IN\",\"children\":[{\"code\":\"IN002-01\",\"name\":\"Kolkata\",\"type\":\"TEST_LOCATION\",\"parent\":\"IN002\"," +
            "\"children\":[]}]},{\"code\":\"IN100\",\"name\":\"West India\",\"type\":\"TEST_CENTRE\",\"parent\":\"IN\",\"children\":[{\"code\":\"IN100-01\",\"name\":\"Ahmedabad\"," +
            "\"type\":\"TEST_LOCATION\",\"parent\":\"IN100\",\"children\":[]},{\"code\":\"IN100-02\",\"name\":\"Pune\",\"type\":\"TEST_LOCATION\",\"parent\":\"IN100\"," +
            "\"children\":[]},{\"code\":\"IN100-03\",\"name\":\"Mumbai\",\"type\":\"TEST_LOCATION\",\"parent\":\"IN100\",\"children\":[]},{\"code\":\"IN100-04\",\"name\":\"Goa\"," +
            "\"type\":\"TEST_LOCATION\",\"parent\":\"IN100\",\"children\":[]}]},{\"code\":\"IN120\",\"name\":\"North India\",\"type\":\"TEST_CENTRE\",\"parent\":\"IN\",\"children\":[{\"code\":\"IN120-01\"," +
            "\"name\":\"Delhi\",\"type\":\"TEST_LOCATION\",\"parent\":\"IN120\",\"children\":[]},{\"code\":\"IN120-02\",\"name\":\"Chandigarh\",\"type\":\"TEST_LOCATION\",\"parent\":\"IN120\",\"children\":[]},{\"code\":\"IN120-03\",\"name\":\"Jalandhar\",\"type\":\"TEST_LOCATION\",\"parent\":\"IN120\",\"children\":[]}]}]},{\"code\":\"IQ\",\"name\":\"Iraq\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"IQ016\",\"name\":\"British Council Iraq\",\"type\":\"TEST_CENTRE\",\"parent\":\"IQ\",\"children\":[{\"code\":\"IQ016-01\",\"name\":\"Baghdad\",\"type\":\"TEST_LOCATION\"," +
            "\"parent\":\"IQ016\",\"children\":[]},{\"code\":\"IQ016-02\",\"name\":\"Erbil\",\"type\":\"TEST_LOCATION\",\"parent\":\"IQ016\",\"children\":[]}]}]},{\"code\":\"JO\",\"name\":\"Jordan\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"JO001\",\"name\":\"British Council Jordan\",\"type\":\"TEST_CENTRE\",\"parent\":\"JO\",\"children\":[{\"code\":\"JO001-01\",\"name\":\"British Council Jordan\",\"type\":\"TEST_LOCATION\",\"parent\":\"JO001\",\"children\":[]}]}]},{\"code\":\"KE\",\"name\":\"Kenya\",\"type\":\"SUB_REGION\"," +
            "\"parent\":\"All\",\"children\":[{\"code\":\"KE001\",\"name\":\"British Council, Nairobi\",\"type\":\"TEST_CENTRE\",\"parent\":\"KE\",\"children\":[{\"code\":\"KE001-01\",\"name\":\"BC Nairobi\",\"type\":\"TEST_LOCATION\",\"parent\":\"KE001\",\"children\":[]}]}]},{\"code\":\"KW\",\"name\":\"Kuwait\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"KW001\",\"name\":\"British Council Kuwait\",\"type\":\"TEST_CENTRE\",\"parent\":\"KW\",\"children\":[{\"code\":\"KW001-01\",\"name\":\"American University of Kuwait\",\"type\":\"TEST_LOCATION\",\"parent\":\"KW001\"," +
            "\"children\":[]}]}]},{\"code\":\"KZ\",\"name\":\"Kazakhstan\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"KZ001\",\"name\":\"British Council Kazakhstan\",\"type\":\"TEST_CENTRE\",\"parent\":\"KZ\",\"children\":[{\"code\":\"KZ001-01\",\"name\":\"Almaty\",\"type\":\"TEST_LOCATION\",\"parent\":\"KZ001\",\"children\":[]},{\"code\":\"KZ001-02\",\"name\":\"Astana\",\"type\":\"TEST_LOCATION\",\"parent\":\"KZ001\",\"children\":[]}]}]},{\"code\":\"LB\",\"name\":\"Lebanon\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"LB001\",\"name\":\"British Council\"," +
            "\"type\":\"TEST_CENTRE\",\"parent\":\"LB\",\"children\":[{\"code\":\"LB001-01\",\"name\":\"Université Saint Joseph\",\"type\":\"TEST_LOCATION\",\"parent\":\"LB001\",\"children\":[]}]}]},{\"code\":\"LK\",\"name\":\"Sri Lanka\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"LK001\",\"name\":\"British Council Colombo\",\"type\":\"TEST_CENTRE\",\"parent\":\"LK\",\"children\":[{\"code\":\"LK001-01\",\"name\":\"BCIS (Bandaranayke Centre of International Studies) at BMICH - Colombo\",\"type\":\"TEST_LOCATION\"," +
            "\"parent\":\"LK001\",\"children\":[]},{\"code\":\"LK001-02\",\"name\":\"BMICH (Bandaranaike Memorial International Conference Hall) - Colombo\",\"type\":\"TEST_LOCATION\",\"parent\":\"LK001\",\"children\":[]},{\"code\":\"LK001-03\",\"name\":\"College of Islamic Studies - Maldives\",\"type\":\"TEST_LOCATION\",\"parent\":\"LK001\",\"children\":[]}]}]},{\"code\":\"MA\",\"name\":\"Morocco\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"MA002\",\"name\":\"British Council Rabat\",\"type\":\"TEST_CENTRE\",\"parent\":\"MA\"," +
            "\"children\":[{\"code\":\"MA002-01\",\"name\":\"École de gouvernance et d'économie de Rabat\"," +
            "\"type\":\"TEST_LOCATION\",\"parent\":\"MA002\",\"children\":[]}]}]},{\"code\":\"MX\",\"name\":\"Mexico\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"MX030\",\"name\":\"BRITISH COUNCIL\",\"type\":\"TEST_CENTRE\",\"parent\":\"MX\",\"children\":[{\"code\":\"MX030-01\",\"name\":\"ITESM Campus Cd. de México\",\"type\":\"TEST_LOCATION\",\"parent\":\"MX030\",\"children\":[]}]}]},{\"code\":\"NG\",\"name\":\"Nigeria\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"NG050\",\"name\":\"British Council  Abuja\",\"type\":\"TEST_CENTRE\",\"parent\":\"NG\"," +
            "\"children\":[{\"code\":\"NG050-01\",\"name\":\"British Nigeria Academy\",\"type\":\"TEST_LOCATION\",\"parent\":\"NG050\",\"children\":[]},{\"code\":\"NG050-04\",\"name\":\"British Council Abuja and Denis Hotel\",\"type\":\"TEST_LOCATION\",\"parent\":\"NG050\",\"children\":[]}]},{\"code\":\"NG150\",\"name\":\"British Council Lagos\",\"type\":\"TEST_CENTRE\",\"parent\":\"NG\",\"children\":[{\"code\":\"NG150-01\",\"name\":\"British Council Lagos\",\"type\":\"TEST_LOCATION\",\"parent\":\"NG150\",\"children\":[]},{\"code\":\"NG150-02\",\"name\":\"National Theatre\",\"type\":\"TEST_LOCATION\",\"parent\":\"NG150\"," +
            "\"children\":[]}]}]},{\"code\":\"NP\",\"name\":\"Nepal\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"NP004\",\"name\":\"British Council, Kathmandu, Nepal\",\"type\":\"TEST_CENTRE\",\"parent\":\"NP\",\"children\":[{\"code\":\"NP004-01\",\"name\":\"Kathmandu\",\"type\":\"TEST_LOCATION\",\"parent\":\"NP004\",\"children\":[]}]}]},{\"code\":\"OM\",\"name\":\"Oman\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"OM001\",\"name\":\"British Council Oman\"," +
            "\"type\":\"TEST_CENTRE\",\"parent\":\"OM\",\"children\":[{\"code\":\"OM001-01\",\"name\":\"Madina Plaza\",\"type\":\"TEST_LOCATION\",\"parent\":\"OM001\",\"children\":[]}]}]},{\"code\":\"PK\",\"name\":\"Pakistan\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"PK010\",\"name\":\"British Council Karachi\",\"type\":\"TEST_CENTRE\",\"parent\":\"PK\",\"children\":[{\"code\":\"PK010-01\",\"name\":\"Marriot Hotel\",\"type\":\"TEST_LOCATION\",\"parent\":\"PK010\",\"children\":[]}]},{\"code\":\"PK011\",\"name\":\"British Council Lahore\",\"type\":\"TEST_CENTRE\",\"parent\":\"PK\",\"children\":[{\"code\":\"PK011-01\"," +
            "\"name\":\"DeSOM\",\"type\":\"TEST_LOCATION\",\"parent\":\"PK011\",\"children\":[]}]},{\"code\":\"PK015\",\"name\":\"British Council Islamabad\",\"type\":\"TEST_CENTRE\",\"parent\":\"PK\",\"children\":[{\"code\":\"PK015-01\",\"name\":\"Ramada Hotel Islamabad\",\"type\":\"TEST_LOCATION\",\"parent\":\"PK015\",\"children\":[]}]}]},{\"code\":\"QA\",\"name\":\"Qatar\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"QA001\",\"name\":\"British Council Qatar\",\"type\":\"TEST_CENTRE\",\"parent\":\"QA\",\"children\":[{\"code\":\"QA001-01\",\"name\":\"British Council\",\"type\":\"TEST_LOCATION\",\"parent\":\"QA001\",\"children\":[]}]}]},{\"code\":\"SA\"," +
            "\"name\":\"Saudi Arabia\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"SA100\",\"name\":\"British Council Jeddah\",\"type\":\"TEST_CENTRE\",\"parent\":\"SA\",\"children\":[{\"code\":\"SA100-01\",\"name\":\"Movenpick Hotel\",\"type\":\"TEST_LOCATION\",\"parent\":\"SA100\",\"children\":[]},{\"code\":\"SA100-02\",\"name\":\"Movenpick Hotel\",\"type\":\"TEST_LOCATION\",\"parent\":\"SA100\",\"children\":[]}]},{\"code\":\"SA102\",\"name\":\"British Council Riyadh\",\"type\":\"TEST_CENTRE\",\"parent\":\"SA\",\"children\":[{\"code\":\"SA102-01\",\"name\":\"British Council Riyadh - Male\",\"type\":\"TEST_LOCATION\",\"parent\":\"SA102\",\"children\":[]},{\"code\":\"SA102-02\"," +
            "\"name\":\"Ibn Khaldun International School - Female\",\"type\":\"TEST_LOCATION\",\"parent\":\"SA102\",\"children\":[]},{\"code\":\"SA102-03\",\"name\":\"British Council Riyadh - Female\",\"type\":\"TEST_LOCATION\",\"parent\":\"SA102\",\"children\":[]}]},{\"code\":\"SA105\",\"name\":\"British Council Al Khobar\",\"type\":\"TEST_CENTRE\",\"parent\":\"SA\",\"children\":[{\"code\":\"SA105-01\",\"name\":\"NOVOTEL hotel,\",\"type\":\"TEST_LOCATION\",\"parent\":\"SA105\",\"children\":[]},{\"code\":\"SA105-02\",\"name\":\"Al-Hussan training Centre\",\"type\":\"TEST_LOCATION\",\"parent\":\"SA105\",\"children\":[]}]}]},{\"code\":\"SD\",\"name\":\"Sudan\",\"type\":\"SUB_REGION\",\"parent\":\"All\"," +
            "\"children\":[{\"code\":\"SD001\",\"name\":\"British Council Sudan\",\"type\":\"TEST_CENTRE\",\"parent\":\"SD\",\"children\":[{\"code\":\"SD001-01\"," +
            "\"name\":\"Mamoun Biherei Conference Hall\",\"type\":\"TEST_LOCATION\",\"parent\":\"SD001\",\"children\":[]}]}]},{\"code\":\"TR\",\"name\":\"Turkey\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"TR002\",\"name\":\"British Council Turkey\",\"type\":\"TEST_CENTRE\",\"parent\":\"TR\",\"children\":[{\"code\":\"TR002-01\",\"name\":\"Adana- Riva Resatbey Hotel\",\"type\":\"TEST_LOCATION\",\"parent\":\"TR002\",\"children\":[]},{\"code\":\"TR002-02\",\"name\":\"Ankara- Limak Ambassadore Boutique Hotel\",\"type\":\"TEST_LOCATION\",\"parent\":\"TR002\",\"children\":[]},{\"code\":\"TR002-03\",\"name\":\"Antalya- Crowne Plaza Hotel\",\"type\":\"TEST_LOCATION\",\"parent\":\"TR002\"," +
            "\"children\":[]},{\"code\":\"TR002-04\",\"name\":\"Bursa- Hampton by Hilton\",\"type\":\"TEST_LOCATION\",\"parent\":\"TR002\",\"children\":[]},{\"code\":\"TR002-05\",\"name\":\"Istanbul- Elite World Hotel Talimhane\",\"type\":\"TEST_LOCATION\",\"parent\":\"TR002\",\"children\":[]},{\"code\":\"TR002-06\",\"name\":\"Izmir- The Address Education Center\",\"type\":\"TEST_LOCATION\",\"parent\":\"TR002\",\"children\":[]}]}]},{\"code\":\"UA\",\"name\":\"Ukraine\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"UA001\",\"name\":\"British Council Ukraine\",\"type\":\"TEST_CENTRE\",\"parent\":\"UA\",\"children\":[{\"code\":\"UA001-01\",\"name\":\"Kyiv UKVI, LS\",\"type\":\"TEST_LOCATION\"," +
            "\"parent\":\"UA001\",\"children\":[]}]}]},{\"code\":\"UG\",\"name\":\"Uganda\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"UG001\",\"name\":\"Kampala The British Council\",\"type\":\"TEST_CENTRE\",\"parent\":\"UG\",\"children\":[{\"code\":\"UG001-01\",\"name\":\"Kabira Country Club\",\"type\":\"TEST_LOCATION\",\"parent\":\"UG001\",\"children\":[]}]}]},{\"code\":\"VE\",\"name\":\"Venezuela\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"VE001\",\"name\":\"British Council Venezuela\",\"type\":\"TEST_CENTRE\",\"parent\":\"VE\"," +
            "\"children\":[{\"code\":\"VE001-01\",\"name\":\"Colegio Simón Bolívar II\",\"type\":\"TEST_LOCATION\",\"parent\":\"VE001\",\"children\":[]}]}]},{\"code\":\"ZA\",\"name\":\"South Africa\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"ZA001\",\"name\":\"South Africa\",\"type\":\"TEST_CENTRE\",\"parent\":\"ZA\",\"children\":[{\"code\":\"ZA001-01\",\"name\":\"Tshwane Events Centre (Pretoria Showgrounds)\",\"type\":\"TEST_LOCATION\",\"parent\":\"ZA001\",\"children\":[]},{\"code\":\"ZA001-02\",\"name\":\"Cape Town\",\"type\":\"TEST_LOCATION\",\"parent\":\"ZA001\",\"children\":[]},{\"code\":\"ZA001-03\",\"name\":\"Durban\",\"type\":\"TEST_LOCATION\",\"parent\":\"ZA001\",\"children\":[]},{\"code\":\"ZA001-04\",\"name\":\"Johannesburg\",\"type\":\"TEST_LOCATION\",\"parent\":\"ZA001\",\"children\":[]},{\"code\":\"ZA001-05\",\"name\":\"Port Elizabeth\",\"type\":\"TEST_LOCATION\",\"parent\":\"ZA001\",\"children\":[]}]}]},{\"code\":\"ZW\",\"name\":\"Zimbabwe\",\"type\":\"SUB_REGION\",\"parent\":\"All\",\"children\":[{\"code\":\"ZW001\",\"name\":\"British Council Zimbabwe\",\"type\":\"TEST_CENTRE\",\"parent\":\"ZW\",\"children\":[{\"code\":\"ZW001-01\",\"name\":\"Jameson Hotel\",\"type\":\"TEST_LOCATION\",\"parent\":\"ZW001\",\"children\":[]}]}]}]}]}";


        var adminUnitHierarchyObject = JSON.parse(adminUnitHierarchyJSON);

        
            it('then something gonna happen', function () {
                            
                            expect(true).toEqual(true);
                        });


    
    //        //Add a new code which should be in the hierarchy
    //        beforeEach(module('eqcs.incident'));

          
    

    //        describe("and input form is dirty", function () {

              
    //            it('then updating commands and cancel commands are available', function () {
    //                var actual = getCommandShown().sort();
    //                var expected = _.union(updatingCommands, ['cancel']).sort();
    //                expect(actual).toEqual(expected);
    //            });
    //        });

      
    });

    
});