using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentModelRefOrganisationCodeNotExists : IncidentModelValidatorTest
    {
        protected override IRepository<OrganisationTypeModel> Given_Org_Type_Repository()
        {
            var repo = Substitute.For<IRepository<OrganisationTypeModel>>();

            var mockedOrgType = new OrganisationTypeModel()
            {
                Organisations = new[]
                {
                    new OrganisationModel() { Code = "YYY" }
                }
            };

            repo.GetByUniqueCode(Arg.Any<string>()).Returns(mockedOrgType);

            return repo;
        }

        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                ReferringOrgType = "XXX",
                ReferringOrganisation = "XXXX",
                ReferringOrgExists = true
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(IncidentValidationErrorMessages.ReferringOrgDoesNotExist);
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Fail("Validation did not throw ValidationFailureException");
        }
    }
}