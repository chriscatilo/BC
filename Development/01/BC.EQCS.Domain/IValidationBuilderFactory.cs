namespace BC.EQCS.Domain
{
    public interface IValidationBuilderFactory<in TModel, in TSchemaCriterion, in TEvent>
    {
        IValidationBuilder<TModel, TSchemaCriterion, TEvent> CreateBuilderForNewModel();

        IValidationBuilder<TModel, TSchemaCriterion, TEvent> CreateBuilder(int modelId);
    }
}