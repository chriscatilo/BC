namespace BC.EQCS.Domain
{
    public interface IAspectValidationBuilderFactory<in TAspect, in TMaster, in TSchemaCriterion, in TEvent>
    {
        IValidationBuilder<TAspect, TSchemaCriterion, TEvent> CreateBuilderForNewModel(TMaster master);

        IValidationBuilder<TAspect, TSchemaCriterion, TEvent> CreateBuilder(TMaster master, int modelId);
    }
}