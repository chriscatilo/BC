using System;
using System.Data.Entity.Core;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using BC.EQCS.Domain.Security;
using BC.EQCS.Entities;
using BC.EQCS.Repositories;
using BC.EQCS.Repositories.Security;
using BC.EQCS.Security.Models;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Startup
{
    [Binding]
    public class SetUp
    {
        [BeforeTestRun]
        public static void SetUpCurrentUser()
        {
            var currentIdentity = WindowsIdentity.GetCurrent();
            Assert.IsNotNull(currentIdentity, "CurrentIdentity is null");
            var currentPrincipal = new WindowsPrincipal(currentIdentity);
     
            using (var entityFactory = new EntityFactory())
            {
                var userRepository = new UserRepository(entityFactory, new UserModelValidator(), new AdminUnitRepository(new EntityFactory()));
                try
                {
                    var user = userRepository.GetUserForClaimsPrincipal(currentPrincipal).Result;
                }
                catch (AggregateException aggex)
                {
                    var allex = aggex.Flatten();
                    if (allex.InnerExceptions.Any(ex => ex is ObjectNotFoundException))
                    {
                        var newUser = TransformUserFromPrinciple(UserPrincipal.Current, currentPrincipal);
                        userRepository.Create(newUser);
                    }
                }
            }
        }

        [BeforeTestRun]
        public static void BuildDatabaseDemoData()
        {
            //Build up the full database
            //using (var entityFactory = new EntityFactory())
            //{
            //    TestDataSeeder.PopulateData(entityFactory.Create());
            //}
        }

        private static SecurityUserModel TransformUserFromPrinciple(UserPrincipal userPrincipal, ClaimsPrincipal claimsPrincipal)
        {
            return new SecurityUserModel(claimsPrincipal)
            {
                EmailAddress = userPrincipal.EmailAddress,
                FirstName = userPrincipal.GivenName,
                Surname = userPrincipal.Surname,
                ObjectGuid = userPrincipal.Guid.Value,
                Enabled = true,
                // TODO: assign application role
                WindowsAccountName = userPrincipal.SamAccountName,
                ApplicationRoles = new[] { new RoleModel { Name = "SuperAdmin" } }
            };
        }
    }
}
