using System;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    // Given a new incident with <Property> value
    // And user is Saving the incident
    // And <Property> is viewable only
    // When incident is validated
    // Then incident is invalid
    // And validation result includes message "'<Property>' cannot be set or altered."
    public class ViewOnlyNewIncidentPropertyInvalidatesTest : IncidentPropertyValidationBySchemaTest
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
                ReferringOrgExists = true
            };
        }

        protected override ValueConstraint Given_Value_Constraint()
        {
            return ValueConstraint.ViewOnly;
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception, Scenario scenario)
        {
            var msg = string.Format("'{0}' cannot be set or altered.", scenario.PropertyLabel);
            exception.AssertValidationResultIncludes(msg);
        }
    }

    // Given an incident with <Property> value
    // And user is Saving the incident with a diffrent <Property> value
    // And referring org name is viewable only
    // When incident is validated
    // Then incident is invalid
    // And validation result includes message "'<Property>' cannot be set or altered."
    public class ViewOnlyExistingIncidentPropertyInvalidatesTest : IncidentPropertyValidationBySchemaTest
    {
        protected override IncidentModel Given_Existing_Model()
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
                ReferringOrgExists = true
            };
        }

        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                IncidentDate = DateTime.Today.AddDays(1),
                IncidentTime = "YYY",
                Description = "YYY",
                Product = "YYY",
                RaisedBy = "YYY",
                TestCentre = "YYY",
                TestLocation = "YYY",
                Category = "YYY",
                SubCategory = "YYY",
                RiskRating = "YYY",
                ResidualRiskRating = "YYY",
                TestDate = DateTime.Today.AddDays(1),
                UkviFollowUpDate = DateTime.Today.AddDays(1),
                ReportUkvi = false,
                ReferringOrgSurname = "YYY",
                ReferringOrgFirstnames = "YYY",
                ReferringOrgJobTitle = "YYY",
                ReferringOrgEmail = "YYY",
                ReferringOrgType = "YYY",
                ReferringOrgCountry = "YYY",
                ReferringOrganisation = "YYY",
            };
        }

        protected override ValueConstraint Given_Value_Constraint()
        {
            return ValueConstraint.ViewOnly;
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception, Scenario scenario)
        {
            var msg = string.Format("'{0}' cannot be set or altered.", scenario.PropertyLabel);
            exception.AssertValidationResultIncludes(msg);
        }
    }

    // Given an incident with <Property> value
    // And user is Saving the incident with the same <Property> value
    // And referring org name is viewable only
    // When incident is validated
    // Then incident is valid
    public class ViewOnlyExistingIncidentPropertyValidatesTest : IncidentPropertyValidationBySchemaTest
    {
        protected override IncidentModel Given_Existing_Model()
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
            };
        }

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
            };
        }

        protected override ValueConstraint Given_Value_Constraint()
        {
            return ValueConstraint.ViewOnly;
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Pass();
        }
    }
}