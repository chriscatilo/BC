namespace BC.EQCS.Domain.Schema
{
    public interface ISchemaBuilderFactory<TModel, in TStatus, in TAugmentKey>
    {
        ISchemataBuilder<TModel, TStatus, TAugmentKey> CreateBuilderByKey(string key);
    }
}