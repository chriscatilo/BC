using BC.EQCS.Contracts;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using NSubstitute;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    // see nested files for concrete tests
    public abstract class IncidentCandidateModelValidatorTest : ModelValidatorTest<IncidentCandidateModel>
    {
        protected override IModelValidator<IncidentCandidateModel> Given_Model_Validator()
        {
            var countryRepository = Substitute.For<IRepository<CountryModel>>();

            countryRepository.GetByUniqueCode("GB").Returns(
                new CountryModel
                {
                    Code = "GB",
                    Name = "United Kingdom"
                });

            var validator = new IncidentCandidateModelValidator(countryRepository);

            return validator;
        }
    }
}