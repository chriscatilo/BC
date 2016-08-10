using BC.EQCS.Contracts;
using BC.EQCS.Domain.Utils;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using FluentValidation;

namespace BC.EQCS.Domain.Incident.Validation
{
    public class IncidentModelValidator : ModelValidator<IncidentModel>
    {
        public IncidentModelValidator(
            IRepository<RiskRatingModel> riskRatingRepository,
            IRepository<ResidualRiskRatingModel> residualRiskRatingRepository,
            IRepository<OrganisationTypeModel> orgTypeRepository,
            ITreeRepository<IncidentClassModel> incidentClassRepository,
            IRepository<UkviImmediateReportTypeModel> ukviImmediateReportTypeRepository,
            IUserContext userContext)
        {
            RuleFor(model => model.RiskRating)
                .MustBeValidNullOrEmptyCode(riskRatingRepository.GetByUniqueCode)
                .WithMessage(IncidentValidationErrorMessages.RiskRatingIsInvalid);

            RuleFor(model => model.RaisedBy).Length(0, 50);
            

            RuleFor(model => model.TestCentre)
                .MustBeValidNullOrEmptyCode(
                    code => userContext.CurrentUser.GetAdminUnit(code, Constants.AdminUnitTypes.TestCentre))
                .WithMessage(IncidentValidationErrorMessages.TestCentreDoesNotExist);

            RuleFor(model => model.TestLocation)
                .MustBeValidNullOrEmptyCode(
                    code => userContext.CurrentUser.GetAdminUnit(code, Constants.AdminUnitTypes.TestLocation))
                .WithMessage(IncidentValidationErrorMessages.TestLocationDoesNotExist)
                .Must((model, code) => model.IsTestLocationBelongToCentre(userContext.CurrentUser) ?? true)
                .WithMessage(IncidentValidationErrorMessages.TestLocationDoesNotBelongToTestCentre);

            RuleFor(model => model.Category)
                .MustBeValidNullOrEmptyCode(code => userContext.CurrentUser.GetIncidentClass(code))
                .WithMessage(IncidentValidationErrorMessages.CategoryDoesNotExist);

            RuleFor(model => model.SubCategory)
                .MustBeValidNullOrEmptyCode(code => userContext.CurrentUser.GetIncidentClass(code))
                .WithMessage(IncidentValidationErrorMessages.SubCategoryDoesNotExist)
                .Must((model, code) => model.IsSubCategoryBelongToCategory(incidentClassRepository) ?? true)
                .WithMessage(IncidentValidationErrorMessages.SubCategoryDoesNotBelongToCategory);

            RuleFor(model => model.ResidualRiskRating)
                .MustBeValidNullOrEmptyCode(residualRiskRatingRepository.GetByUniqueCode)
                .WithMessage(IncidentValidationErrorMessages.ResidualRiskRatingIsInvalid);


            RuleFor(model => model.ReferringOrgSurname)
                .Length(0, 255)
                .When(model => !string.IsNullOrWhiteSpace(model.ReferringOrgSurname));


            RuleFor(model => model.ReferringOrgFirstnames)
                .Length(0, 255)
                .When(model => !string.IsNullOrWhiteSpace(model.ReferringOrgFirstnames));

            RuleFor(model => model.ReferringOrgJobTitle)
                .Length(0, 255)
                .When(model => !string.IsNullOrWhiteSpace(model.ReferringOrgJobTitle));


            RuleFor(model => model.ReferringOrgEmail)
                .EmailAddress()
                .Length(10,255)
                .When(model => !string.IsNullOrWhiteSpace(model.ReferringOrgEmail))
                .WithMessage(IncidentValidationErrorMessages.ReferringOrgEmailMustBeAValidEmailAddress);

            RuleFor(model => model.NoOfCandidates)
                .GreaterThanOrEqualTo(1)
                .WithMessage(IncidentValidationErrorMessages.NoOfCandidateLessThanOne);

            RuleFor(model => model.ReferringOrgType)
                .MustBeValidNullOrEmptyCode(orgTypeRepository.GetByUniqueCode)
                .WithMessage(IncidentValidationErrorMessages.ReferringOrgTypeDoesNotExist);

            RuleFor(model => model.ReferringOrgExists)
                .Must((model, value) => model.IsReferringOrgExistsNotEmpty(orgTypeRepository) ?? true)
                .WithMessage(IncidentValidationErrorMessages.ReferringOrgExistsIsEmpty);

            RuleFor(model => model.ReferringOrganisation)
                .Must((model, value) => model.IsReferringOrganisationValid(orgTypeRepository) ?? true)
                .WithMessage(IncidentValidationErrorMessages.ReferringOrgDoesNotExist);

            RuleFor(model => model.UkviImmediateReportType)
                .MustBeValidNullOrEmptyCode(ukviImmediateReportTypeRepository.GetByUniqueCode)
                .WithMessage(IncidentValidationErrorMessages.UkviImmediateReportTypeDoesNotExist)
                .Must((model, value) => model.IsUkviReportTypeRequiredAndCompleted() ?? true)
                .WithMessage(IncidentValidationErrorMessages.UkviImmediateReportTypeShouldNotBeEmpty)
                .Must((model, value) => model.IsUkviReportTypeNotRequiredAndNullOrEmpty() ?? true)
                .WithMessage(IncidentValidationErrorMessages.UkviImmediateReportTypeShouldBeNull);
        }
    }
}