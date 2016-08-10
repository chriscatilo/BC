using System;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Web.Controllers.API;
using NSubstitute;
using NUnit.Framework;


namespace BC.EQCS.UnitTests.Incident
{
    [TestFixture]
    public class IncidentTypeControllerTest
    {
        private IRepository<IncidentTypeModel> repo;
        private IncidentTypeController newController;
        private int incidentTypeId = 0;

        public void TestSetUp()
        {
            repo = Substitute.For<IRepository<IncidentTypeModel>>();
            newController = Substitute.For<IncidentTypeController>(repo);
        }

        //api/product
        [Test]
        public void GetIncidentTypeList()
        {
            TestSetUp();
            IEnumerable<IncidentTypeModel> results = newController.Get();

            Assert.IsNotNull(results);
        }

        [Test]
        public void GetIncidentTypeByIdWithValidId()
        {
            TestSetUp();
            incidentTypeId = 1;
            repo.GetById(incidentTypeId).Returns(new IncidentTypeModel());
            IncidentTypeModel result = newController.Get(incidentTypeId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetIncidentTypeByIdWithInvalidId()
        {
            TestSetUp();
            incidentTypeId = 0;
            IncidentTypeModel result = newController.Get(incidentTypeId);

            Assert.IsNull(result);
        }
    }
}
