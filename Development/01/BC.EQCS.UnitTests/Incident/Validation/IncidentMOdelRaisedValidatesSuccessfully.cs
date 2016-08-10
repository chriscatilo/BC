using System;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentModelRaisedValidatesSuccessfully : IncidentModelValidatorTest
    {
        private const string Low = "LOW";

        protected override IRepository<RiskRatingModel> Given_Risk_Rating_Repository()
        {
            var repository = Substitute.For<IRepository<RiskRatingModel>>();

            repository.GetByUniqueCode(Arg.Is(Low)).Returns(info => new RiskRatingModel {Code = Low});

            return repository;
        }

        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                IncidentDate = DateTime.Today,
                Description = new RandomValueGenerators.ParagraphGenerator().Generate(),
                RiskRating = Low,
                TestCentre = "GS001",
                TestLocation = "GS001-01",
                Category = "CAT1"
            };
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Pass();
        }
    }
}