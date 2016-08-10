using System;
using System.Threading.Tasks;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Models;
using FluentValidation;

namespace BC.EQCS.Domain.Incident
{
    public class IncidentActivityLogModelValidator :
        ModelValidatorByRuleset<IncidentActivityLogModel, IncidentActivityLogType>,
        IModelValidator<IncidentActivityLogModel>
    {
        private readonly IRepository<IncidentModel> _incidentRepository;
        private readonly IAsyncRepository<SecurityUserModel> _userModelRepository;

        public IncidentActivityLogModelValidator(
            IRepository<IncidentModel> incidentRepository,
            IAsyncRepository<SecurityUserModel> userModelRepository)
        {
            _incidentRepository = incidentRepository;
            _userModelRepository = userModelRepository;

            AddRuleSet(DefaultPlusPayload, IncidentActivityLogType.Submission);
            AddRuleSet(DefaultPlusPayload, IncidentActivityLogType.Acceptance);
            AddRuleSet(DefaultPlusPayload, IncidentActivityLogType.Rejection);
            AddRuleSet(DefaultPlusPayload, IncidentActivityLogType.Reopening);
            AddRuleSet(DefaultPlusPayload, IncidentActivityLogType.Closure);
        }

        private void DefaultNoPayload()
        {
            DefaultSetup();

            RuleFor(model => model.Payload)
                .Must(value => value == null);
        }

        private void DefaultPlusPayload()
        {
            DefaultSetup();

            RuleFor(model => model.Payload)
                .NotEmpty();
        }

        private void DefaultSetup()
        {
            RuleFor(model => model.IncidentId)
                .Must(id => _incidentRepository.Exists(id))
                .WithMessage(IncidentValidationErrorMessages.IncidentDoesNotExist);

            RuleFor(model => model.DateTimeOfActivity)
                .NotEmpty()
                .Must(dateTime => dateTime <= DateTime.Now)
                .WithMessage(IncidentValidationErrorMessages.DateTimeOfActivityCannotBeInTheFuture);

            RuleFor(model => model.LogType)
                .NotEmpty();

            RuleFor(model => model.User)
                .NotEmpty()
                .Must(
                    user =>
                        user != null &&
                        Task.Run(() => _userModelRepository.Exists(user.ObjectGuid.ToString())).Result)
                .WithMessage(IncidentValidationErrorMessages.UserDoesNotExist);
        }

        public void ValidateModel(IncidentActivityLogModel model, string rulesetName)
        {
            throw new NotImplementedException();
        }

        public void ValidateModel(IncidentActivityLogModel model)
        {
            base.ValidateModel(model, model.LogType);
        }
    }
}