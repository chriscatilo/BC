using System;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    // Given a new incident with <Property> value
    // And user is Saving the incident
    // And <Property> is Not Applicable
    // When incident is validated
    // Then incident is invalid
    // And validation result includes message "'<Property>' cannot be set."
    public class NotApplicableIncidentPropertyValidationTest : IncidentPropertyValidationBySchemaTest
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
            return ValueConstraint.NotApplicable;
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception, Scenario scenario)
        {
            var msg = string.Format("'{0}' cannot be set.", scenario.PropertyLabel);
            exception.AssertValidationResultIncludes(msg);
        }
    }
}