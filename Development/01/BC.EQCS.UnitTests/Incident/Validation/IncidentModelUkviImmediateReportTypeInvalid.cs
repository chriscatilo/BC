using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.UnitTests.Utils;
using NSubstitute;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Incident.Validation
{
    public class IncidentModelUkviImmediateReportTypeInvalid : IncidentModelValidatorTest
    {
        protected override IRepository<UkviImmediateReportTypeModel> Given_Ukvi_Immediate_Report_Type_Repository()
        {
            var repository = Substitute.For<IRepository<UkviImmediateReportTypeModel>>();

            repository.GetByUniqueCode("XXX").Returns(new UkviImmediateReportTypeModel {Code = "XXX"});

            return repository;
        }

        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                ReportUkvi = true,
                UkviImmediateReportType = "YYY"
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(IncidentValidationErrorMessages.UkviImmediateReportTypeDoesNotExist);
        }
    }


    public class IncidentModelUkviImmediateReportTypeIsNull : IncidentModelValidatorTest
    {
        protected override IRepository<UkviImmediateReportTypeModel> Given_Ukvi_Immediate_Report_Type_Repository()
        {
            var repository = Substitute.For<IRepository<UkviImmediateReportTypeModel>>();

            repository.GetByUniqueCode("XXX").Returns(new UkviImmediateReportTypeModel {Code = "XXX"});

            return repository;
        }

        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                ReportUkvi = false,
                UkviImmediateReportType = null
            };
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Pass();
        }
    }

    public class IncidentModelUkviImmediateReportTypeIsNotNull : IncidentModelValidatorTest
    {
        protected override IRepository<UkviImmediateReportTypeModel> Given_Ukvi_Immediate_Report_Type_Repository()
        {
            var repository = Substitute.For<IRepository<UkviImmediateReportTypeModel>>();

            repository.GetByUniqueCode("XXX").Returns(new UkviImmediateReportTypeModel {Code = "XXX"});

            return repository;
        }

        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                ReportUkvi = true,
                UkviImmediateReportType = "XXX"
            };
        }

        protected override void Then_On_Passing_Validation()
        {
            Assert.Pass();
        }
    }

    public class IncidentModelUkviImmediateReportTypeIsNullWhenRequired : IncidentModelValidatorTest
    {
        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                ReportUkvi = true,
                UkviImmediateReportType = null
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(
                IncidentValidationErrorMessages.UkviImmediateReportTypeShouldNotBeEmpty);
        }
    }

    public class IncidentModelUkviImmediateReportTypeIsNotNullWhenNotRequired : IncidentModelValidatorTest
    {
        protected override IRepository<UkviImmediateReportTypeModel> Given_Ukvi_Immediate_Report_Type_Repository()
        {
            var repository = Substitute.For<IRepository<UkviImmediateReportTypeModel>>();

            repository.GetByUniqueCode("XXX").Returns(new UkviImmediateReportTypeModel {Code = "XXX"});

            return repository;
        }

        protected override IncidentModel Given_Model()
        {
            return new IncidentModel
            {
                ReportUkvi = false,
                UkviImmediateReportType = "XXX"
            };
        }

        protected override void Then_On_Validation_Failure(ValidationFailureException exception)
        {
            exception.AssertValidationResultIncludes(IncidentValidationErrorMessages.UkviImmediateReportTypeShouldBeNull);
        }
    }
}