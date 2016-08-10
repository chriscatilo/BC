using System;
using System.Linq.Expressions;
using BC.EQCS.Domain.Schema;

namespace BC.EQCS.Domain
{
    public interface IModelUpdateStrategy<TModel>
    {
        void Execute(
            Expression<Func<TModel, dynamic>> persistenceProperty,
            ValueConstraint constraint,
            TModel changeDestination,
            TModel updateSource);
    }

    public interface IModelUpdateStrategy<TModel, out TKey> : IModelUpdateStrategy<TModel>
    {
        IModelUpdateStrategy<TModel, TKey> Decorate(IModelUpdateStrategy<TModel> strategyToDecorate);

        TKey Key { get; }
    }
}