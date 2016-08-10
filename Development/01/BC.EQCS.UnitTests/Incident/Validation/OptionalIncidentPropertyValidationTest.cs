using System;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    // Given a new incident with a <Property> value
    // And user is Saving the incident
    // And <Property> is Optional
    // When incident is validated
    // Then incident is valid
    public class OptionalIncidentPropertyValidationTest : IncidentPropertyValidationBySchemaTest
    {
        protected override IncidentModel Given_Model()
        {
            var model = new IncidentModel
            {
                IncidentDate = DateTime.Today,
                IncidentTime = "XXX",
                Description = "XXX",
                Product = "XXX",
                RaisedBy = "XXX",
                TestCentre = "XXX",
                TestLocation = "XXX",
                Category = "XXX",
                SubCategory = "XXX",
                RiskRating = "XXX",
                ResidualRiskRating = "XXX",
                TestDate = DateTime.Today,
                ReportUkvi = true,
                ReferringOrgSurname = "XXX",
                ReferringOrgFirstnames = "XXX",
                ReferringOrgJobTitle = "XXX",
                ReferringOrgEmail = "XXX",
                ReferringOrgType = "XXX",
                ReferringOrgCountry = "XXX",
                ReferringOrganisation = "XXX",
            };

            return model;
        }

        protected override ValueConstraint Given_Value_Constraint()
        {
            return ValueConstraint.Optional;
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Pass();
        }
    }

    // Given a new incident with an empty <Property> value
    // And user is Saving the incident
    // And <Property> is Optional
    // When incident is validated
    // Then incident is valid
    public class OptionalEmptyIncidentPropertyValidationTest : IncidentPropertyValidationBySchemaTest
    {
        protected override IncidentModel Given_Model()
        {
            var model = new IncidentModel
            {
                IncidentDate = DateTime.Today,
                IncidentTime = "XXX",
                Description = "XXX",
                Product = "XXX",
                RaisedBy = "XXX",
                TestCentre = "XXX",
                TestLocation = "XXX",
                Category = "XXX",
                SubCategory = "XXX",
                RiskRating = "XXX",
                ResidualRiskRating = "XXX",
                TestDate = DateTime.Today,
                ReportUkvi = true,
                ReferringOrgSurname = "XXX",
                ReferringOrgFirstnames = "XXX",
                ReferringOrgJobTitle = "XXX",
                ReferringOrgEmail = "XXX",
                ReferringOrgType = "XXX",
                ReferringOrgCountry = "XXX",
                ReferringOrganisation = "XXX",
            };

            return model;
        }

        protected override ValueConstraint Given_Value_Constraint()
        {
            return ValueConstraint.Optional;
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Pass();
        }
    }
}