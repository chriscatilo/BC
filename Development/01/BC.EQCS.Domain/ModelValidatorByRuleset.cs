using System;
using System.Collections.Generic;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Utils;
using FluentValidation;

namespace BC.EQCS.Domain
{
    public abstract class ModelValidatorByRuleset<TModel, TRuleset> : AbstractValidator<TModel>, IModelValidator<TModel, TRuleset> where TModel : class
    {
        private readonly IDictionary<TRuleset, dynamic> _supportedRulesets = new Dictionary<TRuleset, dynamic>();

        public void ValidateModel(TModel model, TRuleset ruleSet)
        {
            if (!_supportedRulesets.ContainsKey(ruleSet))
            {
                var msg = string.Format("Validation for ruleset '{0}' is not supported", ruleSet);
                throw new NotSupportedException(msg);
            }

            this.ValidateAndThrowIfInvalid(model, ruleSet);
        }

        protected void AddRuleSet(Action ruleSetupAction, params TRuleset[] ruleSets)
        {
            foreach (var ruleSet in ruleSets)
            {
                RuleSet(ruleSet.ToString(), ruleSetupAction);
                _supportedRulesets.Add(ruleSet, null);
            }
        }
    }
}