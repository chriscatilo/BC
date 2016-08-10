using System;
using System.Linq.Expressions;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Models;
using BC.EQCS.Utils;
using FastMember;

namespace BC.EQCS.Domain.Incident.ModelUpdater
{
    public class IncidentModelUpdateStrategyForPresentation : IncidentModelUpdateStrategy
    {
        private readonly TypeAccessor _typeAccessor = TypeAccessor.Create(typeof (IncidentModel));

        public IncidentModelUpdateStrategyForPresentation()
            : base(IncidentModelUpdateStrategyKey.ForPresentation)
        {
        }

        protected override void Execute(
            Expression<Func<IncidentModel, dynamic>> persistenceProperty,
            ValueConstraint constraint,
            IncidentModel changeDestination,
            IncidentModel updateSource)
        {
            var property = TypeHelpers.GetPropertyByExpression(persistenceProperty);

            switch (constraint)
            {
                case ValueConstraint.NotApplicable:
                case ValueConstraint.Restricted:
                {
                    _typeAccessor[changeDestination, property.Name] = null;
                    break;
                }
            }
        }
    }
}