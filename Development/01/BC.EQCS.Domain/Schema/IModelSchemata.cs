namespace BC.EQCS.Domain.Schema
{
    public interface IModelSchemata<TModel>
    {
        ModelSchema<TModel> Get(string key);

        ModelSchema<TModel> GetDefault();
    }
}