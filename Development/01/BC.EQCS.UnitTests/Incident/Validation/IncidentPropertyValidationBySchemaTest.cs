using System;
using System.Linq.Expressions;
using BC.EQCS.Contracts;
using BC.EQCS.Domain;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Incident.Schema;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    [Ignore]
    public abstract class IncidentPropertyValidationBySchemaTest
    {
        protected abstract ValueConstraint Given_Value_Constraint();
        protected abstract IncidentModel Given_Model();

        protected virtual IncidentModel Given_Existing_Model()
        {
            return null;
        }

        protected virtual Scenario[] Given_Test_Scenarios()
        {
            return new[]
            {
                // TODO Chris: Use RESX
                new Scenario(model => model.IncidentDate, "Incident Date"),
                new Scenario(model => model.IncidentTime, "Incident Time"),
                new Scenario(model => model.Description, "Description"),
                new Scenario(model => model.Product, "Product"),
                new Scenario(model => model.RaisedBy, "Raised By"),
                new Scenario(model => model.TestCentre, "Test Centre"),
                new Scenario(model => model.TestLocation, "Test Location"),
                new Scenario(model => model.Category, "Category"),
                new Scenario(model => model.SubCategory, "Sub Category"),
                new Scenario(model => model.RiskRating, "Risk Rating"),
                new Scenario(model => model.ResidualRiskRating, "Residual Risk Rating"),
                new Scenario(model => model.TestDate, "Test Date"),
                new Scenario(model => model.ReportUkvi, "Report Ukvi"),
                new Scenario(model => model.UkviFollowUpDate, "Follow Up Date (GMT)"), 
                new Scenario(model => model.ReferringOrgSurname, "Referring Org Surname"),
                new Scenario(model => model.ReferringOrgFirstnames, "Referring Org Firstnames"),
                new Scenario(model => model.ReferringOrgJobTitle, "Referring Org Job Title"),
                new Scenario(model => model.ReferringOrgEmail, "Referring Org Email"),
                new Scenario(model => model.ReferringOrgType, "Referring Org Type"),
                new Scenario(model => model.ReferringOrgCountry, "Referring Org Country"),
                new Scenario(model => model.ReferringOrganisation, "Referring Organisation"),
                new Scenario(model => model.ReferringOrgExists, "Referring Org Exists")
            };
        }

        [Test, TestCaseSource("Given_Test_Scenarios")]
        public void Test(Scenario scenario)
        {
            var model = Given_Model();

            var validator = Given_Model_Validator(scenario);

            try
            {
                // When I validate the given model
                validator.ValidateModel(model);

                Then_On_Passing_Validation();
            }
            catch (ValidationFailureException ex)
            {
                Then_On_Validation_Failure(ex, scenario);
            }
        }

        protected virtual void Then_On_Validation_Failure(ValidationFailureException exception, Scenario scenario)
        {
            exception.AssertFailureDueToException();
        }

        protected virtual void Then_On_Passing_Validation()
        {
            Assert.Fail("Validation did not throw ValidationFailureException");
        }

        protected IModelValidator<IncidentModel> Given_Model_Validator(Scenario param)
        {
            // Given incident is being saved
            const IncidentCommand command = IncidentCommand.Save;

            // Given schema describes ReferringOrgName as being not-applicable\view-only\optional\mandatory
            var valueConstraint = Given_Value_Constraint();
            var schemaBuildDirector = Substitute.For<ISchemaBuildDirector<IncidentAttributes, IncidentSchemaKeyCriterion>>();

            var namedSchemata = new[]
            {
                new NamedSchema<IncidentAttributes>
                {
                    Name = "default",
                    Members =
                        new ModelSchema<IncidentAttributes>().BuildFor(param.PropertyNavigation, param.PropertyLabel)
                },
                new NamedSchema<IncidentAttributes>
                {
                    Name = command.ToString(),
                    Members =
                        new ModelSchema<IncidentAttributes>().BuildFor(param.PropertyNavigation,
                            constraint: valueConstraint)
                }
            };

            schemaBuildDirector.GetSchemataForNewModel(Arg.Any<IncidentSchemaKeyCriterion>())
                .Returns(namedSchemata);

            schemaBuildDirector.GetSchemata(Arg.Any<int>(), Arg.Any<IncidentSchemaKeyCriterion>())
                .Returns(namedSchemata);

            // Ensure you have the model properties mapped in this class, otherwise you will get an error
            // "Validation did not throw ValidationFailureException"
            var propertyMapping = new IncidentPersistanceAttributeMapping();

            var existingModel = Given_Existing_Model();

            var incidentRepository = Substitute.For<IRepository<IncidentModel>>();
            incidentRepository.GetById(Arg.Any<int>()).Returns(existingModel);

            var emptyValidator = new MockFluentValidator();

            var schemaAggregator = new IncidentPersistanceSchemaAggregator(schemaBuildDirector, propertyMapping);

            var builder = new IncidentValidationBuilder(emptyValidator, existingModel, schemaAggregator);

            var validator = builder
                .ForEvent(command)
                .Build();

            return validator;
        }

        public class Scenario
        {
            public Scenario(Expression<Func<IncidentAttributes, dynamic>> propertyNavigation, string label)
            {
                PropertyNavigation = propertyNavigation;
                PropertyLabel = label;
            }

            public Expression<Func<IncidentAttributes, dynamic>> PropertyNavigation { get; private set; }
            public string PropertyLabel { get; private set; }

            public override string ToString()
            {
                return PropertyLabel;
            }
        }

        private class MockFluentValidator : ModelValidator<IncidentModel>
        {
        }
    }
}