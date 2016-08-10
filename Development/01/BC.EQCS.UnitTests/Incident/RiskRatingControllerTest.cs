using System.Collections.Generic;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Web.Controllers.API;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident
{
    [TestFixture]
    public class RiskRatingControllerTest
    {
        private IRepository<RiskRatingModel> repo;
        private RiskRatingController newController;
        private int riskRatingId = 0;

        public void TestSetUp()
        {
            repo = Substitute.For<IRepository<RiskRatingModel>>();
            newController = Substitute.For<RiskRatingController>(repo);
        }

        //api/product
        [Test]
        public void GetRiskRatingList()
        {
            TestSetUp();
            IEnumerable<RiskRatingModel> results = newController.Get();

            Assert.IsNotNull(results);
        }
    }
}
