using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;
using System.Web.Http.Results;
using BC.EQCS.Entities;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Repositories;
using BC.EQCS.Security;
using BC.EQCS.Web.Controllers.API;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.IncidentList
{
    [TestFixture]
    class IncidentListOdataControllerTests
    {
        [Test]
        public void WhenPassedAValidQueryAndDataIsAvailableAnOKResponseIsReturned()
        {
            var numRowsCreated = 20;


            var incidentDateTime = new DateTime(2015, 03, 14);

          
            //Set up a collection of Incident objects
            IEnumerable<IncidentsListingModel> incidentsForResponse = new List<IncidentsListingModel>();
            for (int i = 0; i < numRowsCreated; i++)
            {
                ((List<IncidentsListingModel>)incidentsForResponse).Add(new IncidentsListingModel
                                {
                                    Id = i,
                                    IncidentDate = incidentDateTime,
                                    Status = Enum.GetName(typeof(IncidentStatus), IncidentStatus.InProgress),
                                    IncidentSubCategory = "Cat" + i,
                                    LoggedBy = "Bob",
                                    RaisedBy = "Bob",
                                    Product = "Product" + i,
                                    TestDate = incidentDateTime,
                                    IncidentNumber = i.ToString(),
                                    TestCentreNumber = (i*2).ToString()
                                });
            }


            //Need the OdataConventionModelBuilder
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<IncidentsListingModel>("IncidentsOData");

            //Odataquerycontext
            var context = new ODataQueryContext(builder.GetEdmModel(), typeof(IncidentsListingModel));

            //The request
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                "http://localtest/odata/IncidentsOData/?%24format=json&%24filter=(Status+eq+'Draft'+or+Status+eq+'Raised'+or+Status+eq+'InProgress'+or+Status+eq+'Rejected')&%24count=true'");


            //ODataQueryOptions
            var queryOptions = new ODataQueryOptions<IncidentsListingModel>(context, request);



            //Mock up the odata repository
            var entityFactoryMock = Substitute.For<IEntityFactory>();
            var mockedRepository = Substitute.For<IncidentsODataRepository>(entityFactoryMock);
            mockedRepository.GetAll(null).ReturnsForAnyArgs(incidentsForResponse);

            //Mock up the authorisor
            var mockedAuthorisor = Substitute.For<IAssetAuthoriser>();
            mockedAuthorisor.IsAuthorised(null).ReturnsForAnyArgs(true);


            //New up the incident list odata controller and dependancies
            var incidentListOdataController = new IncidentsListingController(mockedRepository, mockedAuthorisor);
            


            //Execute the method to get the response
            IHttpActionResult response = incidentListOdataController.GetIncidentsListing(queryOptions).Result;


            //Assert the reponse is correct
            Assert.AreEqual(response.GetType(), typeof(OkNegotiatedContentResult<IEnumerable<IncidentsListingModel>>));


        }


        [Test]
        public void WhenPassedAnInvalidQueryAndDataIsAvailableAnErorResponseIsReturned()
        {
            var numRowsCreated = 20;


            var incidentDateTime = new DateTime(2015, 03, 14);


            //Set up a collection of Incident objects
            IEnumerable<IncidentsListingModel> incidentsForResponse = new List<IncidentsListingModel>();
            for (int i = 0; i < numRowsCreated; i++)
            {
                ((List<IncidentsListingModel>)incidentsForResponse).Add(new IncidentsListingModel
                {
                    Id = i,
                    IncidentDate = incidentDateTime,
                    Status = Enum.GetName(typeof(IncidentStatus), IncidentStatus.InProgress),
                    IncidentSubCategory = "Cat" + i,
                    LoggedBy = "Bob",
                    RaisedBy = "Bob",
                    Product = "Product" + i,
                    TestDate = incidentDateTime,
                    IncidentNumber = i.ToString(),
                    TestCentreNumber = (i * 2).ToString()
                });
            }


            //Need the OdataConventionModelBuilder
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<IncidentsListingModel>("IncidentsOData");

            //Odataquerycontext
            var context = new ODataQueryContext(builder.GetEdmModel(), typeof(IncidentsListingModel));

            //The request
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                "http://localtest/odata/IncidentsOData/?%24format=json&%24filter=(IncorrectPropertyName+eq+'Draft'+or+Status+eq+'Raised'+or+Status+eq+'InProgress'+or+Status+eq+'Rejected')&%24count=true'");


            //ODataQueryOptions
            var queryOptions = new ODataQueryOptions<IncidentsListingModel>(context, request);



            //Mock up the odata repository
            var entityFactoryMock = Substitute.For<IEntityFactory>();
            var mockedRepository = Substitute.For<IncidentsODataRepository>(entityFactoryMock);
            mockedRepository.GetAll(null).ReturnsForAnyArgs(incidentsForResponse);

            //Mock up the authorisor
            var mockedAuthorisor = Substitute.For<IAssetAuthoriser>();
            mockedAuthorisor.IsAuthorised(null).ReturnsForAnyArgs(true);


            //New up the incident list odata controller and dependancies
            var incidentListOdataController = new IncidentsListingController(mockedRepository, mockedAuthorisor);



            //Execute the method to get the response
            IHttpActionResult response = incidentListOdataController.GetIncidentsListing(queryOptions).Result;


            //Assert the reponse is correct
            Assert.AreEqual(response.GetType(), typeof(BadRequestErrorMessageResult));
        }





        [Test]
        public void WhenPassedAQueryTypeWhichIsNotAllowedAndDataIsAvailableAnOKResponseIsReturned()
        {
            var numRowsCreated = 20;


            var incidentDateTime = new DateTime(2015, 03, 14);


            //Set up a collection of Incident objects
            IEnumerable<IncidentsListingModel> incidentsForResponse = new List<IncidentsListingModel>();
            for (int i = 0; i < numRowsCreated; i++)
            {
                ((List<IncidentsListingModel>)incidentsForResponse).Add(new IncidentsListingModel
                {
                    Id = i,
                    IncidentDate = incidentDateTime,
                    Status = Enum.GetName(typeof(IncidentStatus), IncidentStatus.InProgress),
                    IncidentSubCategory = "Cat" + i,
                    LoggedBy = "Bob",
                    RaisedBy = "Bob",
                    Product = "Product" + i,
                    TestDate = incidentDateTime,
                    IncidentNumber = i.ToString(),
                    TestCentreNumber = (i * 2).ToString()
                });
            }


            //Need the OdataConventionModelBuilder
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<IncidentsListingModel>("IncidentsOData");

            //Odataquerycontext
            var context = new ODataQueryContext(builder.GetEdmModel(), typeof(IncidentsListingModel));

            //The request
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                "http://localtest/odata/IncidentsOData/?%24format=json&%24filter=(Status+eq+'Draft'+or+Status+eq+'Raised'+or+Status+eq+'InProgress'+or+Status+eq+'Rejected')&%24count=true'");


            //ODataQueryOptions
            var queryOptions = new ODataQueryOptions<IncidentsListingModel>(context, request);



            //Mock up the odata repository
            var entityFactoryMock = Substitute.For<IEntityFactory>();
            var mockedRepository = Substitute.For<IncidentsODataRepository>(entityFactoryMock);
            mockedRepository.GetAll(null).ReturnsForAnyArgs(incidentsForResponse);

            //Mock up the authorisor
            var mockedAuthorisor = Substitute.For<IAssetAuthoriser>();
            mockedAuthorisor.IsAuthorised(null).ReturnsForAnyArgs(true);


            //New up the incident list odata controller and dependancies
            var incidentListOdataController = new IncidentsListingController(mockedRepository, mockedAuthorisor);



            //Execute the method to get the response
            IHttpActionResult response = incidentListOdataController.GetIncidentsListing(queryOptions).Result;


            //Assert the reponse is correct
            Assert.AreEqual(response.GetType(), typeof(OkNegotiatedContentResult<IEnumerable<IncidentsListingModel>>));
        }

    }
}
