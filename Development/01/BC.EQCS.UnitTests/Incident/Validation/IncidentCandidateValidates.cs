using System;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentCandidateValidates : IncidentCandidateModelValidatorTest
    {
        protected override IncidentCandidateModel Given_Model()
        {
            return new IncidentCandidateModel
            {
                Number = "1234",
                Surname = "Perry",
                Firstnames = "Fred",
                Address = new RandomValueGenerators.ParagraphGenerator().Generate(),
                DateOfBirth = DateTime.Today.AddYears(-25),
                Gender = Gender.Male,
                IdDocumentNumber = "09987",
                TrfNumber = "Trf0987",
                DateTrfCancelled = DateTime.Today.AddDays(-5),
                UKVIRefNumber = "UKVI456789",
                Nationality = "GB"
            };
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Pass();
        }
    }
}