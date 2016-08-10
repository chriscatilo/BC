using BC.EQCS.Contracts;
using BC.EQCS.Domain.Utils;
using FluentValidation;

namespace BC.EQCS.Domain
{
    public abstract class ModelValidator<TModel> : AbstractValidator<TModel>, IModelValidator<TModel>
    {
        public void ValidateModel(TModel model)
        {
            this.ValidateAndThrowIfInvalid(model);
        }
    }
}