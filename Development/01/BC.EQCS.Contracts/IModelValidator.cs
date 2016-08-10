namespace BC.EQCS.Contracts
{
    public interface IModelValidator<in TModel>
    {
        void ValidateModel(TModel model);
    }

    public interface IModelValidator<in TModel, in TParam>
        where TModel : class
    {
        void ValidateModel(TModel model, TParam parameter);
    }
}