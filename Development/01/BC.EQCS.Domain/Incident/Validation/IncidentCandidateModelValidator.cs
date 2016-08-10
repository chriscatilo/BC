using System;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Utils;
using BC.EQCS.Models;
using FluentValidation;

namespace BC.EQCS.Domain.Incident.Validation
{
    public class IncidentCandidateModelValidator : ModelValidator<IncidentCandidateModel>
    {
        public IncidentCandidateModelValidator(IRepository<CountryModel> countryRepository)
        {
            RuleFor(model => model.Nationality)
                .MustBeValidNullOrEmptyCode(countryRepository.GetByUniqueCode)
                .WithMessage(IncidentValidationErrorMessages.CountryIsInvalid);

            RuleFor(model => model.DateOfBirth.Value)
            .LessThanOrEqualTo(DateTime.Now).When(model=>model.DateOfBirth.HasValue)
            .WithMessage(IncidentValidationErrorMessages.BirthDateCannotBeInFuture);

        }
    }
}