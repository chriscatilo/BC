namespace BC.EQCS.Integration.IncidentActivityLogFeature
{
    //public class ActivityLoggerMockControllerClass
    //{
    //    [IncidentActivityLogger]
    //    public dynamic SingleNewModelParameterReturnsOk(IncidentModel model)
    //    {
    //        return new OkResult(new HttpRequestMessage());
    //    }

    //    [IncidentActivityLogger]
    //    public dynamic SingleNewModelParameterReturnsValidationFailed(IncidentModel model)
    //    {
    //        return new ValidationFailedResult(new BadRequestMessage(), new HttpRequestMessage()); ;
    //    }

    //    [IncidentActivityLogger]
    //    public dynamic IdParamModelParameterWithIdReturnsValidationFailed(Int32 id, IncidentModel model)
    //    {
    //        return new ValidationFailedResult(new BadRequestMessage(), new HttpRequestMessage()); ;
    //    }

    //    [IncidentActivityLogger]
    //    public dynamic IdParamModelParameterWithIdReturnsReturnsOK(Int32 id, IncidentModel model)
    //    {
    //         return new OkResult(new HttpRequestMessage());
    //    }
    //}


    //[TestFixture]
    //class IncidentActivityLoggerAspectTests
    //{
    //    private readonly IncidentModel _incidentModel1;
    //    private readonly IncidentModel _incidentModel2;
    //    private readonly IncidentModel _incidentModelNew;
    //    public IncidentActivityLogModel itemPassedAsArg = null;
    //    private SecurityUserModel user;


    //    public IncidentActivityLoggerAspectTests()
    //    {
    //        user = new SecurityUserModel { ObjectGuid = Guid.NewGuid(), FirstName = "Tom", Surname = "Tommington", EmailAddress = "Tomtommington@Tom.Tom" };

    //        _incidentModel1 = new IncidentModel
    //        {
    //            Id = 1,
    //            Category = "Category01",
    //            SubCategory = "SubCategory01",
    //            IncidentDate = new DateTime(2015, 1,1,1,1,1),
    //            Description = "Description01",
    //            //IncidentType = "IncidentType01", TODO Chris: incident type no longer captured
    //            Status = IncidentStatus.Draft,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null}, TODO Chris: fix this
    //            Product = "Product01",
    //            RiskRating = "RiskRating01",
    //            ResidualRiskRating = "ResidualRiskRating01",
    //            TestCentre = "TestCentre01",
    //            TestLocation = "TestLocation01"
    //        };
            

    //        _incidentModel2 = new IncidentModel
    //        {
    //            Id = 1,
    //            Category = "Category02",
    //            SubCategory = "SubCategory02",
    //            IncidentDate = new DateTime(2015, 2, 2, 2, 2, 2),
    //            Description = "Description02",
    //            //IncidentType = "IncidentType02", TODO Chris: incident type no longer captured
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null }, TODO Chris: fix this
    //            Product = "Product02",
    //            RiskRating = "RiskRating02",
    //            ResidualRiskRating = "ResidualRiskRating02",
    //            TestCentre = "TestCentre02",
    //            TestLocation = "TestLocation02"
    //        };

    //        _incidentModelNew = new IncidentModel
    //        {
    //            Id = 0,
    //            Category = "CategoryNew",
    //            SubCategory = "SubCategoryNew",
    //            IncidentDate = new DateTime(2015, 3, 3, 3, 3, 3),
    //            Description = "DescriptionNew",
    //            //IncidentType = "IncidentTypeNew", TODO Chris: incident type no longer captured
    //            Status = IncidentStatus.Closed,
    //            //LoggedByUser = new UserModel { ObjectGuid = Guid.NewGuid(), DisplayName = "Bob Bobbington", FirstName = "Bob", Surname = "Bobbington", EmailAddress = "BobBobbington@BobbyXXX.Bob", AdminStructure = null }, TODO Chris: fix this
    //            Product = "ProductNew",
    //            RiskRating = "RiskRatingNew",
    //            ResidualRiskRating = "ResidualRiskRatingNew",
    //            TestCentre = "TestCentreNew",
    //            TestLocation = "TestLocationNew"
    //        };

    //        var mockIncidentRepo = Substitute.For<IRepository<IncidentModel>>();
    //        mockIncidentRepo.GetById(Arg.Any<Int32>()).Returns(_incidentModel1);

    //        var mockLogRepo = Substitute.For<IRepository<IncidentActivityLogModel>>();
    //        mockLogRepo
    //            .When(a => a.Create(Arg.Any<IncidentActivityLogModel>()))
    //            .Do(a => itemPassedAsArg = a.Arg<IncidentActivityLogModel>());


    //        DependanciesForAspect.SetIncidentRepository(mockIncidentRepo);
    //        DependanciesForAspect.SetIncidentActivityLogRepository(mockLogRepo);
    //        DependanciesForAspect.SetCurrentUser(user);
    //    }

    //    //TODO:BRYAN, Remove this test completly if the aspect does not need to handle logging for incidents before they have Ids
    //    //Bryan: Currently refactoring the code this is testing and may remove the code this is testing, if it doesn't get replaced then delete this test, otherwise restore it.
    //    //[Test]
    //    //public void CallingWithNewCorrectModelAsSingleParameterWithOkResponsePassesLogEntryToRepo()
    //    //{
    //    //    //Arrange
    //    //    var controller = new ActivityLoggerMockControllerClass();

    //    //    //Act
    //    //    controller.SingleNewModelParameterReturnsOk(_incidentModelNew);

    //    //    //Assert
    //    //    Assert.AreEqual(IncidentActivityLogType.IncidentCreate, itemPassedAsArg.LogType);
    //    //}



    //    [Test]
    //    public void CallingWithNewCorrectModelAsSingleParameterWithFailedResponse_NoLogEntryCallToRepo()
    //    {
    //        //Arrange
    //        var controller = new ActivityLoggerMockControllerClass();
    //        DependanciesForAspect.GetIncidentActivityLogRepository().ClearReceivedCalls();

    //        //Act
    //        controller.SingleNewModelParameterReturnsValidationFailed(_incidentModelNew);

    //        //Assert
    //        DependanciesForAspect.GetIncidentActivityLogRepository().DidNotReceiveWithAnyArgs().Create(null);
    //    }


    //    [Test]
    //    public void CallingWithUpdatedIncidentModelAsSecondParameterWithIDAsFirst_OkResponse_PassesChangesPayloadWithLogEntryToRepo()
    //    {
    //        //Arrange
    //        var controller = new ActivityLoggerMockControllerClass();

    //        //Act
    //        controller.IdParamModelParameterWithIdReturnsReturnsOK(1, _incidentModel2);

    //        //Assert
    //        Assert.AreEqual(IncidentActivityLogType.Change, itemPassedAsArg.LogType);
    //    }




    //    [Test]
    //    public void CallingWithUpdatedIncidentModelAsSecondParameterWithIDAsFirst_ValidationFailedResponse_NoLogEntryCallToRepo()
    //    {
    //        //Arrange
    //        var controller = new ActivityLoggerMockControllerClass();
    //        DependanciesForAspect.GetIncidentActivityLogRepository().ClearReceivedCalls();

    //        //Act
    //        controller.IdParamModelParameterWithIdReturnsValidationFailed(1, _incidentModel2);

    //        //Assert
    //        DependanciesForAspect.GetIncidentActivityLogRepository().DidNotReceiveWithAnyArgs().Create(null);
    //    }
    //}
    








}
