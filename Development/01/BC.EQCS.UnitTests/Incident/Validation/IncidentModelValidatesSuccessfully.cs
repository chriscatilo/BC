using System;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentModelValidatesSuccessfully : IncidentModelValidatorTest
    {
        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                IncidentDate = DateTime.Today,
                Description = new RandomValueGenerators.ParagraphGenerator().Generate(),
                TestCentre = "GS001",
                TestLocation = "GS001-01",
                Category = "CAT1",
                SubCategory = "CAT1-01"
            };
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Pass();
        }
    }
}