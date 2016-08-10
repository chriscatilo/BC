using System;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    // Given a new incident with a <Property>
    // And user is Saving the incident
    // And <Property> is Mandatory
    // When incident is validated
    // Then incident is valid
    public class MandatoryIncidentPropertyIsValidTest : IncidentPropertyValidationBySchemaTest
    {
        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
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
                UkviFollowUpDate = DateTime.Today,
                ReportUkvi = true,
                ReferringOrgSurname = "XXX",
                ReferringOrgFirstnames = "XXX",
                ReferringOrgJobTitle = "XXX",
                ReferringOrgEmail = "XXX",
                ReferringOrgType = "XXX",
                ReferringOrgCountry = "XXX",
                ReferringOrganisation = "XXX",
                ReferringOrgExists = true,
            };
        }

        protected override ValueConstraint Given_Value_Constraint()
        {
            return ValueConstraint.Mandatory;
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Pass();
        }
    }

    // Given a new incident with an empty <Property>
    // And user is Saving the incident
    // And <Property> is Mandatory
    // When incident is validated
    // Then incident is invalid
    // And validation result includes message "'<Property>' should not be empty."
    public class MandatoryIncidentPropertyIsInvalidTest : IncidentPropertyValidationBySchemaTest
    {
        protected override IncidentModel Given_Model()
        {
            var model = new IncidentModel
            {
                IncidentDate = null,
                IncidentTime = null,
                Description = string.Empty,
                Product = string.Empty,
                RaisedBy = string.Empty,
                TestCentre = string.Empty,
                TestLocation = string.Empty,
                Category = string.Empty,
                SubCategory = string.Empty,
                RiskRating = string.Empty,
                ResidualRiskRating = string.Empty,
                TestDate = null,
                UkviFollowUpDate = null,
                ReportUkvi = null,
                ReferringOrgSurname = string.Empty,
                ReferringOrgFirstnames = string.Empty,
                ReferringOrgJobTitle = string.Empty,
                ReferringOrgEmail = string.Empty,
                ReferringOrgType = string.Empty,
                ReferringOrgCountry = string.Empty,
                ReferringOrganisation = string.Empty,
                ReferringOrgExists = null,
            };

            return model;
        }

        protected override ValueConstraint Given_Value_Constraint()
        {
            return ValueConstraint.Mandatory;
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception, Scenario scenario)
        {
            var msg = string.Format("'{0}' should not be empty.",scenario.PropertyLabel);
            exception.AssertValidationResultIncludes(msg);
        }
    }
}