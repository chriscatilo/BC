using System;
using System.Linq;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Models;
using FluentValidation;

namespace BC.EQCS.Domain.Utils
{
    public static class ValidatorExtensions
    {
        /// <summary>
        ///     Reference Code must exist (e.g. Person.AddressCountry)
        /// </summary>
        /// <typeparam name="TBeingValidated">e.g. Person</typeparam>
        /// <typeparam name="TValidatedAgainst">e.g. Country</typeparam>
        public static IRuleBuilderOptions<TBeingValidated, string> MustBeValidCode<TBeingValidated, TValidatedAgainst>(
            this IRuleBuilder<TBeingValidated, string> options, Func<string, TValidatedAgainst> getValue)
            where TValidatedAgainst : class
        {
            return options.Must(property =>
            {
                if (string.IsNullOrEmpty(property))
                {
                    return false;
                }

                var value = getValue(property);
                return value != null;
            });
        }

        /// <summary>
        ///     Reference Code must exist, empty or null (e.g. Person.AddressCountry)
        /// </summary>
        /// <typeparam name="TBeingValidated">e.g. Person</typeparam>
        /// <typeparam name="TValidatedAgainst">e.g. Country</typeparam>
        public static IRuleBuilderOptions<TBeingValidated, string> MustBeValidNullOrEmptyCode
            <TBeingValidated, TValidatedAgainst>(
            this IRuleBuilder<TBeingValidated, string> options, Func<string, TValidatedAgainst> getValue)
            where TValidatedAgainst : class
        {
            return options.Must(property =>
            {
                if (string.IsNullOrEmpty(property))
                {
                    return true;
                }

                var value = getValue(property);
                return value != null;
            });
        }

        public static IRuleBuilderOptions<TBeingValidated, dynamic> IsNull<TBeingValidated>(
            this IRuleBuilder<TBeingValidated, dynamic> options)
        {
            return options.Must(property => property == null);
        }

        public static IRuleBuilderOptions<TValue, dynamic> MustBeEqual<TValue>(
            this IRuleBuilder<TValue, dynamic> options, TValue model, Func<TValue, dynamic> getValue)
        {
            return options.Must(value =>
            {
                var left = model == null ? null : getValue(model);
                return left == value;
            });
        }

        public static void ValidateAndThrowIfInvalid<TModel, TRuleset>(this IValidator<TModel> validator, TModel model,
            TRuleset validationRules)
        {
            var result = validator.Validate(model, ruleSet: validationRules.ToString());

            if (result.IsValid) return;

            var failures =
                result.Errors.Select(
                    failure => new ValidationFailure(failure.PropertyName, failure.ErrorMessage, failure.AttemptedValue));

            var validationResult = new ValidationResult(result.IsValid, failures);

            throw new ValidationFailureException(validationResult);
        }

        public static void ValidateAndThrowIfInvalid<TModel>(this IValidator<TModel> validator, TModel model)
        {
            var result = validator.Validate(model);

            if (result.IsValid) return;

            var failures =
                result.Errors.Select(
                    failure => new ValidationFailure(failure.PropertyName, failure.ErrorMessage, failure.AttemptedValue));

            var validationResult = new ValidationResult(result.IsValid, failures);

            throw new ValidationFailureException(validationResult);
        }
    }
}