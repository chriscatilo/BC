namespace BC.EQCS.Domain.Schema
{
    public class NamedSchema<TModel>
    {
        public string Name { get; set; }
        public ModelSchema<TModel> Members { get; set; }
    }
}