using System;
using System.Linq;
using System.Linq.Expressions;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Utils;
using FastMember;
using NUnit.Framework;

namespace BC.EQCS.UnitTests.Utils
{
    public static class AssertExtensions
    {
        public static void AssertThatObjectsAreSame<TModel>(this TModel expected, TModel actual,
            params string[] exclusions)
        {
            var e = ObjectAccessor.Create(expected);
            var a = ObjectAccessor.Create(actual);

            var properties = expected.GetType().GetProperties().Select(prop => prop.Name);

            exclusions = exclusions ?? new string[0];

            var propertyNames = properties.Where(name => !exclusions.Contains(name));

            foreach (var p in propertyNames)
            {
                var msg = string.Format("Expected and actual value of {0}.{1} are not the same",
                    typeof (TModel).Name, p);
                Assert.That(a[p], Is.EqualTo(e[p]), msg);
            }
        }

        public static void AssertValidationResultIncludes(this ValidationFailureException exception, string errorMessage)
        {
            Assert.That(
                exception.ValidationResult.Errors.Select(failure => failure.ErrorMessage), Contains.Item(errorMessage));
        }

        public static void AssertValidatorDidNotInvalidate<TModel>(this ValidationFailureException exception,
            Expression<Func<TModel, dynamic>> expression)
        {
            var propertyInfo = TypeHelpers.GetPropertyByExpression(expression);

            var propertiesInError = exception.ValidationResult.Errors.Select(failure => failure.PropertyName).ToList();

            Assert.IsFalse(propertiesInError.Any(msg => msg.Contains(propertyInfo.Name)),
                propertyInfo.Name + " was invalidated. Errors: " +
                propertiesInError.Aggregate((agg, current) => (agg + "," + current)));
        }

        public static void AssertFailureDueToException(this ValidationFailureException exception)
        {
            var errorMsgs =
                exception.ValidationResult.Errors.Select(failure => failure.ErrorMessage)
                    .Aggregate((aggMsg, nextMsg) => aggMsg + "|" + nextMsg);

            Assert.Fail("Validation failure occured: " + errorMsgs);
        }
    }
}