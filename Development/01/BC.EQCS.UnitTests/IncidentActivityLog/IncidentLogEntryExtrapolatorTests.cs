namespace BC.EQCS.UnitTests.IncidentActivityLog
{
    //[TestFixture]
    //public class IncidentLogEntryExtrapolatorTests
    //{
    //    [Test]
    //    public void GivenIdenticalPreviousAndNewValueIncidentModelsAndAValidUser_GetChangedItemLogEntries_ReturnsAnEmptyLogCollection()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null};

    //        var IncidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1,1,1,1,1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null},
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var IncidentModel2 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1,1,1,1,1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null},
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(IncidentModel1);
    //        extrap.AddNewValuesModel(IncidentModel2);

            

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);
            
    //        //Assert
    //        CollectionAssert.IsEmpty(logEntryCollection);
    //    }

    //    [Test]
    //    public void GivenIdenticalPreviousAndNewIncidentTemplatesAndAValidUser_GetChangedItemLogEntries_ReturnsAnEmptyLogCollection()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var IncidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };

    //        var template1 = IncidentModel1.ModelToComparisonType();


    //        var IncidentModel2 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };

    //        var template2 = IncidentModel2.ModelToComparisonType();

    //        var extrap = new IncidentLogEntryExtrapolator
    //        {
    //            PreviousValueTemplate = template1,
    //            NewValueTemplate = template2
    //        };

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        CollectionAssert.IsEmpty(logEntryCollection);
    //    }




    //    [Test]
    //    public void GivenDifferentPreviousAndNewValueIncidentModelsAndAValidUser_GetChangedItemLogEntries_ReturnsALogCollectionWithASingleLogEntry()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var IncidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var IncidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(IncidentModel1);
    //        extrap.AddNewValuesModel(IncidentModel2);



    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        Assert.AreEqual(1, logEntryCollection.Count);
    //    }




    //    [Test]
    //    public void GivenDifferentPreviousAndNewValueIncidentModelsAndAValidUser_GetChangedItemLogEntries_ReturnsALogCollectionWithALogEntry_WhichHasCorrectNumberOfChangedItemsInTheJSONPayload()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        var payLoadJson = logEntryCollection.FirstOrDefault().Payload;
    //        var parsedLogEntries = JsonConvert.DeserializeObject<List<ValueFromToPayload>>(payLoadJson);

    //        Assert.AreEqual(9, parsedLogEntries.Count);
    //    }


    //    [Test]
    //    public void GivenIncidentModelsWithDifferentSubCategorysAndAValidUser_GetChangedItemLogEntries_ReturnsCorrectBeforeAndAfterSubCategoryEntriesInTheJSONPayload()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        var payLoadJson = logEntryCollection.FirstOrDefault().Payload;
    //        var parsedLogEntries = JsonConvert.DeserializeObject<List<ValueFromToPayload>>(payLoadJson);
    //        var entry = parsedLogEntries.FirstOrDefault(l => l.FieldChanged.Equals("SubCategory"));

    //        Assert.AreEqual("SubCategory01", entry.OriginalValue);
    //        Assert.AreEqual("SubCategory02", entry.NewValue);
    //    }


    //    [Test]
    //    public void GivenIncidentModelsWithDifferentIncidentDatesAndAValidUser_GetChangedItemLogEntries_ReturnsCorrectBeforeAndAfterIncidentDateEntriesInTheJSONPayload()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        var payLoadJson = logEntryCollection.FirstOrDefault().Payload;
    //        var parsedLogEntries = JsonConvert.DeserializeObject<List<ValueFromToPayload>>(payLoadJson);
    //        var entry = parsedLogEntries.FirstOrDefault(l => l.FieldChanged.Equals("IncidentDate"));

    //        Assert.AreEqual(@"01/01/2015 01:01:01", entry.OriginalValue);
    //        Assert.AreEqual(@"02/02/2015 02:02:02", entry.NewValue);
    //    }

    //    [Test]
    //    public void GivenIncidentModelsWithDifferentDescriptionsAndAValidUser_GetChangedItemLogEntries_ReturnsCorrectBeforeAndAfterDescriptionEntriesInTheJSONPayload()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        var payLoadJson = logEntryCollection.FirstOrDefault().Payload;
    //        var parsedLogEntries = JsonConvert.DeserializeObject<List<ValueFromToPayload>>(payLoadJson);
    //        var entry = parsedLogEntries.FirstOrDefault(l => l.FieldChanged.Equals("Description"));

    //        Assert.AreEqual("Description01", entry.OriginalValue);
    //        Assert.AreEqual("Description02", entry.NewValue);
    //    }

    //    [Test]
    //    [Ignore] // TODO Chris: removed as incident type is no longer captured
    //    public void GivenIncidentModelsWithDifferentIncidentTypesAndAValidUser_GetChangedItemLogEntries_ReturnsCorrectBeforeAndAfterIncidentTypeEntriesInTheJSONPayload()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "IncidentType01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "IncidentType02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        var payLoadJson = logEntryCollection.FirstOrDefault().Payload;
    //        var parsedLogEntries = JsonConvert.DeserializeObject<List<ValueFromToPayload>>(payLoadJson);
    //        var entry = parsedLogEntries.FirstOrDefault(l => l.FieldChanged.Equals("IncidentType"));

    //        Assert.AreEqual("IncidentType01", entry.OriginalValue);
    //        Assert.AreEqual("IncidentType02", entry.NewValue);
    //    }

    //    [Test]
    //    public void GivenIncidentModelsWithDifferentStatusAndAValidUser_GetChangedItemLogEntries_ReturnsCorrectBeforeAndAfterStatusEntriesInTheJSONPayload()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        var payLoadJson = logEntryCollection.FirstOrDefault().Payload;
    //        var parsedLogEntries = JsonConvert.DeserializeObject<List<ValueFromToPayload>>(payLoadJson);
    //        var entry = parsedLogEntries.FirstOrDefault(l => l.FieldChanged.Equals("Category"));

    //        Assert.AreEqual("Category01", entry.OriginalValue);
    //        Assert.AreEqual("Category02", entry.NewValue);
    //    }

    //    [Test]
    //    public void GivenIncidentModelsWithDifferentProductsAndAValidUser_GetChangedItemLogEntries_ReturnsCorrectBeforeAndAfterProductEntriesInTheJSONPayload()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        var payLoadJson = logEntryCollection.FirstOrDefault().Payload;
    //        var parsedLogEntries = JsonConvert.DeserializeObject<List<ValueFromToPayload>>(payLoadJson);
    //        var entry = parsedLogEntries.FirstOrDefault(l => l.FieldChanged.Equals("Product"));

    //        Assert.AreEqual("Product01", entry.OriginalValue);
    //        Assert.AreEqual("Product02", entry.NewValue);
    //    }

    //    [Test]
    //    public void GivenIncidentModelsWithDifferentRiskRatingsAndAValidUser_GetChangedItemLogEntries_ReturnsCorrectBeforeAndAfterRiskRatingEntriesInTheJSONPayload()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "RiskRating01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "RiskRating02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        var payLoadJson = logEntryCollection.FirstOrDefault().Payload;
    //        var parsedLogEntries = JsonConvert.DeserializeObject<List<ValueFromToPayload>>(payLoadJson);
    //        var entry = parsedLogEntries.FirstOrDefault(l => l.FieldChanged.Equals("RiskRating"));

    //        Assert.AreEqual("RiskRating01", entry.OriginalValue);
    //        Assert.AreEqual("RiskRating02", entry.NewValue);
    //    }

    //    [Test]
    //    public void GivenIncidentModelsWithDifferentResidualRiskRatingsAndAValidUser_GetChangedItemLogEntries_ReturnsCorrectBeforeAndAfterResidualRiskRatingEntriesInTheJSONPayload()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "ResidualRiskRating01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "ResidualRiskRating02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        var payLoadJson = logEntryCollection.FirstOrDefault().Payload;
    //        var parsedLogEntries = JsonConvert.DeserializeObject<List<ValueFromToPayload>>(payLoadJson);
    //        var entry = parsedLogEntries.FirstOrDefault(l => l.FieldChanged.Equals("ResidualRiskRating"));

    //        Assert.AreEqual("ResidualRiskRating01", entry.OriginalValue);
    //        Assert.AreEqual("ResidualRiskRating02", entry.NewValue);
    //    }

    //    [Test]
    //    public void GivenIncidentModelsWithDifferentTestCentresAndAValidUser_GetChangedItemLogEntries_ReturnsCorrectBeforeAndAfterTestCentreEntriesInTheJSONPayload()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "TestCentre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "TestCentre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        var payLoadJson = logEntryCollection.FirstOrDefault().Payload;
    //        var parsedLogEntries = JsonConvert.DeserializeObject<List<ValueFromToPayload>>(payLoadJson);
    //        var entry = parsedLogEntries.FirstOrDefault(l => l.FieldChanged.Equals("TestCentre"));

    //        Assert.AreEqual("TestCentre01", entry.OriginalValue);
    //        Assert.AreEqual("TestCentre02", entry.NewValue);
    //    }

    //    [Test]
    //    public void GivenIncidentModelsWithDifferentTestLocationsAndAValidUser_GetChangedItemLogEntries_ReturnsCorrectBeforeAndAfterTestLocationEntriesInTheJSONPayload()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntryCollection = extrap.GetChangedItemLogEntries(user);

    //        //Assert
    //        var payLoadJson = logEntryCollection.FirstOrDefault().Payload;
    //        var parsedLogEntries = JsonConvert.DeserializeObject<List<ValueFromToPayload>>(payLoadJson);
    //        var entry = parsedLogEntries.FirstOrDefault(l => l.FieldChanged.Equals("TestLocation"));

    //        Assert.AreEqual("TestLocation01", entry.OriginalValue);
    //        Assert.AreEqual("TestLocation02", entry.NewValue);
    //    }


    //    [Test]
    //    public void GivenIncidentModelsWithDifferentTestLocationsAndAValidUser_GetChangedItemLogEntries_ReturnsALogEntryWithTheCorrectUser()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel1 = new IncidentModel
    //        {
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1, 1, 1, 1, 1),
    //            Description = "Description01",
    //            //IncidentType = "Type01",
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product01",
    //            RiskRating = "Risk01",
    //            ResidualRiskRating = "RisidualRisk01",
    //            TestCentre = "Centre01",
    //            TestLocation = "TestLocation01"
    //        };


    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddPreviousValuesModel(incidentModel1);
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntry = extrap.GetChangedItemLogEntries(user).FirstOrDefault();

    //        //Assert
    //        Assert.AreEqual(user.DisplayName, logEntry.User.DisplayName);
    //    }




    //    [Test]
    //    public void GivenOnlyANewIncidentModelsAndAValidUser_GetNewItemLogEntry_ReturnsALogEntryWithTheCorrectUser()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntry = extrap.GetNewItemLogEntry(user);

    //        //Assert
    //        Assert.AreEqual(user.DisplayName, logEntry.User.DisplayName);
    //    }



    //    [Test]
    //    public void GivenOnlyANewIncidentModelsAndAValidUser_GetNewItemLogEntry_ReturnsALogEntryOfTypeIncidentCreate()
    //    {
    //        //Arrange
    //        var user = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null };

    //        var incidentModel2 = new IncidentModel
    //        {
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "Type02",
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null },
    //            Product = "Product02",
    //            RiskRating = "Risk02",
    //            ResidualRiskRating = "RisidualRisk02",
    //            TestCentre = "Centre02",
    //            TestLocation = "TestLocation02"
    //        };


    //        var extrap = new IncidentLogEntryExtrapolator();
    //        extrap.AddNewValuesModel(incidentModel2);

    //        //Act
    //        var logEntry = extrap.GetNewItemLogEntry(user);

    //        //Assert
    //        Assert.AreEqual(logEntry.LogType, IncidentActivityLogType.IncidentCreate);
    //    }


    //}
}
