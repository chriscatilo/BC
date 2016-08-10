namespace BC.EQCS.Domain
{
    public interface IModelUpdaterFactory<TModel, in TSchemaKeyCriterion, in TEvent, in TStrategy>
    {
        IModelUpdater<TModel, TSchemaKeyCriterion, TEvent> CreateUpdater(params TStrategy[] strategies);
    }
}