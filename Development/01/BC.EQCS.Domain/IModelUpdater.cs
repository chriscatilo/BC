namespace BC.EQCS.Domain
{
    public interface IModelUpdater<TModel, in TSchemaKeyCriterion, in TEvent>
    {
        IModelUpdater<TModel, TSchemaKeyCriterion, TEvent> ForCriterion(TSchemaKeyCriterion criterion);
        IModelUpdater<TModel, TSchemaKeyCriterion, TEvent> ForEvent(TEvent @event);
        TModel Update(TModel update);
        TModel Update(int modelId, TModel update);
    }
}