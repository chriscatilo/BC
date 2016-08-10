using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using BC.EQCS.Contracts;
using BC.EQCS.Entities;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Repositories;
using BC.EQCS.Security;
using BC.EQCS.Web.Controllers.API;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.Integration.IncidentList
{
    //[TestFixture]
    class IncidentsListingControllerTests
    {
        IEnumerable<IncidentsListingModel> incidentsForResponse;

        public IncidentsListingControllerTests()
        {
            var numRowsCreated = 20;
            var incidentDateTime = new DateTime(2015, 03, 14);

            incidentsForResponse = new List<IncidentsListingModel>();
            for (int i = 0; i < numRowsCreated; i++)
            {
                ((List<IncidentsListingModel>)incidentsForResponse).Add(new IncidentsListingModel
                {
                    Id = i,
                    IncidentDate = incidentDateTime,
                    Status = Enum.GetName(typeof(IncidentStatus), IncidentStatus.InProgress),
                    Category = "Cat" + i,
                    LoggedBy = "Bob",
                    Product = "Product" + i,
                    TestDate = incidentDateTime,
                    IncidentNumber = i.ToString(),
                    TestCentreNumber = (i * 2).ToString()
                });
            }
        }


        //[Test]
        //public void WhenPassedAValidQueryAndDataIsAvailableAnOKResponseIsReturned()
        //{
        //    //Arrange
        //    //Need the OdataConventionModelBuilder
        //    var builder = new ODataConventionModelBuilder();
        //    builder.EntitySet<IncidentsListingModel>("IncidentsOData");

        //    //Odataquerycontext
        //    var context = new ODataQueryContext(builder.GetEdmModel(), typeof(IncidentsListingModel), new ODataPath());

        //    //The request
        //    HttpRequestMessage request = new HttpRequestMessage(
        //        HttpMethod.Get,
        //        "http://localtest/odata/IncidentsOData/?%24format=json&%24filter=(Status+eq+'Draft'+or+Status+eq+'Raised'+or+Status+eq+'InProgress'+or+Status+eq+'Rejected')&%24count=true'");


        //    //ODataQueryOptions
        //    var queryOptions = new ODataQueryOptions<IncidentsListingModel>(context, request);



        //    //Mock up the odata repository
        //    var entityFactoryMock = Substitute.For<IEntityFactory>();
        //    var mockedRepository = Substitute.For<IncidentListingODataRepository>(entityFactoryMock);
        //    mockedRepository.GetAll(null).ReturnsForAnyArgs(incidentsForResponse);
        //    mockedRepository.GetAllForGivenAdminUnits(null, null).ReturnsForAnyArgs(incidentsForResponse);

        //    //Mock up the authorisor
        //    var mockedAuthorisor = Substitute.For<IAssetAuthoriser>();
        //    mockedAuthorisor.IsAuthorised(null).ReturnsForAnyArgs(true);

        //    var mockedUserContext = Substitute.For<IUserContext>();
        //    var mockedUser = new UserModel
        //    {
        //        AdminStructure = new AdminUnitModel(),
        //        AvailableIncidentClasses = new IncidentClassModel(),
        //        ViewableIncidentClasses = new IncidentClassModel()
        //    };
        //    mockedUserContext.CurrentUser.Returns(mockedUser);

        //    //New up the incident list odata controller and dependancies
        //    var incidentListOdataController = new IncidentsListingController(mockedRepository, mockedAuthorisor, mockedUserContext);
            


        //    //Execute the method to get the response
        //    IHttpActionResult response = incidentListOdataController.GetIncidentsListing(queryOptions).Result;

        //    //Assert the reponse is correct
        //    Assert.AreEqual(typeof(OkNegotiatedContentResult<IEnumerable<IncidentsListingModel>>), response.GetType());


        //}


        //[Test]
        //public void WhenPassedAnInvalidQueryAndDataIsAvailableAnErorResponseIsReturned()
        //{
        //    //Arrange
        //    //Need the OdataConventionModelBuilder
        //    var builder = new ODataConventionModelBuilder();
        //    builder.EntitySet<IncidentsListingModel>("IncidentsOData");

        //    //Odataquerycontext
        //    var context = new ODataQueryContext(builder.GetEdmModel(), typeof(IncidentsListingModel), new ODataPath());

        //    //The request
        //    HttpRequestMessage request = new HttpRequestMessage(
        //        HttpMethod.Get,
        //        "http://localtest/odata/IncidentsOData/?%24format=json&%24filter=(IncorrectPropertyName+eq+'Draft'+or+Status+eq+'Raised'+or+Status+eq+'InProgress'+or+Status+eq+'Rejected')&%24count=true'");


        //    //ODataQueryOptions
        //    var queryOptions = new ODataQueryOptions<IncidentsListingModel>(context, request);



        //    //Mock up the odata repository
        //    var entityFactoryMock = Substitute.For<IEntityFactory>();
        //    var mockedRepository = Substitute.For<IncidentListingODataRepository>(entityFactoryMock);
        //    mockedRepository.GetAll(null).ReturnsForAnyArgs(incidentsForResponse);

        //    //Mock up the authorisor
        //    var mockedAuthorisor = Substitute.For<IAssetAuthoriser>();
        //    mockedAuthorisor.IsAuthorised(null).ReturnsForAnyArgs(true);

        //    var mockedUserContext = Substitute.For<IUserContext>();
        //    var mockedUser = new UserModel
        //    {
        //        AdminStructure = new AdminUnitModel()
        //    };
        //    mockedUserContext.CurrentUser.Returns(mockedUser);

        //    //New up the incident list odata controller and dependancies
        //    var incidentListOdataController = new IncidentsListingController(mockedRepository, mockedAuthorisor, mockedUserContext);



        //    //Execute the method to get the response
        //    IHttpActionResult response = incidentListOdataController.GetIncidentsListing(queryOptions).Result;


        //    //Assert the reponse is correct
        //    Assert.AreEqual(typeof(BadRequestErrorMessageResult), response.GetType());
        //}





        //[Test]
        //public void WhenPassedAQueryTypeWhichIsNotAllowedAndDataIsAvailableAnErrorResponseIsReturned()
        //{
        //    //Arrange
        //    //Need the OdataConventionModelBuilder
        //    var builder = new ODataConventionModelBuilder();
        //    builder.EntitySet<IncidentsListingModel>("IncidentsOData");

        //    //Odataquerycontext
        //    var context = new ODataQueryContext(builder.GetEdmModel(), typeof(IncidentsListingModel), new ODataPath());

        //    //The request
        //    HttpRequestMessage request = new HttpRequestMessage(
        //        HttpMethod.Get,
        //        "http://localtest/odata/IncidentsOData/?%24format=json&%24filter=(Status+eq+'Draft'+or+Status+eq+'Raised'+or+Status+eq+'InProgress'+or+Status+eq+'Rejected')&%24count=true&%24&$expand=something'");


        //    //ODataQueryOptions
        //    var queryOptions = new ODataQueryOptions<IncidentsListingModel>(context, request);


        //    //Mock up the odata repository
        //    var entityFactoryMock = Substitute.For<IEntityFactory>();
        //    var mockedRepository = Substitute.For<IncidentListingODataRepository>(entityFactoryMock);
        //    mockedRepository.GetAll(null).ReturnsForAnyArgs(incidentsForResponse);

        //    //Mock up the authorisor
        //    var mockedAuthorisor = Substitute.For<IAssetAuthoriser>();
        //    mockedAuthorisor.IsAuthorised(null).ReturnsForAnyArgs(true);

        //    var mockedUserContext = Substitute.For<IUserContext>();
        //    var mockedUser = new UserModel
        //    {
        //        AdminStructure = new AdminUnitModel()
        //    };
        //    mockedUserContext.CurrentUser.Returns(mockedUser);

        //    //New up the incident list odata controller and dependancies
        //    var incidentListOdataController = new IncidentsListingController(mockedRepository, mockedAuthorisor, mockedUserContext);


        //    //Execute the method to get the response
        //    IHttpActionResult response = incidentListOdataController.GetIncidentsListing(queryOptions).Result;


        //    //Assert the reponse is correct
        //    Assert.AreEqual(typeof(BadRequestErrorMessageResult), response.GetType());
        //}

    }
}
