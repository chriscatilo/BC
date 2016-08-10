using System;
using System.Linq.Expressions;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;

namespace BC.EQCS.Domain.Incident.ModelUpdater
{
    public abstract class IncidentModelUpdateStrategy : IModelUpdateStrategy<IncidentModel, IncidentModelUpdateStrategyKey>
    {
        private readonly IncidentModelUpdateStrategyKey _key;

        protected IncidentModelUpdateStrategy(IncidentModelUpdateStrategyKey key)
        {
            _key = key;
        }

        private IModelUpdateStrategy<IncidentModel> _decoratedStrategy;

        IModelUpdateStrategy<IncidentModel, IncidentModelUpdateStrategyKey> IModelUpdateStrategy<IncidentModel, IncidentModelUpdateStrategyKey>.Decorate(IModelUpdateStrategy<IncidentModel> strategyToDecorate)
        {
            _decoratedStrategy = strategyToDecorate;
            return this;
        }

        public IncidentModelUpdateStrategyKey Key
        {
            get { return _key; }
        }

        protected abstract void Execute(
            Expression<Func<IncidentModel, dynamic>> persistenceProperty,
            ValueConstraint constraint,
            IncidentModel changeDestination,
            IncidentModel updateSource);

        void IModelUpdateStrategy<IncidentModel>.Execute(Expression<Func<IncidentModel, dynamic>> persistenceProperty, ValueConstraint constraint, IncidentModel changeDestination,
            IncidentModel updateSource)
        {
            Execute(persistenceProperty, constraint, changeDestination, updateSource);

            // if this is decorating another strategy then execute that strategy too
            if (_decoratedStrategy != null)
            {
                _decoratedStrategy.Execute(persistenceProperty, constraint, changeDestination, updateSource);
            }
        }
    }
}