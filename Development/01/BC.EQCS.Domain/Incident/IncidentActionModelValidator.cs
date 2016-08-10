using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Models;
using BC.EQCS.Security.Models;
using FluentValidation;

namespace BC.EQCS.Domain.Incident
{
    public class IncidentActionModelValidator : ModelValidatorByRuleset<IncidentActionModel, IncidentActionRuleSet>, IModelValidator<IncidentActionModel>
    {
        private readonly IRepository<IncidentModel> _incidentRepository;
        private readonly IAsyncRepository<SecurityUserModel> _userModelRepository;

        public IncidentActionModelValidator(IRepository<IncidentModel> incidentRepository, IAsyncRepository<SecurityUserModel> userModelRepository)
        {
            _incidentRepository = incidentRepository;
            _userModelRepository = userModelRepository;

            AddRuleSet(CreatingActionRules, IncidentActionRuleSet.CreatingActionRules);
            AddRuleSet(EditingActionRules, IncidentActionRuleSet.EditingActionRules);
            AddRuleSet(RespondingToActionAndClosingRules, IncidentActionRuleSet.RespondingToActionAndClosingRules);
        }

        private void CreatingActionRules()
        {
            DefaultRulesSetup();
        }

        private void EditingActionRules()
        {
            DefaultRulesSetup();
        }

        private void RespondingToActionAndClosingRules()
        {
            DefaultRulesSetup();

            RuleFor(model => model.ActionResponse)
                .NotEmpty();
        }

        private void DefaultRulesSetup()
        {
            RuleFor(model => model.IncidentId)
                .Must(id => _incidentRepository.Exists(id))
                .WithMessage(IncidentValidationErrorMessages.IncidentDoesNotExist);


            RuleFor(model => model.ActionDescription)
                 .NotEmpty()
                 .WithMessage("Action description cannot be blank");

            RuleFor(model => (model.AssignedTo))
                .Must(list => list != null && list.Any())
                .When(model => !model.AssignedToTestCentre)
                .WithName("Assigned to");

            RuleFor(model => (model.AssignedTo))
                .Must(list => list == null || !list.Any())
                .When(model => model.AssignedToTestCentre)
                .WithName("Assigned to");


            //RuleFor(model => model.AssignedTo)
            //    .Must(users => users.All(u => _userModelRepository.Exists(u).Result))
            //    .When(model => (model.AssignedTo != null && model.AssignedTo.Any()))
            //    .WithMessage(IncidentValidationErrorMessages.UserDoesNotExist);

            //Task.Run(() => _userModelRepository.Exists(users.ObjectGuid.ToString())).Result)
            //
        }

        //public void ValidateModel(IncidentActionModel model, Int32 ruleSet)
        //{
        //    base.ValidateModel(model, ruleSet);
        //}

        public void ValidateModel(IncidentActionModel model)
        {
            base.ValidateModel(model, IncidentActionRuleSet.CreatingActionRules);
        }

        
    }

    public enum IncidentActionRuleSet : byte
    {
        CreatingActionRules,
        EditingActionRules,
        RespondingToActionAndClosingRules
    }
}