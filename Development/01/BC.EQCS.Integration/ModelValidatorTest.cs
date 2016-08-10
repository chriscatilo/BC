using System;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using NUnit.Framework;

namespace BC.EQCS.Integration
{
    public abstract class ModelValidatorTest<TModel, TRuleset>
        where TModel : class
    {
        [Test]
        public void Test()
        {
            var model = Given_Model();

            var validator = Given_Model_Validator();

            try
            {
                // When I validate the given model
                validator.ValidateModel(model, Given_Validation_Ruleset());

                Then_On_Passing_Validation();
            }
            catch (ValidationFailureException ex)
            {
                Then_On_Validation_Failure(ex);
            }
            catch (NotSupportedException)
            {
                Then_On_RuleSet_Not_Supported_Failure();
            }

        }

        protected abstract TRuleset Given_Validation_Ruleset();

        protected virtual bool Given_Validation_Ruleset_Is_Supported()
        {
            return true;
        }

        protected abstract IModelValidator<TModel, TRuleset> Given_Model_Validator();

        protected abstract TModel Given_Model();

        protected abstract void Then_On_Validation_Failure(ValidationFailureException exception);

        protected abstract void Then_On_Passing_Validation();

        private void Then_On_RuleSet_Not_Supported_Failure()
        {
            if (Given_Validation_Ruleset_Is_Supported())
            {
                Assert.Fail("Validator failed in supporting ruleset '{0}'", Given_Validation_Ruleset());
            }
            else
            {
                Assert.Pass();
            }
        }
    }


    public abstract class ModelValidatorTest<TModel>
        where TModel : class
    {
        [Test]
        public void Test()
        {
            var model = Given_Model();

            var validator = Given_Model_Validator();

            try
            {
                // When I validate the given model
                validator.ValidateModel(model);

                Then_On_Passing_Validation();
            }
            catch (ValidationFailureException ex)
            {
                Then_On_Validation_Failure(ex);
            }
        }
        
        protected abstract IModelValidator<TModel> Given_Model_Validator();

        protected abstract TModel Given_Model();

        protected abstract void Then_On_Validation_Failure(ValidationFailureException exception);

        protected abstract void Then_On_Passing_Validation();
    }
}