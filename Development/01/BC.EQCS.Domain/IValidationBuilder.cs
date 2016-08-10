using BC.EQCS.Contracts;

namespace BC.EQCS.Domain
{
    public interface IValidationBuilder<in TModel, in TSchemaCriterion, in TEvent>
    {
        IValidationBuilder<TModel, TSchemaCriterion, TEvent> ForCriterion(TSchemaCriterion criterion);
        IValidationBuilder<TModel, TSchemaCriterion, TEvent> ForEvent(TEvent command);
        IModelValidator<TModel> Build();
    }
}