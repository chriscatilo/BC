using System.Collections.Generic;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Web.Controllers.API;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident
{
    [TestFixture]
    public class ProductControllerTest
    {
        private IRepository<ProductModel> repo;
        private ProductController newController;
        private int productId = 0;
        public void TestSetUp()
        {
            repo = Substitute.For<IRepository<ProductModel>>();
            newController = Substitute.For<ProductController>(repo);
        }

        //api/product
        [Test]
        public void GetProductList()
        {
            TestSetUp();
            IEnumerable<ProductModel> results = newController.Get();

            Assert.IsNotNull(results);
        }
    }
}
