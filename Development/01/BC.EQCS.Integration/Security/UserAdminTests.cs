using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using BC.EQCS.Entities.Models;
using BC.EQCS.Integration.Utils;
using BC.EQCS.Repositories.Security;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Security
{
    [Ignore("This is obsolete, the users are now populated in the database directly. This may be bought back when the user admin screen is completed ")]
    [TestFixture]
    public class UserAdminTests
    {
        private const string DummyAccountName = "DummyAccount";
        private ITestRunner _testRunner;

        [TestFixtureSetUp]
        public virtual void FeatureSetup()
        {
            if (!DomainChecker.IsInDomain())
            {
                Assert.Ignore("Domain unavailable - tests not running");
            }

            var context = new EqcsEntities();
            var dummyUser = context.Users.FirstOrDefault(au => au.Login == DummyAccountName);
            if (dummyUser != null)
            {
                context.Entry(dummyUser).State = EntityState.Deleted;
                context.SaveChanges();
            }
            _testRunner = TestRunnerManager.GetTestRunner();
            FeatureInfo featureInfo = new FeatureInfo(new CultureInfo("en-US"), "User Admin Tests", "", ProgrammingLanguage.CSharp, null);
            _testRunner.OnFeatureStart(featureInfo);
        }


        [Test]
        public void FindAUserCreateAndGetAndUpdate()
        {
            var client = new Client();
            var search = new SearchFilter
            {
                FirstName = "dum",
                Surname = "acc"
            };
            var users = client.SearchUsersInActiveDirectory(search).ToList();
            
            Assert.IsTrue(users.Count > 0);
            var dummyAccount = users.First();
            Assert.AreEqual("Dummy", dummyAccount.FirstName); 
            Assert.AreEqual("Account", dummyAccount.Surname);
            Assert.AreNotEqual(Guid.Empty, dummyAccount.ObjectGuid);

            var createResponse = client.CreateUserFromActiveDirectory(dummyAccount.ObjectGuid);
            Assert.IsTrue(createResponse.IsSuccessStatusCode);

            var createdUser = client.GetUser(dummyAccount.ObjectGuid);
            Assert.IsNotNull(createdUser);
            Assert.AreEqual(dummyAccount.FirstName, createdUser.FirstName);
            Assert.AreEqual(dummyAccount.Surname, createdUser.Surname);
            Assert.AreEqual(dummyAccount.Login, createdUser.WindowsAccountName);
            Assert.IsNotNull(createdUser.ApplicationRoles.First());
            Assert.IsNotNull(createdUser.Role);
            Assert.AreEqual(createdUser.Role.Name, createdUser.ApplicationRoles.First().Name);
            Assert.AreEqual("ReadOnlyUser", createdUser.Role.Name);

            Assert.IsFalse(createdUser.Enabled);

            createdUser.Enabled = true;
            var updateResponse = client.UpdateUser(createdUser);
            Assert.IsTrue(updateResponse.IsSuccessStatusCode);

            var updatedUser = client.GetUser(dummyAccount.ObjectGuid);
            Assert.IsNotNull(updatedUser);
            Assert.IsTrue(updatedUser.Enabled);
        }



        [TestFixtureTearDown]
        public virtual void FeatureTearDown()
        {
            if (_testRunner != null)
            {
                _testRunner.OnFeatureEnd();
                _testRunner = null;
            }
        }
        
    }
}
